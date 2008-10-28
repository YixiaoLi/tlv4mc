<?xml version="1.0" encoding="utf-8"?>
<itron4:kernelObjects xmlns:itron4="http://133.6.51.8/svn/ojl-mprtos/TLV/Resource/ITRON4">
  <itron4:tasks>
    <itron4:task name="LOGTASK">
      <itron4:id>1</itron4:id>
      <itron4:atr>TA_ACT</itron4:atr>
      <itron4:pri>3</itron4:pri>
      <itron4:exinf>LOGTASK_PORTID</itron4:exinf>
      <itron4:task>logtask_main</itron4:task>
      <itron4:stksz>1024</itron4:stksz>
      <itron4:stk>NULL</itron4:stk>
    </itron4:task>
    <itron4:task name="TASK1">
      <itron4:id>2</itron4:id>
      <itron4:atr>TA_NULL</itron4:atr>
      <itron4:pri>10</itron4:pri>
      <itron4:exinf>1</itron4:exinf>
      <itron4:task>task</itron4:task>
      <itron4:stksz>4096</itron4:stksz>
      <itron4:stk>NULL</itron4:stk>
    </itron4:task>
    <itron4:task name="TASK2">
      <itron4:id>3</itron4:id>
      <itron4:atr>TA_NULL</itron4:atr>
      <itron4:pri>10</itron4:pri>
      <itron4:exinf>2</itron4:exinf>
      <itron4:task>task</itron4:task>
      <itron4:stksz>4096</itron4:stksz>
      <itron4:stk>NULL</itron4:stk>
    </itron4:task>
    <itron4:task name="TASK3">
      <itron4:id>4</itron4:id>
      <itron4:atr>TA_NULL</itron4:atr>
      <itron4:pri>10</itron4:pri>
      <itron4:exinf>3</itron4:exinf>
      <itron4:task>task</itron4:task>
      <itron4:stksz>4096</itron4:stksz>
      <itron4:stk>NULL</itron4:stk>
    </itron4:task>
    <itron4:task name="MATIN_TASK">
      <itron4:id>5</itron4:id>
      <itron4:atr>TA_ACT</itron4:atr>
      <itron4:pri>5</itron4:pri>
      <itron4:exinf>0</itron4:exinf>
      <itron4:task>main_task</itron4:task>
      <itron4:stksz>4096</itron4:stksz>
      <itron4:stk>NULL</itron4:stk>
    </itron4:task>
  </itron4:tasks>
</itron4:kernelObjects>
