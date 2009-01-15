TARGET	= goto-thesis-paper
SRC		= src/chapter1.tex \
		  src/chapter2.tex \
		  src/chapter3.tex \
		  src/chapter4.tex \
		  src/chapter5.tex \
		  src/chapter6.tex \
		  src/chapter7.tex \
		  src/abstract_en.tex \
		  src/abstract_jp.tex
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

$(TARGET).dvi: $(TARGET).tex $(SRC)

pdf: $(TARGET).pdf

dvi: $(TARGET).dvi

all: $(TARGET).pdf
	$(SVNCOMMIT)

clean:
	$(RM) *.dvi *.aux *.log *.pdf *.ps *.gz *~ *.core
	$(SVNDEL)

commit:
	$(SVNCOMMIT)
