using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

namespace MyWebControls
{
    /// <summary>
    /// Summary description for FormViewPersistant
    /// </summary>
    public class FormViewPersistant : FormView
    {
        protected override void OnDataSourceViewChanged(object sender, EventArgs e)
        {
            base.OnDataSourceViewChanged(sender, e);
            if (this.CurrentMode == FormViewMode.Insert)
                RequiresDataBinding = false;
        }
    }
}
