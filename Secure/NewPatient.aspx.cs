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

public partial class Secure_NewPatient : KMobile.Web.UI.LookupPage
{
    private int patientid;
    
    protected void Page_Load(object sender, EventArgs e)
    {

        TextBox surnameTextBox = (TextBox)NewPatientFormView.FindControl("surnameTextBox");

        string scriptString = "";

        scriptString += "<script language=JavaScript> ";
        scriptString += "function body_onload() {";
        scriptString += "shortcut.add('l',function() { document.getElementById('NewPatientFormView_InsertButton').click();},{'disable_in_input':true});";
        scriptString += "shortcut.add('ESC',function() {window.opener='x';window.close();},{'disable_in_input':false});";
        scriptString += "document.getElementById('" + surnameTextBox.ClientID + "').focus();}";
        scriptString += " </script>";
        RegisterStartupScript("BodyLoadScript", scriptString);

        Page.RegisterStartupScript("Shortcut",
       "<script language=javascript src='shortcut.js'></script>");

    }


    protected void NewPatientFormView_ItemInserted(object sender, FormViewInsertedEventArgs e)
    {
        e.KeepInInsertMode = true;
        
    
        if (e.Exception != null)
        {
            InsertPatientLabel.Visible = true;
            
            InsertPatientLabel.Text = "Det uppstod ett problem när patienten skulle läggas till: \n";
            InsertPatientLabel.Text += e.Exception.Message;

            if (e.Exception.InnerException != null)
            {
                Exception innerException = e.Exception.InnerException;

                if (innerException is System.Data.Common.DbException)
                    InsertPatientLabel.Text += "Databasen fungerar för närvarande inte som den ska.";
                else if (innerException is System.ArgumentException)
                {
                    string paramName = ((ArgumentException)innerException).ParamName;
                    InsertPatientLabel.Text += string.Concat("Värdet ", paramName, " är inte giltigt. Var god kontrollera patientuppgifterna.");
                }
                else if (innerException is NoNullAllowedException)
                    InsertPatientLabel.Text += "Det saknas ett eller flera nödvändiga fält. Var god kontrollera patientuppgifterna.";
                else if (innerException is ApplicationException)
                    InsertPatientLabel.Text += innerException.Message;
            }
            e.ExceptionHandled = true;
        }
        else
        {
            if (e.AffectedRows == -1)
            {
                //Hide error message that could be shown earlier
                InsertPatientLabel.Visible = false;
                
                

                Patient patient = new Patient();

                if (e.Values["surname"] != null)
                    patient.Surname = (string)e.Values["surname"];
                if (e.Values["firstname"] != null)
                    patient.Firstname = (string)e.Values["firstname"];
                if (e.Values["personnumber"] != null)
                    patient.Personnumber = (string)e.Values["personnumber"];
                if (e.Values["freecarddate"] != null)
                    patient.FreecardDate = (string)e.Values["freecarddate"];
                if (e.Values["homephone"] != null)
                    patient.HomePhone = (string)e.Values["homephone"];
                if (e.Values["workphone"] != null)
                    patient.WorkPhone = (string)e.Values["workphone"];
                if (e.Values["mobilehone"] != null)
                    patient.MobilePhone = (string)e.Values["mobilephone"];

                if (patientid != null)
                    patient.PatientId = patientid;
                    
                Result = patient;

                SelectedEvent = EventTypes.Selected;

                RefreshParentDialog();
            }

        }
    }

    protected void NewPatientFormView_ItemCommand(object sender, FormViewCommandEventArgs e)
    {
        if (e.CommandName == "Cancel")
        {
            Response.Write("<script>window.opener='x';window.close();</script>");

        }
    }

    protected void NewPatientObjectDataSource_Inserting(object sender, ObjectDataSourceMethodEventArgs e)
    {
        //
        //e.InputParameters["freecarddate"] = DateTimePicker1.Value;
        //FilterLetter = (string)e.InputParameters["surname"];

        DateTime date = new DateTime((long)0);

        KMobile.Web.UI.WebControls.DateTimePicker DateTimePicker1 = (KMobile.Web.UI.WebControls.DateTimePicker)NewPatientFormView.FindControl("DateTimePicker1");

        if (!String.IsNullOrEmpty(DateTimePicker1.Text))
        {
            try
            {
                DateTimePicker1 = (KMobile.Web.UI.WebControls.DateTimePicker)NewPatientFormView.Row.FindControl("DateTimePicker1");
                date = DateTime.Parse(DateTimePicker1.Text);
            }
            catch (Exception exception)
            {
                InsertPatientLabel.Visible = true;
                InsertPatientLabel.Text = "Det uppstod ett problem när patienten skulle läggas till: \n";
                InsertPatientLabel.Text += String.Format("{0} är inte ett giltigt datum.", DateTimePicker1.Text);
                e.Cancel = true;
            }
        }

    }
    protected void NewPatientObjectDataSource_Inserted(object sender, ObjectDataSourceStatusEventArgs e)
    {
        patientid = Convert.ToInt32(e.ReturnValue);
        
        
    }

}
