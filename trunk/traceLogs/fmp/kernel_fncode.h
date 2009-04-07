/*
 *  TOPPERS/FMP Kernel
 *      Toyohashi Open Platform for Embedded Real-Time Systems/
 *      Flexible MultiProcessor Kernel
 * 
 *  Copyright (C) 2008-2009 by Embedded and Real-Time Systems Laboratory
 *              Graduate School of Information Science, Nagoya Univ., JAPAN
 * 
 *  上記著作権者は，以下の(1)〜(4)の条件を満たす場合に限り，本ソフトウェ
 *  ア（本ソフトウェアを改変したものを含む．以下同じ）を使用・複製・改
 *  変・再配布（以下，利用と呼ぶ）することを無償で許諾する．
 *  (1) 本ソフトウェアをソースコードの形で利用する場合には，上記の著作
 *      権表示，この利用条件および下記の無保証規定が，そのままの形でソー
 *      スコード中に含まれていること．
 *  (2) 本ソフトウェアを，ライブラリ形式など，他のソフトウェア開発に使
 *      用できる形で再配布する場合には，再配布に伴うドキュメント（利用
 *      者マニュアルなど）に，上記の著作権表示，この利用条件および下記
 *      の無保証規定を掲載すること．
 *  (3) 本ソフトウェアを，機器に組み込むなど，他のソフトウェア開発に使
 *      用できない形で再配布する場合には，次のいずれかの条件を満たすこ
 *      と．
 *    (a) 再配布に伴うドキュメント（利用者マニュアルなど）に，上記の著
 *        作権表示，この利用条件および下記の無保証規定を掲載すること．
 *    (b) 再配布の形態を，別に定める方法によって，TOPPERSプロジェクトに
 *        報告すること．
 *  (4) 本ソフトウェアの利用により直接的または間接的に生じるいかなる損
 *      害からも，上記著作権者およびTOPPERSプロジェクトを免責すること．
 *      また，本ソフトウェアのユーザまたはエンドユーザからのいかなる理
 *      由に基づく請求からも，上記著作権者およびTOPPERSプロジェクトを
 *      免責すること．
 * 
 *  本ソフトウェアは，無保証で提供されているものである．上記著作権者お
 *  よびTOPPERSプロジェクトは，本ソフトウェアに関して，特定の使用目的
 *  に対する適合性も含めて，いかなる保証も行わない．また，本ソフトウェ
 *  アの利用により直接的または間接的に生じたいかなる損害に関しても，そ
 *  の責任を負わない．
 * 
 *  @(#) $Id: kernel_fncode.h 113 2009-01-28 15:12:44Z ertl-honda $
 */

#ifndef TOPPERS_KERNEL_FNCODE_H
#define TOPPERS_KERNEL_FNCODE_H

/*
 *  FMPで追加されたAPIの機能コード（暫定）
 */
#define TFN_MIG_TSK (-346)
#define TFN_MACT_TSK (-345)
#define TFN_IMACT_TSK (-344) 
#define TFN_MSTA_CYC (-343)
#define TFN_MSTA_ALM (-342)
#define TFN_IMSTA_ALM (-341)
#define TFN_LOC_SPN (-340)
#define TFN_ILOC_SPN (-339)
#define TFN_TRY_SPN (-338)
#define TFN_ITRY_SPN (-337)
#define TFN_UNL_SPN (-336)
#define TFN_IUNL_SPN (-335)
#define TFN_GET_PID (-334)
#define TFN_IGET_PID (-333)
#define TFN_MROT_RDQ (-332)
#define TFN_IMROT_RDQ (-331)

/*
 *  ASPのサポートするAPIの機能コード
 */

#define TFN_ACT_TSK (-5)
#define TFN_IACT_TSK (-6)
#define TFN_CAN_ACT (-7)
#define TFN_EXT_TSK (-8)
#define TFN_TER_TSK (-9)
#define TFN_CHG_PRI (-10)
#define TFN_GET_PRI (-11)
#define TFN_GET_INF (-12)
#define TFN_SLP_TSK (-13)
#define TFN_TSLP_TSK (-14)
#define TFN_WUP_TSK (-15)
#define TFN_IWUP_TSK (-16)
#define TFN_CAN_WUP (-17)
#define TFN_REL_WAI (-18)
#define TFN_IREL_WAI (-19)
#define TFN_DIS_WAI (-21)
#define TFN_IDIS_WAI (-22)
#define TFN_ENA_WAI (-23)
#define TFN_IENA_WAI (-24)
#define TFN_SUS_TSK (-25)
#define TFN_RSM_TSK (-26)
#define TFN_DLY_TSK (-27)
#define TFN_RAS_TEX (-29)
#define TFN_IRAS_TEX (-30)
#define TFN_DIS_TEX (-31)
#define TFN_ENA_TEX (-32)
#define TFN_SNS_TEX (-33)
#define TFN_REF_TEX (-34)
#define TFN_SIG_SEM (-37)
#define TFN_ISIG_SEM (-38)
#define TFN_WAI_SEM (-39)
#define TFN_POL_SEM (-40)
#define TFN_TWAI_SEM (-41)
#define TFN_SET_FLG (-45)
#define TFN_ISET_FLG (-46)
#define TFN_CLR_FLG (-47)
#define TFN_WAI_FLG (-48)
#define TFN_POL_FLG (-49)
#define TFN_TWAI_FLG (-50)
#define TFN_SND_DTQ (-53)
#define TFN_PSND_DTQ (-54)
#define TFN_IPSND_DTQ (-55)
#define TFN_TSND_DTQ (-56)
#define TFN_FSND_DTQ (-57)
#define TFN_IFSND_DTQ (-58)
#define TFN_RCV_DTQ (-59)
#define TFN_PRCV_DTQ (-60)
#define TFN_TRCV_DTQ (-61)
#define TFN_SND_PDQ (-65)
#define TFN_PSND_PDQ (-66)
#define TFN_IPSND_PDQ (-67)
#define TFN_TSND_PDQ (-68)
#define TFN_RCV_PDQ (-69)
#define TFN_PRCV_PDQ (-70)
#define TFN_TRCV_PDQ (-71)
#define TFN_SND_MBX (-73)
#define TFN_RCV_MBX (-74)
#define TFN_PRCV_MBX (-75)
#define TFN_TRCV_MBX (-76)
#define TFN_LOC_MTX (-77)
#define TFN_PLOC_MTX (-78)
#define TFN_TLOC_MTX (-79)
#define TFN_UNL_MTX (-80)
#define TFN_SND_MBF (-81)
#define TFN_PSND_MBF (-82)
#define TFN_TSND_MBF (-83)
#define TFN_RCV_MBF (-84)
#define TFN_PRCV_MBF (-85)
#define TFN_TRCV_MBF (-86)
#define TFN_GET_MPF (-89)
#define TFN_PGET_MPF (-90)
#define TFN_TGET_MPF (-91)
#define TFN_REL_MPF (-92)
#define TFN_GET_TIM (-93)
#define TFN_GET_UTM (-94)
#define TFN_STA_CYC (-97)
#define TFN_STP_CYC (-98)
#define TFN_STA_ALM (-101)
#define TFN_ISTA_ALM (-102)
#define TFN_STP_ALM (-103)
#define TFN_ISTP_ALM (-104)
#define TFN_STA_OVR (-105)
#define TFN_STP_OVR (-106)
#define TFN_REF_OVR (-107)
#define TFN_SAC_SYS (-109)
#define TFN_REF_SYS (-110)
#define TFN_ROT_RDQ (-111)
#define TFN_IROT_RDQ (-112)
#define TFN_GET_DID (-113)
#define TFN_GET_CDM (-114)
#define TFN_GET_TID (-115)
#define TFN_IGET_TID (-116)
#define TFN_LOC_CPU (-117)
#define TFN_ILOC_CPU (-118)
#define TFN_UNL_CPU (-119)
#define TFN_IUNL_CPU (-120)
#define TFN_DIS_DSP (-121)
#define TFN_ENA_DSP (-122)
#define TFN_SNS_CTX (-123)
#define TFN_SNS_LOC (-124)
#define TFN_SNS_DSP (-125)
#define TFN_SNS_DPN (-126)
#define TFN_SNS_KER (-127)
#define TFN_EXT_KER (-128)
#define TFN_ATT_MEM (-129)
#define TFN_DET_MEM (-130)
#define TFN_SAC_MEM (-131)
#define TFN_PRB_MEM (-132)
#define TFN_REF_MEM (-133)
#define TFN_CFG_INT (-137)
#define TFN_DIS_INT (-138)
#define TFN_ENA_INT (-139)
#define TFN_REF_INT (-140)
#define TFN_CHG_IPM (-141)
#define TFN_GET_IPM (-142)
#define TFN_XSNS_DPN (-145)
#define TFN_XSNS_XPN (-146)
#define TFN_REF_CFG (-149)
#define TFN_REF_VER (-150)
#define TFN_INI_SEM (-162)
#define TFN_INI_FLG (-163)
#define TFN_INI_DTQ (-164)
#define TFN_INI_PDQ (-165)
#define TFN_INI_MBX (-166)
#define TFN_INI_MTX (-167)
#define TFN_INI_MBF (-168)
#define TFN_INI_MPF (-169)
#define TFN_REF_TSK (-177)
#define TFN_REF_SEM (-178)
#define TFN_REF_FLG (-179)
#define TFN_REF_DTQ (-180)
#define TFN_REF_PDQ (-181)
#define TFN_REF_MBX (-182)
#define TFN_REF_MTX (-183)
#define TFN_REF_MBF (-184)
#define TFN_REF_MPF (-185)
#define TFN_REF_CYC (-186)
#define TFN_REF_ALM (-187)
#define TFN_REF_ISR (-188)
#define TFN_ACRE_TSK (-193)
#define TFN_ACRE_SEM (-194)
#define TFN_ACRE_FLG (-195)
#define TFN_ACRE_DTQ (-196)
#define TFN_ACRE_PDQ (-197)
#define TFN_ACRE_MBX (-198)
#define TFN_ACRE_MTX (-199)
#define TFN_ACRE_MBF (-200)
#define TFN_ACRE_MPF (-201)
#define TFN_ACRE_CYC (-202)
#define TFN_ACRE_ALM (-203)
#define TFN_ACRE_ISR (-204)
#define TFN_DEL_TSK (-209)
#define TFN_DEL_SEM (-210)
#define TFN_DEL_FLG (-211)
#define TFN_DEL_DTQ (-212)
#define TFN_DEL_PDQ (-213)
#define TFN_DEL_MBX (-214)
#define TFN_DEL_MTX (-215)
#define TFN_DEL_MBF (-216)
#define TFN_DEL_MPF (-217)
#define TFN_DEL_CYC (-218)
#define TFN_DEL_ALM (-219)
#define TFN_DEL_ISR (-220)
#define TFN_SAC_TSK (-225)
#define TFN_SAC_SEM (-226)
#define TFN_SAC_FLG (-227)
#define TFN_SAC_DTQ (-228)
#define TFN_SAC_PDQ (-229)
#define TFN_SAC_MTX (-231)
#define TFN_SAC_MBF (-232)
#define TFN_SAC_MPF (-233)
#define TFN_SAC_CYC (-234)
#define TFN_SAC_ALM (-235)
#define TFN_SAC_ISR (-236)
#define TFN_DEF_TEX (-241)
#define TFN_DEF_OVR (-242)
#define TFN_DEF_INH (-243)
#define TFN_DEF_EXC (-244)
#define TFN_DEF_SVC (-245)

#endif /* TOPPERS_KERNEL_FNCODE_H_ */
