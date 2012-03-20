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
using System.Web.Configuration;

public partial class Secure_Test : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Configuration config = WebConfigurationManager.OpenWebConfiguration(Request.ApplicationPath);

        TrustSection configSections = (TrustSection)config.GetSection("system.web/trust");
        Response.Write("ASP.NET Configuration Info<br>");

        // Display Config details.
        Response.Write("File Path: " + config.FilePath + "<BR />");
        Response.Write("Section Path: " + configSections.SectionInformation.Name + "<BR />");

        // Display Level property.
        Response.Write("Level: " + configSections.Level + "<BR />");

        

    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        Configuration config = WebConfigurationManager.OpenWebConfiguration(Request.ApplicationPath);
        
        ConnectionStringsSection connectionStringsSection = (ConnectionStringsSection)config.GetSection("connectionStrings");
        if (!connectionStringsSection.SectionInformation.IsProtected)
        {
            connectionStringsSection.SectionInformation.ProtectSection("DataProtectionConfigurationProvider");
            connectionStringsSection.SectionInformation.ForceSave = true;
            config.Save(ConfigurationSaveMode.Modified);
            protectionInfoLabel.Text = "ConnectionStrings är nu krypterad";
        }
        else
            protectionInfoLabel.Text = "ConnectionStrings var redan krypterad";

    }
}
