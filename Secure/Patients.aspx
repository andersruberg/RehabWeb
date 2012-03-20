<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" Title="Patientregister - Doris Ruberg REHAB AB" CodeFile="Patients.aspx.cs" Inherits="_Default" Theme="SkinFile" %>

<%@ Register Assembly="KMobile.Web" Namespace="KMobile.Web.UI.WebControls" TagPrefix="asp" %>

<%@ Register Assembly="MyWebDataGrid" Namespace="MyWebDataGrid" TagPrefix="cc1" %>

<%@ Register Namespace="MyWebControls" TagPrefix="asp" %>




<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

&nbsp; <!----------------- ObjectDataSource1 ---------------><asp:ObjectDataSource ID="ObjectDataSource1" runat="server"
        OldValuesParameterFormatString="{0}" SelectMethod="GetPatientsByName"
        TypeName="PatientsBLL" onselecting="ObjectDataSource1_Selecting">
<SelectParameters>
<asp:Parameter Type="String" Name="surname"></asp:Parameter>
<asp:Parameter Type="String" Name="firstname"></asp:Parameter>
</SelectParameters>
</asp:ObjectDataSource> 

<asp:Button ID="NewPatientButton" runat="server" Style="position: relative" Text="Ny patient..." /> <br /><br />
<!----------------- SearchPatient --------------->
<asp:Panel id="SearchPanel" runat=server DefaultButton=SearchBtn>
Sök Patient <br /><asp:TextBox ID="SearchTextBox" runat="server"></asp:TextBox> 
<asp:Button ID="SearchBtn" runat="server" OnClick="SearchBtn_Click" Text="Sök" /> 
<asp:Button ID="ClearBtn" runat="server" Text="Rensa" OnClick="ClearBtn_Click" /> 
</asp:Panel>
<br />
<!----------------- Alphapaging --------------->
<table style="width: auto; position: relative; height: 173px"><tr><td>
<asp:Repeater ID="Repeater1" runat="server" OnItemCommand="Repeater1_ItemCommand" OnItemDataBound="Repeater1_ItemDataBound">
        <ItemTemplate>
        <asp:LinkButton ID="lnkLetter" runat=server
            commandname="Filter" 
            commandargument='<%# DataBinder.Eval(Container, "DataItem.Letter")%>'>
      <%# DataBinder.Eval(Container, "DataItem.Letter")%>
        </asp:LinkButton>
        </ItemTemplate>
        </asp:Repeater> </td></tr><tr><td style="width: auto" valign=top><!----------------- PatientsGridView ---------------><cc1:highlightdatagrid id="PatientsGridView" runat="server"
        Height="1px" Width="100%" Style="position: relative; top: 0px; left: 0px;" AutoGenerateColumns="False" CssClass="myDatagrid" DataKeyNames="patientid" DataSourceID="ObjectDataSource1" OnPreRender="PatientsGridView_PreRender"><EmptyDataTemplate>
        Hittade ingen patient...
        
</EmptyDataTemplate>
<Columns>
<asp:TemplateField HeaderText="Namn"><ItemTemplate>
<asp:Label id="lblName" runat="server" Text='<%#Eval("surname") + " " + Eval("firstname") %>' __designer:wfdid="w3"></asp:Label> 
</ItemTemplate>
</asp:TemplateField>
<asp:BoundField DataField="personnumber" HeaderText="Personnr"/>
<asp:TemplateField HeaderText="Telefon"><itemTemplate>
                <table>
                <tr>
                <td style="padding-left:10px;padding-right:10px">
                <bold>Hem</bold>
                </td>
                <td style="padding-left:10px;padding-right:10px">
                <bold>Arbete</bold>
                </td>
                <td style="padding-left:10px;padding-right:10px">
                <bold>Mobil</bold>
                </td>
                </tr>
                <tr>
                <td style="padding-left:10px;padding-right:10px">
                <asp:NullLabel ID="lblHomephone" runat="server" NullText="-" Text=<%#Eval("homephone") %> />
                </td>
                <td style="padding-left:10px;padding-right:10px">
                <asp:NullLabel ID="lblWorkphone" runat="server" NullText="-" Text=<%#Eval("workphone") %> />
                </td>
                <td style="padding-left:10px;padding-right:10px">
                <asp:NullLabel ID="lblMobilephone" runat="server" NullText="-" Text=<%#Eval("mobilephone") %> />
                </td>
                </tr>
                </table>
                
</itemTemplate>
</asp:TemplateField>
<asp:TemplateField HeaderText="Frikort"><ItemTemplate>
                <asp:Label ID="lblFreecardDate" runat="server" Text='<%#Eval("freecarddate")%>' />
                
</ItemTemplate>
</asp:TemplateField>
<asp:TemplateField HeaderText="Adress"><ItemTemplate>
                <asp:Label ID="lblStreet" runat="server" Text=<%#Eval("street") %> />
                <br />
                <asp:Label ID="lblPostaddres" runat="server" Text=<%#Eval("zipcode")  + " " +  Eval("city") %> />
                
</ItemTemplate>
</asp:TemplateField>
</Columns>
            <HeaderStyle HorizontalAlign="Left" />
</cc1:highlightdatagrid> </td></tr></table>
    
</asp:Content>

