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

public partial class Secure_NewBooking : KMobile.Web.UI.LookupPage
{
    #region Page initiation

    protected void Page_Load(object sender, EventArgs e)
    {


        Button insertButton = (Button)NewBookingFormView.FindControl("InsertButton");
        //KMobile.Web.UI.WebControls.Lookup lookup = (KMobile.Web.UI.WebControls.Lookup)NewBookingFormView.FindControl("LookupChoosePatient");
        
        string scriptString = "";

        scriptString += "<script language=JavaScript> ";
        scriptString += "function init_shortcuts() {";
        scriptString += "shortcut.add('b',function() { document.getElementById('NewBookingFormView_InsertButton').click();},{'disable_in_input':true});";
        scriptString += "shortcut.add('v',function() { document.getElementById('NewBookingFormView_LookupChoosePatient').click();},{'disable_in_input':true});";
        scriptString += "shortcut.add('n',function() { document.getElementById('NewBookingFormView_LookupNewPatient').click();},{'disable_in_input':true});";
        scriptString += "shortcut.add('ESC',function() {window.opener='x';window.close();},{'disable_in_input':false});";
        scriptString += "}";
        scriptString += " </script>";
        RegisterStartupScript("BodyLoadScript", scriptString);

        Page.RegisterStartupScript("Shortcut",
           "<script language=javascript src='shortcut.js'></script>");

        RegisterStartupScript("Shortcut",
           "<script language=javascript src='NewBooking.aspx.js'></script>");
        
        if (!Page.IsPostBack)
        {
            KMobile.Web.UI.WebControls.DateTimePicker dateTimePicker = (KMobile.Web.UI.WebControls.DateTimePicker)NewBookingFormView.Row.FindControl("DateTimePicker1");
            if (Page.Request.QueryString["Date"] != null)
                dateTimePicker.Value = DateTime.Parse(Page.Request.QueryString["Date"]);
            else
                dateTimePicker.Value = DateTime.Now.AddDays(1.0);

            TextBox starttimeTextBox = (TextBox)NewBookingFormView.Row.FindControl("starttimeTextBox");

            if ((Page.Request.QueryString["Hour"] != null) && (Page.Request.QueryString["Minute"] != null))
                starttimeTextBox.Text = Page.Request.QueryString["Hour"] + ":" + Page.Request.QueryString["Minute"];
            else
                starttimeTextBox.Text += "8:30";



            if (User.IsInRole("Admin"))
            {
                TextBox endtimeTextBox = (TextBox)NewBookingFormView.Row.FindControl("endtimeTextBox");
                endtimeTextBox.Enabled = true;
                endtimeTextBox.Visible = true;
                try
                {
                    endtimeTextBox.Text = ((DateTime.Parse(starttimeTextBox.Text)).AddMinutes(30.0)).ToShortTimeString();
                }
                catch (Exception exception) { }


            }
            else
            {
                RegularExpressionValidator endtimeRegularExpressionValidator = (RegularExpressionValidator)NewBookingFormView.Row.FindControl("EndtimeRegularExpressionValidator");

                endtimeRegularExpressionValidator.Enabled = false;
            }

            
            //Handle the case when a patient already is selected (query string)
            //also disbale the possible to choose another patient
            if (Page.Request.QueryString["Patientid"] != null)
            {
                int selectedPatientId = Convert.ToInt32(Page.Request.QueryString["Patientid"]);

                //These control are not suppose to be visible if the patient already is given in by the query string
                KMobile.Web.UI.WebControls.Lookup lookupChoosePatient = (KMobile.Web.UI.WebControls.Lookup)NewBookingFormView.Row.FindControl("LookupChoosePatient");
                lookupChoosePatient.Visible = false;
                lookupChoosePatient.Enabled = false;
                KMobile.Web.UI.WebControls.Lookup lookupNewPatient = (KMobile.Web.UI.WebControls.Lookup)NewBookingFormView.Row.FindControl("LookupNewPatient");
                lookupNewPatient.Visible = false;
                lookupNewPatient.Enabled = false;
               
                IsRefreshParentOnCancel = true;

                //Get patient data
                ObjectDataSource selectedPatientObjectDataSource = new ObjectDataSource("PatientsBLL", "GetPatientsByID");
                selectedPatientObjectDataSource.SelectParameters.Add("patientid", selectedPatientId.ToString());
                DataView dv = (DataView)selectedPatientObjectDataSource.Select();
                Rehab.PatientsDataTable patients = (Rehab.PatientsDataTable)dv.Table;
                Rehab.PatientsRow patientsRow = (Rehab.PatientsRow)patients.Rows[0];
                Patient selectedPatient = new Patient(patientsRow);
                
                //Fill textboxes with patient info
                FillPatientData(selectedPatient);
            }
        }

    }
    #endregion

    #region Properties
    /*
     * To be able to keep the selected patient when making a new booking
    */
    private int SelectedPatientId
    {
        get
        {
            int? id = (int?)ViewState["SelectedPatientId"];
            if (id.HasValue)
                return id.Value;
            else
                return -1;
        }
        set
        {
            ViewState["SelectedPatientId"] = value;

        }
    }

    

    private Patient SelectedPatient
    {
        get
        {
             return (Patient)ViewState["SelectedPatient"];
        }
        set
        {
            ViewState["SelectedPatient"] = value;

        }
    }

    private Booking booking;

    private bool IsRefreshParentOnCancel
    {
        get
        {
            object o = ViewState["IsRefreshParentOnCancel"];
            if (o != null)
                return (bool)o;
            else
                return false;
        }
        set
        {
            ViewState["IsRefreshParentOnCancel"] = value;
        }
    }

    

    #endregion

    #region NewBookingFormView events

    protected void NewBookingObjectDataSource_Inserting(object sender, ObjectDataSourceMethodEventArgs e)
    {

        //TODO: Use try and catch over the hole function
        
        if (User.IsInRole("Admin"))
            NewBookingObjectDataSource.InsertMethod = "AddBooking";

        DateTime date = new DateTime((long)0);
        DateTime startTime = new DateTime((long)0);
        DateTime endTime = new DateTime((long)0);
        TextBox starttimeTextBox = new TextBox() ;
        TextBox endtimeTextBox = new TextBox();

        booking = new Booking();
        
        
        KMobile.Web.UI.WebControls.DateTimePicker dateTimePicker = new KMobile.Web.UI.WebControls.DateTimePicker();
        

        try
        {
            dateTimePicker = (KMobile.Web.UI.WebControls.DateTimePicker)NewBookingFormView.Row.FindControl("DateTimePicker1");
            date = DateTime.Parse(dateTimePicker.Text);
        }
        catch (Exception exception)
        {
            InsertBookingExceptionLabel.Visible = true;
            InsertBookingExceptionLabel.Text = "Det uppstod ett problem när bokningen skulle läggas till: \n";
            InsertBookingExceptionLabel.Text += "Datumet som angivits är inte giltigt.";
            e.Cancel = true;
        }

        try
        {
            starttimeTextBox = (TextBox)NewBookingFormView.Row.FindControl("starttimeTextBox");
            startTime = DateTime.Parse(starttimeTextBox.Text);
        }
        catch (Exception exception)
        {
            InsertBookingExceptionLabel.Visible = true;
            InsertBookingExceptionLabel.Text = "Det uppstod ett problem när bokningen skulle läggas till: \n";
            InsertBookingExceptionLabel.Text += String.Format("{0} är inte ett giltigt klockslag", starttimeTextBox.Text);
            e.Cancel = true;
        }

        DateTime startDateTime = new DateTime(date.Year, date.Month, date.Day, startTime.Hour, startTime.Minute, 0);
        DateTime endDateTime = new DateTime((long)0);

        if (User.IsInRole("Admin"))
        {
            try
            {
                endtimeTextBox = (TextBox)NewBookingFormView.Row.FindControl("endtimeTextBox");
                endTime = DateTime.Parse(endtimeTextBox.Text);
                endDateTime = new DateTime(date.Year, date.Month, date.Day, endTime.Hour, endTime.Minute, 0);
            }
            catch (Exception exception)
            {
                InsertBookingExceptionLabel.Visible = true;
                InsertBookingExceptionLabel.Text = "Det uppstod ett problem när bokningen skulle läggas till: \n";
                InsertBookingExceptionLabel.Text += String.Format("{0} är inte ett giltigt klockslag", endtimeTextBox.Text);
                e.Cancel = true;
            }
        }
        else
            endDateTime = startDateTime.AddMinutes(30.0);


        e.InputParameters["startdatetime"] = startDateTime.ToString();
        e.InputParameters["enddatetime"] = endDateTime.ToString();
        booking.Startdatetime = startDateTime;
        booking.Enddatetime = endDateTime;

        try
        {
            e.InputParameters["patientid"] = SelectedPatientId;
            TextBox titleTextBox = (TextBox)NewBookingFormView.Row.FindControl("titleTextBox");
            e.InputParameters["title"] = titleTextBox.Text;
        }
        catch (Exception exception)
        {
            //TODO: Show an error text
            e.Cancel = true;
        }

        /*DropDownList drpBookingtype = (DropDownList)NewBookingFormView.FindControl("drpBookingtype");
        if (drpBookingtype != null)
            e.InputParameters["bookingtypeid"] = drpBookingtype.SelectedValue;*/
      
    }

    protected void NewBookingFormView_ItemInserted(object sender, FormViewInsertedEventArgs e)
    {
        e.KeepInInsertMode = true;
       
        if (e.Exception != null)
        {

            Common.ExceptionHandling(e.Exception, InsertBookingExceptionLabel);
            
            
            //Handle colliding booking exception
            if (e.Exception.InnerException != null)
            {
                if (e.Exception.InnerException is Common.CollidingBookingException)
                {
                    Common.CollidingBookingException collidingBookingException = (Common.CollidingBookingException)e.Exception.InnerException;
                    TextBox starttimeTextBox = (TextBox)NewBookingFormView.Row.FindControl("starttimeTextBox");
                    starttimeTextBox.Text = collidingBookingException.ProposedStartdatetime.ToShortTimeString();

                    if (User.IsInRole("Admin"))
                    {
                        TextBox endtimeTextBox = (TextBox)NewBookingFormView.Row.FindControl("endtimeTextBox");
                        endtimeTextBox.Text = collidingBookingException.ProposedEnddatetime.ToShortTimeString();
                    }

                    InsertBookingExceptionLabel.Text = collidingBookingException.Message + " Nästa lediga tid är kl. " + collidingBookingException.ProposedStartdatetime.ToShortTimeString() + ". Tryck Boka igen om du vill boka den föreslagna tiden, annars skriv en annan tid och tryck Boka.";
                    InsertBookingExceptionLabel.Visible = true;
                }
            }

           

            e.ExceptionHandled = true;
        }
        else
        {
           
            if (e.AffectedRows == -1)
            {
                InsertBookingExceptionLabel.Visible = false;
                Response.Write(Server.HtmlEncode(e.Values["title"].ToString()) + " är nu inbokad mellan " + booking.Startdatetime.ToShortTimeString() + "-" + booking.Enddatetime.ToShortTimeString() + " " + booking.Startdatetime.ToLongDateString());


                //Store the bookingid as the Result so it will be possible to get it from the Parent window (i.e. the Bookings view window)
                /*if ((bool)e.Values["bookingid"])
                {
                    Result = (int)e.Values["bookingid"];
                    SelectedEvent = EventTypes.Selected;
                }*/

                RefreshParentDialogOnConfirmationCancel("Vill du boka ytterliggare en tid för patienten?");
                //Response.Write("<script>if (!confirm('Vill du boka ytterliggare en tid?')){window.close();window.opener.location.href='javascript:void(__doPostBack())';} </script>");
                
                //RefreshParentDialog();
                IsRefreshParentOnCancel = true;
                
            }
            

        }
    }



    protected void NewBookingFormView_ItemCommand(object sender, FormViewCommandEventArgs e)
    {
        if (e.CommandName == "Cancel")
        {
            if (!IsRefreshParentOnCancel)
                Response.Write("<script>window.opener='x';window.close();</script>");
            else
            {
                RefreshParentDialog();
            }
        }
    }
    #endregion

    protected void LookupChoosePatient_Selected(object sender, EventArgs e)
    {
        KMobile.Web.UI.WebControls.Lookup LookupChoosePatient = (KMobile.Web.UI.WebControls.Lookup)NewBookingFormView.Row.FindControl("LookupChoosePatient");
        Patient selectedPatient = (Patient)LookupChoosePatient.Result;
        
        FillPatientData(selectedPatient);
    }

    protected void LookupNewPatient_Selected(object sender, EventArgs e)
    {
        KMobile.Web.UI.WebControls.Lookup LookupNewPatient = (KMobile.Web.UI.WebControls.Lookup)NewBookingFormView.Row.FindControl("LookupNewPatient");
        Patient selectedPatient = (Patient)LookupNewPatient.Result;

        FillPatientData(selectedPatient);
    }

    protected void LookupPatientInfo_Selected(object sender, EventArgs e)
    {
        KMobile.Web.UI.WebControls.Lookup LookupPatientInfo = (KMobile.Web.UI.WebControls.Lookup)NewBookingFormView.Row.FindControl("LookupPatientInfo");
        Patient selectedPatient = (Patient)LookupPatientInfo.Result;

        FillPatientData(selectedPatient);
    }

    private void FillPatientData(Patient selectedPatient)
    {

        
        if (selectedPatient != null)
        {

            TextBox titleTextBox = (TextBox)NewBookingFormView.Row.FindControl("titleTextBox");
            titleTextBox.Text = selectedPatient.Surname + " " + selectedPatient.Firstname;
            TextBox personnumberTextBox = (TextBox)NewBookingFormView.Row.FindControl("personnumberTextBox");
            personnumberTextBox.Text = selectedPatient.Personnumber;
            TextBox freecarddareTextBox = (TextBox)NewBookingFormView.Row.FindControl("freecarddateTextBox");
            freecarddareTextBox.Text = selectedPatient.FreecardDate;
            TextBox homephoneTextBox = (TextBox)NewBookingFormView.Row.FindControl("homephoneTextBox");
            homephoneTextBox.Text = selectedPatient.HomePhone;
            TextBox workphoneTextBox = (TextBox)NewBookingFormView.Row.FindControl("workphoneTextBox");
            workphoneTextBox.Text = selectedPatient.WorkPhone;
            TextBox mobilephoneTextBox = (TextBox)NewBookingFormView.Row.FindControl("mobilephoneTextBox");
            mobilephoneTextBox.Text = selectedPatient.MobilePhone;

            SelectedPatientId = selectedPatient.PatientId;
            

            KMobile.Web.UI.WebControls.Lookup lookupPatientInfo = (KMobile.Web.UI.WebControls.Lookup)NewBookingFormView.Row.FindControl("LookupPatientInfo");

            if (SelectedPatientId > 0)
            {

                lookupPatientInfo.DialogNavigateUrl = "~/PatientInfo.aspx?PatientId=" + selectedPatient.PatientId;
                lookupPatientInfo.Enabled = true;
                lookupPatientInfo.Visible = true;
            }
            else
            {
                lookupPatientInfo.Enabled = false;
                lookupPatientInfo.Visible = false;
            }
        }
        
    }

    
    protected void BookingtypeObjectDataSource_Selecting(object sender, ObjectDataSourceSelectingEventArgs e)
    {
        if (User.IsInRole("Admin"))
            BookingtypeObjectDataSource.SelectMethod = "GetAllBookingtypes";
    }

   



}
