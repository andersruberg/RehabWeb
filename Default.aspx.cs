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

public partial class _Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        
        /*if (!Request.IsSecureConnection)
        {
            Response.Redirect("https://dorisruberg-se.loopiasecure.com");
            return;
        }*/

        TextBox usernameTextBox = (TextBox)Login1.FindControl("UserName");

        string scriptString = "";

        scriptString += "<script language=JavaScript> ";
        scriptString += "function body_onload() {";
        scriptString += "document.getElementById('" + usernameTextBox.ClientID + "').focus();}";
        scriptString += " </script>";
        RegisterStartupScript("BodyLoadScript", scriptString);

        HttpBrowserCapabilities browserCapabilities = Request.Browser;
        
        // Access Browser details through the Browser property.
        Version jScriptVersion = browserCapabilities.JScriptVersion;

        // Test if the browser supports Javascript.
        if (jScriptVersion == null)
        {
            Label JavaScriptEnabledLabel = (Label)Login1.FindControl("JavaScriptEnabledLabel");
            JavaScriptEnabledLabel.Text = "Javascript är inte aktiverat. Den här sidan kommer inte att fungera korrekt.";
            JavaScriptEnabledLabel.ForeColor = System.Drawing.Color.Red;
        }

        if (!browserCapabilities.Cookies)
        {
            Label CookiesEnabledLabel = (Label)Login1.FindControl("CookiesEnabledLabel");
            CookiesEnabledLabel.Text = "Cookies är inte aktiverat. Den här sidan kommer inte att fungera korrekt";
            CookiesEnabledLabel.ForeColor = System.Drawing.Color.Red;
        }

    }
}
