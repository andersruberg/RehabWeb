<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Bookings_week.aspx.cs" Inherits="Secure_Bookings_week" Title="Bokningar veckoöversikt - Doris Ruberg REHAB AB" Theme="SkinFile" %>

<%@ Register Assembly="KMobile.Web" Namespace="KMobile.Web.UI.WebControls" TagPrefix="asp" %>

<%@ Register Namespace="DayPilot.Web.Ui" TagPrefix="DayPilot" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:ObjectDataSource ID="BookingObjectDataSource" runat="server"
        SelectMethod="GetBookingsByDates" TypeName="BookingsBLL" OnSelecting="BookingObjectDataSource_Selecting">
        <SelectParameters>
            <asp:ControlParameter Name="startdate" Type=datetime PropertyName="StartDate" ControlID="DayPilotCalendar1" />
            <asp:Parameter Name="enddate" Type=dateTime />
        </SelectParameters>
    </asp:ObjectDataSource>
    
    
  
    <table style="position: relative" width="100%">
        
        <tr>
            <td align=right>
            
            </td>
            <td align=right>
            <asp:Button ID="showPreviousWeekLinkButton" runat="server" Text="<< Vecka " Width="100px" OnClick="showPreviousWeekLinkButton_Click" />
            </td>
            <td align=right>
            
            </td>
           
            <td align=center>
                <asp:Button runat=server ID="showCurrentWeekLinkButton" Text="Nuvarande vecka" Font-Bold="True" OnClick="showCurrentWeekLinkButton_Click" />
                    </td>
            <td align=left></td>
            
            <td align=left>
            <asp:Button ID="showNextWeekLinkButton" runat="server" Text="Vecka >>" Width="100px"  OnClick="showNextWeekLinkButton_Click" />
            </td>
            <td align=left>
            
            </td>
        </tr>
        <tr>
        <td></td>
        <td></td>
        <td></td>
        <td align=center valign=bottom><br /><asp:LinkButton Font-Size="10px" Font-Names="Verdana" ID="ShowWeekLinkButton" runat=server Text="Visa dagsvy" PostBackUrl="~/Secure/Bookings_pilot.aspx"></asp:LinkButton></td>
        <td></td>
        <td></td>
        <td></td>
        </tr>

        
        <tr>
        <td colspan=7>
        
        <br />
        
        
        </td>
        </tr>
        
        <tr>
            <td colspan="7" rowspan="1">
            
            <DayPilot:DayPilotCalendar ID="DayPilotCalendar1" runat="server" BackColor="#FFFFD5"
        BorderColor="#000000" BusinessBeginsHour="8" BusinessEndsHour="17" DataEndField="enddatetime"
         DataSourceID="BookingObjectDataSource" DataStartField="startdatetime" Days="5"
        DataTextField="title" DataValueField="bookingid" MobilephoneField="mobilephone" BookingtypeField="bookingtype" NoteField="note" PersonnumberField="personnumber" FreecarddateField="freecarddate" DayFontFamily="Tahoma" DayFontSize="12pt"
        EventBackColor="#FFFFFF" EventBorderColor="#000000" EventFontFamily="Tahoma"
        EventFontSize="12pt" EventHoverColor="#DCDCDC" EventLeftBarColor="Blue" HourBorderColor="#EAD098"
        HourFontFamily="Tahoma" HourFontSize="18pt" HourHalfBorderColor="#F3E4B1" HourNameBackColor="#ECE9D8"
        HourNameBorderColor="#ACA899" HoverColor="#FFED95" NonBusinessBackColor="#FFF4BC"
        StartDate="2007-08-19" Style="position: relative" TimeFormat="Clock24Hours" Width="100%" HeaderDateFormat="M" HeaderHeight="30" HourHeight="80" JavaScriptFreeAction="newBooking('{0}','{1}');" JavaScriptEventAction="showBookingInfo('{0}');" />
            
            </td>
            
        </tr>
    </table>
    
    
</asp:Content>


