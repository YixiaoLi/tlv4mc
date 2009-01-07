TARGET	= goto-thesis-paper
LATEX	= platex --kanji=utf8
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

pdf: $(TARGET).pdf

dvi: $(TARGET).dvi

all: $(TARGET).pdf
	$(SVNCOMMIT)

clean:
	$(RM) *.dvi *.aux *.log *.pdf *.ps *.gz *~ *.core
	$(SVNDEL)

commit:
	$(SVNCOMMIT)
