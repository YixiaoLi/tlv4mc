#ifndef _KERNEL_FNCODE_H_
#define _KERNEL_FNCODE_H_

/* 任意に追加した部分  */
#define TFN_ALM (-332)
#define TFN_CYC (-331)
#define TFN_GET_INF (-330)
#define TFN_SND_PDQ (-329)
#define TFN_PSND_PDQ (-328)
#define TFN_IPSND_PDQ (-327)
#define TFN_TSND_PDQ (-326)
#define TFN_RCV_PDQ (-325)
#define TFN_PRCV_PDQ (-324)
#define TFN_TRCV_PDQ (-323)
#define TFN_INI_PDQ (-322)
#define TFN_REF_PDQ (-321)
#define TFN_SAC_PDQ (-320)

#define TFN_SAC_MBP (-319)
#define TFN_SAC_MPP (-318)
#define TFN_SAC_ISR (-317)
#define TFN_SAC_ALM (-316) /* アラームハンドラ */
#define TFN_SAC_CYC (-315) /* 周期ハンドラ */
#define TFN_SAC_MPL (-314)
#define TFN_SAC_MPF (-313) /* 固定長メモリプール */
#define TFN_SAC_POR (-312)
#define TFN_SAC_MBF (-311)
#define TFN_SAC_MTX (-310)
#define TFN_SAC_MBX (-309) /* メールボックス */
#define TFN_SAC_DTQ (-308) /* データキュー */
#define TFN_SAC_FLG (-307) /* イベントフラグ */
#define TFN_SAC_SEM (-306) /* セマフォ*/
#define TFN_SAC_TSK (-305) /* タスク管理・タスク付属同期 */
#define TFN_ACRA_MBP (-303)
#define TFN_ACRA_MPP (-302)
#define TFN_ACRA_ISR (-301)
#define TFN_ACRA_ALM (-300)
#define TFN_ACRA_CYC (-299)
#define TFN_ACRA_MPL (-298)
#define TFN_ACRA_MPF (-297)
#define TFN_ACRA_POR (-296)
#define TFN_ACRA_MBF (-295)
#define TFN_ACRA_MTX (-294)
#define TFN_ACRA_MBX (-293)
#define TFN_ACRA_DTQ (-292)
#define TFN_ACRA_FLG (-291)
#define TFN_ACRA_SEM (-290)
#define TFN_ACRA_TSK (-289)
#define TFN_CRA_MBP (-287)
#define TFN_CRA_MPP (-286)
#define TFN_CRA_ISR (-285)
#define TFN_CRA_ALM (-284)
#define TFN_CRA_CYC (-283)
#define TFN_CRA_MPL (-282)
#define TFN_CRA_MPF (-281)
#define TFN_CRA_POR (-280)
#define TFN_CRA_MBF (-279)
#define TFN_CRA_MTX (-278)
#define TFN_CRA_MBX (-277)
#define TFN_CRA_DTQ (-276)
#define TFN_CRA_FLG (-275)
#define TFN_CRA_SEM (-274)
#define TFN_CRA_TSK (-273)
#define TFN_REF_MBP (-272)
#define TFN_TRCV_MBP (-271)
#define TFN_PRCV_MBP (-270)
#define TFN_RCV_MBP (-269)
#define TFN_SND_MBP (-267)
#define TFN_DEL_MBP (-266)
#define TFN_CRE_MBP (-265)
#define TFN_REF_MPP (-264)
#define TFN_TGET_MPP (-263)
#define TFN_PGET_MPP (-262)
#define TFN_GET_MPP (-261)
#define TFN_REL_MPP (-259)
#define TFN_DEL_MPP (-258)
#define TFN_CRE_MPP (-257)
#define TFN_INI_MBP (-255)
#define TFN_INI_MPP (-254)
#define TFN_INI_MPL (-250) /* 可変長メモリプール */
#define TFN_INI_MPF (-249) /* 固定長メモリプール */
#define TFN_INI_POR (-248)
#define TFN_INI_MBF (-247)
#define TFN_INI_MTX (-246)
#define TFN_INI_MBX (-245) /* メールボックス */
#define TFN_INI_DTQ (-244) /* データキュー */
#define TFN_INI_FLG (-243) /* イベントフラグ */
#define TFN_INI_SEM (-242) /* セマフォ*/
#define TFN_EXT_KER (-234)
#define TFN_GET_UTM (-233) /* システム時刻管理 */
#define TFN_SNS_KER (-232)
#define TFN_XSNS_XPN (-230) /* CPU例外処理 */
#define TFN_XSNS_DPN (-228) /* CPU例外処理 */
#define TFN_ACRE_MBP (-207)
#define TFN_ACRE_MPP (-206)
#define TFN_ACRE_ISR (-205)
#define TFN_ACRE_ALM (-204)
#define TFN_ACRE_CYC (-203)
#define TFN_ACRE_MPL (-202)
#define TFN_ACRE_MPF (-201)
#define TFN_ACRE_POR (-200)
#define TFN_ACRE_MBF (-199)
#define TFN_ACRE_MTX (-198)
#define TFN_ACRE_MBX (-197)
#define TFN_ACRE_DTQ (-196)
#define TFN_ACRE_FLG (-195)
#define TFN_ACRE_SEM (-194)
#define TFN_ACRE_TSK (-193)
#define TFN_SAC_MEM (-190)
#define TFN_REF_MEM (-189)
#define TFN_PRB_MEM (-188)
#define TFN_DET_MEM (-187)
#define TFN_ATA_MEM (-186)
#define TFN_ATT_MEM (-185)
#define TFN_SAC_TIM (-182)/* システム時刻管理  */
#define TFN_REF_TIM (-181)/* システム時刻管理  */
#define TFN_REF_OVR (-180)
#define TFN_STP_OVR (-179)
#define TFN_STA_OVR (-178)
#define TFN_DEF_OVR (-177)
#define TFN_REF_ALM (-173) /* アラームハンドラ */
#define TFN_STP_ALM (-172) /* アラームハンドラ */
#define TFN_STA_ALM (-171) /* アラームハンドラ */
#define TFN_DEL_ALM (-170) /* アラームハンドラ */
#define TFN_CRE_ALM (-169) /* アラームハンドラ */
#define TFN_REF_MPL (-168) /* 可変長メモリプール */
#define TFN_TGET_MPL (-167) /* 可変長メモリプール */
#define TFN_PGET_MPL (-166) /* 可変長メモリプール */
#define TFN_GET_MPL (-165) /* 可変長メモリプール */
#define TFN_REL_MPL (-163) /* 可変長メモリプール */
#define TFN_DEL_MPL (-162) /* 可変長メモリプール */
#define TFN_CRE_MPL (-161) /* 可変長メモリプール */
#define TFN_REF_RDV (-159)
#define TFN_REF_POR (-158)
#define TFN_RPL_RDV (-157)
#define TFN_FWD_POR (-156)
#define TFN_TACP_POR (-155)
#define TFN_PACP_POR (-154)
#define TFN_ACP_POR (-153)
#define TFN_TCAL_POR (-152)
#define TFN_CAL_POR (-151)
#define TFN_DEL_POR (-150)
#define TFN_CRE_POR (-149)
#define TFN_REF_MBF (-148)
#define TFN_TRCV_MBF (-147)
#define TFN_PRCV_MBF (-146)
#define TFN_RCV_MBF (-145)
#define TFN_TSND_MBF (-143)
#define TFN_PSND_MBF (-142)
#define TFN_SND_MBF (-141)
#define TFN_DEL_MBF (-138)
#define TFN_CRE_MBF (-137)
#define TFN_REF_MTX (-136)
#define TFN_TLOC_MTX (-135)
#define TFN_PLOC_MTX (-134)
#define TFN_LOC_MTX (-133)
#define TFN_UNL_MTX (-131)
#define TFN_DEL_MTX (-130)
#define TFN_CRE_MTX (-129)
#define TFN_ISTP_ALM (-127) /* アラームハンドラ */
#define TFN_ISTA_ALM (-126) /* アラームハンドラ */
#define TFN_ISIG_TIM (-125) /* システム時刻管理 - サービスコールとしてのisig_timは廃止 */
#define TFN_IUNL_CPU (-124) /* システム状態参照 */
#define TFN_ILOC_CPU (-123) /* システム状態参照 */
#define TFN_IGET_TID (-122) /* システム状態参照 */
#define TFN_IROT_RDQ (-121) /* システム状態参照 */
#define TFN_IFSND_DTQ (-120) /* データキュー */
#define TFN_IPSND_DTQ (-119) /* データキュー */
#define TFN_ISET_FLG (-118) /* イベントフラグ */
#define TFN_ISIG_SEM (-117) /* セマフォ */
#define TFN_IRAS_TEX (-116) /* タスク例外処理 */
#define TFN_IREL_WAI (-115) /* タスク付属同期 */
#define TFN_IWUP_TSK (-114) /* タスク付属同期 */
#define TFN_IACT_TSK (-113) /* タスク管理 */
#define TFN_REF_VER (-112)
#define TFN_REF_CFG (-111)
#define TFN_DEF_EXC (-110)
#define TFN_DEF_SVC (-109)
#define TFN_GET_IPM (-108) /* 割込み管理 */
#define TFN_CHG_IPM (-107) /* 割込み管理 */
#define TFN_ENA_INT (-106) /* 割込み管理 */
#define TFN_DIS_INT (-105) /* 割込み管理 */
#define TFN_REF_ISR (-104)
#define TFN_DEL_ISR (-103)
#define TFN_CRE_ISR (-102)
#define TFN_DEF_INH (-101)
#define TFN_SAC_SYS (-98)
#define TFN_REF_SYS (-97)
#define TFN_SNS_DPN (-96) /* システム状態参照  */
#define TFN_SNS_DSP (-95) /* システム状態参照  */
#define TFN_SNS_LOC (-94) /* システム状態参照  */
#define TFN_SNS_CTX (-93) /* システム状態参照  */
#define TFN_ENA_DSP (-92) /* システム状態参照  */
#define TFN_DIS_DSP (-91) /* システム状態参照  */
#define TFN_UNL_CPU (-90) /* システム状態参照  */
#define TFN_LOC_CPU (-89) /* システム状態参照  */
#define TFN_GET_DID (-87) /* システム状態参照  */
#define TFN_GET_TID (-86) /* システム状態参照  */
#define TFN_ROT_RDQ (-85) /* システム状態参照  */
#define TFN_REF_CYC (-83) /* 周期ハンドラ */
#define TFN_STP_CYC (-82) /* 周期ハンドラ */
#define TFN_STA_CYC (-81) /* 周期ハンドラ */
#define TFN_DEL_CYC (-80) /* 周期ハンドラ */
#define TFN_CRE_CYC (-79) /* 周期ハンドラ */
#define TFN_GET_TIM (-78) /* システム時刻管理  */
#define TFN_SET_TIM (-77) /* システム時刻管理  */
#define TFN_REF_MPF (-76) /* 固定長メモリプール */
#define TFN_TGET_MPF (-75) /* 固定長メモリプール */
#define TFN_PGET_MPF (-74) /* 固定長メモリプール */
#define TFN_GET_MPF (-73) /* 固定長メモリプール */
#define TFN_REL_MPF (-71) /* 固定長メモリプール */
#define TFN_DEL_MPF (-70) /* 固定長メモリプール */
#define TFN_CRE_MPF (-69) /* 固定長メモリプール */
#define TFN_REF_MBX (-68) /* メールボックス */
#define TFN_TRCV_MBX (-67) /* メールボックス */
#define TFN_PRCV_MBX (-66) /* メールボックス */
#define TFN_RCV_MBX (-65) /* メールボックス */
#define TFN_SND_MBX (-63) /* メールボックス */
#define TFN_DEL_MBX (-62) /* メールボックス */
#define TFN_CRE_MBX (-61) /* メールボックス */
#define TFN_REF_DTQ (-60) /* データキュー */
#define TFN_TRCV_DTQ (-59) /* データキュー */
#define TFN_PRCV_DTQ (-58) /* データキュー */
#define TFN_RCV_DTQ (-57) /* データキュー */
#define TFN_FSND_DTQ (-56) /* データキュー */
#define TFN_TSND_DTQ (-55) /* データキュー */
#define TFN_PSND_DTQ (-54) /* データキュー */
#define TFN_SND_DTQ (-53) /* データキュー */
#define TFN_DEL_DTQ (-50) /* データキュー */
#define TFN_CRE_DTQ (-49) /* データキュー */
#define TFN_REF_FLG (-48) /* イベントフラグ */
#define TFN_TWAI_FLG (-47) /* イベントフラグ */
#define TFN_POL_FLG (-46) /* イベントフラグ */
#define TFN_WAI_FLG (-45) /* イベントフラグ */
#define TFN_CLR_FLG (-44) /* イベントフラグ */
#define TFN_SET_FLG (-43) /* イベントフラグ */
#define TFN_DEL_FLG (-42) /* イベントフラグ */
#define TFN_CRE_FLG (-41) /* イベントフラグ */
#define TFN_REF_SEM (-40) /* セマフォ*/
#define TFN_TWAI_SEM (-39) /* セマフォ*/
#define TFN_POL_SEM (-38) /* セマフォ*/
#define TFN_WAI_SEM (-37) /* セマフォ*/
#define TFN_SIG_SEM (-35) /* セマフォ*/
#define TFN_DEL_SEM (-34) /* セマフォ*/
#define TFN_CRE_SEM (-33) /* セマフォ*/
#define TFN_REF_TEX (-32) /* タスク例外処理 */
#define TFN_SNS_TEX (-31) /* タスク例外処理 */
#define TFN_ENA_TEX (-30) /* タスク例外処理 */
#define TFN_DIS_TEX (-29) /* タスク例外処理 */
#define TFN_RAS_TEX (-28) /* タスク例外処理 */
#define TFN_DEF_TEX (-27) /* タスク例外処理 */
#define TFN_DLY_TSK (-25) /* タスク付属同期 - tast_except.c に宣言されていない。 */
#define TFN_FRSM_TSK (-24) /* タスク付属同期 - tast_synk.c に宣言されていない。*/
#define TFN_RSM_TSK (-23) /* タスク付属同期 */
#define TFN_SUS_TSK (-22) /* タスク付属同期 */
#define TFN_REL_WAI (-21) /* タスク付属同期 */
#define TFN_CAN_WUP (-20) /* タスク付属同期 */
#define TFN_WUP_TSK (-19) /* タスク付属同期 */
#define TFN_TSLP_TSK (-18) /* タスク付属同期 */
#define TFN_SLP_TSK (-17) /* タスク付属同期 */
#define TFN_REF_TST (-16) /* どこにも宣言されていない。*/
#define TFN_REF_TSK (-15)) /* タスク状態参照 - task_refer.c に宣言されている。*/
#define TFN_GET_PRI (-14) /* タスク管理 */
#define TFN_CHG_PRI (-13) /* タスク管理 */
#define TFN_TER_TSK (-12) /* タスク管理 */
#define TFN_EXD_TSK (-11) /* タスク管理 - tast_manage.c に宣言されていない。 */
#define TFN_EXT_TSK (-10) /* タスク管理 */
#define TFN_STA_TSK (-9) /* タスク管理期 - tast_manage.c に宣言されていない。 */
#define TFN_CAN_ACT (-8) /* タスク管理 */
#define TFN_ACT_TSK (-7) /* タスク管理 */
#define TFN_DEL_TSK (-6) /* タスク管理期 - tast_manage.c に宣言されていない。 */
#define TFN_CRE_TSK (-5) /* タスク管理期 - tast_manage.c に宣言されていない。 */

#endif /* _KERNEL_FNCODE_H_ */
