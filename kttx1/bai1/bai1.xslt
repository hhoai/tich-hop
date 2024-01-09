<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
    xmlns:msxsl="urn:schemas-microsoft-com:xslt" exclude-result-prefixes="msxsl"
>
    <xsl:output method="html" indent="yes"/>

    <xsl:template match="/">
		<html>
			<style>
				h2,th {text-align:center;}
			</style>
			<body>
				<h2 style="" >DANH MUC SACH</h2>
				<xsl:for-each select="TV/NhaXB">
					<b>Nha xuat ban: </b>
					<xsl:value-of select="@TenNXB"/>
					<table cellspacing="0" border="2" width="600px"> 
						<tr>
							<th>STT</th>
							<th>TenSach</th>
							<th>SoTrang</th>
							<th>Gia</th>
						</tr>
						<xsl:for-each select="Sach">
							<tr>
								<td>
									<xsl:value-of select="position()"/>
								</td>
								<td>
									<xsl:value-of select="TenSach"/>
								</td>
								<td>
									<xsl:value-of select="SoTrang"/>
								</td>
								<td>
									<xsl:variable name="sotrang" select="SoTrang"/>
									<xsl:if test="$sotrang &lt;= 100">
										<xsl:value-of select="$sotrang * 4000"/>
									</xsl:if>
									<xsl:if test="$sotrang &lt;= 150 and $sotrang &gt; 100">
										<xsl:value-of select="($sotrang - 100)*3000 + 400000"/>
									</xsl:if>
									<xsl:if test="$sotrang &gt; 150">
										<xsl:value-of select="($sotrang - 150) + 300000 + 4000*50"/>
									</xsl:if>
								</td>
							</tr>
						</xsl:for-each>
					</table>
				</xsl:for-each>
			</body>
		</html>
    </xsl:template>
</xsl:stylesheet>
