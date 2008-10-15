<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
                 xmlns:ko="http://133.6.51.8/svn/ojl-mprtos/TLV/Resource/ITRON4"
                 xmlns:rs="http://133.6.51.8/svn/ojl-mprtos/TLV/Resource">
  <xsl:output method="xml" indent="yes" />
  
  <xsl:template match="ko:kernelObjects">
    <rs:resources>
      <xsl:apply-templates />
    </rs:resources>
  </xsl:template>

  <xsl:template match="ko:tasks">
      <xsl:apply-templates select="ko:task"/>
  </xsl:template>
  
  <xsl:template match="ko:task">
    <rs:resource>
      <rs:name>
        <xsl:value-of select="ko:name"/>
      </rs:name>
      <rs:attributes>
        <rs:attribute>
          <rs:name>type</rs:name>
          <rs:value>Task</rs:value>
          <rs:variableType>String</rs:variableType>
          <rs:allocationType>Static</rs:allocationType>
          <rs:grouping>true</rs:grouping>
        </rs:attribute>
        <rs:attribute>
          <rs:name>id</rs:name>
          <rs:value>
            <xsl:value-of select="ko:id"/>
          </rs:value>
          <rs:variableType>Decimal</rs:variableType>
          <rs:allocationType>Static</rs:allocationType>
        </rs:attribute>
        <rs:attribute>
          <rs:name>atr</rs:name>
          <rs:value>
            <xsl:value-of select="ko:atr"/>
          </rs:value>
          <rs:variableType>String</rs:variableType>
          <rs:allocationType>Static</rs:allocationType>
        </rs:attribute>
        <rs:attribute>
          <rs:name>pri</rs:name>
          <rs:value>
            <xsl:value-of select ="ko:pri"/>
          </rs:value>
          <rs:variableType>Decimal</rs:variableType>
          <rs:allocationType>Dynamic</rs:allocationType>
        </rs:attribute>
        <rs:attribute>
          <rs:name>exinf</rs:name>
          <rs:value>
            <xsl:value-of select ="exinf"/>
          </rs:value>
          <rs:variableType>Decimal</rs:variableType>
          <rs:allocationType>Static</rs:allocationType>
        </rs:attribute>
        <rs:attribute>
          <rs:name>task</rs:name>
          <rs:value>
            <xsl:value-of select ="ko:task"/>
          </rs:value>
          <rs:variableType>String</rs:variableType>
          <rs:allocationType>Static</rs:allocationType>
        </rs:attribute>
        <rs:attribute>
          <rs:name>stksz</rs:name>
          <rs:value>
            <xsl:value-of select ="ko:stksz"/>
          </rs:value>
          <rs:variableType>Decimal</rs:variableType>
          <rs:allocationType>Static</rs:allocationType>
        </rs:attribute>
        <rs:attribute>
          <rs:name>stk</rs:name>
          <rs:value>
            <xsl:value-of select ="stk"/>
          </rs:value>
          <rs:variableType>String</rs:variableType>
          <rs:allocationType>Static</rs:allocationType>
        </rs:attribute>
        <rs:attribute>
          <rs:name>state</rs:name>
          <rs:value>
            <xsl:choose>
              <xsl:when test="ko:atr = TA_ACT">
                RUNNING
              </xsl:when>
              <xsl:when test="ko:atr = TA_NULL">
                DORMANT
              </xsl:when>
            </xsl:choose>
          </rs:value>
          <rs:variableType>Enumeration</rs:variableType>
          <rs:allocationType>Dynamic</rs:allocationType>
        </rs:attribute>
      </rs:attributes>
    </rs:resource>
  </xsl:template>
  
</xsl:stylesheet>
