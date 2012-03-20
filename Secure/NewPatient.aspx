<%@ Page Language="C#" AutoEventWireup="true" CodeFile="NewPatient.aspx.cs" Inherits="Secure_NewPatient" Theme="SkinFile" %>

<%@ Register Assembly="KMobile.Web" Namespace="KMobile.Web.UI.WebControls" TagPrefix="asp" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Ny patient - Doris Ruberg REHAB AB</title>
    <link href="../StyleSheet.css" rel="stylesheet" type="text/css" />
</head>

<body onload="body_onload();">
    <form id="form1" runat="server">
    <div>
    
    <asp:ObjectDataSource ID="NewPatientObjectDataSource" runat="server"
        InsertMethod="AddPatient" OldValuesParameterFormatString="{0}"
        TypeName="PatientsBLL" oninserted="NewPatientObjectDataSource_Inserted" oninserting="NewPatientObjectDataSource_Inserting">
<InsertParameters>
<asp:Parameter Type="String" Name="surname"></asp:Parameter>
<asp:Parameter Type="String" Name="firstname"></asp:Parameter>
<asp:Parameter Type="String" Name="personnumber"></asp:Parameter>
<asp:Parameter Type="String" Name="street"></asp:Parameter>
<asp:Parameter Type="String" Name="zipcode"></asp:Parameter>
<asp:Parameter Type="String" Name="city"></asp:Parameter>
<asp:Parameter Type="String" Name="homephone"></asp:Parameter>
<asp:Parameter Type="String" Name="workphone"></asp:Parameter>
<asp:Parameter Type="String" Name="mobilephone"></asp:Parameter>
<asp:Parameter Type="String" Name="info"></asp:Parameter>
<asp:Parameter Type="String" Name="freecarddate"></asp:Parameter>
</InsertParameters>
</asp:ObjectDataSource> 
    
    <!----------------- FormView1 --------------->
        
        <asp:FormView id="NewPatientFormView" runat="server" DataKeyNames="patientid" DataSourceID="NewPatientObjectDataSource"
            style="position: relative" DefaultMode="Insert" OnItemInserted="NewPatientFormView_ItemInserted" OnItemCommand="NewPatientFormView_ItemCommand">
           
            <insertitemtemplate>
                    

<div class=header>
Ny patient
</div>                  
<table>
<tr class=tableHeader>
<td colspan="3">Personuppgifter</td>
</tr>
<tr>
<td>Efternamn</td>
<td>Förnamn</td>
<td>Personnummer</td>
</tr>       
<tr>
<td><asp:TextBox ID="surnameTextBox" Text='<%#Bind("surname") %>' runat="server" ></asp:TextBox>
<asp:RequiredFieldValidator ID="surNameValidator" runat="server" ValidationGroup="insertValidationGroup" ControlToValidate="surnameTextBox"
ErrorMessage="Vänligen ange ett efternamn"></asp:RequiredFieldValidator></td>

<td><asp:TextBox ID="firstnameTextBox" Text='<%#Bind("firstname") %>' runat="server" ></asp:TextBox>
<asp:RequiredFieldValidator ID="firstNameValidator" runat="server" ValidationGroup="insertValidationGroup" ControlToValidate="firstnameTextBox"
ErrorMessage="Vänligen ange ett förnamn"></asp:RequiredFieldValidator></td>


<td><asp:TextBox runat="server" Text='<%#Bind("personnumber") %>' id="personnumberTextBox"/><br />
<asp:RegularExpressionValidator id="PersonnumberRegularExpressionValidator" ValidationGroup="insertValidationGroup" ControlToValidate="personnumberTextBox" ValidationExpression="[1-2][0|9][0-9]{2}[0-1][0-9][0-3][0-9][-][0-9]{4}" ErrorMessage="Ange persnr på formen ÅÅÅÅMMDD-XXXX" EnableClientScript="False" runat="server"/> 
</td>
</tr>
<tr>
<tr class=tableHeader>
<td colspan="3">Kontaktuppgifter</td>
</tr>
<td>Gata</td>
<td>Postnummer</td>
<td>Ort</td>
</tr>       
<tr>
<td><asp:TextBox ID="streetTextBox" runat="server" Text='<%#Bind("street") %>' ></asp:TextBox></td>
<td><asp:TextBox ID="zipcodeTextBox" runat="server" Text='<%#Bind("zipcode") %>'></asp:TextBox></td>
<td><asp:TextBox ID="cityTextBox" runat="server" Text='<%#Bind("city") %>'></asp:TextBox></td>
</tr>           

<tr>
<td>Telefon hem</td>
<td>Telefon arbete</td>
<td>Mobiltelefon</td>
</tr>       
<tr>
<td><asp:TextBox runat="server" id="homephoneTextBox" Text='<%#Bind("homephone") %>'/>
</td>
<td><asp:TextBox runat="server" id="workphoneTextBox" Text='<%#Bind("workphone") %>'/></td>
<td><asp:TextBox runat="server" id="mobilephoneTextBox" Text='<%#Bind("mobilephone") %>'/></td>
</tr>           
<tr class=tableHeader>
<td colspan="3">Övrig information</td>
</tr>

<tr>
<td>Frikortet går ut</td>
<td colspan=2>Övrig information</td>
</tr>
<tr>
<td><asp:DateTimePicker id="DateTimePicker1" runat="server" AutoFormat="Colorful" Height="27px"
                    style="position: relative" Width="183px" Text='<%#Bind("freecarddate") %>'></asp:DateTimePicker></td>
<td colspan=2><asp:TextBox runat="server" id="infoTextBox" Text='<%#Bind("info") %>'/></td>
</tr>


<tr>
<td colspan=4>
<hr />
<br />
</td>
</tr>
<tr>
<td><asp:Button runat="server" Text="Lägg till patient" ValidationGroup="insertValidationGroup" CommandName="Insert" id="InsertButton" CausesValidation="True" /> </td>
<td><asp:Button runat="server" Text="Avbryt" CommandName="Cancel" id="InsertCancelButton" CausesValidation="False" /></td>
</tr>
</table>
</insertitemtemplate>
            
        </asp:FormView>
        
        <asp:Label ID="InsertPatientLabel" runat=server></asp:Label>
    
    </div>
    </form>
</body>
</html>
