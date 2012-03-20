using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;



public partial class _Default : System.Web.UI.Page
{
    public string FilterLetter
    {
        get
        {
            object o = ViewState["filterLetter"];
            if (o != null)
                return (string)o;
            else
                return null;
        }
        set
        {
            ViewState["filterLetter"] = value;
        }
    }
    
    
    void Page_Load(Object Sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            
            Common.SetupRepeater(Repeater1);
            //"A" instead of null makes the page faster to load
            FilterLetter = "A";

            
        }

        //Check for certain postbacks here
        if (this.Context.Request["__EVENTTARGET"] == "NewPatient" &&
                this.Context.Request["__EVENTARGUMENT"] == "Selected")
        {
            try
            {
                Patient patient = (Patient)Session["NewPatient_Result"];
                FilterLetter = patient.Surname + " " + patient.Firstname;
            }
            catch (Exception exception)
            {
                //throw new ApplicationException("Kan ej visa den nya patienten");
            }
            
        }

        if (this.Context.Request["__EVENTTARGET"] == "PatientInfo" &&
                this.Context.Request["__EVENTARGUMENT"] == "Selected")
        {
            try
            {
                Patient patient = (Patient)Session["PatientInfo_Result"];
                FilterLetter = patient.Surname + " " + patient.Firstname;
            }
            catch (Exception exception)
            {
                //throw new ApplicationException("Kan ej visa den nya patienten");
            }
            
            //We better update the gridview if the user has changed any patientinfo
            
        }

        PatientsGridView.DataBind();

        SetupJavaScript();

    }

    protected void SetupJavaScript()
    {

        string scriptString = "";

        scriptString += "<script language=JavaScript> ";
        scriptString += " function newPatient(){";

        scriptString += " var url = 'NewPatient.aspx?Parent=NewPatient';";
        scriptString += " window.open(url,'NewPatient','height=600,width=600,status=0,toolbar=0,menubar=0,resizable=1,scrollbars=1');";
        scriptString += " }";
        scriptString += " function showPatientInfo(patientid){";
        scriptString += " var url = 'PatientInfo.aspx?Parent=PatientInfo';";
        scriptString += " var addQuery = '&PatientId=';";
        scriptString += " addQuery = addQuery.concat(patientid);";
        scriptString += " url = url.concat(addQuery);";
        scriptString += " window.open(url,'PatientInfo','height=600,width=600,status=0,toolbar=0,menubar=0,resizable=1,scrollbars=1');";
        scriptString += " }";
        scriptString += " </script>";
        RegisterStartupScript("PatientScript", scriptString);

        scriptString = "";

        TextBox searchTextBox = (TextBox)SearchPanel.FindControl("SearchTextBox");

        scriptString += "<script language=JavaScript> ";
        scriptString += "function body_onload() {";
        scriptString += "doLoad();";
        scriptString += "shortcut.add('t',function() {	window.location.replace('Bookings_pilot.aspx');},{'disable_in_input':true});";
        scriptString += "shortcut.add('n',function() {	document.getElementById('" + NewPatientButton.ClientID + "').click();},{'disable_in_input':true});";
        scriptString += "shortcut.add('v',function() {	window.location.replace('Bookings_week.aspx');},{'disable_in_input':true});";
        scriptString += "shortcut.add('ESC',function() {	window.location.replace('../Logout.aspx');},{'disable_in_input':false});"; 
        scriptString += "document.getElementById('" + searchTextBox.ClientID + "').focus();}";
        scriptString += " </script>";
        RegisterStartupScript("BodyLoadScript", scriptString);

        NewPatientButton.Attributes.Add("onclick", "javascript:newPatient();return false;");

        scriptString = "";
        scriptString += "<script language=JavaScript> ";
        scriptString += "var sURL = unescape(window.location.pathname);";
        scriptString += "function doLoad() {";
        scriptString += "setTimeout( 'refresh()', 910*1000 ); }";
        scriptString += "function refresh() {";
        scriptString += "window.location.replace( sURL ); }";
        scriptString += " </script>";
        RegisterStartupScript("AutomaticRefreshScript", scriptString);


        Page.RegisterStartupScript("Shortcut",
           "<script language=javascript src='shortcut.js'></script>");
        
    }

    

    protected void SearchBtn_Click(object sender, EventArgs e)
    {
        TextBox searchTextBox = (TextBox)SearchPanel.FindControl("SearchTextBox");
        FilterLetter = searchTextBox.Text;
        PatientsGridView.DataBind();
    }

    protected void ClearBtn_Click(object sender, EventArgs e)
    {
        TextBox searchTextBox = (TextBox)SearchPanel.FindControl("SearchTextBox");
        if (String.IsNullOrEmpty(searchTextBox.Text))
            return;
        FilterLetter = null;
        PatientsGridView.DataBind();
        searchTextBox.Text = "";
    }

    
    
    protected void Repeater1_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        if (e.CommandName == "Filter")
        {
            FilterLetter = e.CommandArgument.ToString();
            if (FilterLetter == "[ Visa alla ]")
                FilterLetter = null;
            PatientsGridView.DataBind();
            
        }
    }
    protected void Repeater1_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        DataRowView dr = (DataRowView)e.Item.DataItem;

        if ((string)dr[0] == FilterLetter)
        {
            LinkButton linkButton = (LinkButton)e.Item.FindControl("lnkLetter");
            linkButton.Enabled = false;
        }
    }
    
    

    protected void ObjectDataSource1_Selecting(object sender, ObjectDataSourceSelectingEventArgs e)
    {
        if (!String.IsNullOrEmpty(FilterLetter))
        {
            string[] name = FilterLetter.Split(' ');

            e.InputParameters["surname"] = name[0];
            if (name.Length == 2)
                e.InputParameters["firstname"] = name[1];

            //TODO: Handle odd inputs here
        }
        else
        {
            e.InputParameters["surname"] = null;
            e.InputParameters["firstname"] = null;
        }

        
    }


    protected void PatientsGridView_PreRender(object sender, EventArgs e)
    {
        DataKeyArray keys = PatientsGridView.DataKeys;
        int i = 0;
        foreach (GridViewRow row in PatientsGridView.Rows)
        {
            DataKey key = keys[i];
            row.Attributes.Add("onclick", "javascript:showPatientInfo(" + key.Value.ToString() + ");");
            i++;
        }
    }
}
