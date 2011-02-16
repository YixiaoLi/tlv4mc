このファイルには，要旨用レイアウト設定ファイルの構成が記載されています．

apulayout.zip を解凍すると，

README.1st.EUC.txt	-----> このファイル
README.1st.SJIS.txt	-----> このファイルと同じ内容（ SJIS 版）
apulayout.sty		-----> 要旨レイアウト設定ファイル
figsample.eps		-----> 利用マニュアルで使用する図
geometry.sty		-----> レイアウト設定で使用するスタイル
nidanfloat.sty		-----> 二段抜きをするためのスタイル
bachelor.tex		-----> 学部生用の利用マニュアル・ソース
master.tex		-----> 院生用の利用マニュアル・ソース
SJIS/			-----> SJIS 版

の 8 つのファイルと 1 つのディレクトリが生成されます．

geometry.sty と nidanfloat.sty は，標準の LaTeX	環境にあるとは思います
が，念のため同梱しました．

bachelor.tex，master.tex は，要旨レイアウトの利用方法について説明してい
ます．

platex bachelor.tex
platex bachelor.tex
dvips bachelor.dvi

または，

platex master.tex
platex master.tex
dvips master.dvi

と実行して，利用マニュアルを生成して下さい．このマニュアル・ソースも抄
録レイアウトを使用しているので参考になるかも知れません． 

SJIS ディレクトリ以下には，SJIS コードのファイルが配置されています．
EUC コード版で正常に動作しない場合に試してみて下さい．

----------------------------------------------------------------------
文書，レイアウト作成：戸田研究室 兼弘
