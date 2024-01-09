<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
    xmlns:msxsl="urn:schemas-microsoft-com:xslt" exclude-result-prefixes="msxsl"
>
    <xsl:output method="html" indent="yes"/>

    <xsl:template match="/">
		<html>
			<style>
				h2 {
					text-transform: uppercase;
					text-align: center;
				}
				table {
					margin: auto;
					background: aliceblue;
				}
				.so {4
					color:red;
				}
			</style>
			<body>
				<h2>Phieu mua hang</h2>
				<b>Hoa don: </b>
				<xsl:value-of select="HD/@MaHD"/>
				<br/>
				<xsl:for-each select="HD/LoaiHang">
					<b>Loai hang: </b>
					<xsl:value-of select="@TenLoai"/>
					<table border="1px" cellspacing="0" width="600px">
						<tr>
							<th>STT</th>
							<th>Ten hang</th>
							<th>So luong</th>
							<th>Don gia</th>
							<th>Thanh tien</th>
						</tr>
						<!--<xsl:for-each select="Hang">
							<tr>
								<td>
									<xsl:value-of select="position()"/>
								</td>
								<td>
									<xsl:value-of select="tenhang"/>
								</td>
								<td>
									<xsl:value-of select="soluong"/>
								</td>
								<td>
									<xsl:value-of select="dongia"/>
								</td>
								<td>
									<xsl:variable name="sl" select="soluong"/>
									<xsl:variable name="dg" select="dongia"/>
									<xsl:choose>
										<xsl:when test="$sl &gt; 100 and $sl &lt;= 200">
											<xsl:value-of select="$sl * $dg * 0.8"/>
										</xsl:when>
										<xsl:when test="$sl &gt; 200">
											<xsl:value-of select="$sl * $dg * 0.7"/>
										</xsl:when>
										<xsl:otherwise>
											<xsl:value-of select="$sl * $dg"/>
										</xsl:otherwise>
									</xsl:choose>
								</td>
							</tr>
						</xsl:for-each>-->
						<xsl:apply-templates select="Hang"/>
					</table>
					<br/>
				</xsl:for-each>
			</body>
		</html>
    </xsl:template>
	<xsl:template match="Hang">
		<tr>
			<td>
				<xsl:value-of select="position()"/>
			</td>
			<td>
				<xsl:value-of select="TenHang"/>
			</td>
			<td>
				<xsl:value-of select="SoLuong"/>
			</td>
			<td>
				<xsl:value-of select="DonGia"/>
			</td>
			<td class="so"> 
				<xsl:variable name="sl" select="SoLuong"/>
				<xsl:variable name="dg" select="DonGia"/>
				<xsl:choose>
					<xsl:when test="$sl &gt; 100 and $sl &lt;= 200">
						<xsl:value-of select="$sl * $dg * 0.8"/>
					</xsl:when>
					<xsl:when test="$sl &gt; 200">
						<xsl:value-of select="$sl * $dg * 0.7"/>
					</xsl:when>
					<xsl:otherwise>
						<xsl:value-of select="$sl * $dg"/>
					</xsl:otherwise>
				</xsl:choose>
			</td>
		</tr>
	</xsl:template>
</xsl:stylesheet>
