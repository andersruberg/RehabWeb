<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PatientInfo.aspx.cs" Inherits="Secure_PatientInfo" Theme="SkinFile" %>

<%@ Register Assembly="KMobile.Web" Namespace="KMobile.Web.UI.WebControls" TagPrefix="asp" %>

<%@ Register Assembly="MyWebDataGrid" Namespace="MyWebDataGrid" TagPrefix="cc1" %>

<%@ Register Namespace="MyWebControls" TagPrefix="asp" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Patientinformation - Doris Ruberg REHAB AB</title>
    <link href="../StyleSheet.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
    <!----------------- SelectedPatientObjectDataSource ---------------><asp:ObjectDataSource ID="SelectedPatientObjectDataSource" runat="server" DeleteMethod="DeletePatient"
        OldValuesParameterFormatString="{0}" SelectMethod="GetPatientsByID"
        TypeName="PatientsBLL" UpdateMethod="UpdatePatient" onupdating="SelectedPatientObjectDataSource_Updating" OnUpdated="SelectedPatientObjectDataSource_Updated">
        <DeleteParameters>
<asp:Parameter Type="Int32" Name="patientid"></asp:Parameter>
</DeleteParameters>
<UpdateParameters>
<asp:Parameter Type="Int32" Name="patientid"></asp:Parameter>
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
</UpdateParameters>
<SelectParameters>
<asp:QueryStringParameter Name="patientid" QueryStringField="PatientId" Type=int32 DefaultValue="-1" />
</SelectParameters>

</asp:ObjectDataSource> 

<asp:ObjectDataSource ID="FutureBookingsObjectDataSource" runat="server"
        OldValuesParameterFormatString="{0}" SelectMethod="GetFutureBookingsByPatientID"
        TypeName="BookingsBLL">
        <SelectParameters>
            <asp:QueryStringParameter Name="patientid" QueryStringField="PatientId" Type=int32 DefaultValue="-1" />
        </SelectParameters>
    </asp:ObjectDataSource>
    
    <!----------------- SelectedPatientFormView --------------->
        <asp:FormView id="SelectedPatientFormView" runat="server" DataKeyNames="patientid" DataSourceID="SelectedPatientObjectDataSource"
            style="position: relative" DefaultMode=ReadOnly OnItemCommand="SelectedPatientFormView_ItemCommand" onitemcreated="SelectedPatientFormView_ItemCreated" ondatabound="SelectedPatientFormView_DataBound" OnItemUpdated="SelectedPatientFormView_ItemUpdated" onitemdeleted="SelectedPatientFormView_ItemDeleted">
            
            <ItemTemplate>            

<div class=header>
Patientinformation
</div>
<table width="100%">
<tr class=tableHeader>
<td colspan="3">Personuppgifter</td>
</tr>
<tr>
<td class=smallHeader>Efternamn:</td>
<td><%# Eval("surname")%></td>
</tr>
<tr>
<td class=smallHeader>Förnamn:</td>
<td><%# Eval("firstname")%></td>
</tr>
<tr>
<td class=smallHeader>Personnummer</td>
<td><%#Eval("personnumber") %></td>
</tr>       

<tr class=tableHeader>
<td colspan="3">Adress</td>
</tr>
<tr>
<td class=smallHeader>Gata:</td>
<td><asp:NullLabel ID="label6" runat="server" NullText="-" Text='<%# Eval("street")%>'></asp:NullLabel></td>
</tr>
<tr>
<td class=smallHeader>Postnummer:</td>
<td><asp:NullLabel ID="label5" runat="server" NullText="-" Text='<%# Eval("zipcode")%>'></asp:NullLabel></td>
</tr>
<tr>
<td class=smallHeader>Ort:</td>
<td><asp:NullLabel ID="label4" runat="server" NullText="-" Text='<%#Eval("city") %>'></asp:NullLabel></td>
</tr>       

<tr class=tableHeader>
<td colspan="3">Telefon</td>
</tr>

<tr>
<td class=smallHeader>Telefon hem</td>
<td class=smallHeader>Telefon arbete</td>
<td class=smallHeader>Mobiltelefon</td>
</tr>       
<tr>
<td><asp:NullLabel ID="label1" runat="server" NullText="-" Text='<%# Eval("homephone")%>'></asp:NullLabel></td>
<td><asp:NullLabel runat=server id="label2" NullText="-" Text='<%# Eval("workphone")%>'></asp:NullLabel></td>
<td><asp:NullLabel runat=server id="label3" NullText="-" Text='<%# Eval("mobilephone")%>'></asp:NullLabel></td>
</tr>           
<tr class=tableHeader>
<td colspan="3">Övrigt</td>
</tr>

<tr>
<td class=smallHeader>Frikortet går ut</td>
<td class=smallHeader colspan=2>Övrig information</td>
</tr>
<tr>
<td><%#Eval("freecarddate", "{0:d}")%></td>
<td colspan=2><%#Eval("info") %></td>
</tr>
<tr>
<td colspan=3><asp:LinkButton runat="server" Text="Se kommande bokningar för patienten" CommandName="ShowFutureBookings" id="ShowFutureBookingsButton" CausesValidation="False" /> </td>
</tr>
<tr>
<td colspan=3>
<cc1:highlightdatagrid id="FutureBookingsGridView" runat="server" AutoGenerateColumns="False" DataKeyNames="bookingid" DataSourceID="FutureBookingsObjectDataSource" Visible=false>
        <EmptyDataTemplate>
        Inga kommande bokningar hittades
        </EmptyDataTemplate>
        <Columns>
            <asp:TemplateField HeaderText="Datum">
            <ItemTemplate>
            <%#Eval("startdatetime", "{0:d}")%>
            </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Tid">
            <ItemTemplate>
            <%#Eval("startdatetime", "{0:t}")%> - <%#Eval("enddatetime", "{0:t}")%>
            </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Typ">
            <ItemTemplate>
            <%#Eval("bookingtype")%>
            </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Kommentar">
            <ItemTemplate>
            <%#Eval("note")%>
            </ItemTemplate>
            </asp:TemplateField>
            </Columns>
</cc1:highlightdatagrid>
</td>
</tr>




<tr>
<td colspan=3><hr /><br /></td>
</tr>
<td><asp:Button runat="server" Text="Ändra uppgifter" CommandName="Edit" id="EditButton" CausesValidation="True" /> </td>
<td>
<asp:LoginView ID="LoginView1" runat=server>
<RoleGroups>
<asp:RoleGroup Roles="Admin">
<ContentTemplate>
<asp:Button runat="server" Text="Ta bort patienten" CommandName="Delete" id="DeleteButton" CausesValidation="False" /> 
</ContentTemplate>
</asp:RoleGroup>
</RoleGroups>
</asp:LoginView>
</td>
<td><asp:Button runat="server" Text="Stäng" CommandName="Close" id="CloseButton" CausesValidation="False" /> </td>
</table>
</ItemTemplate>
            

<EditItemTemplate>

<div class=header>
Uppdatera patientinformation
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
<asp:RequiredFieldValidator ID="surNameValidator" runat="server" ValidationGroup="editValidationGroup" ControlToValidate="surnameTextBox"
ErrorMessage="Vänligen ange ett efternamn"></asp:RequiredFieldValidator></td>

<td><asp:TextBox ID="firstnameTextBox" Text='<%#Bind("firstname") %>' runat="server" ></asp:TextBox>
<asp:RequiredFieldValidator ID="firstNameValidator" runat="server" ValidationGroup="editValidationGroup" ControlToValidate="firstnameTextBox"
ErrorMessage="Vänligen ange ett förnamn"></asp:RequiredFieldValidator></td>


<td><asp:TextBox runat="server" Text='<%#Bind("personnumber") %>' id="personnumberTextBox"/><br />
<asp:RegularExpressionValidator id="PersonnumberRegularExpressionValidator" ValidationGroup="editValidationGroup" ControlToValidate="personnumberTextBox" ValidationExpression="[1-2][0|9][0-9]{2}[0-1][0-9][0-3][0-9][-][0-9]{4}" ErrorMessage="Ange persnr på formen ÅÅÅÅMMDD-XXXX" EnableClientScript="False" runat="server"/> 
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
<td><asp:TextBox runat="server" id="homephoneTextBox" Text='<%#Bind("homephone") %>'/></td>
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

</table>


<asp:Button runat="server" Text="Uppdatera" CommandName="Update" ValidationGroup="editValidationGroup" id="UpdateButton" CausesValidation="True" /> 
<asp:Button runat="server" Text="Avbryt" CommandName="Cancel" id="CancelButton" CausesValidation="True" /> 
</EditItemTemplate>            
        </asp:FormView>
        <asp:Label ID="UpdatePatientExceptionLabel" runat=server Visible=false></asp:Label>
    
    </div>
    </form>
</body>
</html>
