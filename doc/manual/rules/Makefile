TARGET	= goto-thesis-paper
TEX		= $(shell ls src/*.tex)
PRN		= $(shell ls img/*.prn)
EPS		= $(PRN:%.prn=%.eps)
LATEX	= platex --kanji=utf8
DVIPDFM	= dvipdfmx.exe
PS2EPSI	= ps2epsi
RM		= rm -f

.SUFFIXES:
.SUFFIXES:	.tex .prn .eps .dvi .pdf

.prn.eps:
	$(PS2EPSI) $< $@

.tex.dvi:
	$(LATEX) $<
	$(LATEX) $<
	$(LATEX) $<

.dvi.pdf:
	$(DVIPDFM) $<

all: $(TARGET).dvi

$(TARGET).dvi: $(EPS) $(TEX) $(TARGET).tex

pdf: $(TARGET).pdf

dvi: $(TARGET).dvi



clean:
	$(RM) *.dvi *.aux *.log *.pdf *.ps *.gz *~ *.core

