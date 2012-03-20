<%@ Page Language="C#" AutoEventWireup="true" CodeFile="BookingInfo.aspx.cs" Inherits="Secure_BookingInfo" Theme="SkinFile" %>

<%@ Register Assembly="KMobile.Web" Namespace="KMobile.Web.UI.WebControls" TagPrefix="asp" %>

<%@ Register Assembly="MyWebDataGrid" Namespace="MyWebDataGrid" TagPrefix="cc1" %>

<%@ Register Namespace="MyWebControls" TagPrefix="asp" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Bokningsinformation - Doris Ruberg REHAB AB</title>
    <link href="../StyleSheet.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
    <asp:ObjectDataSource ID="SelectedBookingObjectDataSource" runat="server" DeleteMethod="DeleteBooking"
        OldValuesParameterFormatString="{0}" SelectMethod="GetBookingsByBookingID"
        TypeName="BookingsBLL" UpdateMethod="UpdateBookingWithRestrictions" OnUpdating="SelectedBookingObjectDataSource_Updating">
        <DeleteParameters>
            <asp:Parameter Name="bookingid" Type="Int32" />
        </DeleteParameters>
        <UpdateParameters>
            <asp:Parameter Name="bookingid" Type="Int32" />
            <asp:Parameter Name="startdatetime" Type="DateTime" />
            <asp:Parameter Name="enddatetime" Type="DateTime" />
            <asp:Parameter Name="note" Type="String" />
            <asp:Parameter Name="bookingtypeid" Type="Int32" />
            <asp:Parameter Name="arrived" Type=Boolean />
            <asp:Parameter Name="notshown" Type=Boolean />
            <asp:Parameter Name="cancelled" Type=Boolean />
            <asp:Parameter Name="cancellednote" Type="String" />
        </UpdateParameters>
        <SelectParameters>
            <asp:QueryStringParameter DefaultValue="-1" Name="bookingid" QueryStringField="BookingId" Type=Int32/>
        </SelectParameters>
    </asp:ObjectDataSource>
    
    <asp:ObjectDataSource ID="FutureBookingsObjectDataSource" runat="server"
        OldValuesParameterFormatString="{0}" SelectMethod="GetFutureBookingsByPatientID"
        TypeName="BookingsBLL" OnSelecting="FutureBookingsObjectDataSource_Selecting">
        <SelectParameters>
            <asp:Parameter Name="patientid" Type=Int32/>
        </SelectParameters>
    </asp:ObjectDataSource>
    
     <asp:ObjectDataSource ID="BookingArrivalObjectDataSource" runat="server"
        OldValuesParameterFormatString="{0}"
        TypeName="BookingsBLL" UpdateMethod="SetBookingArrived" OnUpdating="BookingArrivalObjectDataSource_Updating">
        <UpdateParameters>
            <asp:QueryStringParameter DefaultValue="-1" Name="bookingid" QueryStringField="BookingId" Type=Int32/>
            <asp:Parameter Name="arrived" Type=Boolean />
        </UpdateParameters>
    </asp:ObjectDataSource>
    
     <asp:ObjectDataSource ID="BookingNotShownObjectDataSource" runat="server"
        OldValuesParameterFormatString="{0}"
        TypeName="BookingsBLL" UpdateMethod="SetBookingNotShown" OnUpdating="BookingNotShownObjectDataSource_Updating">
        <UpdateParameters>
            <asp:QueryStringParameter DefaultValue="-1" Name="bookingid" QueryStringField="BookingId" Type=Int32/>
            <asp:Parameter Name="notshown" Type=Boolean />
        </UpdateParameters>
    </asp:ObjectDataSource>
    
    <asp:ObjectDataSource ID="BookingCancelledObjectDataSource" runat="server"
        OldValuesParameterFormatString="{0}"
        TypeName="BookingsBLL" UpdateMethod="SetBookingCancelled" OnUpdating="BookingCancelledObjectDataSource_Updating" onupdated="BookingCancelledObjectDataSource_Updated">
        <UpdateParameters>
            <asp:QueryStringParameter DefaultValue="-1" Name="bookingid" QueryStringField="BookingId" Type=Int32/>
            <asp:Parameter Name="cancelled" Type=Boolean />
            <asp:Parameter Name="cancellednote" Type=string />
        </UpdateParameters>
    </asp:ObjectDataSource>
    
    <asp:ObjectDataSource ID="BookingtypeObjectDataSource" runat=server SelectMethod="GetBookingtypesByAccess" TypeName="BookingtypeBLL" OnSelecting="BookingtypeObjectDataSource_Selecting">
    
    </asp:ObjectDataSource>        
    
                <asp:FormView id="SelectedBookingFormView" runat="server" DataKeyNames="bookingid" DataSourceID="SelectedBookingObjectDataSource"
            style="position: relative" Width="100%" DefaultMode=ReadOnly OnItemCommand="SelectedBookingFormView_ItemCommand" ondatabound="SelectedBookingFormView_DataBound" OnItemUpdated="SelectedBookingFormView_ItemUpdated">
            
 <itemtemplate>
<div class=header>
Bokningsinformation
</div>
<table>
<tr>
<td valign=top>
<table>
<tr class=tableHeader>
<td colspan="3">Bokningsuppgifter</td>
</tr>
<tr>
<td class=smallHeader>Datum:</td>
<td class=smallHeader>Tid</td>
<td></td>
</tr>
<tr>
<td><%#Eval("startdatetime", "{0:d}")%></td>
<td><%#Eval("startdatetime", "{0:t}")%> - <%#Eval("enddatetime", "{0:t}")%></td>                    
<td></td>
</tr>
                    
<tr>
<td class=smallHeader>Typ</td>
<td class=smallHeader>Kommentar</td>
<td>
</td>
</tr>
 
<tr>
<td>
<%#Eval("bookingtype")%></td>
<td><%#Eval("note")%></td>
<td>
</td>

<tr>
<td class=smallHeader>
Återbud
</td>
<td>
<asp:CheckBox runat=server ID="cancelledCheckBox" Checked=<%#Eval("cancelled") %> Enabled=false />
</td>
<td></td>
</tr>

<tr>
<td class=smallHeader>
Orsak till återbud:
</td>
<td>
<asp:NullLabel runat="server" NullText="-" Text='<%#Eval("cancellednote") %>'></asp:NullLabel>
</td>
<td></td>
</tr>



</tr> 
<tr>
<td class=smallHeader>
Ankommen
</td>
<td>
<asp:CheckBox runat=server ID="arrivedCheckBox" Checked=<%#Eval("arrived") %> Enabled=false />
</td>
<td></td>
</tr>

<tr>
<td class=smallHeader>
Utebliven
</td>
<td>
<asp:CheckBox runat=server ID="notShownCheckBox" Checked=<%#Eval("notshown") %> Enabled=false />
</td>
<td></td>
</tr>
<tr>
<td colspan="3">
<br />
<asp:Button runat="server" Text="Ändra datum/tid" CommandName="Edit" id="EditButton" CausesValidation="True" /> 
<asp:Button runat="server" Text="Ta bort bokningen" CommandName="Delete" id="DeleteButton" CausesValidation="False" OnClientClick="return confirm('Är du säker på att du vill ta bort den här bokningen?')"/>
</td>
</tr>



</table>
</td>
<td valign=top>
<table>
<tr class=tableHeader>
<td colspan="3">Patientuppgifter</td>
</tr>
<tr>
<td class=smallHeader>Namn</td>

<td class=smallHeader>Personnummer</td>
<td></td>
</tr>
<tr>
<td><%#Eval("title")%></td>

<td><%#Eval("personnumber")%></td>
<td></td>
</tr>

<tr>
<td colspan=3>
Telefon 
</td>
</tr>
<tr>
<td class=smallHeader>
Hem
</td>
<td class=smallHeader>
Arbete
</td>
<td class=smallHeader>
Mobil
</td>
</tr>
<tr>
<td>
 <%#Eval("homephone")%>
</td>
<td>
 <%#Eval("workphone")%>
</td>
<td>
 <%#Eval("mobilephone")%>
</td>
</tr>
<tr>
<td colspan="3" class=smallHeader>Frikort:</td>
</tr>
<tr>
<td><%#Eval("freecarddate", "{0:d}")%></td>
</tr>
<tr>
<td colspan=3><asp:LinkButton runat="server" Text="Se kommande bokningar för patienten" CommandName="ShowFutureBookings" id="ShowFutureBookingsButton" CausesValidation="False" /> </td>
</tr>
<tr>
<td colspan=3>
<cc1:highlightdatagrid id="FutureBookingsGridView" runat="server" AutoGenerateColumns="False" DataKeyNames="bookingid" DataSourceID="FutureBookingsObjectDataSource" Visible=false RowClickEventCommandName="Select" OnSelectedIndexChanged="FutureBookingsGridView_SelectedIndexChanged">
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
</table>
</td>
</tr>
<tr>
<td colspan="2" style="padding-bottom:10px; padding-top:10px"><hr /></td>
</tr>

<tr>
<td colspan=2 valign=top style="padding-bottom:10px">
<br />
<asp:CollapsiblePanel runat=server ButtonClass=collapsibleButton StayExpended=true ID="CollapsiblePanel1" ButtonType=Button ExpandText="Patienten har lämnat återbud" CollapseText="Patienten har lämnat återbud">
<br />
Fyll i en orsak till att patienten lämnade återbud:
<br />
<asp:TextBox ID="CancellednoteTextBox" runat=server Text='<%# Eval("cancellednote") %>'></asp:TextBox>
<asp:Button runat="server" Text="Fortsätt" CommandName="SetAsCancelled" id="SetAsCancelledButton" ValidationGroup="CancelledRequiredValidator" CausesValidation="True" />
<br />
<asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat=server ControlToValidate="CancellednoteTextBox" ValidationGroup="CancelledRequiredValidator" ErrorMessage="Vänligen ange en orsak till återbudet"></asp:RequiredFieldValidator>
</asp:CollapsiblePanel>
</td>
</tr>


<tr>
<td colspan=2 style="padding-bottom:10px" valign=top><asp:Button Width="305px"  runat="server" Text="Patienten har ankommit" CommandName="SetAsArrived" id="SetAsArrivedButton" CausesValidation="False" /></td>
</tr>

<tr>
<td colspan=2 style="padding-bottom:20px" valign=top><asp:Button Width="305px"  runat="server" Text="Patienten har uteblivit" CommandName="SetAsNotShown" id="SetAsNotShownButton" CausesValidation="False" /></td>

</tr>

<tr>
<td colspan="2"><asp:Button Width="200px" runat="server" Text="Stäng" CommandName="Close" id="CloseButton" CausesValidation="False"/></td>

</tr>


</table>

</itemtemplate>

            
<edititemtemplate>
<table>
<tr class=tableHeader>
<td colspan="3">Ändra bokningsuppgifter</td>
</tr>
<tr>
<td>Datum:</td>
<td>Tid</td>
<td></td>
</tr>
<tr>
<td><asp:DateTimePicker id="DateTimePicker1" runat="server" AutoFormat="Colorful" Height="27px" style="position: relative" Width="183px"></asp:DateTimePicker></td>
<td> <asp:TextBox runat="server" id="starttimeTextBox" Text=<%#Eval("startdatetime", "{0:t}")%> /></td>                    
<td><asp:TextBox runat="server" id="endtimeTextBox" Text=<%#Eval("enddatetime", "{0:t}")%> Enabled=false Visible=false /></td>

</tr>
<tr>
<td></td>
<td><asp:RegularExpressionValidator id="StarttimeRegularExpressionValidator" ValidationGroup="UpdateValidationGroup" ControlToValidate="starttimeTextBox" ValidationExpression="[0-9]{1,2}:[0-9][0-9]" ErrorMessage="Ange starttiden på formen HH:MM" EnableClientScript="True" runat="server"/> </td>
<td><asp:RegularExpressionValidator id="EndtimeRegularExpressionValidator" ValidationGroup="UpdateValidationGroup" ControlToValidate="endtimeTextBox" ValidationExpression="[0-9]{1,2}:[0-9][0-9]" ErrorMessage="Ange sluttiden på formen HH:MM" EnableClientScript="True" runat="server"/> </td>
</tr>
                    
<tr>
<td>Typ</td>
<td>Kommentar</td>
<td></td>
</tr>
 
<tr>
<td>
<asp:DropDownList ID="drpBookingtype" runat=server DataSourceID="BookingtypeObjectDataSource" DataTextField="bookingtype" DataValueField="bookingtypeid" SelectedValue='<%# Bind("bookingtypeid") %>'></asp:DropDownList>   
</td>
<td><asp:TextBox Text='<%# Bind("note") %>' runat="server" id="noteTextBox" /></td>
<td>
</td>
</tr> 

<tr>
<td>
Återbud
</td>
<td>
<asp:CheckBox runat=server ID="cancelledCheckBox" Checked=<%#Bind("cancelled") %>  Enabled=true/>
</td>
<td></td>
</tr>

<tr>
<td>
Orsak till återbud
</td>
<td>
<asp:TextBox runat=server ID="cancellednoteTextBox" Text=<%#Bind("cancellednote") %>/>
</td>
<td></td>
</tr>



<tr>
<td>
Ankommen
</td>
<td>
<asp:CheckBox runat=server ID="arrivedCheckBox" Checked=<%#Bind("arrived") %>  Enabled=true/>
</td>
<td></td>
</tr>

<tr>
<td>
Utebliven
</td>
<td>
<asp:CheckBox runat=server ID="notShownCheckBox" Checked=<%#Bind("notshown") %> Enabled=true />
</td>
<td></td>
</tr>

<tr>
<td><asp:Button runat="server" Text="Uppdatera" CommandName="Update" id="UpdateButton" ValidationGroup="UpdateValidationGroup" CausesValidation="True" /> </td>
<td><asp:Button runat="server" Text="Avbryt" CommandName="Cancel" id="UpdateCancelButton" CausesValidation="False" /></td>
<td></td>
</tr>
</table>



</edititemtemplate>

</asp:FormView>
<asp:Label ID="UpdateBookingExceptionLabel" runat=server Visible=false></asp:Label>

    
    </div>
    </form>
</body>
</html>
