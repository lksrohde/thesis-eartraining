#!/bin/bash
# This script compiles a TeX file to PDF

# this is the filename of your main .tex
FILENAME=tudkom_students
#
sudo pdflatex -synctex=1 "$FILENAME"
sudo bibtex "$FILENAME"
sudo pdflatex -synctex=1 "$FILENAME"
sudo pdflatex -synctex=1 "$FILENAME"
#
sudo rm -f *.aux
sudo rm -f *.bcf
sudo rm -f *.out
sudo rm -f *.bbl
sudo rm -f *.blg
sudo rm -f *.toc
sudo rm -f *.xml
sudo rm -f *.fdb_latexmk
sudo rm -f *.fls
sudo rm -f *-blx.bib
sudo rm -f *.gz

sudo rm -f *.log


sudo chmod 777 ${FILENAME}.pdf

echo " --- Compiled : ${FILENAME}.pdf ---"
