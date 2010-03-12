PAPER_NAME = tlv-paper

all:
	platex $(PAPER_NAME).tex
	platex $(PAPER_NAME).tex
	dvipdfm $(PAPER_NAME).dvi

clean:
	rm -f $(PAPER_NAME).aux
	rm -f $(PAPER_NAME).log
	rm -f $(PAPER_NAME).dvi
	rm -f *.pbm
