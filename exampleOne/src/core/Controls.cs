using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace ExampleOne;

public static class Controls
{
    public static void Input(GameState gameState, GameTime gameTime)
    {
        bool gamepadConnected = GamePad.GetState(PlayerIndex.One).IsConnected;

        Vector2 move = Vector2.Zero;
        bool running = false;
        bool changeColorPressed = false;
        bool quitPressed = false;

        if (gamepadConnected)
        {
            // Movement (left stick)
            var stick = gameState.CurrentGamePadState.ThumbSticks.Left;
            move = new Vector2(stick.X, -stick.Y); // invert Y for screen coords

            // Actions
            changeColorPressed =
                gameState.CurrentGamePadState.Buttons.A == ButtonState.Pressed &&
                gameState.PreviousGamePadState.Buttons.A == ButtonState.Released;

            running = gameState.CurrentGamePadState.Buttons.X == ButtonState.Pressed;
            quitPressed = gameState.CurrentGamePadState.Buttons.Start == ButtonState.Pressed;

            gameState.PreviousGamePadState = gameState.CurrentGamePadState;
        }
        else
        {
            // Movement (WASD)
            if (gameState.CurrentKeyboardState.IsKeyDown(Keys.W))
            {
                move.Y -= 1;
            }

            if (gameState.CurrentKeyboardState.IsKeyDown(Keys.S))
            {
                move.Y += 1;
            }

            if (gameState.CurrentKeyboardState.IsKeyDown(Keys.A))
            {
                move.X -= 1;
            }

            if (gameState.CurrentKeyboardState.IsKeyDown(Keys.D))
            {
                move.X += 1;
            }

            // Normalize so diagonals aren't faster
            if (move != Vector2.Zero)
            {
                move.Normalize();
            }

            // Set Actions
            // Change color
            changeColorPressed =
                gameState.CurrentKeyboardState.IsKeyDown(Keys.E) &&
                !gameState.PreviousKeyboardState.IsKeyDown(Keys.E);
            // Run
            running = gameState.CurrentKeyboardState.IsKeyDown(Keys.LeftShift);
            // Quit
            quitPressed = gameState.CurrentKeyboardState.IsKeyDown(Keys.Escape);

            // Update gameState
            gameState.PreviousKeyboardState = gameState.CurrentKeyboardState;
        }

        // Apply run/walk
        gameState.Player.SpeedScalar = running ? 1.5f : 1f;

        // Apply movement
        gameState.Player.UpdateVelocity(move * gameState.Player.MovementSpeed);
        gameState.Player.UpdatePosition(
            gameState.Player.Velocity,
            (float)gameTime.ElapsedGameTime.TotalSeconds);

        // Apply actions
        if (changeColorPressed)
        {
            gameState.Player.ChangeColor();
        }

        if (quitPressed)
        {
            Environment.Exit(0);
        }
    }
}
