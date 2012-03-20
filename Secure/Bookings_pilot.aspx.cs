using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

public partial class Secure_Bookings_pilor : System.Web.UI.Page
{
    
    private Common.CenteredDateTime currentDate;

    #region Page Load and PreRender
    protected void Page_Load(object sender, EventArgs e)
    {
     
            if (Session["CurrentDate"] == null)
                currentDate = new Common.CenteredDateTime(Session, DateTime.Now);
            else
                currentDate = new Common.CenteredDateTime(Session, (DateTime)Session["CurrentDate"]);

            DayPilotCalendar1.StartDate = currentDate.CurrentDate ;
       

    
        SetupJavaScript();
            
    }

    protected void Page_PreRender(object sender, EventArgs e)
    {
       
        //Here we have the possiblity to check for certain postbacks and change the date to show
        if (this.Context.Request["__EVENTTARGET"] == "BookingInfo")
        {
            if (this.Context.Request["__EVENTARGUMENT"] == "Selected")
            {
                //Now we want to select a certain date to show
                //This is useful when e.g. showing future bookings for a patient.
                string key = "BookingInfo_Result";


                int bookingid = -1;
                try
                {
                    bookingid = (int)Session[key];
                }
                catch (Exception exception)
                {
                    throw new ApplicationException("Kan ej visa den valda bokningen, ej giltigt bokningsid");
                }

                //Lookup the BookingId that was queryed
                ObjectDataSource selectedBookingObjectDataSource = new ObjectDataSource("BookingsBLL", "GetBookingsByBookingID");
                selectedBookingObjectDataSource.SelectParameters.Add("bookingid", bookingid.ToString());
                DataView selectedBookingDataView = (DataView)selectedBookingObjectDataSource.Select();
                Rehab.BookingsRow selectedBookingDataRow = (Rehab.BookingsRow)selectedBookingDataView[0].Row;
                DateTime selectedBookingStartdatetime = selectedBookingDataRow.startdatetime;

                currentDate.CurrentDate = selectedBookingStartdatetime;
                
            }
            if (this.Context.Request["__EVENTARGUMENT"] == "Updated")
            {
                
            }
        }
        if (this.Context.Request["__EVENTTARGET"] == "NewBooking" &&
                this.Context.Request["__EVENTARGUMENT"] == "Selected")
        {
            
        }

        if (this.Context.Request["__EVENTTARGET"] == "DateTimePicker")
        {

            DateTime date = DateTime.Parse(this.Context.Request["__EVENTARGUMENT"]);
            currentDate.CurrentDate = date;
            
        }

        DayPilotCalendar1.StartDate = currentDate.CurrentDate;

        DayPilotCalendar1.DataBind();

        LookupPrintBookings.DialogNavigateUrl = "~/Bookings_print.aspx?Date=" + currentDate.CurrentDate.ToShortDateString();
        
        DailyinfoFormView.DataBind();

        showNextWeekLinkButton.Text = "Vecka " + currentDate.GetNextWeek().ToString() + " >>";
        showPreviousWeekLinkButton.Text = "<< Vecka " + currentDate.GetPreviousWeek().ToString();

        showNextMonthLinkButton.Text = currentDate.GetNextMonth() + " >>>";
        showPreviousMonthLinkButton.Text = "<<< " + currentDate.GetPreviousMonth();



    }
    #endregion

    #region Setup controls

    protected void SetupJavaScript()
    {

        string scriptString = "";

        scriptString += "<script language=JavaScript> ";
        scriptString += " function newBooking(date, time){";
        scriptString += " var a = new Array(2);";
        scriptString += " a = time.split(':');";
        scriptString += " var hour = a[0];";
        scriptString += " var minute = a[1];";
        scriptString += " var url = 'NewBooking.aspx?Parent=NewBooking';";
        scriptString += " var addQuery = '&Date=';";
        scriptString += " addQuery = addQuery.concat(date);";
        scriptString += " url = url.concat(addQuery);";
        scriptString += " addQuery = '&Hour=';";
        scriptString += " addQuery = addQuery.concat(hour);";
        scriptString += " url = url.concat(addQuery);";
        scriptString += " addQuery = '&Minute=';";
        scriptString += " addQuery = addQuery.concat(minute);";
        scriptString += " url = url.concat(addQuery);";
        scriptString += " window.open(url,'NewBooking','height=650,width=650,status=0,toolbar=0,menubar=0,resizable=1,scrollbars=1');";
        scriptString += " }";
        scriptString += " function showBookingInfo(bookingid){";
        scriptString += " var url = 'BookingInfo.aspx?Parent=BookingInfo';";
        scriptString += " var addQuery = '&BookingId=';";
        scriptString += " addQuery = addQuery.concat(bookingid);";
        scriptString += " url = url.concat(addQuery);";
        scriptString += " window.open(url,'BookingInfo','height=600,width=800,status=0,toolbar=0,menubar=0,resizable=1,scrollbars=1');";
        scriptString += " }";
        scriptString += " </script>";
        RegisterStartupScript("BookingScript", scriptString);

        scriptString = "";

        scriptString += "<script language=JavaScript> ";
        scriptString += "function body_onload() {";
        

        scriptString += "shortcut.add('p',function() {	window.location.replace('Patients.aspx');},{'disable_in_input':true});";
        scriptString += "shortcut.add('t',function() {	window.location.replace('Bookings_week.aspx');},{'disable_in_input':true});";
        scriptString += "shortcut.add('d',function() {	document.getElementById('" + DateTimePicker1.ClientID + "').click();},{'disable_in_input':true});";
        scriptString += "shortcut.add('RIGHT',function() {	document.getElementById('" + showNextDayLinkButton.ClientID + "').click();},{'disable_in_input':true});";
        scriptString += "shortcut.add('LEFT',function() {	document.getElementById('" + showPreviousDayLinkButton.ClientID + "').click();},{'disable_in_input':true});";
        //scriptString += "shortcut.add('UP',function() {	document.getElementById('" + showNextWeekLinkButton.ClientID + "').click();},{'disable_in_input':true});";
        //scriptString += "shortcut.add('DOWN',function() {	document.getElementById('" + showPreviousWeekLinkButton.ClientID + "').click();},{'disable_in_input':true});";
        scriptString += "shortcut.add('ESC',function() {	window.location.replace('../Logout.aspx');},{'disable_in_input':false});";
        scriptString += "shortcut.add('g',function() {	if (document.getElementById('" + hideSensitive.ClientID + "').value == 'false') hideSensitiveInfo(); else showSensitiveInfo();},{'disable_in_input':true});";
        scriptString += "if (document.getElementById('" + hideSensitive.ClientID + "').value == 'true') hideSensitiveInfo();";
        scriptString += "doLoad();}";
        scriptString += " </script>";
        RegisterStartupScript("BodyLoadScript", scriptString);
        
        scriptString = "";
        scriptString += "<script language=JavaScript> ";
        scriptString += "var sURL = unescape(window.location.pathname);"; 
        scriptString += "function doLoad() {";
        scriptString += "setTimeout( 'refresh()', 120*1000 ); }";
        scriptString += "function refresh() {";
        scriptString += "window.location.replace( sURL ); }";
        scriptString += " </script>";
        RegisterStartupScript("AutomaticRefreshScript", scriptString);

        scriptString = "";
        scriptString += "<script language=JavaScript> ";
        //scriptString += "var hideSensitive = false;"; 
        scriptString += "function hideSensitiveInfo() {";
        scriptString += "document.getElementById('" + hideSensitive.ClientID + "').value = true;";
        scriptString += "var e = document.getElementsByTagName('span');";
        scriptString += "for(var i=0; i < e.length; i++) { if (!e[i].id.search('sensitive')) {e[i].style.display='none';}}}";
        scriptString += "function showSensitiveInfo() {";
        scriptString += "document.getElementById('" + hideSensitive.ClientID + "').value = false;";
        scriptString += "var e = document.getElementsByTagName('span');";
        scriptString += "for(var i=0; i < e.length; i++) { if (!e[i].id.search('sensitive')) {e[i].style.display='inline';}}}";
        scriptString += " </script>";
        RegisterStartupScript("HideSensitiveInfoScript", scriptString);

        

        Page.RegisterStartupScript("Shortcut",
           "<script language=javascript src='shortcut.js'></script>");
       
    }

    
    
    #endregion


    protected void BookingObjectDataSource_Selecting(object sender, ObjectDataSourceSelectingEventArgs e)
    {
        
        e.InputParameters["enddate"] = DayPilotCalendar1.StartDate.AddDays(DayPilotCalendar1.Days);
        
    }

    #region Day and Week navigation

    protected void btnShowToday_Click(object sender, EventArgs e)
    {
        DayPilotCalendar1.StartDate = currentDate.GetCurrentStartDate();
        DayPilotCalendar1.DataBind();
    }

    protected void btnShowNextWeek_Click(object sender, EventArgs e)
    {
        DayPilotCalendar1.StartDate = currentDate.GetNextWeekStartDate();
        currentDate.SetNextWeek();
    }

    protected void showNextWeekLinkButton_Click(object sender, EventArgs e)
    {
        currentDate.SetNextWeek();
    }
    protected void showTodayLinkButton_Click(object sender, EventArgs e)
    {
        currentDate.CurrentDate = DateTime.Now;
    }
    protected void showPreviousWeekLinkButton_Click(object sender, EventArgs e)
    {
        currentDate.SetPreviousWeek();
    }
    protected void showNextDayLinkButton_Click(object sender, EventArgs e)
    {
        currentDate.SetNextDay();
    }
    protected void showPreviousDayLinkButton_Click(object sender, EventArgs e)
    {
        currentDate.SetPreviousDay();
    }
    protected void DailyinfoObjectDataSource_Selecting(object sender, ObjectDataSourceSelectingEventArgs e)
    {
        e.InputParameters["date"] = currentDate.CurrentDate;
    }

    protected void showPreviousMonthLinkButton_Click(object sender, EventArgs e)
    {
        currentDate.SetPreviousMonth();
    }
    protected void showNextMonthLinkButton_Click(object sender, EventArgs e)
    {
        currentDate.SetNextMonth();
    }

    

#endregion

    protected void AddDailyinfoButton_Click(object sender, EventArgs e)
    {
        DailyinfoObjectDataSource.Insert();
    }

    protected void DeleteDailyinfoButton_Click(object sender, EventArgs e)
    {
        DailyinfoObjectDataSource.Delete();
    }

    protected void SaveDailymessageButton_Click(object sender, EventArgs e)
    {
        DailymessagesObjectDataSource.Insert();
        KMobile.Web.UI.WebControls.CollapsiblePanel collapsiblePanel = (KMobile.Web.UI.WebControls.CollapsiblePanel)DailyinfoLoginView.FindControl("CollapsiblePanel1");
        TextBox newDailymessageTextBox = (TextBox)collapsiblePanel.FindControl("NewDailymessageTextBox");
        newDailymessageTextBox.Text = "";
    }

    protected void DailyinfoObjectDataSource_Inserting(object sender, ObjectDataSourceMethodEventArgs e)
    {
        KMobile.Web.UI.WebControls.CollapsiblePanel collapsiblePanel = (KMobile.Web.UI.WebControls.CollapsiblePanel)DailyinfoLoginView.FindControl("CollapsiblePanel1");
        DropDownList selectDailymessageDropDownList = (DropDownList)collapsiblePanel.FindControl("SelectDailymessageDropDownList");
        e.InputParameters["dailymessageid"] = selectDailymessageDropDownList.SelectedValue;
        e.InputParameters["date"] = currentDate.CurrentDate;
    }

    protected void DailymessagesObjectDataSource_Inserting(object sender, ObjectDataSourceMethodEventArgs e)
    {
        KMobile.Web.UI.WebControls.CollapsiblePanel collapsiblePanel = (KMobile.Web.UI.WebControls.CollapsiblePanel)DailyinfoLoginView.FindControl("CollapsiblePanel1");
        TextBox newDailymessageTextBox = (TextBox)collapsiblePanel.FindControl("NewDailymessageTextBox");
        if (String.IsNullOrEmpty(newDailymessageTextBox.Text))
            e.Cancel = true;
        e.InputParameters["message"] = newDailymessageTextBox.Text;
        collapsiblePanel.State = KMobile.Web.UI.WebControls.PanelState.Expanded;
    }
    protected void DailyinfoObjectDataSource_Inserted(object sender, ObjectDataSourceStatusEventArgs e)
    {

        DailyinfoLoginView.DataBind();
        KMobile.Web.UI.WebControls.CollapsiblePanel collapsiblePanel = (KMobile.Web.UI.WebControls.CollapsiblePanel)DailyinfoLoginView.FindControl("CollapsiblePanel1");
        collapsiblePanel.State = KMobile.Web.UI.WebControls.PanelState.Collapsed;
    }

    protected void BookingtypeObjectDataSource_Selecting(object sender, ObjectDataSourceSelectingEventArgs e)
    {
        if (User.IsInRole("Admin"))
            BookingtypeObjectDataSource.SelectMethod = "GetAllBookingtypes";
    }
    protected void DailyinfoObjectDataSource_Deleting(object sender, ObjectDataSourceMethodEventArgs e)
    {
        e.InputParameters["date"] = currentDate.CurrentDate;
    }

    protected void ButtonHideSensitiveData_Click(object sender, EventArgs e)
    {
        DayPilotCalendar1.HideSensitiveData = true;
    }
}
