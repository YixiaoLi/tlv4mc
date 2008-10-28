<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
                 xmlns:itron4="http://133.6.51.8/svn/ojl-mprtos/TLV/Resource/ITRON4"
                 xmlns:rs="http://133.6.51.8/svn/ojl-mprtos/TLV/Resource">
  <xsl:output method="xml" indent="yes" />

  <xsl:template match="/">
    <xsl:apply-templates />
  </xsl:template>

  <xsl:template match="itron4:kernelObjects">
    <rs:resources>
      <xsl:apply-templates />
    </rs:resources>
  </xsl:template>

  <xsl:template match="itron4:tasks">
      <xsl:apply-templates />
  </xsl:template>
  
  <xsl:template match="itron4:task">
    <rs:resource>
      <xsl:attribute name="name">
        <xsl:value-of select="@name"/>
      </xsl:attribute>
      <rs:attributes>
        <rs:attribute>
          <xsl:attribute name="name">type</xsl:attribute>
          <rs:displayName>タイプ</rs:displayName>
          <rs:value>Task</rs:value>
          <rs:variableType>String</rs:variableType>
          <rs:allocationType>Static</rs:allocationType>
          <rs:grouping>true</rs:grouping>
        </rs:attribute>
        <rs:attribute>
          <xsl:attribute name="name">id</xsl:attribute>
          <rs:displayName>ID</rs:displayName>
          <rs:value>
            <xsl:value-of select="itron4:id"/>
          </rs:value>
          <rs:variableType>Decimal</rs:variableType>
          <rs:allocationType>Static</rs:allocationType>
        </rs:attribute>
        <rs:attribute>
          <xsl:attribute name="name">atr</xsl:attribute>
          <rs:displayName>属性</rs:displayName>
          <rs:value>
            <xsl:value-of select="itron4:atr"/>
          </rs:value>
          <rs:variableType>String</rs:variableType>
          <rs:allocationType>Static</rs:allocationType>
        </rs:attribute>
        <rs:attribute>
          <xsl:attribute name="name">pri</xsl:attribute>
          <rs:displayName>優先度</rs:displayName>
          <rs:value>
            <xsl:value-of select ="itron4:pri"/>
          </rs:value>
          <rs:variableType>Decimal</rs:variableType>
          <rs:allocationType>Dynamic</rs:allocationType>
        </rs:attribute>
        <rs:attribute>
          <xsl:attribute name="name">exinf</xsl:attribute>
          <rs:displayName>拡張情報</rs:displayName>
          <rs:value>
            <xsl:value-of select ="itron4:exinf"/>
          </rs:value>
          <rs:variableType>String</rs:variableType>
          <rs:allocationType>Static</rs:allocationType>
        </rs:attribute>
        <rs:attribute>
          <xsl:attribute name="name">task</xsl:attribute>
          <rs:displayName>起動関数</rs:displayName>
          <rs:value>
            <xsl:value-of select ="itron4:task"/>
          </rs:value>
          <rs:variableType>String</rs:variableType>
          <rs:allocationType>Static</rs:allocationType>
        </rs:attribute>
        <rs:attribute>
          <xsl:attribute name="name">stksz</xsl:attribute>
          <rs:displayName>スタックサイズ</rs:displayName>
          <rs:value>
            <xsl:value-of select ="itron4:stksz"/>
          </rs:value>
          <rs:variableType>Decimal</rs:variableType>
          <rs:allocationType>Static</rs:allocationType>
        </rs:attribute>
        <rs:attribute>
          <xsl:attribute name="name">stk</xsl:attribute>
          <rs:displayName>スタック領域</rs:displayName>
          <rs:value>
            <xsl:value-of select ="itron4:stk"/>
          </rs:value>
          <rs:variableType>String</rs:variableType>
          <rs:allocationType>Static</rs:allocationType>
        </rs:attribute>
        <rs:attribute>
          <xsl:attribute name="name">state</xsl:attribute>
          <rs:displayName>状態</rs:displayName>
          <rs:value>
            <xsl:choose>
              <xsl:when test="itron4:atr='TA_ACT'">RUNNABLE</xsl:when>
              <xsl:when test="itron4:atr='TA_NULL'">DORMANT</xsl:when>
            </xsl:choose>
          </rs:value>
          <rs:variableType>Enumeration</rs:variableType>
          <rs:allocationType>Dynamic</rs:allocationType>
        </rs:attribute>
      </rs:attributes>
    </rs:resource>
  </xsl:template>
  
</xsl:stylesheet>
