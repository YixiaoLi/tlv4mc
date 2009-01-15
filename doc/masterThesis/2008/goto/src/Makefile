pdf:
    make pdf ..

dvi: $(TARGET).dvi

all: $(TARGET).pdf
	$(SVNCOMMIT)

clean:
	$(RM) *.dvi *.aux *.log *.pdf *.ps *.gz *~ *.core
	$(SVNDEL)

commit:
	$(SVNCOMMIT)
