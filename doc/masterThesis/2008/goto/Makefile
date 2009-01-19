TARGET	= goto-thesis-paper
TEX		= $(shell ls src/*.tex)
PRN		= $(shell ls img/*.prn)
EPS		= $(PRN:%.prn=%.eps)
LATEX	= platex --kanji=utf8
DVIPDFM	= dvipdfmx.exe
PS2EPSI	= ps2epsi
RM		= rm -f
SVNADD	= svn add
SVNDEL	= svn st | grep '^!' | awk '{ print $$2 }' | xargs svn delete
SVNCOMMIT	= svn commit -m ''

.SUFFIXES:
.SUFFIXES:	.tex .prn .eps .dvi .pdf

.prn.eps:
	$(PS2EPSI) $< $@
	$(SVNADD) $@

.tex.dvi:
	$(LATEX) $<
	$(LATEX) $<
	$(SVNADD) $@

.dvi.pdf:
	$(DVIPDFM) $<
	$(SVNADD) $@

$(TARGET).dvi: $(EPS) $(TEX) $(TARGET).tex

pdf: $(TARGET).pdf

dvi: $(TARGET).dvi

all: $(TARGET).pdf
	$(SVNCOMMIT)

clean:
	$(RM) *.dvi *.aux *.log *.pdf *.ps *.gz *~ *.core
	$(SVNDEL)

commit:
	$(SVNCOMMIT)
