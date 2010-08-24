/*
 *  TOPPERS/ASP Kernel
 *      Toyohashi Open Platform for Embedded Real-Time Systems/
 *      Advanced Standard Profile Kernel
 * 
 *  Copyright (C) 2000-2003 by Embedded and Real-Time Systems Laboratory
 *                              Toyohashi Univ. of Technology, JAPAN
 *  Copyright (C) 2004-2008 by Embedded and Real-Time Systems Laboratory
 *              Graduate School of Information Science, Nagoya Univ., JAPAN
 * 
 *  æÂµ≠√¯∫˚‘¢º‘§œ°§∞ ≤º§Œ(1)°¡(4)§ŒæÚ∑ÅEÚÀ˛§ø§πæÅEÁ§À∏¬§Í°§À‹•Ω•’•»•¶•ß
 *  •¢° À‹•Ω•’•»•¶•ß•¢§Ú≤˛ —§∑§ø§‚§Œ§Ú¥ﬁ§‡°•∞ ≤º∆±§∏°À§Úª»Õ—°¶ £¿Ω°¶≤˛
 *   —°¶∫∆«€…€° ∞ ≤º°§Õ¯Õ—§»∏∆§÷°À§π§ÅE≥§»§ÚÃµΩ˛§«µˆ¬˙§π§ÅE•
 *  (1) À‹•Ω•’•»•¶•ß•¢§Ú•Ω°º•π•≥°º•…§Œ∑¡§«Õ¯Õ—§π§ÅEÅEÁ§À§œ°§æÂµ≠§Œ√¯∫ÅE *      ∏¢…Ωº®°§§≥§ŒÕ¯Õ—æÚ∑ÅE™§Ë§”≤ºµ≠§ŒÃµ ›æ⁄µ¨ƒÍ§¨°§§Ω§Œ§ﬁ§ﬁ§Œ∑¡§«•Ω°º
 *      •π•≥°º•…√Ê§À¥ﬁ§ﬁ§ÅE∆§§§ÅE≥§»°•
 *  (2) À‹•Ω•’•»•¶•ß•¢§Ú°§•È•§•÷•È•ÅE¡º∞§ §…°§¬æ§Œ•Ω•’•»•¶•ß•¢≥´»Ø§Àª»
 *      Õ—§«§≠§ÅE¡§«∫∆«€…€§π§ÅEÅEÁ§À§œ°§∫∆«€…€§À»º§¶•…•≠•Â•·•Û•»° Õ¯Õ—
 *      º‘•ﬁ•À•Â•¢•ÅE §…°À§À°§æÂµ≠§Œ√¯∫˚‘¢…Ωº®°§§≥§ŒÕ¯Õ—æÚ∑ÅE™§Ë§”≤ºµ≠
 *      §ŒÃµ ›æ⁄µ¨ƒÍ§Ú∑«∫‹§π§ÅE≥§»°•
 *  (3) À‹•Ω•’•»•¶•ß•¢§Ú°§µ°¥ÅEÀ¡»§ﬂπ˛§‡§ §…°§¬æ§Œ•Ω•’•»•¶•ß•¢≥´»Ø§Àª»
 *      Õ—§«§≠§ §§∑¡§«∫∆«€…€§π§ÅEÅEÁ§À§œ°§º°§Œ§§§∫§ÅE´§ŒæÚ∑ÅEÚÀ˛§ø§π§≥
 *      §»°•
 *    (a) ∫∆«€…€§À»º§¶•…•≠•Â•·•Û•»° Õ¯Õ—º‘•ﬁ•À•Â•¢•ÅE §…°À§À°§æÂµ≠§Œ√ÅE *        ∫˚‘¢…Ωº®°§§≥§ŒÕ¯Õ—æÚ∑ÅE™§Ë§”≤ºµ≠§ŒÃµ ›æ⁄µ¨ƒÍ§Ú∑«∫‹§π§ÅE≥§»°•
 *    (b) ∫∆«€…€§Œ∑¡¬÷§Ú°§ Ã§ÀƒÍ§·§ÅE˝À°§À§Ë§√§∆°§TOPPERS•◊•˙¡∏•ß•Ø•»§À
 *         Ûπ§π§ÅE≥§»°•
 *  (4) À‹•Ω•’•»•¶•ß•¢§ŒÕ¯Õ—§À§Ë§ÅEæ¿‹≈™§ﬁ§ø§œ¥÷¿‹≈™§À¿∏§∏§ÅE§§´§ §ÅEª
 *      ≥≤§´§È§‚°§æÂµ≠√¯∫˚‘¢º‘§™§Ë§”TOPPERS•◊•˙¡∏•ß•Ø•»§ÚÃ»¿’§π§ÅE≥§»°•
 *      §ﬁ§ø°§À‹•Ω•’•»•¶•ß•¢§Œ•Ê°º•∂§ﬁ§ø§œ•®•Û•…•Ê°º•∂§´§È§Œ§§§´§ §ÅE˝
 *      Õ≥§À¥§≈§Ø¿¡µ·§´§È§‚°§æÂµ≠√¯∫˚‘¢º‘§™§Ë§”TOPPERS•◊•˙¡∏•ß•Ø•»§ÅE *      Ã»¿’§π§ÅE≥§»°•
 * 
 *  À‹•Ω•’•»•¶•ß•¢§œ°§Ãµ ›æ⁄§«ƒÛ∂°§µ§ÅE∆§§§ÅE‚§Œ§«§¢§ÅE•æÂµ≠√¯∫˚‘¢º‘§™
 *  §Ë§”TOPPERS•◊•˙¡∏•ß•Ø•»§œ°§À‹•Ω•’•»•¶•ß•¢§À¥ÿ§∑§∆°§∆√ƒÍ§Œª»Õ—Ã‹≈™
 *  §À¬–§π§ÅE¨πÁ¿≠§‚¥ﬁ§·§∆°§§§§´§ §ÅE›æ⁄§‚π‘§ÅE §§°•§ﬁ§ø°§À‹•Ω•’•»•¶•ß
 *  •¢§ŒÕ¯Õ—§À§Ë§ÅEæ¿‹≈™§ﬁ§ø§œ¥÷¿‹≈™§À¿∏§∏§ø§§§´§ §ÅEª≥≤§À¥ÿ§∑§∆§‚°§§Ω
 *  §Œ¿’«§§Ú…È§ÅE §§°•
 * 
 *  @(#) $Id: trace_config.h 264 2007-07-12 08:23:10Z hiro $
 */

/*
 *		•»•ÅEº•π•˙¡∞§À¥ÿ§π§ÅEﬂƒÅE *
 *  §≥§Œ•§•Û•Ø•ÅEº•…•’•°•§•ÅEœ°§target_config.h§™§Ë§”target_syssvc.h§Œ
 *  §ﬂ§´§È•§•Û•Ø•ÅEº•…§µ§ÅEÅE•¬æ§Œ•’•°•§•ÅE´§Èƒæ¿‹•§•Û•Ø•ÅEº•…§∑§∆§œ§ 
 *  §È§ §§°•
 */

#ifndef TOPPERS_TRACE_CONFIG_H
#define TOPPERS_TRACE_CONFIG_H

/*
 *  µ°«Ω•≥°º•…§Œ∆…§ﬂπ˛§ﬂ
 */
#include "kernel_fncode.h"

/*
 *  •»•ÅEº•π•˙¡∞•–•√•’•°§Œ•µ•§•∫
 */
#ifndef TCNT_TRACE_BUFFER
#define TCNT_TRACE_BUFFER	8192
#endif /* TCNT_TRACE_BUFFER */

/*
 *  •»•ÅEº•πª˛πÅEË∆¿Õ—•œ°º•…•¶•ß•¢§ŒΩÈ¥ÅEΩ
 */
#ifndef TRACE_HW_INIT
#define TRACE_HW_INIT()
#endif  /* TRACE_HW_INIT */

/*
 *  •»•ÅEº•πª˛πÅEŒºË∆¿ ˝À°
 */
#ifndef TRACE_GET_TIM
#ifdef TOPPERS_SUPPORT_GET_UTM
#define TRACE_GET_TIM()		(trace_get_tim_utime())
#else
#define TRACE_GET_TIM()		(current_time)
#endif /* TOPPERS_SUPPORT_GET_UTM */
#endif /* TRACE_GET_TIM */

#ifndef TOPPERS_MACRO_ONLY

/*
 *  •»•ÅEº•π•˙¡∞§Œ•«°º•øπΩ¬§
 *
 *  •∑•π•∆•‡•˙¡∞µ°«Ω§Œ•˙¡∞æ Û§Œ•«°º•øπΩ¬§§»∆±§∏§‚§Œ§ÚÕ—§§§ÅE•
 */
#include <t_syslog.h>
typedef	SYSLOG	TRACE;

/*
 *  •»•ÅEº•π•˙¡∞•–•√•’•°§»§Ω§ÅEÀ•¢•Ø•ª•π§π§ÅEø§·§Œ•›•§•Û•ø
 */
extern TRACE	trace_buffer[];		/* •»•ÅEº•π•˙¡∞•–•√•’•° */
extern uint_t	trace_count;		/* •»•ÅEº•π•˙¡∞•–•√•’•°√Ê§Œ•˙¡∞§ŒøÅE*/
extern uint_t	trace_head;			/* ¿Ë∆¨§Œ•»•ÅEº•π•˙¡∞§Œ≥ «º∞Ã√÷ */
extern uint_t	trace_tail;			/* º°§Œ•»•ÅEº•π•˙¡∞§Œ≥ «º∞Ã√÷ */
extern uint_t	trace_lost;			/* º∫§ÅEÅEø•»•ÅEº•π§ŒøÅE*/

#endif /* TOPPERS_MACRO_ONLY */

/*
 *  •»•ÅEº•π•‚°º•…§ŒƒÅE¡
 */
#define TRACE_STOP			UINT_C(0x00)	/* •»•ÅEº•πƒ‰ªﬂ */
#define TRACE_RINGBUF		UINT_C(0x01)	/* •ÅEÛ•∞•–•√•’•°•‚°º•… */
#define TRACE_AUTOSTOP		UINT_C(0x02)	/* º´∆∞ƒ‰ªﬂ•‚°º•… */
#define TRACE_CLEAR			UINT_C(0x04)	/* •»•ÅEº•π•˙¡∞§Œ•Ø•ÅE¢ */

#ifndef TOPPERS_MACRO_ONLY

/*
 *  •»•ÅEº•π•˙¡∞µ°«Ω§ŒΩÈ¥ÅEΩ
 *
 *  •»•ÅEº•π•˙¡∞µ°«Ω§ÚΩÈ¥ÅEΩ§π§ÅE•ΩÈ¥ÅEΩ•ÅEº•¡•Û§»§∑§∆≈–œø§π§ÅE≥§»§Ú¡€
 *  ƒÍ§∑§∆§§§ÅE•∞˙øÙ§À§Ë§ÅE°§Œ∆∞∫˚¿Úπ‘§¶°•
 *
 *  TRACE_STOP°ßΩÈ¥ÅEΩ§Œ§ﬂ§«•»•ÅEº•π§œ≥´ªœ§∑§ §§°•
 *  TRACE_RINGBUF°ß•ÅEÛ•∞•–•√•’•°•‚°º•…§«•»•ÅEº•π§Ú≥´ªœ°•
 *  TRACE_AUTOSTOP°ßº´∆∞ƒ‰ªﬂ•‚°º•…§«•»•ÅEº•π§Ú≥´ªœ°•
 */
extern void	trace_initialize(intptr_t exinf);

/*
 *  •»•ÅEº•π•˙¡∞§Œ≥´ªœ
 *
 *  •»•ÅEº•π•˙¡∞§Œµ≠œø§Ú≥´ªœ°øƒ‰ªﬂ§π§ÅE•∞˙øÙ§À§Ë§ÅE°§Œ∆∞∫˚¿Úπ‘§¶°•
 *
 *  TRACE_STOP°ß•»•ÅEº•π§Úƒ‰ªﬂ°•
 *  TRACE_RINGBUF°ß•ÅEÛ•∞•–•√•’•°•‚°º•…§«•»•ÅEº•π§Ú≥´ªœ°•
 *  TRACE_AUTOSTOP°ßº´∆∞ƒ‰ªﬂ•‚°º•…§«•»•ÅEº•π§Ú≥´ªœ°•
 *  TRACE_CLEAR°ß•»•ÅEº•π•˙¡∞§Ú•Ø•ÅE¢°•
 */
extern ER	trace_sta_log(MODE mode);

/*
 *  •»•ÅEº•π•˙¡∞§ŒΩÒπ˛§ﬂ
 */
extern ER	trace_wri_log(TRACE *p_trace);

/*
 *  •»•ÅEº•π•˙¡∞§Œ∆…Ω–§∑
 */
extern ER	trace_rea_log(TRACE *p_trace);

/* 
 *  •»•ÅEº•π•˙¡∞§Œ•¿•Û•◊° trace_dump.c°À
 *
 *  •»•ÅEº•π•˙¡∞§Ú•¿•Û•◊§π§ÅE•Ω™ŒªΩËÕ˝•ÅEº•¡•Û§»§∑§∆≈–œø§π§ÅE≥§»§‚¡€ƒÅE *  §∑§∆§§§ÅE•∞˙øÙ§»§∑§∆°§•¿•Û•◊¿Ë§»§ §ÅE∏ª˙Ω–Œœ¥ÿøÙ§ÿ§Œ•›•§•Û•ø§Ú≈œ§π°•
 *  •ø°º•≤•√•»∞Õ¬∏§Œƒ„•ÅEŸ•ÅE∏ª˙Ω–Œœ§ÚÕ¯Õ—§π§ÅEÅEÁ§À§œ°§target_putc§Ú≈œ
 *  §π°•
 */
extern void	trace_dump(intptr_t exinf);

/*
 *  •»•ÅEº•π•˙¡∞§ÚΩ–Œœ§π§ÅEø§·§Œ•È•§•÷•È•ÅEÿøÅE */
extern void	trace_write_0(uint_t type);
extern void	trace_write_1(uint_t type, intptr_t arg1);
extern void	trace_write_2(uint_t type, intptr_t arg1, intptr_t arg2);
extern void	trace_write_3(uint_t type, intptr_t arg1, intptr_t arg2, intptr_t arg3);
extern void	trace_write_4(uint_t type, intptr_t arg1, intptr_t arg2, intptr_t arg3, \
                              intptr_t arg4);
extern void	trace_write_5(uint_t type, intptr_t arg1, intptr_t arg2, intptr_t arg3, \
                              intptr_t arg4, intptr_t arg5);
extern void	trace_write_6(uint_t type, intptr_t arg1, intptr_t arg2, intptr_t arg3, \
                              intptr_t arg4, intptr_t arg5, intptr_t arg6);

/*
 *  •»•ÅEº•π•˙¡∞§ÚΩ–Œœ§π§ÅEø§·§Œ•ﬁ•Ø•ÅE */

#define trace_0(type) \
				trace_write_0(type)

#define trace_1(type, arg1) \
				trace_write_1(type, (intptr_t)(arg1))

#define trace_2(type, arg1, arg2) \
				trace_write_2(type, (intptr_t)(arg1), (intptr_t)(arg2))

#define trace_3(type, arg1, arg2, arg3) \
				trace_write_3(type, (intptr_t)(arg1), (intptr_t)(arg2), \
						(intptr_t)(arg3))

#define trace_4(type, arg1, arg2, arg3, arg4) \
				trace_write_4(type, (intptr_t)(arg1), (intptr_t)(arg2), \
						(intptr_t)(arg3), (intptr_t)(arg4))

#define trace_5(type, arg1, arg2, arg3, arg4, arg5) \
				trace_write_5(type, (intptr_t)(arg1), (intptr_t)(arg2), \
						(intptr_t)(arg3), (intptr_t)(arg4), \
                                              (intptr_t)(arg5))

#define trace_6(type, arg1, arg2, arg3, arg4, arg5, arg6) \
				trace_write_6(type, (intptr_t)(arg1), (intptr_t)(arg2), \
						(intptr_t)(arg3), (intptr_t)(arg4), \
                                              (intptr_t)(arg5), (intptr_t)(arg6))

#endif /* TOPPERS_MACRO_ONLY */

/*
 *  •»•ÅEº•π•˙¡∞ ˝À°§Œ¿ﬂƒÅE */

/*
 *  ≥‰π˛§ﬂ•œ•Û•…•È§Œ¡∞∏ÅE */
#define LOG_INH_ENTER(inhno) trace_1(LOG_TYPE_INH|LOG_ENTER, inhno)
#define LOG_INH_LEAVE(inhno) trace_1(LOG_TYPE_INH|LOG_LEAVE, inhno)

/*
 *  ≥‰π˛§ﬂ•µ°º•”•π•ÅEº•¡•Û§Œ¡∞∏ÅE */
#define LOG_ISR_ENTER(intno) trace_1(LOG_TYPE_ISR|LOG_ENTER, intno)
#define LOG_ISR_LEAVE(intno) trace_1(LOG_TYPE_ISR|LOG_LEAVE, intno)

/*
 *  º˛¥ÅEœ•Û•…•È§Œ¡∞∏ÅE */
#define LOG_CYC_ENTER(p_cyccb) trace_1(LOG_TYPE_CYC|LOG_ENTER, p_cyccb)
#define LOG_CYC_LEAVE(p_cyccb) trace_1(LOG_TYPE_CYC|LOG_LEAVE, p_cyccb)

/*
 *  •¢•È°º•‡•œ•Û•…•È§Œ¡∞∏ÅE */
#define LOG_ALM_ENTER(p_almcb) trace_1(LOG_TYPE_ALM|LOG_ENTER, p_almcb)
#define LOG_ALM_LEAVE(p_almcb) trace_1(LOG_TYPE_ALM|LOG_LEAVE, p_almcb)

/*
 *  CPUŒ„≥∞•œ•Û•…•È§Œ¡∞∏ÅE */
#define LOG_EXC_ENTER(excno) trace_1(LOG_TYPE_EXC|LOG_ENTER, excno)
#define LOG_EXC_LEAVE(excno) trace_1(LOG_TYPE_EXC|LOG_LEAVE, excno)

/*
 *  •ø•π•ØŒ„≥∞ΩËÕ˝•ÅEº•¡•ÅE */
#define LOG_TEX_ENTER(p_tcb, texptn) trace_2(LOG_TYPE_TEX|LOG_ENTER, p_tcb, texptn)
#if TKERNEL_PRVER < 0x1050
#define LOG_TEX_LEAVE(p_tcb, texptn) trace_2(LOG_TYPE_TEX|LOG_LEAVE,p_tcb, texptn)
#else
#define LOG_TEX_LEAVE(p_tcb) trace_2(LOG_TYPE_TEX|LOG_LEAVE, p_tcb, texptn)
#endif /* TKERNEL_PRVER < 0x1050 */
/*#define LOG_TEX_LEAVE(p_tcb, texptn) trace_2(LOG_TYPE_TEX|LOG_LEAVE, p_tcb, texptn)*/

/*
 *  •ø•π•Ø§Œæı¬÷ —ππ
 */
#define LOG_TSKSTAT(p_tcb) trace_2(LOG_TYPE_TSKSTAT, p_tcb, p_tcb->tstat)

/*
 *  •«•£•π•—•√•¡•„§Œ¡∞∏ÅE */
#define LOG_DSP_ENTER(p_tcb) trace_1(LOG_TYPE_DSP|LOG_ENTER, p_tcb)
#define LOG_DSP_LEAVE(p_tcb) trace_1(LOG_TYPE_DSP|LOG_LEAVE, p_tcb)

/*
 *  •∑•π•∆•‡•˙¡∞§ÿ§ŒΩÒ§≠Ω–§∑
 */
#define LOG_SYSLOG_WRI_LOG_ENTER(prio, p_syslog) trace_wri_log((TRACE *) p_syslog)

/*
 *  •∑•π•∆•‡•≥°º•ÅE */

/*
 *  •ø•π•Ø¥…Õ˝µ°«Ω
 */
#define LOG_ACT_TSK_ENTER(tskid) trace_2(LOG_TYPE_SVC|LOG_ENTER, TFN_ACT_TSK, tskid)
#define LOG_ACT_TSK_LEAVE(ercd) trace_2(LOG_TYPE_SVC|LOG_LEAVE, TFN_ACT_TSK, ercd)

#define LOG_IACT_TSK_ENTER(tskid) trace_2(LOG_TYPE_SVC|LOG_ENTER, TFN_IACT_TSK, tskid)
#define LOG_IACT_TSK_LEAVE(ercd) trace_2(LOG_TYPE_SVC|LOG_LEAVE, TFN_IACT_TSK, ercd)

#define LOG_CAN_ACT_ENTER(tskid) trace_2(LOG_TYPE_SVC|LOG_ENTER, TFN_CAN_ACT, tskid)
#define LOG_CAN_ACT_LEAVE(ercd) trace_2(LOG_TYPE_SVC|LOG_LEAVE, TFN_CAN_ACT, ercd)

#define LOG_EXT_TSK_ENTER() trace_1(LOG_TYPE_SVC|LOG_ENTER, TFN_EXT_TSK)
#define LOG_EXT_TSK_LEAVE(ercd) trace_2(LOG_TYPE_SVC|LOG_LEAVE, TFN_EXT_TSK, ercd)

#define LOG_TER_TSK_ENTER(tskid) trace_2(LOG_TYPE_SVC|LOG_ENTER, TFN_TER_TSK, tskid)
#define LOG_TER_TSK_LEAVE(ercd) trace_2(LOG_TYPE_SVC|LOG_LEAVE, TFN_TER_TSK, ercd)

#define LOG_CHG_PRI_ENTER(tskid, tskpri) trace_3(LOG_TYPE_SVC|LOG_ENTER, TFN_CHG_PRI, tskid, tskpri)
#define LOG_CHG_PRI_LEAVE(ercd) trace_2(LOG_TYPE_SVC|LOG_LEAVE, TFN_CHG_PRI, ercd)

#define LOG_GET_PRI_ENTER(tskid, p_tskpri) trace_3(LOG_TYPE_SVC|LOG_ENTER, TFN_GET_PRI, tskid, p_tskpri)
#define LOG_GET_PRI_LEAVE(ercd, tskpri) trace_3(LOG_TYPE_SVC|LOG_LEAVE, TFN_GET_PRI, ercd, tskpri)

#define LOG_GET_INF_ENTER(p_exinf) trace_2(LOG_TYPE_SVC|LOG_ENTER, TFN_GET_INF, p_exinf)
#define LOG_GET_INF_LEAVE(ercd, exinf) trace_3(LOG_TYPE_SVC|LOG_LEAVE, TFN_GET_INF, ercd, exinf)


/*
 *  •ø•π•Øæı¬÷ª≤æ»µ°«Ω
 */
#define LOG_REF_TSK_ENTER(tskid, pk_rtsk) trace_3(LOG_TYPE_SVC|LOG_ENTER, TFN_REF_TSK, tskid, pk_rtsk)
#define LOG_REF_TSK_LEAVE(ercd, pk_rtsk) trace_3(LOG_TYPE_SVC|LOG_LEAVE, TFN_REF_TSK, ercd, pk_rtsk)


/*
 *  •ø•π•Ø…’¬∞∆±¥ÅE°«Ω
 */
#define LOG_SLP_TSK_ENTER() trace_1(LOG_TYPE_SVC|LOG_ENTER, TFN_SLP_TSK)
#define LOG_SLP_TSK_LEAVE(ercd) trace_2(LOG_TYPE_SVC|LOG_LEAVE, TFN_SLP_TSK, ercd)

#define LOG_TSLP_TSK_ENTER(tmout) trace_2(LOG_TYPE_SVC|LOG_ENTER, TFN_TSLP_TSK, tmout)
#define LOG_TSLP_TSK_LEAVE(ercd) trace_2(LOG_TYPE_SVC|LOG_LEAVE, TFN_TSLP_TSK, ercd)

#define LOG_WUP_TSK_ENTER(tskid) trace_2(LOG_TYPE_SVC|LOG_ENTER, TFN_WUP_TSK, tskid)
#define LOG_WUP_TSK_LEAVE(ercd) trace_2(LOG_TYPE_SVC|LOG_LEAVE, TFN_WUP_TSK, ercd)

#define LOG_IWUP_TSK_ENTER(tskid) trace_2(LOG_TYPE_SVC|LOG_ENTER, TFN_IWUP_TSK, tskid) 
#define LOG_IWUP_TSK_LEAVE(ercd) trace_2(LOG_TYPE_SVC|LOG_LEAVE, TFN_IWUP_TSK, ercd)

#define LOG_CAN_WUP_ENTER(tskid) trace_2(LOG_TYPE_SVC|LOG_ENTER, TFN_CAN_WUP, tskid)
#define LOG_CAN_WUP_LEAVE(ercd) trace_2(LOG_TYPE_SVC|LOG_LEAVE, TFN_CAN_WUP, ercd)

#define LOG_REL_WAI_ENTER(tskid) trace_2(LOG_TYPE_SVC|LOG_ENTER, TFN_REL_WAI, tskid)
#define LOG_REL_WAI_LEAVE(ercd) trace_2(LOG_TYPE_SVC|LOG_LEAVE, TFN_REL_WAI, ercd)

#define LOG_IREL_WAI_ENTER(tskid) trace_2(LOG_TYPE_SVC|LOG_ENTER, TFN_IREL_WAI, tskid)
#define LOG_IREL_WAI_LEAVE(ercd) trace_2(LOG_TYPE_SVC|LOG_LEAVE, TFN_IREL_WAI, ercd)

#define LOG_SUS_TSK_ENTER(tskid) trace_2(LOG_TYPE_SVC|LOG_ENTER, TFN_SUS_TSK, tskid)
#define LOG_SUS_TSK_LEAVE(ercd) trace_2(LOG_TYPE_SVC|LOG_LEAVE, TFN_SUS_TSK, ercd)

#define LOG_RSM_TSK_ENTER(tskid) trace_2(LOG_TYPE_SVC|LOG_ENTER, TFN_RSM_TSK, tskid)
#define LOG_RSM_TSK_LEAVE(ercd) trace_2(LOG_TYPE_SVC|LOG_LEAVE, TFN_RSM_TSK, ercd)

#define LOG_DLY_TSK_ENTER(dlytim) trace_2(LOG_TYPE_SVC|LOG_ENTER, TFN_DLY_TSK, dlytim)
#define LOG_DLY_TSK_LEAVE(ercd) trace_2(LOG_TYPE_SVC|LOG_LEAVE, TFN_DLY_TSK, ercd)


/*
 *  •ø•π•ØŒ„≥∞ΩËÕ˝µ°«Ω
 */
#define LOG_RAS_TEX_ENTER(tskid, rasptn) trace_3(LOG_TYPE_SVC|LOG_ENTER, TFN_RAS_TEX, tskid, rasptn)
#define LOG_RAS_TEX_LEAVE(ercd) trace_2(LOG_TYPE_SVC|LOG_LEAVE, TFN_RAS_TEX, ercd)

#define LOG_IRAS_TEX_ENTER(tskid, rasptn) trace_3(LOG_TYPE_SVC|LOG_ENTER, TFN_IRAS_TEX, tskid, rasptn)
#define LOG_IRAS_TEX_LEAVE(ercd) trace_2(LOG_TYPE_SVC|LOG_LEAVE, TFN_IRAS_TEX, ercd)

#define LOG_DIS_TEX_ENTER() trace_1(LOG_TYPE_SVC|LOG_ENTER, TFN_DIS_TEX)
#define LOG_DIS_TEX_LEAVE(ercd) trace_2(LOG_TYPE_SVC|LOG_LEAVE, TFN_DIS_TEX, ercd)

#define LOG_ENA_TEX_ENTER() trace_1(LOG_TYPE_SVC|LOG_ENTER, TFN_ENA_TEX)
#define LOG_ENA_TEX_LEAVE(ercd) trace_2(LOG_TYPE_SVC|LOG_LEAVE, TFN_ENA_TEX, ercd)

#define LOG_SNS_TEX_ENTER() trace_1(LOG_TYPE_SVC|LOG_ENTER, TFN_SNS_TEX)
#define LOG_SNS_TEX_LEAVE(state) trace_2(LOG_TYPE_SVC|LOG_LEAVE, TFN_SNS_TEX, state)

#define LOG_REF_TEX_ENTER(tskid, pk_rtex) trace_3(LOG_TYPE_SVC|LOG_ENTER, TFN_REF_TEX, tskid, pk_rtex)
#define LOG_REF_TEX_LEAVE(ercd, pk_rtex) trace_3(LOG_TYPE_SVC|LOG_LEAVE, TFN_REF_TEX, ercd, pk_rtex)


/*
 *  •ª•ﬁ•’•©µ°«Ω
 */
#define LOG_SIG_SEM_ENTER(semid) trace_2(LOG_TYPE_SVC|LOG_ENTER, TFN_SIG_SEM, semid)
#define LOG_SIG_SEM_LEAVE(ercd) trace_2(LOG_TYPE_SVC|LOG_LEAVE, TFN_SIG_SEM, ercd)

#define LOG_ISIG_SEM_ENTER(semid) trace_2(LOG_TYPE_SVC|LOG_ENTER, TFN_ISIG_SEM, semid)
#define LOG_ISIG_SEM_LEAVE(ercd) trace_2(LOG_TYPE_SVC|LOG_LEAVE, TFN_ISIG_SEM, ercd)

#define LOG_WAI_SEM_ENTER(semid) trace_2(LOG_TYPE_SVC|LOG_ENTER, TFN_WAI_SEM, semid)
#define LOG_WAI_SEM_LEAVE(ercd) trace_2(LOG_TYPE_SVC|LOG_LEAVE, TFN_WAI_SEM, ercd)

#define LOG_POL_SEM_ENTER(semid) trace_2(LOG_TYPE_SVC|LOG_ENTER, TFN_POL_SEM, semid)
#define LOG_POL_SEM_LEAVE(ercd) trace_2(LOG_TYPE_SVC|LOG_LEAVE, TFN_POL_SEM, ercd)

#define LOG_TWAI_SEM_ENTER(semid, tmout) trace_3(LOG_TYPE_SVC|LOG_ENTER, TFN_TWAI_SEM, semid, tmout)
#define LOG_TWAI_SEM_LEAVE(ercd) trace_2(LOG_TYPE_SVC|LOG_LEAVE, TFN_TWAI_SEM, ercd)

#define LOG_INI_SEM_ENTER(semid) trace_2(LOG_TYPE_SVC|LOG_ENTER, TFN_INI_SEM, semid)
#define LOG_INI_SEM_LEAVE(ercd) trace_2(LOG_TYPE_SVC|LOG_LEAVE, TFN_INI_SEM, ercd)

#define LOG_REF_SEM_ENTER(semid, pk_rsem) trace_3(LOG_TYPE_SVC|LOG_ENTER, TFN_REF_SEM, semid, pk_rsem)
#define LOG_REF_SEM_LEAVE(ercd, pk_rsem) trace_3(LOG_TYPE_SVC|LOG_LEAVE, TFN_REF_SEM, ercd, pk_rsem)


/*
 *  •§•Ÿ•Û•»•’•È•∞
 */
#define LOG_SET_FLG_ENTER(flgid, setptn) trace_3(LOG_TYPE_SVC|LOG_ENTER, TFN_SET_FLG, flgid, setptn)
#define LOG_SET_FLG_LEAVE(ercd) trace_2(LOG_TYPE_SVC|LOG_LEAVE, TFN_SET_FLG, ercd)

#define LOG_ISET_FLG_ENTER(flgid, setptn) trace_3(LOG_TYPE_SVC|LOG_ENTER, TFN_ISET_FLG, flgid, setptn)
#define LOG_ISET_FLG_LEAVE(ercd) trace_2(LOG_TYPE_SVC|LOG_LEAVE, TFN_ISET_FLG, ercd)

#define LOG_CLR_FLG_ENTER(flgid, clrptn) trace_3(LOG_TYPE_SVC|LOG_ENTER, TFN_CLR_FLG, flgid, clrptn)
#define LOG_CLR_FLG_LEAVE(ercd) trace_2(LOG_TYPE_SVC|LOG_LEAVE, TFN_CLR_FLG, ercd)

#define LOG_WAI_FLG_ENTER(flgid, waiptn, wfmode, p_flgptn) trace_5(LOG_TYPE_SVC|LOG_ENTER, TFN_WAI_FLG, flgid, waiptn, wfmode, p_flgptn)
#define LOG_WAI_FLG_LEAVE(ercd, flgptn) trace_3(LOG_TYPE_SVC|LOG_LEAVE, TFN_WAI_FLG, ercd, flgptn)

#define LOG_POL_FLG_ENTER(flgid, waiptn, wfmode, p_flgptn) trace_5(LOG_TYPE_SVC|LOG_ENTER, TFN_POL_FLG, flgid, waiptn, wfmode, p_flgptn)
#define LOG_POL_FLG_LEAVE(ercd, flgptn) trace_3(LOG_TYPE_SVC|LOG_LEAVE, TFN_POL_FLG, ercd, flgptn)

#define LOG_TWAI_FLG_ENTER(flgid, waiptn, wfmode, p_flgptn, tmout) trace_6(LOG_TYPE_SVC|LOG_ENTER, TFN_TWAI_FLG, flgid, waiptn, wfmode, p_flgptn, tmout)
#define LOG_TWAI_FLG_LEAVE(ercd, flgptn) trace_3(LOG_TYPE_SVC|LOG_LEAVE, TFN_TWAI_FLG, ercd, flgptn)

#define LOG_INI_FLG_ENTER(flgid) trace_2(LOG_TYPE_SVC|LOG_ENTER, TFN_INI_FLG, flgid)
#define LOG_INI_FLG_LEAVE(ercd) trace_2(LOG_TYPE_SVC|LOG_LEAVE, TFN_INI_FLG, ercd)

#define LOG_REF_FLG_ENTER(flgid, pk_rflg) trace_3(LOG_TYPE_SVC|LOG_ENTER, TFN_REF_FLG, flgid, pk_rflg)
#define LOG_REF_FLG_LEAVE(ercd, pk_rflg) trace_3(LOG_TYPE_SVC|LOG_LEAVE, TFN_REF_FLG, ercd, pk_rflg)


/*
 *  •«°º•ø•≠•Â°ºµØÕ—
 */
#define LOG_SND_DTQ_ENTER(dtqid, data) trace_3(LOG_TYPE_SVC|LOG_ENTER, TFN_SND_DTQ, dtqid, data)
#define LOG_SND_DTQ_LEAVE(ercd) trace_2(LOG_TYPE_SVC|LOG_LEAVE, TFN_SND_DTQ, ercd)

#define LOG_PSND_DTQ_ENTER(dtqid, data) trace_3(LOG_TYPE_SVC|LOG_ENTER, TFN_PSND_DTQ, dtqid, data)
#define LOG_PSND_DTQ_LEAVE(ercd) trace_2(LOG_TYPE_SVC|LOG_LEAVE, TFN_PSND_DTQ, ercd)

#define LOG_IPSND_DTQ_ENTER(dtqid, data) trace_3(LOG_TYPE_SVC|LOG_ENTER, TFN_IPSND_DTQ, dtqid, data)
#define LOG_IPSND_DTQ_LEAVE(ercd) trace_2(LOG_TYPE_SVC|LOG_LEAVE, TFN_IPSND_DTQ, ercd)

#define LOG_TSND_DTQ_ENTER(dtqid, data, tmout) trace_4(LOG_TYPE_SVC|LOG_ENTER, TFN_TSND_DTQ, dtqid, data, tmout)
#define LOG_TSND_DTQ_LEAVE(ercd) trace_2(LOG_TYPE_SVC|LOG_LEAVE, TFN_TSND_DTQ, ercd)

#define LOG_FSND_DTQ_ENTER(dtqid, data) trace_3(LOG_TYPE_SVC|LOG_ENTER, TFN_FSND_DTQ, dtqid, data)
#define LOG_FSND_DTQ_LEAVE(ercd) trace_2(LOG_TYPE_SVC|LOG_LEAVE, TFN_FSND_DTQ, ercd)

#define LOG_IFSND_DTQ_ENTER(dtqid, data) trace_3(LOG_TYPE_SVC|LOG_ENTER, TFN_IFSND_DTQ, dtqid, data)
#define LOG_IFSND_DTQ_LEAVE(ercd) trace_2(LOG_TYPE_SVC|LOG_LEAVE, TFN_IFSND_DTQ, ercd)

#define LOG_RCV_DTQ_ENTER(dtqid, p_data) trace_3(LOG_TYPE_SVC|LOG_ENTER, TFN_RCV_DTQ, dtqid, p_data)
#define LOG_RCV_DTQ_LEAVE(ercd, data) trace_3(LOG_TYPE_SVC|LOG_LEAVE, TFN_RCV_DTQ, ercd, data)

#define LOG_PRCV_DTQ_ENTER(dtqid, p_data) trace_3(LOG_TYPE_SVC|LOG_ENTER, TFN_PRCV_DTQ, dtqid, p_data)
#define LOG_PRCV_DTQ_LEAVE(ercd, data) trace_3(LOG_TYPE_SVC|LOG_LEAVE, TFN_PRCV_DTQ, ercd, data)

#define LOG_TRCV_DTQ_ENTER(dtqid, p_data, tmout) trace_4(LOG_TYPE_SVC|LOG_ENTER, TFN_TRCV_DTQ, dtqid, p_data, tmout)
#define LOG_TRCV_DTQ_LEAVE(ercd, data) trace_3(LOG_TYPE_SVC|LOG_LEAVE, TFN_TRCV_DTQ, ercd, data)

#define LOG_INI_DTQ_ENTER(dtqid) trace_2(LOG_TYPE_SVC|LOG_ENTER, TFN_INI_DTQ, dtqid)
#define LOG_INI_DTQ_LEAVE(ercd) trace_2(LOG_TYPE_SVC|LOG_LEAVE, TFN_INI_DTQ, ercd)

#define LOG_REF_DTQ_ENTER(dtqid, pk_rdtq) trace_3(LOG_TYPE_SVC|LOG_ENTER, TFN_REF_DTQ, dtqid, pk_rdtq)
#define LOG_REF_DTQ_LEAVE(ercd, pk_rdtq) trace_3(LOG_TYPE_SVC|LOG_LEAVE, TFN_REF_DTQ, ercd, pk_rdtq)


/*
 *  Õ•¿Ë≈Ÿ•«°º•ø•≠•Â°ºµ°«Ω
 */
#define LOG_SND_PDQ_ENTER(pdqid, data, datapri) trace_4(LOG_TYPE_SVC|LOG_ENTER, TFN_SND_PDQ, pdqid, data, datapri)
#define LOG_SND_PDQ_LEAVE(ercd) trace_2(LOG_TYPE_SVC|LOG_LEAVE, TFN_SND_PDQ, ercd)

#define LOG_PSND_PDQ_ENTER(pdqid, data, datapri) trace_4(LOG_TYPE_SVC|LOG_ENTER, TFN_PSND_PDQ, pdqid, data, datapri)
#define LOG_PSND_PDQ_LEAVE(ercd) trace_2(LOG_TYPE_SVC|LOG_LEAVE, TFN_PSND_PDQ, ercd)

#define LOG_IPSND_PDQ_ENTER(pdqid, data, datapri) trace_4(LOG_TYPE_SVC|LOG_ENTER, TFN_IPSND_PDQ, pdqid, data, datapri)
#define LOG_IPSND_PDQ_LEAVE(ercd) trace_2(LOG_TYPE_SVC|LOG_LEAVE, TFN_IPSND_PDQ, ercd)

#define LOG_TSND_PDQ_ENTER(pdqid, data, datapri, tmout) trace_5(LOG_TYPE_SVC|LOG_ENTER, TFN_TSND_PDQ, pdqid, data, datapri, tmout)
#define LOG_TSND_PDQ_LEAVE(ercd) trace_2(LOG_TYPE_SVC|LOG_LEAVE, TFN_TSND_PDQ, ercd)

#define LOG_RCV_PDQ_ENTER(pdqid, p_data, p_datapri) trace_4(LOG_TYPE_SVC|LOG_ENTER, TFN_RCV_PDQ, pdqid, p_data, p_datapri)
#define LOG_RCV_PDQ_LEAVE(ercd, data, datapri) trace_4(LOG_TYPE_SVC|LOG_LEAVE, TFN_RCV_PDQ, ercd, data, datapri)

#define LOG_PRCV_PDQ_ENTER(pdqid, p_data, p_datapri) trace_4(LOG_TYPE_SVC|LOG_ENTER, TFN_PRCV_PDQ, pdqid, p_data, p_datapri)
#define LOG_PRCV_PDQ_LEAVE(ercd, data, datapri) trace_4(LOG_TYPE_SVC|LOG_LEAVE, TFN_PRCV_PDQ, ercd, data, datapri)

#define LOG_TRCV_PDQ_ENTER(pdqid, p_data, p_datapri, tmout) trace_5(LOG_TYPE_SVC|LOG_ENTER, TFN_TRCV_PDQ, pdqid, p_data, p_datapri, tmout)
#define LOG_TRCV_PDQ_LEAVE(ercd, data, datapri) trace_4(LOG_TYPE_SVC|LOG_LEAVE, TFN_TRCV_PDQ, ercd, data, datapri)

#define LOG_INI_PDQ_ENTER(pdqid) trace_2(LOG_TYPE_SVC|LOG_ENTER, TFN_INI_PDQ, pdqid)
#define LOG_INI_PDQ_LEAVE(ercd) trace_2(LOG_TYPE_SVC|LOG_LEAVE, TFN_INI_PDQ, ercd)

#define LOG_REF_PDQ_ENTER(pdqid, pk_rpdq) trace_3(LOG_TYPE_SVC|LOG_ENTER, TFN_REF_PDQ, pdqid, pk_rpdq)
#define LOG_REF_PDQ_LEAVE(ercd, pk_rpdq) trace_3(LOG_TYPE_SVC|LOG_LEAVE, TFN_REF_PDQ, ercd, pk_rpdq)


/*
 *  •·°º•ÅE‹•√•Ø•πµ°«Ω
 */
#define LOG_SND_MBX_ENTER(mbxid, pk_msg) trace_3(LOG_TYPE_SVC|LOG_ENTER, TFN_SND_MBX, mbxid, pk_msg)
#define LOG_SND_MBX_LEAVE(ercd) trace_2(LOG_TYPE_SVC|LOG_LEAVE, TFN_SND_MBX, ercd)

#define LOG_RCV_MBX_ENTER(mbxid, ppk_msg) trace_3(LOG_TYPE_SVC|LOG_ENTER, TFN_RCV_MBX, mbxid, ppk_msg)
#define LOG_RCV_MBX_LEAVE(ercd, pk_msg) trace_3(LOG_TYPE_SVC|LOG_LEAVE, TFN_RCV_MBX, ercd, pk_msg)

#define LOG_PRCV_MBX_ENTER(mbxid, ppk_msg) trace_3(LOG_TYPE_SVC|LOG_ENTER, TFN_PRCV_MBX, mbxid, ppk_msg)
#define LOG_PRCV_MBX_LEAVE(ercd, pk_msg) trace_3(LOG_TYPE_SVC|LOG_LEAVE, TFN_PRCV_MBX, ercd, pk_msg)

#define LOG_TRCV_MBX_ENTER(mbxid, ppk_msg, tmout) trace_4(LOG_TYPE_SVC|LOG_ENTER, TFN_TRCV_MBX, mbxid, ppk_msg, tmout)
#define LOG_TRCV_MBX_LEAVE(ercd, pk_msg) trace_3(LOG_TYPE_SVC|LOG_LEAVE, TFN_TRCV_MBX, ercd, pk_msg)

#define LOG_INI_MBX_ENTER(mbxid) trace_2(LOG_TYPE_SVC|LOG_ENTER, TFN_INI_MBX, mbxid)
#define LOG_INI_MBX_LEAVE(ercd) trace_2(LOG_TYPE_SVC|LOG_LEAVE, TFN_INI_MBX, ercd)

#define LOG_REF_MBX_ENTER(mbxid, pk_rmbx) trace_3(LOG_TYPE_SVC|LOG_ENTER, TFN_REF_MBX, mbxid, pk_rmbx)
#define LOG_REF_MBX_LEAVE(ercd, pk_rmbx) trace_3(LOG_TYPE_SVC|LOG_LEAVE, TFN_REF_MBX, ercd, pk_rmbx)


/*
 *  ∏«ƒÅEπ•·•‚•ÅE◊°º•ÅE°«Ω
 */
#define LOG_GET_MPF_ENTER(mpfid, p_blk) trace_3(LOG_TYPE_SVC|LOG_ENTER, TFN_GET_MPF, mpfid, p_blk)
#define LOG_GET_MPF_LEAVE(ercd, blk) trace_3(LOG_TYPE_SVC|LOG_LEAVE, TFN_GET_MPF, ercd, blk)

#define LOG_PGET_MPF_ENTER(mpfid, p_blk) trace_3(LOG_TYPE_SVC|LOG_ENTER, TFN_PGET_MPF, mpfid, p_blk)
#define LOG_PGET_MPF_LEAVE(ercd, blk) trace_3(LOG_TYPE_SVC|LOG_LEAVE, TFN_PGET_MPF, ercd, blk)

#define LOG_TGET_MPF_ENTER(mpfid, p_blk, tmout) trace_4(LOG_TYPE_SVC|LOG_ENTER, TFN_TGET_MPF, mpfid, p_blk, tmout)
#define LOG_TGET_MPF_LEAVE(ercd, blk) trace_3(LOG_TYPE_SVC|LOG_LEAVE, TFN_TGET_MPF, ercd, blk)

#define LOG_REL_MPF_ENTER(mpfid, blk) trace_3(LOG_TYPE_SVC|LOG_ENTER, TFN_REL_MPF, mpfid, blk)
#define LOG_REL_MPF_LEAVE(ercd) trace_2(LOG_TYPE_SVC|LOG_LEAVE, TFN_REL_MPF, ercd)

#define LOG_INI_MPF_ENTER(mpfid) trace_2(LOG_TYPE_SVC|LOG_ENTER, TFN_INI_MPF, mpfid)
#define LOG_INI_MPF_LEAVE(ercd) trace_2(LOG_TYPE_SVC|LOG_LEAVE, TFN_INI_MPF, ercd)

#define LOG_REF_MPF_ENTER(mpfid, pk_rmpf) trace_3(LOG_TYPE_SVC|LOG_ENTER, TFN_REF_MPF, mpfid, pk_rmpf)
#define LOG_REF_MPF_LEAVE(ercd, pk_rmpf) trace_3(LOG_TYPE_SVC|LOG_LEAVE, TFN_REF_MPF, ercd, pk_rmpf)


/*
 *  •∑•π•∆•‡ª˛πÅE…Õ˝µ°«Ω
 */
#define LOG_GET_TIM_ENTER(p_systim) trace_2(LOG_TYPE_SVC|LOG_ENTER, TFN_GET_TIM, p_systim)
#define LOG_GET_TIM_LEAVE(ercd, systim) trace_3(LOG_TYPE_SVC|LOG_LEAVE, TFN_GET_TIM, ercd, systim)

//•˙¡∞ ›¬∏ª˛§Àget_utm§Úª»Õ—§π§ÅEø§·°§•˙¡∞§ÚÕ≠∏˙§À§π§ÅE»Ω€¥ƒ§∑§∆§§§∑§ﬁ§¶§ø§·Ãµ∏˙§À
//#define LOG_GET_UTM_ENTER(p_sysutm) trace_2(LOG_TYPE_SVC|LOG_ENTER, TFN_GET_UTM, p_sysutm)
//#define LOG_GET_UTM_LEAVE(ercd, sysutm) trace_3(LOG_TYPE_SVC|LOG_LEAVE, TFN_GET_UTM, ercd, sysutm)


/*
 *  º˛¥ÅEœ•Û•…•Èµ°«Ω
 */
#define LOG_STA_CYC_ENTER(cycid) trace_2(LOG_TYPE_SVC|LOG_ENTER, TFN_STA_CYC, cycid)
#define LOG_STA_CYC_LEAVE(ercd) trace_2(LOG_TYPE_SVC|LOG_LEAVE, TFN_STA_CYC, ercd)

#define LOG_STP_CYC_ENTER(cycid) trace_2(LOG_TYPE_SVC|LOG_ENTER, TFN_STP_CYC, cycid)
#define LOG_STP_CYC_LEAVE(ercd) trace_2(LOG_TYPE_SVC|LOG_LEAVE, TFN_STP_CYC, ercd)

#define LOG_REF_CYC_ENTER(cycid, pk_rcyc) trace_3(LOG_TYPE_SVC|LOG_ENTER, TFN_REF_CYC, cycid, pk_rcyc)
#define LOG_REF_CYC_LEAVE(ercd, pk_rcyc) trace_3(LOG_TYPE_SVC|LOG_LEAVE, TFN_REF_CYC, ercd, pk_rcyc)


/*
 *  •¢•È°º•‡•œ•Û•…•Èµ°«Ω
 */
#define LOG_STA_ALM_ENTER(almid, almtim) trace_3(LOG_TYPE_SVC|LOG_ENTER, TFN_STA_ALM, almid, almtim)
#define LOG_STA_ALM_LEAVE(ercd) trace_2(LOG_TYPE_SVC|LOG_LEAVE, TFN_STA_ALM, ercd)

#define LOG_ISTA_ALM_ENTER(almid, almtim) trace_3(LOG_TYPE_SVC|LOG_ENTER, TFN_ISTA_ALM, almid, almtim)
#define LOG_ISTA_ALM_LEAVE(ercd) trace_2(LOG_TYPE_SVC|LOG_LEAVE, TFN_ISTA_ALM, ercd)

#define LOG_STP_ALM_ENTER(almid) trace_2(LOG_TYPE_SVC|LOG_ENTER, TFN_STP_ALM, almid)
#define LOG_STP_ALM_LEAVE(ercd) trace_2(LOG_TYPE_SVC|LOG_LEAVE, TFN_STP_ALM, ercd)

#define LOG_ISTP_ALM_ENTER(almid) trace_2(LOG_TYPE_SVC|LOG_ENTER, TFN_ISTP_ALM, almid)
#define LOG_ISTP_ALM_LEAVE(ercd) trace_2(LOG_TYPE_SVC|LOG_LEAVE, TFN_ISTP_ALM, ercd)

#define LOG_REF_ALM_ENTER(almid, pk_ralm) trace_3(LOG_TYPE_SVC|LOG_ENTER, TFN_REF_ALM, almid, pk_ralm)
#define LOG_REF_ALM_LEAVE(ercd, pk_ralm) trace_3(LOG_TYPE_SVC|LOG_LEAVE, TFN_REF_ALM, ercd, pk_ralm)


/*
 *  ≥‰π˛§ﬂ¥…Õ˝µ°«Ω
 */
#define LOG_DIS_INT_ENTER(intno) trace_2(LOG_TYPE_SVC|LOG_ENTER, TFN_DIS_INT, intno)
#define LOG_DIS_INT_LEAVE(ercd) trace_2(LOG_TYPE_SVC|LOG_LEAVE, TFN_DIS_INT, ercd)

#define LOG_ENA_INT_ENTER(intno) trace_2(LOG_TYPE_SVC|LOG_ENTER, TFN_ENA_INT, intno)
#define LOG_ENA_INT_LEAVE(ercd) trace_2(LOG_TYPE_SVC|LOG_LEAVE, TFN_ENA_INT, ercd)

#define LOG_CHG_IPM_ENTER(intpri) trace_2(LOG_TYPE_SVC|LOG_ENTER, TFN_CHG_IPM, intpri)
#define LOG_CHG_IPM_LEAVE(ercd) trace_2(LOG_TYPE_SVC|LOG_LEAVE, TFN_CHG_IPM, ercd)

#define LOG_GET_IPM_ENTER(p_intpri) trace_2(LOG_TYPE_SVC|LOG_ENTER, TFN_GET_IPM, p_intpri)
#define LOG_GET_IPM_LEAVE(ercd, intpri) trace_3(LOG_TYPE_SVC|LOG_LEAVE, TFN_GET_IPM, ercd, intpri)


/*
 *  CPUŒ„≥∞¥…Õ˝µ°«Ω
 */
#define LOG_XSNS_DPN_ENTER(p_excinf) trace_2(LOG_TYPE_SVC|LOG_ENTER, TFN_XSNS_DPN, p_excinf)
#define LOG_XSNS_DPN_LEAVE(state) trace_2(LOG_TYPE_SVC|LOG_LEAVE, TFN_XSNS_DPN, state)

#define LOG_XSNS_XPN_ENTER(p_excinf) trace_2(LOG_TYPE_SVC|LOG_ENTER, TFN_XSNS_XPN, p_excinf)
#define LOG_XSNS_XPN_LEAVE(state) trace_2(LOG_TYPE_SVC|LOG_LEAVE, TFN_XSNS_XPN, state)


/*
 *  •∑•π•∆•‡æı¬÷¥…Õ˝µ°«Ω
 */
#define LOG_ROT_RDQ_ENTER(tskpri) trace_2(LOG_TYPE_SVC|LOG_ENTER, TFN_ROT_RDQ, tskpri)
#define LOG_ROT_RDQ_LEAVE(ercd) trace_2(LOG_TYPE_SVC|LOG_LEAVE, TFN_ROT_RDQ, ercd)

#define LOG_IROT_RDQ_ENTER(tskpri) trace_2(LOG_TYPE_SVC|LOG_ENTER, TFN_IROT_RDQ, tskpri)
#define LOG_IROT_RDQ_LEAVE(ercd) trace_2(LOG_TYPE_SVC|LOG_LEAVE, TFN_IROT_RDQ, ercd)

//#define LOG_GET_TID_ENTER(p_tskid) trace_2(LOG_TYPE_SVC|LOG_ENTER, TFN_GET_TID, p_tskid)
//#define LOG_GET_TID_LEAVE(ercd, tskid) trace_3(LOG_TYPE_SVC|LOG_LEAVE, TFN_GET_TID, ercd, tskid)

//#define LOG_IGET_TID_ENTER(p_tskid) trace_2(LOG_TYPE_SVC|LOG_ENTER, TFN_IGET_TID, p_tskid)
//#define LOG_IGET_TID_LEAVE(ercd, tskid) trace_3(LOG_TYPE_SVC|LOG_LEAVE, TFN_IGET_TID, ercd, tskid)

//#define LOG_LOC_CPU_ENTER() trace_1(LOG_TYPE_SVC|LOG_ENTER, TFN_LOC_CPU)
//#define LOG_LOC_CPU_LEAVE(ercd) trace_2(LOG_TYPE_SVC|LOG_LEAVE, TFN_LOC_CPU, ercd)

//#define LOG_ILOC_CPU_ENTER() trace_1(LOG_TYPE_SVC|LOG_ENTER, TFN_ILOC_CPU)
//#define LOG_ILOC_CPU_LEAVE(ercd) trace_2(LOG_TYPE_SVC|LOG_LEAVE, TFN_ILOC_CPU, ercd)

//#define LOG_UNL_CPU_ENTER() trace_1(LOG_TYPE_SVC|LOG_ENTER, TFN_UNL_CPU)
//#define LOG_UNL_CPU_LEAVE(ercd) trace_2(LOG_TYPE_SVC|LOG_LEAVE, TFN_UNL_CPU, ercd)

//#define LOG_IUNL_CPU_ENTER() trace_1(LOG_TYPE_SVC|LOG_ENTER, TFN_IUNL_CPU)
//#define LOG_IUNL_CPU_LEAVE(ercd) trace_2(LOG_TYPE_SVC|LOG_LEAVE, TFN_IUNL_CPU, ercd)

//#define LOG_DIS_DSP_ENTER() trace_1(LOG_TYPE_SVC|LOG_ENTER, TFN_DIS_DSP)
//#define LOG_DIS_DSP_LEAVE(ercd) trace_2(LOG_TYPE_SVC|LOG_LEAVE, TFN_DIS_DSP, ercd)

//#define LOG_ENA_DSP_ENTER() trace_1(LOG_TYPE_SVC|LOG_ENTER, TFN_ENA_DSP)
//#define LOG_ENA_DSP_LEAVE(ercd) trace_2(LOG_TYPE_SVC|LOG_LEAVE, TFN_ENA_DSP, ercd)

//#define LOG_SNS_CTX_ENTER() trace_1(LOG_TYPE_SVC|LOG_ENTER, TFN_SNS_CTX)
//#define LOG_SNS_CTX_LEAVE(state) trace_2(LOG_TYPE_SVC|LOG_LEAVE, TFN_SNS_CTX, state)

//#define LOG_SNS_LOC_ENTER() trace_1(LOG_TYPE_SVC|LOG_ENTER, TFN_SNS_LOC)
//#define LOG_SNS_LOC_LEAVE(state) trace_2(LOG_TYPE_SVC|LOG_LEAVE, TFN_SNS_LOC, state)

//#define LOG_SNS_DSP_ENTER() trace_1(LOG_TYPE_SVC|LOG_ENTER, TFN_SNS_DSP)
//#define LOG_SNS_DSP_LEAVE(state) trace_2(LOG_TYPE_SVC|LOG_LEAVE, TFN_SNS_DSP, state)

//#define LOG_SNS_DPN_ENTER() trace_1(LOG_TYPE_SVC|LOG_ENTER, TFN_SNS_DPN)
//#define LOG_SNS_DPN_LEAVE(state) trace_2(LOG_TYPE_SVC|LOG_LEAVE, TFN_SNS_DPN, state)

//#define LOG_SNS_KER_ENTER() trace_1(LOG_TYPE_SVC|LOG_ENTER, TFN_SNS_KER)
//#define LOG_SNS_KER_LEAVE(state) trace_2(LOG_TYPE_SVC|LOG_LEAVE, TFN_SNS_KER, state)


/*
 *  •´°º•Õ•ÅEŒΩÈ¥ÅEΩ§»Ω™ŒªΩËÕ˝
 */
#define LOG_KER_ENTER()
#define LOG_KER_LEAVE()
#define LOG_EXT_KER_ENTER() trace_1(LOG_TYPE_SVC|LOG_ENTER, TFN_EXT_KER)
#define LOG_EXT_KER_LEAVE()


/*
 *  •∑•π•∆•‡•˙¡∞µ°«Ω
 */
#define LOG_SYSLOG_WRI_LOG_LEAVE(ercd)
#define LOG_SYSLOG_REA_LOG_ENTER(p_syslog)
#define LOG_SYSLOG_REA_LOG_LEAVE(ercd, p_syslog)
#define LOG_SYSLOG_MSK_LOG_ENTER(logmask, lowmask)
#define LOG_SYSLOG_MSK_LOG_LEAVE(ercd)
#define LOG_SYSLOG_REF_LOG_ENTER(pk_rlog)
#define LOG_SYSLOG_REF_LOG_LEAVE(pk_rlog)

#endif /* TOPPERS_TRACE_CONFIG_H */
