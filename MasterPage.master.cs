using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

/// <summary>
/// Summary description for MasterPage
/// </summary>
public partial class MasterPage : System.Web.UI.MasterPage
{
    public void Page_Load(object sender, EventArgs e)
    {
        if (Page.User.IsInRole("Admin"))
        {
            Menu1.DataSourceID = "SiteMapDataSource2";
        }
    }
	
}
