
	TraceLogVisualizer(TLV)（Release 1.0rc1）

					最終更新 : 2009年04月21日

----------------------------------------------------------------------
 TraceLogVisualizer(TLV)

 Copyright (C) 2008-2009 Nagoya University, JAPAN
 
 上記著作権者は，以下の(1)～(4)の条件を満たす場合に限り，本ソフトウェ
 ア（本ソフトウェアを改変したものを含む．以下同じ）を使用・複製・改
 変・再配布（以下，利用と呼ぶ）することを無償で許諾する．
 (1) 本ソフトウェアをソースコードの形で利用する場合には，上記の著作
     権表示，この利用条件および下記の無保証規定が，そのままの形でソー
     スコード中に含まれていること．
 (2) 本ソフトウェアを，ライブラリ形式など，他のソフトウェア開発に使
     用できる形で再配布する場合には，再配布に伴うドキュメント（利用
     者マニュアルなど）に，上記の著作権表示，この利用条件および下記
     の無保証規定を掲載すること．
 (3) 本ソフトウェアを，機器に組み込むなど，他のソフトウェア開発に使
     用できない形で再配布する場合には，次のいずれかの条件を満たすこ
     と．
   (a) 再配布に伴うドキュメント（利用者マニュアルなど）に，上記の著
       作権表示，この利用条件および下記の無保証規定を掲載すること．
   (b) 再配布の形態を，別に定める方法によって，TOPPERSプロジェクトに
       報告すること．
 (4) 本ソフトウェアの利用により直接的または間接的に生じるいかなる損
     害からも，上記著作権者およびTOPPERSプロジェクトを免責すること．
     また，本ソフトウェアのユーザまたはエンドユーザからのいかなる理
     由に基づく請求からも，上記著作権者およびTOPPERSプロジェクトを
     免責すること．

 本ソフトウェアは，無保証で提供されているものである．上記著作権者お
 よびTOPPERSプロジェクトは，本ソフトウェアに関して，特定の使用目的
 に対する適合性も含めて，いかなる保証も行わない．また，本ソフトウェ
 アの利用により直接的または間接的に生じたいかなる損害に関しても，そ
 の責任を負わない．

 ----------------------------------------------------------------------

TraceLogVisualizer（以下，TLV)は，各種のログを可視化するWindows上で動
作するアプリケーションである．TLVは，文部科学省 先導的ITスペシャリスト
育成推進プログラム『OJL(On-the-Job Learning)による最先端技術適応能力を
持つIT人材育成拠点形成』の一環として名古屋大学で開発している．

本リリースは，TOPPERS会員向けの早期リリースであるため，会員以外に再配
布してはならない．

【ドキュメント】

TLVの使用方法や変換ルール・可視化ルールに関するマニュアルが，doc にあ
る．内容はまだ限定的であるため，今後充実させる予定である．

  ・TLV.pdf                   
     TLVマニュアル

  ・TLV_convert_rules.pdf
     TOPPERS/ASPカーネルのトレースログの標準形式トレースログへの変換ルール例

  ・TLV_visualize_rules.pdf
     TOPPERS/ASPカーネル標準トレースログの可視化変換ルール例

【サポートするログ形式】

TLVは，変換ルールや可視化ルールを要することにより，各種のログを可視化
することが可能である．標準では，以下のログに対する変換ルールと可視化ル
ールが含まれている．

 ・TOPPERS/ASPカーネルのトレースログ
 ・TOPPERS/FMPカーネルのトレースログ
 ・TECSのトレースログ

TOPPERS/ASPカーネルのトレースログを取得するためのルーチンは，
logtrace/asp に，TOPPERS/FMPカーネルのトレースログを取得するためのルー
チンは，logtrace/fmpにある．使用方法はTLVマニュアルを参照のこと．

【利用条件】

TLVの利用条件は，各ファイルの先頭に表示されているTOPPERSライセンスです．
TOPPERSライセンスに関するFAQが，以下のページにあります．

	http://www.toppers.jp/faq/faq_ct12.html

【現状分かっている問題点】

・【ASP/FMP】多重割込みのサポート
  多重割込みが入ると，システムコールの発行元の割込みハンドラが正しく表
  示されないという問題がある．

・変換・可視化ルールのマニュアル
  現状，変換・可視化ルールのマニュアルは存在しない．今後整備していく予
  定である．

【質問・バグレポート・意見等の送付先】

TLVをより良いものにするためのご意見等を歓迎します．TLVに関する質問やバ
グレポート，ご意見等は，TOPPERSプロジェクトの会員はTOPPERS開発者メーリ
ングリスト（dev＠toppers.jp）宛に，その他の方はTOPPERSユーザーズメーリ
ングリスト（users＠toppers.jp）宛にお願いします．もしくは，tlv＠
nces.is.nagoya-u.ac.jp にお送り下さい．
（＠は@に置き換えて下さい）

TOPPERSユーザーズメーリングリストへの登録方法については，以下のページに
説明があります．

	http://www.toppers.jp/community.html

【ファイル構成】

	README.txt					TLVの簡単な紹介
	TraceLogVisualizer.exe		TLVの本体

	doc/
		TLV.pdf					TLV本体のマニュアル
		TLV_convert_rules.pdf	変換ルールのマニュアル
		TLV_visualize_rules.pdf	可視化ルールのマニュアル

	convertRules/
		asp.cnv					TOPPERS/ASPカーネルの変換ルール
		fmp.cnv					TOPPERS/FMPカーネルの変換ルール
		tecs.cnv				TECSの変換ルール

	resourceHeaders/
		asp.resh				TOPPERS/ASPカーネルのリソースファイル
		fmp.resh				TOPPERS/FMPカーネルのリソースファイル
		tecs.resh				TECSのリソースファイル

	visualizeRules/
		asp_rules.viz			TOPPERS/ASPカーネルの可視化ルール
		asp_shapes.viz			TOPPERS/ASPカーネルの図形定義
		fmp_rules.viz			TOPPERS/FMPカーネルの可視化ルール
		fmp_shapes.viz			TOPPERS/FMPカーネルの図形定義
		tecs.viz				TECSの可視化ルール
		toppers_rules.viz		TOPPERS共通の可視化ルール
		toppers_shapes.viz		TOPPERS共通の図形定義

	sampleFiles/
		asp/					TOPPERS/ASPカーネルのトレースログのサンプル
			asp.log
			asp.res
			asp.tlv
			full.log
			full.res
			full.tlv

		fmp/					TOPPERS/FMPカーネルのトレースログのサンプル
			fmp.log
			fmp.res
			fmp.tlv

		tecs/					TECSのトレースログのサンプル
			tecs.log
			tecs.res
			tecs.tlv

	logtrace/
		asp/					TOPPERS/ASPカーネルのログトレースモジュール
			kernel_fncode.h
			tlv.tf
			trace_config.c
			trace_config.h
			trace_dump.c
		fmp/					TOPPERS/FMPカーネルのログトレースモジュール
			kernel_fncode.h
			tlv.tf
			trace_config.c
			trace_config.h
			trace_dump.c

【バージョン履歴】

	2009年04月23日	Release	1.0rc1		TOPPERS会員向け早期リリース

以上．
