#!/usr/bin/env python3
from pathlib import Path

repo = Path(__file__).resolve().parent / ".."
common_textures = repo / "common" / "textures"
out = repo / "common" / "Content.mgcb"

files = sorted(
    [p for p in common_textures.rglob("*") if p.suffix.lower() in (".png", ".bmp")]
)

out.parent.mkdir(parents=True, exist_ok=True)

header = """#----------------------------- Global Properties ----------------------------#

/outputDir:bin/DesktopGL/Content
/intermediateDir:obj/DesktopGL/net8.0/Content
/platform:DesktopGL
/config:
/profile:Reach
/compress:False

#-------------------------------- References --------------------------------#


#---------------------------------- Content ---------------------------------#

"""


def texture_block(src: Path) -> str:
    rel_from_repo = src.relative_to(repo)  # common/textures/...
    logical = rel_from_repo.relative_to("common")  # textures/...
    return f"""#begin ../{rel_from_repo.as_posix()}
/importer:TextureImporter
/processor:TextureProcessor
/processorParam:ColorKeyColor=255,0,255,255
/processorParam:ColorKeyEnabled=False
/processorParam:GenerateMipmaps=False
/processorParam:PremultiplyAlpha=True
/processorParam:ResizeToPowerOfTwo=False
/processorParam:MakeSquare=False
/processorParam:TextureFormat=Color
/build:{logical.as_posix()}

"""


with out.open("w", encoding="utf-8", newline="\n") as f:
    f.write(header)
    for p in files:
        f.write(texture_block(p))

print(f"Wrote {out} with {len(files)} textures.")
