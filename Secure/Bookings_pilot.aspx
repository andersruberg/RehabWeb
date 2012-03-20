<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Bookings_pilot.aspx.cs" Inherits="Secure_Bookings_pilor" Title="Bokningar dagöversikt - Doris Ruberg REHAB AB" Theme="SkinFile" %>

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
    
    <asp:ObjectDataSource ID="BookingtypeObjectDataSource" runat=server SelectMethod="GetBookingtypesByAccess" TypeName="BookingtypeBLL" OnSelecting="BookingtypeObjectDataSource_Selecting">
    </asp:ObjectDataSource>
    
     <asp:ObjectDataSource ID="DailymessagesObjectDataSource" runat="server"
        SelectMethod="GetAllDailymessages" InsertMethod="AddDailymessage" TypeName="DailymessagesBLL" OnInserting="DailymessagesObjectDataSource_Inserting">
        <InsertParameters>
        <asp:Parameter Name="message" Type=string />
        </InsertParameters>
        </asp:ObjectDataSource>
                    <asp:ObjectDataSource ID="DailyinfoObjectDataSource" runat="server"
        SelectMethod="GetDailyinfoByDate" DeleteMethod="DeleteDailyInfo" InsertMethod="AddDailyInfo" TypeName="DailyinfoBLL" OnInserting="DailyinfoObjectDataSource_Inserting" OnInserted="DailyinfoObjectDataSource_Inserted" OnDeleting="DailyinfoObjectDataSource_Deleting" OnSelecting="DailyinfoObjectDataSource_Selecting">
        <SelectParameters>
            <asp:Parameter Name="date" Type=dateTime />
        </SelectParameters>
        <InsertParameters>
            <asp:Parameter Name="date" Type=datetime />
            <asp:Parameter Name="dailymessageid" Type=int16 />
        </InsertParameters>
        <DeleteParameters>
        <asp:Parameter Name="date" Type=DateTime />
        </DeleteParameters>
        </asp:ObjectDataSource>        
    
   
    <table style="position: relative" width="100%">
        <tr>
            <td align=right>
            <asp:Button ID="showPreviousMonthLinkButton" runat="server" Text="<<< Månad" Width="120px" OnClick="showPreviousMonthLinkButton_Click" />
            </td>
            <td align=left>
            <asp:Button ID="showPreviousWeekLinkButton" runat="server" Text="<< Vecka " Width="100px" OnClick="showPreviousWeekLinkButton_Click" />
            </td>
            <td align=left>
            <asp:Button ID="showPreviousDayLinkButton" runat="server" Text="<" Width="70px" OnClick="showPreviousDayLinkButton_Click" />
            </td>
           
            <td align=center>
                <asp:Button runat=server ID="showTodayLinkButton" Font-Bold="True" Text="Visa idag" OnClick="showTodayLinkButton_Click" />               
                    <asp:SimpleDateTimePicker id="DateTimePicker1" runat="server" AutoFormat="Colorful"></asp:SimpleDateTimePicker></td>
            <td align=right>
             <asp:Button ID="showNextDayLinkButton" runat="server" Text=">" Width="70px" OnClick="showNextDayLinkButton_Click" />
            <td align=right>
            <asp:Button ID="showNextWeekLinkButton" runat="server" Text="Vecka >>" Width="100px"  OnClick="showNextWeekLinkButton_Click" />
            </td>
            <td align=left>
            <asp:Button ID="showNextMonthLinkButton" runat="server" Text="Månad >>>" Width="120px" OnClick="showNextMonthLinkButton_Click"/>
            </td>
        </tr>
        <tr>
        <td colspan="7" align=center><br /><asp:LinkButton Font-Size="10px" Font-Names="Verdana" ID="ShowWeekLinkButton" runat=server Text="Visa veckoöversikt" PostBackUrl="~/Secure/Bookings_week.aspx"></asp:LinkButton></td>       
        </tr>
        </table>
        
        
            
            
            <asp:LoginView ID="DailyinfoLoginView" runat="server">
                <RoleGroups>
                    <asp:RoleGroup Roles="Admin">
                    <ContentTemplate>
       
                <br />
               <asp:CollapsiblePanel ID="CollapsiblePanel1" runat="server" Style="position: relative" ButtonType=Button  ExpandText="Lägg till meddelande" CollapseText="-">      
                  <br />
                  <table>
                  <tr>
                  <td>
                                         <asp:DropDownList id="SelectDailymessageDropDownList" runat="server" datasourceid="DailymessagesObjectDataSource"
            datatextfield="message" datavaluefield="dailymessageid"></asp:DropDownList>
                  </td>
                  <td>
                  <asp:Button runat=server ID="AddDailyinfoButton" Text="Lägg till" OnClick="AddDailyinfoButton_Click" />                    
                  <asp:Button runat=server ID="DeleteDailyinfoButton" Text="Ta bort" OnClick="DeleteDailyinfoButton_Click" />                    
                  </td>
                  </tr>
                  <tr>
                  <td>
                    <asp:TextBox ID="NewDailymessageTextBox" runat=server></asp:TextBox>
                  </td>
                  <td>
                  <asp:Button runat=server ID="SaveDailymessageButton" Text="Spara nytt meddelande" OnClick="SaveDailymessageButton_Click" />
                  </td>
                  </tr>
                  </table>
                  
            
                    
                    
                    <br />
                  
                    
                    </asp:CollapsiblePanel>
                    </ContentTemplate>
                    
                    
                    
                    </asp:RoleGroup>
                </RoleGroups>
                </asp:LoginView>
                <br />                                   
                <asp:FormView id="DailyinfoFormView" runat="server" DataKeyNames="dailyinfoid" DataSourceID="DailyinfoObjectDataSource"
            style="position: relative" DefaultMode=ReadOnly>
            
 <itemtemplate>
 <span class="tableHeader">Meddelande:</span><span style="font-family:Verdana; font-size:14pt; font-weight:bold">&nbsp;&nbsp;<%#Eval("message")%></span>
 </itemtemplate>
 </asp:FormView>
                
              
            <table width="100%">
        
        
        <tr>
        <td colspan=7>
        
      
        </td>
        </tr>
        
        <tr>
            <td colspan="7" rowspan="1">
            
            <DayPilot:DayPilotCalendar ID="DayPilotCalendar1" runat="server" BackColor="#FFFFD5"
        BorderColor="#000000" BusinessBeginsHour="8" BusinessEndsHour="17" DataEndField="enddatetime"
         DataSourceID="BookingObjectDataSource" DataStartField="startdatetime" Days="1"
        DataTextField="title" DataValueField="bookingid" MobilephoneField="mobilephone" BookingtypeField="bookingtype" NoteField="note" PersonnumberField="personnumber" FreecarddateField="freecarddate" DayFontFamily="Tahoma" DayFontSize="12pt"
        EventBackColor="#FFFFFF" EventBorderColor="#000000" EventFontFamily="Tahoma"
        EventFontSize="12pt" EventHoverColor="#DCDCDC" EventLeftBarColor="Blue" HourBorderColor="#EAD098"
        HourFontFamily="Tahoma" HourFontSize="18pt" HourHalfBorderColor="#F3E4B1" HourNameBackColor="#ECE9D8"
        HourNameBorderColor="#ACA899" HoverColor="#FFED95" NonBusinessBackColor="Silver"
        ArrivedField="arrived" NotShownField="notshown"
        StartDate="2007-08-19" Style="position: relative" TimeFormat="Clock24Hours" Width="100%" HeaderDateFormat="dddden \den d MMMM yyyy" HeaderHeight="30" HourHeight="80" JavaScriptFreeAction="newBooking('{0}','{1}');" JavaScriptEventAction="showBookingInfo('{0}');" EventFreecardExpiredBackColor="Red" />
            
            </td>
            
        </tr>
    </table>
    <br />
    <br />
    <asp:Lookup id="LookupPrintBookings" runat="server" ButtonType="button" DialogHeight="600px"
DialogLeft="" DialogNavigateUrl="~/Bookings_print.aspx" DialogTop="" DialogWidth="1000px"
 style="position: relative" Text="Skriv ut dagens bokningar..."></asp:Lookup>
    
 <asp:Button ID="ButtonHideSensitiveData" runat=server Visible=false OnClick="ButtonHideSensitiveData_Click" />   
 <input id="hideSensitive" type=hidden  value="false" runat=server />
</asp:Content>

