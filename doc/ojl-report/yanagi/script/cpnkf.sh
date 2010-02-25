#!/bin/bash

cp ../../mzp/*.tex ../input
cp ../../mzp/*.png ../input
cp ../../mzp/*.eps ../input

texfiles=$(ls ../input/*.tex)
for tex in ${texfiles[@]};do
  nkf --sjis --windows --overwrite ${tex} 
  echo ${tex}
done

