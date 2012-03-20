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

public partial class Secure_PatientInfo : KMobile.Web.UI.LookupPage
{
    #region Properties

    private int? _patientid = null;

    private bool IsPatientUpdated
    {
        get
        {
            object o = ViewState["IsPatientUpdated"];

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
            ViewState["IsPatientUpdated"] = value;
        }
    }
    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void SelectedPatientObjectDataSource_Updating(object sender, ObjectDataSourceMethodEventArgs e)
    {
         DateTime date = new DateTime((long)0);

        KMobile.Web.UI.WebControls.DateTimePicker dateTimePicker = new KMobile.Web.UI.WebControls.DateTimePicker();

        if (!String.IsNullOrEmpty(dateTimePicker.Text))
        {
            try
            {
                dateTimePicker = (KMobile.Web.UI.WebControls.DateTimePicker)SelectedPatientFormView.FindControl("DateTimePicker1");
                date = DateTime.Parse(dateTimePicker.Text);
            }
            catch (Exception exception)
            {
                UpdatePatientExceptionLabel.Visible = true;
                UpdatePatientExceptionLabel.Text = "Det uppstod ett problem när patienten skulle uppdateras: \n";
                UpdatePatientExceptionLabel.Text += String.Format("{0} är inte ett giltigt datum.", dateTimePicker.Text);
                e.Cancel = true;
            }
        }

    }

    protected void SelectedPatientFormView_ItemCommand(object sender, FormViewCommandEventArgs e)
    {

        if (e.CommandName == "Cancel")
        {
            SelectedPatientFormView.ChangeMode(FormViewMode.ReadOnly);
            UpdatePatientExceptionLabel.Visible = false;
        }
        if (e.CommandName == "Close")
        {
            if (IsPatientUpdated)
            {
                SelectedEvent = EventTypes.Selected;
                RefreshParentDialog();
            }
            else
                Response.Write("<script>window.opener='x';window.close();</script>");
        }
        if (e.CommandName == "Delete")
        {

         

        }
        
        if (e.CommandName == "ShowFutureBookings")
        {
            GridView futureBookingsGridView = (GridView)SelectedPatientFormView.Row.FindControl("FutureBookingsGridView");
            futureBookingsGridView.DataBind();
            futureBookingsGridView.Visible = true;
        }
    }

    protected void SelectedPatientFormView_ItemCreated(object sender, EventArgs e)
    {
        /*KMobile.Web.UI.WebControls.DateTimePicker DateTimePicker1 = (KMobile.Web.UI.WebControls.DateTimePicker)SelectedPatientFormView.FindControl("DateTimePicker1");
        DataRowView dataRowView = (DataRowView)SelectedPatientFormView.DataItem;
        try
        {
            DateTimePicker1.Value = DateTime.Parse((string)dataRowView["freecarddate"]);
        }
        catch (Exception exception)
        {
            new ArgumentException("Det uppstod ett fel vid inläsning av frikortsdatumet:\n" + exception.Message);
        }*/
    }

    protected void SelectedPatientObjectDataSource_Updated(object sender, ObjectDataSourceStatusEventArgs e)
    {

        _patientid = Convert.ToInt32(e.ReturnValue);
    }

    protected void SelectedPatientFormView_DataBound(object sender, EventArgs e)
    {
        /*if (SelectedPatientFormView.CurrentMode == FormViewMode.Edit)
        {
            DataRowView drv = (DataRowView)SelectedPatientFormView.DataItem;
            KMobile.Web.UI.WebControls.DateTimePicker DateTimePicker1 = (KMobile.Web.UI.WebControls.DateTimePicker)SelectedPatientFormView.FindControl("DateTimePicker1");

            DateTimePicker1.Value = (DateTime)drv["freecarddate"];
        }*/

    }

    protected void SelectedPatientFormView_ItemUpdated(object sender, FormViewUpdatedEventArgs e)
    {
        if (e.Exception != null)
        {
            Common.ExceptionHandling(e.Exception, UpdatePatientExceptionLabel);
            
            e.ExceptionHandled = true;

            IsPatientUpdated = false;

            e.KeepInEditMode = true;
        }
        else
        {
            if (e.AffectedRows == -1)
            {
                UpdatePatientExceptionLabel.Visible = false;

                IsPatientUpdated = true;

                Patient patient = new Patient();

                if (e.NewValues["surname"] != null)
                    patient.Surname = (string)e.NewValues["surname"];
                if (e.NewValues["firstname"] != null)
                    patient.Firstname = (string)e.NewValues["firstname"];
                if (e.NewValues["personnumber"] != null)
                    patient.Personnumber = (string)e.NewValues["personnumber"];
                if (e.NewValues["freecarddate"] != null)
                    patient.FreecardDate = (string)e.NewValues["freecarddate"];
                if (e.NewValues["homephone"] != null)
                    patient.HomePhone = (string)e.NewValues["homephone"];
                if (e.NewValues["workphone"] != null)
                    patient.WorkPhone = (string)e.NewValues["workphone"];
                if (e.NewValues["mobilehone"] != null)
                    patient.MobilePhone = (string)e.NewValues["mobilephone"];
                if (_patientid != null)
                    patient.PatientId = _patientid.Value;

                Result = patient;
            }

        }
    }


    protected void SelectedPatientFormView_ItemDeleted(object sender, FormViewDeletedEventArgs e)
    {
        if (e.Exception != null)
        {
            Common.ExceptionHandling(e.Exception, UpdatePatientExceptionLabel);

            e.ExceptionHandled = true;

            IsPatientUpdated = false;
        }
        else if (e.AffectedRows == -1)
        {

            Result = new Patient();

            SelectedEvent = EventTypes.Selected;
            RefreshParentDialog();

        }
        
    }
}
