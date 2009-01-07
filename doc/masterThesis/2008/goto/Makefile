TARGET	= goto-thesis-paper
LATEX	= platex
DVIPDFM	= dvipdfmx.exe
RM		= rm -f
SVNADD	= svn add
SVNDEL	= svn st | grep '^!' | awk '{ print $$2 }' | xargs svn delete
SVNCOMMIT	= svn commit -m ''

.SUFFIXES:
.SUFFIXES:	.tex .dvi .pdf

.tex.dvi:
	$(LATEX) $<
	$(LATEX) $<
	$(SVNADD) $@

.dvi.pdf:
	$(DVIPDFM) $<
	$(SVNADD) $@

all: $(TARGET).pdf

pdf: $(TARGET).pdf

dvi: $(TARGET).dvi

clean:
	$(RM) *.dvi *.aux *.log *.pdf *.ps *.gz *~ *.core
	$(SVNDEL)

commit:
	$(SVNCOMMIT)
