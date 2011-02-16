@echo off
xbb *.png
set INPUT=%1
platex %INPUT%
platex %INPUT%
platex %INPUT%
dvipdfmx %INPUT:.tex=.dvi%
%INPUT:.tex=.pdf%