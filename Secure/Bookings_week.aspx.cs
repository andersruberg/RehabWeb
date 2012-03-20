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

public partial class Secure_Bookings_week : System.Web.UI.Page
{
    
    private Common.CenteredDateTime currentDate;

    

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["CurrentDate"] == null)
            currentDate = new Common.CenteredDateTime(Session, DateTime.Now);
        else
            currentDate = new Common.CenteredDateTime(Session, (DateTime)Session["CurrentDate"]);

        DayPilotCalendar1.StartDate = currentDate.GetCurrentStartDate();

        SetupJavaScript();

    }

    protected void Page_PreRender(object sender, EventArgs e)
    {

        showNextWeekLinkButton.Text = "Vecka " + currentDate.GetNextWeek().ToString() + " >>";
        showPreviousWeekLinkButton.Text = "<< Vecka " + currentDate.GetPreviousWeek().ToString();

        DayPilotCalendar1.StartDate = currentDate.GetCurrentStartDate();
        DayPilotCalendar1.DataBind();

    
    }

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
        scriptString += "shortcut.add('t',function() {	window.location.replace('Bookings_pilot.aspx');},{'disable_in_input':true});";
        scriptString += "shortcut.add('ESC',function() {	window.location.replace('../Logout.aspx');},{'disable_in_input':false});";
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



        Page.RegisterStartupScript("Shortcut",
           "<script language=javascript src='shortcut.js'></script>");
    }

    

    #endregion


    protected void btnShowToday_Click(object sender, EventArgs e)
    {
        DayPilotCalendar1.StartDate = currentDate.GetCurrentStartDate();
        DayPilotCalendar1.DataBind();
    }
    protected void BookingObjectDataSource_Selecting(object sender, ObjectDataSourceSelectingEventArgs e)
    {
        e.InputParameters["enddate"] = DayPilotCalendar1.StartDate.AddDays(DayPilotCalendar1.Days);
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
    protected void showCurrentWeekLinkButton_Click(object sender, EventArgs e)
    {
        currentDate.CurrentDate = DateTime.Now;
    }
    protected void showPreviousWeekLinkButton_Click(object sender, EventArgs e)
    {
        currentDate.SetPreviousWeek();
    }
    
    
}

