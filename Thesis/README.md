# KOM Thesis Template
TU Darmstadt, Multimedia Communications Lab (KOM), 2020
# Version 1.5.1 "standalone"
This package contains the KOM Lab Thesis template. Do not modify this template or any of the formatting parameters.

## Overview
This template can be used only with the TU Darmstadt Fonts installed, which are provided in this package's tudfonts/ folder. You have to install them first!

The installation is straight-forward and follows https://www.tug.org/fonts/fontinstall.html, which gives instructions for TeX Live, MacTeX, and MiKTeX, respectively. We provide a shell script for Ubuntu for a clean install of linux and the fonts and another script for OS X for installing the fonts. Windows users have to manually install a latex distribution first and the fonts afterwards.

Following the instructions for installing the fonts or using the provided scripts were tested on:
- OS X 10.14 using MacTeX (installed via Homebrew, fonts installed manually and via script)
- Ubuntu 18.04 LTS using TeX Live (installed via apt-get as full installation and manual font installation, installed via apt-get using the provided script including font installation)
- Windows 10 using MiKTeX (manual installation)

If compile logs don't warn you that fonts are missing, it should be correctly installed. If the font on the front page looks similar to Times New Roman, it is not ;-)

Contact your supervisor, if the instructions do not work for you. Most probably, you have some issues regarding the location of local latex folders. Multiple distributions or deprecated system links are the most common problems. A single, clean install of your latex distribution of choice can help. 

## LINUX/UNIX (Tested on Ubuntu 18.04)
- We recommend to start with a clean install of the latex installation of your choice. The install script will use apt with texlive, which should work w/o problems. If you install TeXLive, e.g., on Ubuntu using the download installer, this may cause trouble later if you also install an editor (over apt) that also installs basic latex in the background (as observed with TeXstudio over apt-get after using the TeXLive download installer)!
- The font script installs the fonts into the standard local texmf folder (e.g., /usr/local/share/texmf-local) that is known to your computer. If you have an old or multiple latex distributions installed, this may cause trouble as fonts can be in the wrong directory. If you use another texmf folder, follow https://www.tug.org/fonts/fontinstall.html to install the fonts (or alter paths in the script). 
- If everything is correct, the compile script can be used to create the pdf. If you use, e.g., TeXStudio, make sure that it uses the correct latex binary, that uses the texmf folder the fonts are in. 

## OS X (Tested on OS X 10.14)
- The recommended way is to make sure you only have a single latex distribution installed. We think the easiest way is a clean install of MacTeX using Homebrew.
- The font script installs the fonts into the standard local texmf folder (e.g., /opt/local/share/texmf-local) that is known to your computer. If you have an old or multiple latex distributions installed, this may cause some trouble as fonts can be in the wrong directory. If you use another folder, follow https://www.tug.org/fonts/fontinstall.html to install the fonts (or alter paths in the script). If the script cannot find your local texmf folder, restart terminal. 
- If everything is correct, the compile script can be used to create the pdf. If you use, e.g., TeXStudio, make sure that it uses the correct latex binary, that uses the texmf folder the fonts are in. 

## Windows (Tested on Windows 10)
- The recommended way it to use only one distribution and a clean install. We tested it with MiKTeX, which seems to be the easiest way for LaTeX on Windows. 
- Follow the instructions on https://www.tug.org/fonts/fontinstall.html to install the fonts provided in the tudfonts/ folder. Make sure to add the files in this package's tudfonts/texmf/ subfolder into your distribution's texmf/ folder (do not make something like ../texmf/texmf/..!). 
- Make sure that your editor uses the correct latex executables. Depending on your installation (like local vs. global) you might have to manually set the location of your executables in the editor (as observed using TeXstudio with a local installation of MiKTeX). 
- If you use MiKTeX, it is recommended to allow the automatic installation of latex packages in the background. 

# MiKTeX 2.9 Windows Tutorial
- Create a folder for your local latex files, e.g. in your userspace (for example C:\Users\Username\latex), to which you copy the texmf folder and its contents provided in the tudfonts folder (should then look like C:\Users\Username\latex\texmf).
- Open the MiKTeX Console (cmd: miktex-console)
- Navigate to Settings >> Directories. Add the path to the texmf folder you just copied. The link has to point to texmf, not the latex folder above.
- Open a cmd prompt and execute "initexmf --update-fndb" and then "initexmf --edit-config-file updmap"
- In the opening file add
	Map 5ch.map
	Map 5fp.map
	Map 5sf.map
  then save the file.
- In cmd prompt execute "initexmf --mkmaps"
- In MiKTeX Console go to "Tasks" in the menu bar and refresh filename and font databases.
- should work now

## Font maps
These three font maps have to be registered. Instructions given at https://www.tug.org/fonts/fontinstall.html use newfont.map as placeholder, substitue newfont with the respective font name! (Map newfont.map << Example) 
The LINUX/OSX scripts will do that for you in the standard case.

Map 5ch.map
Map 5fp.map
Map 5sf.map


## Changelog
* 1.5.1: Corrected page spacing and font size