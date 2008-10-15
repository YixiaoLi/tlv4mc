<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
  <xsl:output method="xml" indent="yes"/>
  
  <xsl:template match="/">
    <resources xmlns="http://133.6.51.8/svn/ojl-mprtos/TLV/Resource">
      <xsl:apply-templates />
    </resources>
  </xsl:template>
  
  <xsl:template match="task">
    <resource>
      <name>
        <xsl:value-of select="name"/>
      </name>
      <attributes>
        <attribute>
          <name>type</name>
          <value>Task</value>
          <variableType>String</variableType>
          <allocationType>Static</allocationType>
          <grouping>true</grouping>
        </attribute>
        <attribute>
          <name>id</name>
          <value>
            <xsl:value-of select="id"/>
          </value>
          <variableType>Decimal</variableType>
          <allocationType>Static</allocationType>
        </attribute>
        <attribute>
          <name>atr</name>
          <value>
            <xsl:value-of select="atr"/>
          </value>
          <variableType>String</variableType>
          <allocationType>Static</allocationType>
        </attribute>
        <attribute>
          <name>pri</name>
          <value>
            <xsl:value-of select ="pri"/>
          </value>
          <variableType>Decimal</variableType>
          <allocationType>Dynamic</allocationType>
        </attribute>
        <attribute>
          <name>exinf</name>
          <value>
            <xsl:value-of select ="exinf"/>
          </value>
          <variableType>Decimal</variableType>
          <allocationType>Static</allocationType>
        </attribute>
        <attribute>
          <name>task</name>
          <value>
            <xsl:value-of select ="task"/>
          </value>
          <variableType>String</variableType>
          <allocationType>Static</allocationType>
        </attribute>
        <attribute>
          <name>stksz</name>
          <value>
            <xsl:value-of select ="stksz"/>
          </value>
          <variableType>Decimal</variableType>
          <allocationType>Static</allocationType>
        </attribute>
        <attribute>
          <name>stk</name>
          <value>
            <xsl:value-of select ="stk"/>
          </value>
          <variableType>String</variableType>
          <allocationType>Static</allocationType>
        </attribute>
        <attribute>
          <name>state</name>
          <value>
            <xsl:choose>
              <xsl:when test="atr = TA_ACT">
                RUNNING
              </xsl:when>
              <xsl:when test="atr = TA_NULL">
                DORMANT
              </xsl:when>
            </xsl:choose>
          </value>
          <variableType>Enumeration</variableType>
          <allocationType>Dynamic</allocationType>
        </attribute>
      </attributes>
    </resource>
  </xsl:template>
  
</xsl:stylesheet>
