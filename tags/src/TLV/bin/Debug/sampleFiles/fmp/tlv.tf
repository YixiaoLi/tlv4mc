$FILE "kernel.res"$
{$NL$
$TAB$"TimeScale" :"us",$NL$
$TAB$"TimeRadix" :10,$NL$
$TAB$"ConvertRules"   :["asp","fmp"],$NL$
$TAB$"VisualizeRules" :["toppers","fmp","fmp_core$+TNUM_PRC$"],$NL$
$TAB$"ResourceHeaders":["fmp"],$NL$
$TAB$"Resources":$NL$
$TAB${$NL$
$JOINEACH tskid TSK.ID_LIST ",\n"$
    $TAB$$TAB$"$tskid$":{$NL$
    $TAB$$TAB$$TAB$"Type":"Task",$NL$
    $TAB$$TAB$$TAB$"Attributes":$NL$
    $TAB$$TAB$$TAB${$NL$
    $TAB$$TAB$$TAB$$TAB$"prcId" :$CLASS_AFFINITY_INI[TSK.CLASS[tskid]]$,$NL$
    $TAB$$TAB$$TAB$$TAB$"id"    :$+tskid$,$NL$
    $TAB$$TAB$$TAB$$TAB$"atr"   :"$TSK.TSKATR[tskid]$",$NL$
    $TAB$$TAB$$TAB$$TAB$"pri"   :$+TSK.ITSKPRI[tskid]$,$NL$
    $TAB$$TAB$$TAB$$TAB$"exinf" :$TSK.EXINF[tskid]$,$NL$
    $TAB$$TAB$$TAB$$TAB$"task"  :"$TSK.TASK[tskid]$",$NL$
    $TAB$$TAB$$TAB$$TAB$"stksz" :$+TSK.STKSZ[tskid]$,$NL$
    $TAB$$TAB$$TAB$$TAB$"stk"   :"NULL",$NL$
    $TAB$$TAB$$TAB$$TAB$"state" :
        $IF TSK.TSKATR[tskid] == TA_ACT$
            "RUNNABLE"$NL$
        $ELSE$
            "DORMANT"$NL$
        $END$
    $TAB$$TAB$$TAB$}$NL$
    $TAB$$TAB$}
$END$
$NL$
$TAB$}$NL$
}$NL$
$FILE "kernel_cfg.c"$
