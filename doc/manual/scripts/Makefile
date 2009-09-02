TARGET	= scripts
LATEX   = platex
RM		= rm -f

.SUFFIXES:	.tex .prn .eps .dvi .pdf

.prn.eps:
	$(PS2EPSI) $< $@

.tex.dvi:
	$(LATEX) $<
	$(LATEX) $<

.dvi.pdf:
	$(DVIPDFM) $<

all: $(TARGET).dvi

$(TARGET).dvi: $(TARGET).tex

pdf: $(TARGET).pdf

dvi: $(TARGET).dvi



clean:
	$(RM) *.dvi *.aux *.log *.pdf *.ps *.gz *~ *.core

