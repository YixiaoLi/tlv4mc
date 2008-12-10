<?xml version="1.0"?>
<resources xmlns="http://133.6.51.8/svn/ojl-mprtos/TLV/Resource">
  <resource xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema" name="LOGTASK">
    <attributes>
      <attribute name="type">
        <displayName>タイプ</displayName>
        <value>Task</value>
        <variableType>String</variableType>
        <allocationType>Static</allocationType>
        <grouping>true</grouping>
      </attribute>
      <attribute name="id">
        <displayName>ID</displayName>
        <value>1</value>
        <variableType>Decimal</variableType>
        <allocationType>Static</allocationType>
        <grouping>false</grouping>
      </attribute>
      <attribute name="atr">
        <displayName>属性</displayName>
        <value>TA_ACT</value>
        <variableType>String</variableType>
        <allocationType>Static</allocationType>
        <grouping>false</grouping>
      </attribute>
      <attribute name="pri">
        <displayName>優先度</displayName>
        <value>3</value>
        <variableType>Decimal</variableType>
        <allocationType>Dynamic</allocationType>
        <grouping>false</grouping>
      </attribute>
      <attribute name="exinf">
        <displayName>拡張情報</displayName>
        <value>LOGTASK_PORTID</value>
        <variableType>String</variableType>
        <allocationType>Static</allocationType>
        <grouping>false</grouping>
      </attribute>
      <attribute name="task">
        <displayName>起動関数</displayName>
        <value>logtask_main</value>
        <variableType>String</variableType>
        <allocationType>Static</allocationType>
        <grouping>false</grouping>
      </attribute>
      <attribute name="stksz">
        <displayName>スタックサイズ</displayName>
        <value>1024</value>
        <variableType>Decimal</variableType>
        <allocationType>Static</allocationType>
        <grouping>false</grouping>
      </attribute>
      <attribute name="stk">
        <displayName>スタック領域</displayName>
        <value>NULL</value>
        <variableType>String</variableType>
        <allocationType>Static</allocationType>
        <grouping>false</grouping>
      </attribute>
      <attribute name="state">
        <displayName>状態</displayName>
        <value>RUNNABLE</value>
        <variableType>Enumeration</variableType>
        <allocationType>Dynamic</allocationType>
        <grouping>false</grouping>
      </attribute>
    </attributes>
  </resource>
  <resource xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema" name="TASK1">
    <attributes>
      <attribute name="type">
        <displayName>タイプ</displayName>
        <value>Task</value>
        <variableType>String</variableType>
        <allocationType>Static</allocationType>
        <grouping>true</grouping>
      </attribute>
      <attribute name="id">
        <displayName>ID</displayName>
        <value>2</value>
        <variableType>Decimal</variableType>
        <allocationType>Static</allocationType>
        <grouping>false</grouping>
      </attribute>
      <attribute name="atr">
        <displayName>属性</displayName>
        <value>TA_NULL</value>
        <variableType>String</variableType>
        <allocationType>Static</allocationType>
        <grouping>false</grouping>
      </attribute>
      <attribute name="pri">
        <displayName>優先度</displayName>
        <value>10</value>
        <variableType>Decimal</variableType>
        <allocationType>Dynamic</allocationType>
        <grouping>false</grouping>
      </attribute>
      <attribute name="exinf">
        <displayName>拡張情報</displayName>
        <value>1</value>
        <variableType>String</variableType>
        <allocationType>Static</allocationType>
        <grouping>false</grouping>
      </attribute>
      <attribute name="task">
        <displayName>起動関数</displayName>
        <value>task</value>
        <variableType>String</variableType>
        <allocationType>Static</allocationType>
        <grouping>false</grouping>
      </attribute>
      <attribute name="stksz">
        <displayName>スタックサイズ</displayName>
        <value>4096</value>
        <variableType>Decimal</variableType>
        <allocationType>Static</allocationType>
        <grouping>false</grouping>
      </attribute>
      <attribute name="stk">
        <displayName>スタック領域</displayName>
        <value>NULL</value>
        <variableType>String</variableType>
        <allocationType>Static</allocationType>
        <grouping>false</grouping>
      </attribute>
      <attribute name="state">
        <displayName>状態</displayName>
        <value>DORMANT</value>
        <variableType>Enumeration</variableType>
        <allocationType>Dynamic</allocationType>
        <grouping>false</grouping>
      </attribute>
    </attributes>
  </resource>
  <resource xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema" name="TASK2">
    <attributes>
      <attribute name="type">
        <displayName>タイプ</displayName>
        <value>Task</value>
        <variableType>String</variableType>
        <allocationType>Static</allocationType>
        <grouping>true</grouping>
      </attribute>
      <attribute name="id">
        <displayName>ID</displayName>
        <value>3</value>
        <variableType>Decimal</variableType>
        <allocationType>Static</allocationType>
        <grouping>false</grouping>
      </attribute>
      <attribute name="atr">
        <displayName>属性</displayName>
        <value>TA_NULL</value>
        <variableType>String</variableType>
        <allocationType>Static</allocationType>
        <grouping>false</grouping>
      </attribute>
      <attribute name="pri">
        <displayName>優先度</displayName>
        <value>10</value>
        <variableType>Decimal</variableType>
        <allocationType>Dynamic</allocationType>
        <grouping>false</grouping>
      </attribute>
      <attribute name="exinf">
        <displayName>拡張情報</displayName>
        <value>2</value>
        <variableType>String</variableType>
        <allocationType>Static</allocationType>
        <grouping>false</grouping>
      </attribute>
      <attribute name="task">
        <displayName>起動関数</displayName>
        <value>task</value>
        <variableType>String</variableType>
        <allocationType>Static</allocationType>
        <grouping>false</grouping>
      </attribute>
      <attribute name="stksz">
        <displayName>スタックサイズ</displayName>
        <value>4096</value>
        <variableType>Decimal</variableType>
        <allocationType>Static</allocationType>
        <grouping>false</grouping>
      </attribute>
      <attribute name="stk">
        <displayName>スタック領域</displayName>
        <value>NULL</value>
        <variableType>String</variableType>
        <allocationType>Static</allocationType>
        <grouping>false</grouping>
      </attribute>
      <attribute name="state">
        <displayName>状態</displayName>
        <value>DORMANT</value>
        <variableType>Enumeration</variableType>
        <allocationType>Dynamic</allocationType>
        <grouping>false</grouping>
      </attribute>
    </attributes>
  </resource>
  <resource xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema" name="TASK3">
    <attributes>
      <attribute name="type">
        <displayName>タイプ</displayName>
        <value>Task</value>
        <variableType>String</variableType>
        <allocationType>Static</allocationType>
        <grouping>true</grouping>
      </attribute>
      <attribute name="id">
        <displayName>ID</displayName>
        <value>4</value>
        <variableType>Decimal</variableType>
        <allocationType>Static</allocationType>
        <grouping>false</grouping>
      </attribute>
      <attribute name="atr">
        <displayName>属性</displayName>
        <value>TA_NULL</value>
        <variableType>String</variableType>
        <allocationType>Static</allocationType>
        <grouping>false</grouping>
      </attribute>
      <attribute name="pri">
        <displayName>優先度</displayName>
        <value>10</value>
        <variableType>Decimal</variableType>
        <allocationType>Dynamic</allocationType>
        <grouping>false</grouping>
      </attribute>
      <attribute name="exinf">
        <displayName>拡張情報</displayName>
        <value>3</value>
        <variableType>String</variableType>
        <allocationType>Static</allocationType>
        <grouping>false</grouping>
      </attribute>
      <attribute name="task">
        <displayName>起動関数</displayName>
        <value>task</value>
        <variableType>String</variableType>
        <allocationType>Static</allocationType>
        <grouping>false</grouping>
      </attribute>
      <attribute name="stksz">
        <displayName>スタックサイズ</displayName>
        <value>4096</value>
        <variableType>Decimal</variableType>
        <allocationType>Static</allocationType>
        <grouping>false</grouping>
      </attribute>
      <attribute name="stk">
        <displayName>スタック領域</displayName>
        <value>NULL</value>
        <variableType>String</variableType>
        <allocationType>Static</allocationType>
        <grouping>false</grouping>
      </attribute>
      <attribute name="state">
        <displayName>状態</displayName>
        <value>DORMANT</value>
        <variableType>Enumeration</variableType>
        <allocationType>Dynamic</allocationType>
        <grouping>false</grouping>
      </attribute>
    </attributes>
  </resource>
  <resource xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema" name="MATIN_TASK">
    <attributes>
      <attribute name="type">
        <displayName>タイプ</displayName>
        <value>Task</value>
        <variableType>String</variableType>
        <allocationType>Static</allocationType>
        <grouping>true</grouping>
      </attribute>
      <attribute name="id">
        <displayName>ID</displayName>
        <value>5</value>
        <variableType>Decimal</variableType>
        <allocationType>Static</allocationType>
        <grouping>false</grouping>
      </attribute>
      <attribute name="atr">
        <displayName>属性</displayName>
        <value>TA_ACT</value>
        <variableType>String</variableType>
        <allocationType>Static</allocationType>
        <grouping>false</grouping>
      </attribute>
      <attribute name="pri">
        <displayName>優先度</displayName>
        <value>5</value>
        <variableType>Decimal</variableType>
        <allocationType>Dynamic</allocationType>
        <grouping>false</grouping>
      </attribute>
      <attribute name="exinf">
        <displayName>拡張情報</displayName>
        <value>0</value>
        <variableType>String</variableType>
        <allocationType>Static</allocationType>
        <grouping>false</grouping>
      </attribute>
      <attribute name="task">
        <displayName>起動関数</displayName>
        <value>main_task</value>
        <variableType>String</variableType>
        <allocationType>Static</allocationType>
        <grouping>false</grouping>
      </attribute>
      <attribute name="stksz">
        <displayName>スタックサイズ</displayName>
        <value>4096</value>
        <variableType>Decimal</variableType>
        <allocationType>Static</allocationType>
        <grouping>false</grouping>
      </attribute>
      <attribute name="stk">
        <displayName>スタック領域</displayName>
        <value>NULL</value>
        <variableType>String</variableType>
        <allocationType>Static</allocationType>
        <grouping>false</grouping>
      </attribute>
      <attribute name="state">
        <displayName>状態</displayName>
        <value>RUNNABLE</value>
        <variableType>Enumeration</variableType>
        <allocationType>Dynamic</allocationType>
        <grouping>false</grouping>
      </attribute>
    </attributes>
  </resource>
</resources>