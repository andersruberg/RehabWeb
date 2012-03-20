<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Bookings_print.aspx.cs" Inherits="_Default" Theme="SkinFile"%>

<%@ Register Namespace="MyWebControls" TagPrefix="asp" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>Utskrift av tidbokning - Doris Ruberg REHAB AB</title>
    <link href="../StyleSheet.css" rel="stylesheet" type="text/css" />
</head>
<body onload="javascript:PrintWindow()">
    <form id="form1" runat="server">
    <div>

    <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" SelectMethod="GetBookingsByDate" TypeName="BookingsBLL" OnSelecting="ObjectDataSource1_Selecting"><SelectParameters>
<asp:QueryStringParameter Name="date" QueryStringField="Date"></asp:QueryStringParameter>
</SelectParameters>
</asp:ObjectDataSource>
    
    <div style="font-family:Arial; font-size:16pt">Doris Ruberg REHAB AB</div>
    <br />
<asp:label id="DateLabel" runat="server" style="position: relative" text="Datum:" Font-Names="Arial" Font-Size="14pt"></asp:label>
<hr />

    <table width="100%">
        <tr>
            <td  colspan="2">
            <asp:GridView ID="GridView1" runat="server"
            Width="100%" AutoGenerateColumns="False" DataSourceID="ObjectDataSource1" 
            DataKeyNames="bookingid" EmptyDataText="Ingen tid är inbokad" OnRowCreated="GridView1_RowCreated" ShowFooter="False">
            <Columns>

<asp:TemplateField HeaderText="Tid"><ItemTemplate>
            <%#Eval("startdatetime", "{0:t}")%>
            
</ItemTemplate>
</asp:TemplateField>

<asp:TemplateField HeaderText="Persnr"><ItemTemplate>
                            <asp:Label ID="lblPersonnumber" Runat="Server" Text='<%# Eval("personnumber") %>' />
                    
</ItemTemplate>
</asp:TemplateField>

<asp:TemplateField HeaderText="Namn"><ItemTemplate>
                            <asp:Label ID="Label1" runat="server" Text='<%# Eval("title") %>'></asp:Label>
                        
</ItemTemplate>
</asp:TemplateField>

<asp:TemplateField HeaderText="Telefon"><ItemTemplate>
                            <asp:NullLabel ID="lblPhone" NullText="-" Runat="Server" />
                        
</ItemTemplate>
</asp:TemplateField>
<asp:TemplateField HeaderText="Typ"><ItemTemplate>
                            <asp:Label ID="Label2" runat="server" Text='<%# Eval("bookingtype") %>'></asp:Label>
</ItemTemplate>
</asp:TemplateField>



</Columns>

<RowStyle Wrap="True"></RowStyle>

<HeaderStyle HorizontalAlign="Left"></HeaderStyle>
</asp:GridView>

<tr>
<td colspan="2">
<hr />
<asp:Label ID="NrofBookingsLabel" runat=server></asp:Label>
<br />
</td>
</tr>
<tr>
<td colspan="2">
<br />
<asp:GridView ID="BookingtypesGridView" runat=server AutoGenerateColumns=false CssClass="myDatagrid" >
<Columns>
<asp:TemplateField HeaderText="Bokningstyp">
<ItemTemplate>
<%# Eval("key") %>
</ItemTemplate>
</asp:TemplateField>

<asp:TemplateField HeaderText="Antal">
<ItemTemplate>
<%# Eval("value") %>
</ItemTemplate>
</asp:TemplateField>

</Columns>

</asp:GridView>
</td>
</tr>
         <tr>
            <td colspan="2">
                <asp:button id="PrintButton" Visible=false Enabled=false runat="server" text="Skriv ut..." />
                <asp:button id="CloseButton" Visible=false Enabled=false runat="server"  text="Stäng" style="position: relative; left: 5px; top: 0px;" OnClick="CloseButton_Click" /></td>
            
        </tr>
    </table>
</div>
</form>    
</body>
</html>