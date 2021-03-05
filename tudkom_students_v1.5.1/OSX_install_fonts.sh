#!/bin/bash
# This script installs and registers the TUDa fonts to the system's known local texmf folder
# Tested on OS X 10.14 with MacTex installed using Homebrew

# retrieve local texmf folder
TEXMF="$(kpsewhich --var-value TEXMFLOCAL)"

echo "TEXMFLOCAL : ${TEXMF}"

# Move fonts to the local texmf location
sudo cp -Rav tudfonts/texmf/* ${TEXMF}

# Add fonts to filename database and update latex fonts
# If there are problems when creating the maps, most probably updmap looks into the wrong folder. This happens, e.g., if there are multiple latex distributions. A clean install helps.
sudo -H mktexlsr
sudo -H updmap-sys
sudo -H updmap-sys --enable Map 5ch.map
sudo -H updmap-sys --enable Map 5fp.map
sudo -H updmap-sys --enable Map 5sf.map
sudo -H mktexlsr

echo " --- END ---"