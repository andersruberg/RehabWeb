<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ChoosePatient.aspx.cs" Inherits="Secure_ChoosePatient" Theme="SkinFile"%>

<%@ Register Assembly="MyWebDataGrid" Namespace="MyWebDataGrid" TagPrefix="cc1" %>
<%@ Register Assembly="KMobile.Web" Namespace="KMobile.Web.UI.WebControls" TagPrefix="asp" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Välj patient - Doris Ruberg REHAB AB</title>
    <link href="../StyleSheet.css" rel="stylesheet" type="text/css" />
</head>


<body>
    <form id="form1" defaultfocus="SearchTxtBox" defaultbutton="SearchBtn" runat="server">
    <div>
        &nbsp;&nbsp;
        <asp:ObjectDataSource ID="ObjectDataSource1" runat="server"
        OldValuesParameterFormatString="{0}" SelectMethod="GetPatientsByName"
        TypeName="PatientsBLL" onselecting="ObjectDataSource1_Selecting">
<SelectParameters>
<asp:Parameter Type="String" Name="surname"></asp:Parameter>
<asp:Parameter Type="String" Name="firstname"></asp:Parameter>
</SelectParameters>
</asp:ObjectDataSource>

Sök Patient <br /><asp:TextBox ID="SearchTxtBox" runat="server"></asp:TextBox> 
<asp:Button ID="SearchBtn" runat="server" OnClick="SearchBtn_Click" Text="Sök" /> 
<asp:Button ID="ClearBtn" runat="server" Text="Rensa" OnClick="ClearBtn_Click" /> 
<br />
<table width="100%">
    <tr>
    <td>
        <asp:Repeater ID="Repeater1" runat="server" OnItemCommand="Repeater1_ItemCommand" OnItemDataBound="Repeater1_ItemDataBound">
        <ItemTemplate>
        <asp:LinkButton ID="lnkLetter" runat=server
            commandname="Filter" 
            commandargument='<%# DataBinder.Eval(Container, "DataItem.Letter")%>'>
      <%# DataBinder.Eval(Container, "DataItem.Letter")%>
        </asp:LinkButton>
        </ItemTemplate>
        </asp:Repeater>
    
    </td>
    </tr>
        <tr>
            <td valign=top>
            <cc1:highlightdatagrid id="HighlightGridView1" runat="server" GridLines=Horizontal
        RowHighlightColor="#284E98" Style="position: relative; top: 4px; left: 0px;" Width="100%" RowClickEventCommandName="Select" OnSelectedIndexChanged="HighlightGridView1_SelectedIndexChanged" CssClass="dataGrid" DataKeyNames="patientid" AutoGenerateColumns="False" RowForeColor="" ShowWhenEmpty="False" DataSourceID="ObjectDataSource1">
        <SelectedRowStyle BackColor="#66CC33" Font-Bold="False" Font-Italic="False" Font-Overline="False"
                Font-Strikeout="False" Font-Underline="False" />    
                <HeaderStyle CssClass="dataGridHeader" HorizontalAlign=Left />
                <EmptyDataTemplate>
                Ingen patient hittades...
                </EmptyDataTemplate>
                <Columns>
                    <asp:BoundField DataField="surname" HeaderText="Efternamn" />
                    <asp:BoundField DataField="firstname" HeaderText="F&#246;rnamn" />
                    <asp:BoundField DataField="personnumber" HeaderText="Personnummer" />
                    <asp:BoundField DataField="freecarddate" HeaderText="Frikort" />
                </Columns>
              </cc1:highlightdatagrid>
            </td>
            <td valign=top>
                &nbsp;</td>
            <td>
            </td>
        </tr>

        </table>
        
        &nbsp;
    
    </div>
    </form>
</body>
</html>
