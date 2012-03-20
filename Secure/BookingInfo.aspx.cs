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

public partial class Secure_BookingInfo : KMobile.Web.UI.LookupPage
{
    #region Properties

    private int PatientId
    {
        get
        {
            object o = ViewState["PatientId"];

            int patientId = -1;

            try
            {
                patientId = Convert.ToInt32(o);
            }
            catch (Exception exception)
            {
             
            }
            return patientId;
        }
        set
        {
            ViewState["PatientId"] = value;
        }
    }

    private bool IsBookingUpdated
    {
        get
        {
            object o = ViewState["IsBookingUpdated"];

            bool updated = false;

            try
            {
                updated = Convert.ToBoolean(o);
            }
            catch (Exception exception)
            {
                
            }
            return updated;
        }
        set
        {
            ViewState["IsBookingUpdated"] = value;
        }
    }
    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        
    }

    protected void Pre_Render(object sender, EventArgs e)
    {
        
    }

    #region SelectedBookingFormView events
    protected void SelectedBookingFormView_ItemCommand(object sender, FormViewCommandEventArgs e)
    {

        if (e.CommandName == "Cancel")
        {
            SelectedBookingFormView.ChangeMode(FormViewMode.ReadOnly);
        }
        if (e.CommandName == "Close")
        {
            //Has anything been updated?
            if (IsBookingUpdated)
            {
                SelectedEvent = EventTypes.Updated;
                RefreshParentDialog();
            }
            else
                Response.Write("<script>window.opener='x';window.close();</script>");
        }
        if (e.CommandName == "Delete")
        {
            SelectedEvent = EventTypes.Updated;
            RefreshParentDialog();
        }
        if (e.CommandName == "SetAsArrived")
        {
            BookingArrivalObjectDataSource.Update();

            SelectedEvent = EventTypes.Updated;
            RefreshParentDialog();
        }
        if (e.CommandName == "SetAsNotShown")
        {
            BookingNotShownObjectDataSource.Update();

            SelectedEvent = EventTypes.Updated;
            RefreshParentDialog();
        }
        if (e.CommandName == "SetAsCancelled")
        {
            BookingCancelledObjectDataSource.Update();

            RefreshParentDialogOnConfirmationCancel("Återbudet är registrerat. Vill du boka en ny tid för patienten?");
            
            string url = "NewBooking.aspx?Parent=NewBooking&Patientid=" + PatientId;

            Response.Redirect(url);
        }
        if (e.CommandName == "ShowFutureBookings")
        {
            GridView futureBookingsGridView = (GridView)SelectedBookingFormView.Row.FindControl("FutureBookingsGridView");
            futureBookingsGridView.DataBind();
            futureBookingsGridView.Visible = true;
            
        }
            
    }

    protected void SelectedBookingFormView_DataBound(object sender, EventArgs e)
    {

        DataRowView dataRowView = (DataRowView)SelectedBookingFormView.DataItem;

        try
        {
            PatientId = (int)dataRowView.Row["patientid"];
        }
        catch (Exception exception)
        {
            new ArgumentException("Internt problem. Det uppstod ett fel vid inläsning av bokningen:\n" + exception.Message);
        }
        
        if (SelectedBookingFormView.CurrentMode == FormViewMode.Edit)
        {
            DataRowView drv = (DataRowView)SelectedBookingFormView.DataItem;

            KMobile.Web.UI.WebControls.DateTimePicker DateTimePicker1 = (KMobile.Web.UI.WebControls.DateTimePicker)SelectedBookingFormView.Row.FindControl("DateTimePicker1");

            DateTimePicker1.Value = (DateTime)drv.Row["startdatetime"];

            if (User.IsInRole("Admin"))
            {
                TextBox endtimeTextBox = (TextBox)SelectedBookingFormView.Row.FindControl("endtimeTextBox");
                endtimeTextBox.Enabled = true;
                endtimeTextBox.Visible = true;
            }
        }
        

    }

    

    protected void SelectedBookingFormView_ItemUpdated(object sender, FormViewUpdatedEventArgs e)
    {

        UpdateBookingExceptionLabel.Text = "";

        if (e.Exception != null)
        {
            Common.ExceptionHandling(e.Exception, UpdateBookingExceptionLabel);

            //Handle colliding booking exception
            if (e.Exception.InnerException != null)
            {
                if (e.Exception.InnerException is Common.CollidingBookingException)
                {
                    Common.CollidingBookingException collidingBookingException = (Common.CollidingBookingException)e.Exception.InnerException;
                    TextBox starttimeTextBox = (TextBox)SelectedBookingFormView.Row.FindControl("starttimeTextBox");
                    starttimeTextBox.Text = collidingBookingException.ProposedStartdatetime.ToShortTimeString();

                    if (User.IsInRole("Admin"))
                    {
                        TextBox endtimeTextBox = (TextBox)SelectedBookingFormView.Row.FindControl("endtimeTextBox");
                        endtimeTextBox.Text = collidingBookingException.ProposedEnddatetime.ToShortTimeString();
                    }

                    UpdateBookingExceptionLabel.Text = collidingBookingException.Message + " Nästa lediga tid är kl. " + collidingBookingException.ProposedStartdatetime.ToShortTimeString() + ". Tryck Boka igen om du vill boka den föreslagna tiden, annars skriv en annan tid och tryck Boka.";
                    UpdateBookingExceptionLabel.Visible = true;
                }
            }

            e.ExceptionHandled = true;

            IsBookingUpdated = false;

            e.KeepInEditMode = true;
        }
        else
        {
            //TODO: Bekräftelse att bokningen lades till?
            /*DateTime start = DateTime.Parse(e.Values["startdatetime"].ToString());
            DateTime end = DateTime.Parse(e.Values["enddatetime"].ToString());
            DateTime date = DateTime.Parse(e.Values["startdatetime"].ToString());

            Response.Write(e.Values["title"].ToString() + " är nu bokad mellan " + start.ToShortTimeString() + " - " + end.ToShortTimeString() + " " + date.ToLongDateString());
            */

            //Response.Write(Server.HtmlEncode(e.OldValues["title"].ToString()) + " är nu updaterad");

            //TODO: What has been changed? If the date has been changed, goto to that date in the calendar.

            UpdateBookingExceptionLabel.Visible = false;
            IsBookingUpdated = (e.AffectedRows == -1);

        }
    }

    #endregion

    #region ObjectDataSource events
    protected void SelectedBookingObjectDataSource_Updating(object sender, ObjectDataSourceMethodEventArgs e)
    {

        if (User.IsInRole("Admin"))
            SelectedBookingObjectDataSource.UpdateMethod = "UpdateBooking";

        DateTime date = new DateTime((long)0);
        DateTime startTime = new DateTime((long)0);
        DateTime endTime = new DateTime((long)0);
        TextBox starttimeTextBox = new TextBox();
        TextBox endtimeTextBox = new TextBox();

        KMobile.Web.UI.WebControls.DateTimePicker dateTimePicker = new KMobile.Web.UI.WebControls.DateTimePicker();

        try
        {
            dateTimePicker = (KMobile.Web.UI.WebControls.DateTimePicker)SelectedBookingFormView.Row.FindControl("DateTimePicker1");
            date = DateTime.Parse(dateTimePicker.Text);
        }
        catch (Exception exception)
        {
            UpdateBookingExceptionLabel.Visible = true;
            UpdateBookingExceptionLabel.Text = "Det uppstod ett problem när bokningen skulle uppdateras: \n";
            UpdateBookingExceptionLabel.Text += String.Format("{0} är inte ett giltigt datum.", dateTimePicker.Text);
            e.Cancel = true;
        }

        try
        {
            starttimeTextBox = (TextBox)SelectedBookingFormView.Row.FindControl("starttimeTextBox");
            startTime = DateTime.Parse(starttimeTextBox.Text);
        }
        catch (Exception exception)
        {
            UpdateBookingExceptionLabel.Visible = true;
            UpdateBookingExceptionLabel.Text = "Det uppstod ett problem när bokningen skulle uppdateras: \n";
            UpdateBookingExceptionLabel.Text += String.Format("{0} är inte ett giltigt klockslag", starttimeTextBox.Text);
            e.Cancel = true;
        }



        DateTime startDateTime = new DateTime(date.Year, date.Month, date.Day, startTime.Hour, startTime.Minute, 0);
        DateTime endDateTime = new DateTime((long)0);

        if (User.IsInRole("Admin"))
        {
            try
            {
                endtimeTextBox = (TextBox)SelectedBookingFormView.Row.FindControl("endtimeTextBox");
                endTime = DateTime.Parse(endtimeTextBox.Text);
                endDateTime = new DateTime(date.Year, date.Month, date.Day, endTime.Hour, endTime.Minute, 0);
            }
            catch (Exception exception)
            {
                UpdateBookingExceptionLabel.Visible = true;
                UpdateBookingExceptionLabel.Text = "Det uppstod ett problem när bokningen skulle läggas till: \n";
                UpdateBookingExceptionLabel.Text += String.Format("{0} är inte ett giltigt klockslag", endtimeTextBox.Text);
                e.Cancel = true;
            }
        }
        else
            endDateTime = startDateTime.AddMinutes(30.0);

        e.InputParameters["startdatetime"] = startDateTime.ToString();
        e.InputParameters["enddatetime"] = endDateTime.ToString();



        /*DropDownList drpBookingtype = (DropDownList)SelectedBookingFormView.FindControl("drpBookingtype");
        if (drpBookingtype != null)
            e.InputParameters["bookingtypeid"] = drpBookingtype.SelectedValue;*/


    }
    
    protected void BookingtypeObjectDataSource_Selecting(object sender, ObjectDataSourceSelectingEventArgs e)
    {
        if (User.IsInRole("Admin"))
            BookingtypeObjectDataSource.SelectMethod = "GetAllBookingtypes";
    }

    protected void BookingArrivalObjectDataSource_Updating(object sender, ObjectDataSourceMethodEventArgs e)
    {
        e.InputParameters["arrived"] = true;
    }

    protected void BookingNotShownObjectDataSource_Updating(object sender, ObjectDataSourceMethodEventArgs e)
    {
        e.InputParameters["notshown"] = true;
    }

    protected void BookingCancelledObjectDataSource_Updating(object sender, ObjectDataSourceMethodEventArgs e)
    {
        e.InputParameters["cancelled"] = true;
        TextBox cancellednoteTextBox = (TextBox)SelectedBookingFormView.FindControl("CancellednoteTextBox");
        e.InputParameters["cancellednote"] = cancellednoteTextBox.Text;
    }

    protected void FutureBookingsObjectDataSource_Selecting(object sender, ObjectDataSourceSelectingEventArgs e)
    {
        e.InputParameters["patientid"] = PatientId;
    }
 
    
    protected void BookingCancelledObjectDataSource_Updated(object sender, ObjectDataSourceStatusEventArgs e)
    {
        if (e.AffectedRows == -1)
        {
            
        }

    }
    #endregion

    protected void FutureBookingsGridView_SelectedIndexChanged(object sender, EventArgs e)
    {
        GridView futureBookingsGridView = (GridView)SelectedBookingFormView.Row.FindControl("FutureBookingsGridView");
        DataKey key = futureBookingsGridView.SelectedDataKey;

        //Store the bookingid as the Result so it will be possible to get it from the Parent window (i.e. the Bookings view window)
        Result = key.Value;
        SelectedEvent = EventTypes.Selected;
        RefreshParentDialog();

    }
    
}
