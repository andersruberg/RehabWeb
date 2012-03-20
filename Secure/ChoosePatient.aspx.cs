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
using KMobile.Web.UI.WebControls;

public partial class Secure_ChoosePatient : KMobile.Web.UI.LookupPage
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
    
    
    protected void HighlightGridView1_SelectedIndexChanged(object sender, EventArgs e)
    {
        
        //Extract the primary key from the selected row
        int primaryKey = (int)HighlightGridView1.SelectedValue;

        
        if (primaryKey > 0)
        {
            
            DataRowView drv = (DataRowView)HighlightGridView1.CachedDataItems[HighlightGridView1.SelectedIndex];
            Rehab.PatientsRow patientsRow = (Rehab.PatientsRow)drv.Row;

            if (patientsRow != null)
            {
                Patient patient = new Patient(patientsRow);
                this.Result = patient;
            }

            this.RefreshParentDialog();
        }
        else
            throw new Exception("Den valda patienten är inte giltig");
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            FilterLetter = "A";
            Common.SetupRepeater(Repeater1);
            HighlightGridView1.DataBind();
            SelectedEvent = EventTypes.Selected;
        }
    }

    protected void SearchBtn_Click(object sender, EventArgs e)
    {
        FilterLetter = SearchTxtBox.Text;
        HighlightGridView1.DataBind();

    }

    protected void ClearBtn_Click(object sender, EventArgs e)
    {
        FilterLetter = null;
        HighlightGridView1.DataBind();
        SearchTxtBox.Text = "";
    }




    protected void Repeater1_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        if (e.CommandName == "Filter")
        {
            FilterLetter = e.CommandArgument.ToString();
            if (FilterLetter == "[ Visa alla ]")
                FilterLetter = null;
            HighlightGridView1.DataBind();

        }
    }

    protected void Repeater1_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        DataRowView dr = (DataRowView)e.Item.DataItem;

        if ((string)dr[0] == (string)ViewState["filterLetter"])
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
}
