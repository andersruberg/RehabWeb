<%@ Page Language="C#" AutoEventWireup="true" CodeFile="NewBooking.aspx.cs" Inherits="Secure_NewBooking" Theme="SkinFile" %>

<%@ Register Assembly="KMobile.Web" Namespace="KMobile.Web.UI.WebControls" TagPrefix="asp" %>

<%@ Register Namespace="MyWebControls" TagPrefix="asp" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Ny bokning - Doris Ruberg REHAB AB</title>
    <link href="../StyleSheet.css" rel="stylesheet" type="text/css" />
</head>

<body onload="init_shortcuts();">
    <form id="form1" runat="server">
    <div>
    
    <asp:ObjectDataSource ID="NewBookingObjectDataSource" runat="server"
        InsertMethod="AddBookingWithRestrictions" OldValuesParameterFormatString="{0}"
        TypeName="BookingsBLL"  OnInserting="NewBookingObjectDataSource_Inserting" >
        <InsertParameters>
            <asp:Parameter Name="patientid" Type="Int32" />
            <asp:Parameter Name="title" Type="String" />
            <asp:Parameter Name="startdatetime" Type="DateTime" />
            <asp:Parameter Name="enddatetime" Type="DateTime" />
            <asp:Parameter Name="note" Type="String" />
            <asp:Parameter Name="bookingtypeid" Type="Int32" />
        </InsertParameters>
    </asp:ObjectDataSource>
    
    <asp:ObjectDataSource ID="BookingtypeObjectDataSource" runat=server 
    SelectMethod="GetBookingtypesByAccess" TypeName="BookingtypeBLL" 
    OnSelecting="BookingtypeObjectDataSource_Selecting">
    </asp:ObjectDataSource>        
    
    
    <asp:FormViewPersistant Width="100%" id="NewBookingFormView" runat="server" DataKeyNames="bookingid" DataSourceID="NewBookingObjectDataSource"
            DefaultMode="Insert" OnItemInserted="NewBookingFormView_ItemInserted" OnItemCommand="NewBookingFormView_ItemCommand">
            <insertitemtemplate>
<div class=header>
Skapa ny bokning
<br />
</div>
<table>
<tr class=tableHeader>
<td colspan="2">Bokningsuppgifter
<br />
</td>
</tr>
<tr>
<td class=smallHeader>Datum:</td>
<td class=smallHeader>Tid</td>
</tr>
<tr>
<td><asp:DateTimePicker id="DateTimePicker1" runat="server" AutoFormat="Colorful" Height="27px"
                    style="position: relative" Width="183px"></asp:DateTimePicker></td>
<td> <asp:TextBox runat="server" id="starttimeTextBox" />
<asp:TextBox runat="server" id="endtimeTextBox" Enabled=false Visible=false />
</td>
</tr>

<tr>
<td colspan="2">
<asp:RegularExpressionValidator id="StarttimeRegularExpressionValidator" ValidationGroup="InsertValidationGroup" ControlToValidate="starttimeTextBox" ValidationExpression="[0-9]{1,2}:[0-9][0-9]" ErrorMessage="Ange starttiden på formen HH:MM" runat="server"/> 
<asp:RegularExpressionValidator id="EndtimeRegularExpressionValidator" ValidationGroup="InsertValidationGroup" ControlToValidate="endtimeTextBox" ValidationExpression="[0-9]{1,2}:[0-9][0-9]" ErrorMessage="Ange sluttiden på formen HH:MM" runat="server" /> 
</td>
</tr>
                    
<tr>
<td class=smallHeader>Typ</td>
<td class=smallHeader>Kommentar</td>
</tr>
 
<tr>
<td>
<asp:DropDownList ID="drpBookingtype" runat=server DataSourceID="BookingtypeObjectDataSource" DataTextField="bookingtype" DataValueField="bookingtypeid" SelectedValue = '<%# Bind("bookingtypeid")%>'></asp:DropDownList>        
</td>
<td><asp:TextBox Width="300px" Text='<%# Bind("note") %>' runat="server" id="noteTextBox" /></td>

</tr>
<tr>
<td colspan="2"><br /></td>
</tr>
</table>
<table>
<tr class=tableHeader>
<td colspan="3">Patientuppgifter</td>
</tr>
<tr>
<td>
<asp:Lookup id="LookupChoosePatient" runat="server" ButtonType="button" DialogHeight="600px"
DialogLeft="" DialogNavigateUrl="~/ChoosePatient.aspx" DialogTop="" DialogWidth="800px"
OnSelected="LookupChoosePatient_Selected" style="position: relative">
Välj patient</asp:Lookup>

</td>
<td>

<asp:Lookup id="LookupNewPatient" runat="server" ButtonType="button" DialogHeight="600px"
DialogLeft="" DialogNavigateUrl="~/NewPatient.aspx" DialogTop="" DialogWidth="800px"
OnSelected="LookupNewPatient_Selected" style="position: relative">
Ny patient</asp:Lookup>

</td>
<td>
<asp:Lookup id="LookupPatientInfo" runat="server" ButtonType="button" DialogHeight="600px"
DialogLeft="" DialogNavigateUrl="~/PatientInfo.aspx" DialogTop="" DialogWidth="800px"
OnSelected="LookupPatientInfo_Selected" style="position: relative" Enabled=false Visible=false>
Ändra patientuppgifter</asp:Lookup>

</td>
</tr>
<tr>
<td colspan="3">
<hr />
</td>
</tr>
<tr>
<td>Namn</td>
<td>Personnummer</td>
<td></td>
</tr>
<tr>
<td><asp:TextBox ID="titleTextBox" Text='<%# Bind("title") %>' runat="server" ReadOnly=true></asp:TextBox>
</td>
<td><asp:TextBox runat="server" id="personnumberTextBox" ReadOnly=true/></td>
<td>
</td>
</tr>
<tr>
<td colspan="3"><asp:RequiredFieldValidator ID="TitleValidator" runat="server" ValidationGroup="InsertValidationGroup" ControlToValidate="titleTextBox" 
ErrorMessage="Vänligen ange en patient"></asp:RequiredFieldValidator></td>
</tr>
<tr>
<td colspan=3>
Telefon 
</td>
</tr>
<tr>
<td>
Hem
</td>
<td>
Mobil
</td>
<td>
Arbete
</td>
</tr>
<tr>
<td>
 <asp:TextBox runat="server" id="homephoneTextBox" ReadOnly=true/>
</td>
<td>
 <asp:TextBox runat="server" id="mobilephoneTextBox" ReadOnly=true/>
</td>
<td>
 <asp:TextBox runat="server" id="workphoneTextBox" ReadOnly=true/>
</td>
</tr>
<tr>
<td colspan="3">Frikort:</td>
</tr>
<tr>
<td><asp:TextBox runat="server" id="freecarddateTextBox" ReadOnly=true/></td>
</tr>
<tr>
<td colspan="3">
<hr />
<br />
</td>
</tr>
<tr>
<td><asp:Button runat="server" Text="Boka" CommandName="Insert" id="InsertButton" CausesValidation="True" ValidationGroup="InsertValidationGroup" /> </td>
<td><asp:Button runat="server" Text="Avbryt" CommandName="Cancel" id="InsertCancelButton" CausesValidation="False" /></td>
<td></td>
</tr>
</table>


</insertitemtemplate>
            
        </asp:FormViewPersistant>
        
       <asp:Label ID="InsertBookingExceptionLabel" runat="server" EnableViewState=false Visible=false CssClass="warning" Text=""></asp:Label>
    
    </div>
    </form>
</body>
</html>
