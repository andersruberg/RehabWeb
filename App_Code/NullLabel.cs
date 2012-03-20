using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.ComponentModel;
using System.Security.Permissions;

namespace MyWebControls {
    /// <summary>
    /// Summary description for NullLabel
    /// </summary>
    [
    AspNetHostingPermission(SecurityAction.Demand,
        Level = AspNetHostingPermissionLevel.Minimal),
    AspNetHostingPermission(SecurityAction.InheritanceDemand, 
        Level=AspNetHostingPermissionLevel.Minimal),
    DefaultProperty("NullText"),
    ToolboxData("<{0}:NullLabel runat=\"server\"> </{0}:NullLabel>")
    ]

    public class NullLabel : System.Web.UI.WebControls.Label
    {
        [
        Bindable(true),
        Category("Appearance"),
        DefaultValue(""),
        Description("The null text."),
        Localizable(true)
        ]

        public virtual string NullText
        {
            get
            {
                string s = (string)ViewState["NullText"];
                return (s == null) ? String.Empty : s;
            }
            set
            {
                ViewState["NullText"] = value;
            }
        }

        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);

            if ((Text == null) || (Text.Length == 0))
                Text = NullText;
        }
    }
}
