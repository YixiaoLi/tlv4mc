TARGET	= goto-thesis-paper
LATEX	= platex
DVIPDFM	= dvipdfmx.exe
RM		= rm -f
SVNADD	= svn add

.SUFFIXES:
.SUFFIXES:	.tex .dvi .pdf

.dvi.pdf:
	$(DVIPDFM) $<
	$(SVNADD) $@

.tex.dvi:
	$(LATEX) $<
	$(LATEX) $<
	$(SVNADD) $@

dvi: $(TARGET).dvi

pdf: $(TARGET).pdf

all: $(TARGET).pdf

clean:
	$(RM) *.dvi *.aux *.log *.pdf *.ps *.gz *~ *.core
	svn st | grep '^!' | awk '{ print $$2 }' | xargs svn delete

commit:
	svn commit -m ''
