#!/bin/bash
# This script installs TeXlive and moves the TUDa fonts to the correct location
# Tested on Ubuntu 18.04 LTS

# Install Latex packages from apt.
sudo apt-get update && sudo apt-get install texlive texlive-lang-german texlive-fonts-extra texlive-latex-extra

# retrieve local texmf folder
TEXMF="$(kpsewhich --var-value TEXMFLOCAL)"
echo "TEXMFLOCAL : ${TEXMF}"

# Move fonts to the local texmf location location
sudo cp -r tudfonts/texmf/* ${TEXMF}

# Add fonts to the latex core.
# If there are problems when creating the maps, most probably updmap looks into the wrong folder. This happens, e.g., if there are multiple latex distributions. A clean install helps.
sudo mktexlsr
sudo updmap-sys
sudo updmap-sys --enable Map 5ch.map
sudo updmap-sys --enable Map 5fp.map
sudo updmap-sys --enable Map 5sf.map
sudo mktexlsr

echo " --- END ---"

