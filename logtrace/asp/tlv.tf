$FILE "kernel.res"$
$FOREACH tskid TSK.ID_LIST $
TSK, 
    $+tskid$, 
    $tskid$, 
    $TSK.TSKATR[tskid]$, 
    $+TSK.ITSKPRI[tskid]$, 
    $TSK.EXINF[tskid]$, 
    $+TSK.STKSZ[tskid]$, 
    $TSK.TASK[tskid]$ $NL$
$END$
$FILE "kernel_cfg.c"$

