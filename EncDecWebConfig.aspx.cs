using System;
using System.Web.Configuration;
using System.IO;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

public partial class management_EncDecWebConfig : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        // load web.config into TextBox on first page visit...
        if (!Page.IsPostBack)
        {
            RefreshWebConfig();
        }
    }

    private void RefreshWebConfig()
    {
        // Read in the value of Web.config into the TextBox...
        using (StreamReader reader = File.OpenText(Server.MapPath("../Web.config")))
        {
            webConfig.Text = reader.ReadToEnd();
        }
    }

    protected void btnRefreshWebConfig_Click(object sender, EventArgs e)
    {
        RefreshWebConfig();
    }
    
    protected void btnEncrypt_Click(object sender, EventArgs e)
    {
        ProtectSection("connectionStrings", "DataProtectionConfigurationProvider");
        RefreshWebConfig();
    }

    protected void btnDescrypt_Click(object sender, EventArgs e)
    {
        UnProtectSection("connectionStrings");
        RefreshWebConfig();
    }

    protected void btnEncAuthentication_Click(object sender, EventArgs e)
    {
        ProtectSection("system.web/authentication", "DataProtectionConfigurationProvider");
        RefreshWebConfig();
    }

    protected void btnDecAuthentication_Click(object sender, EventArgs e)
    {
        UnProtectSection("system.web/authentication");
        RefreshWebConfig();
    }
    protected void btnEncAppSettings_Click(object sender, EventArgs e)
    {
        ProtectSection("appSettings", "DataProtectionConfigurationProvider");
        RefreshWebConfig();
    }
    protected void btnDecAppSettings_Click(object sender, EventArgs e)
    {
        UnProtectSection("appSettings");
        RefreshWebConfig();
    }
    protected void btnEncAll_Click(object sender, EventArgs e)
    {
        ProtectSection("connectionStrings", "DataProtectionConfigurationProvider");
        ProtectSection("system.web/authentication", "DataProtectionConfigurationProvider");
        ProtectSection("appSettings", "DataProtectionConfigurationProvider");
        RefreshWebConfig();
    }
    protected void btnDecAll_Click(object sender, EventArgs e)
    {
        UnProtectSection("connectionStrings");
        UnProtectSection("system.web/authentication");
        UnProtectSection("appSettings");
        RefreshWebConfig();
    }

#region "Protect/Unprotect Methods"
    // Code taken verbatim from David Hayden's blog [http://davidhayden.com/blog/dave/]
    // Entry: Encrypt Connection Strings AppSettings and Web.Config in ASP.NET 2.0 - Security Best Practices
    // [http://davidhayden.com/blog/dave/archive/2005/11/17/2572.aspx]

    private void ProtectSection(string sectionName,
                                   string provider)
    {
        Configuration config =
            WebConfigurationManager.
                OpenWebConfiguration(Request.ApplicationPath);
        
        ConfigurationSection section =
                     config.GetSection(sectionName);

        if (section != null &&
                  !section.SectionInformation.IsProtected)
        {
            section.SectionInformation.ProtectSection(provider);
            config.Save();
        }
    }

    private void UnProtectSection(string sectionName)
    {
        Configuration config =
            WebConfigurationManager.
                OpenWebConfiguration(Request.ApplicationPath);

        ConfigurationSection section =
                  config.GetSection(sectionName);

        if (section != null &&
              section.SectionInformation.IsProtected)
        {
            section.SectionInformation.UnprotectSection();
            config.Save();
        }
    }
    #endregion
}
