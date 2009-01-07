TARGET	= goto-thesis-paper
LATEX   = platex
DVIPDFM = dvipdfmx.exe
RM      = rm -f

.SUFFIXES:
.SUFFIXES:  .tex .dvi .pdf

.tex.dvi:
	$(LATEX) $<
	$(LATEX) $<

.dvi.pdf:
	$(DVIPDFM) $<

dvi: $(TARGET).dvi

pdf: $(TARGET).pdf

all: $(TARGET).pdf

clean:
	$(RM) *.dvi *.aux *.log *.pdf *.ps *.gz *~ *.core
