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
/// Summary description for Rehab
/// </summary>
public partial class Rehab
{
    public partial class BookingsDataTable
    {
        public override void BeginInit()
        {
            base.BeginInit();
            this.ColumnChanging += new DataColumnChangeEventHandler(ValidateColumn);
        }

        void ValidateColumn(object sender, DataColumnChangeEventArgs e)
        {
            if (e.Column.Equals(this.patientidColumn))
            {
                if (Convert.IsDBNull(e.ProposedValue) || ((int)e.ProposedValue < 0))
                {
                    throw new ArgumentException(string.Format("{0} kan inte vara null eller negativt.", e.Column.ColumnName), e.Column.ColumnName);
                }
            }

            if (e.Column.Equals(this.startdatetimeColumn))
            {
                if (Convert.IsDBNull(e.ProposedValue) || (((DateTime)e.ProposedValue).Year == DateTime.MinValue.Year) || (((DateTime)e.ProposedValue).Year == DateTime.MaxValue.Year))
                {
                    throw new ArgumentException(string.Format("{0} är inte ett giltigt datum.", e.Column.ColumnName), "frikortsdatum");
                }
            }

            if (e.Column.Equals(this.titleColumn))
            {
                if (Convert.IsDBNull(e.ProposedValue) || ((string)e.ProposedValue).Length == 0)
                {
                    throw new ArgumentException(string.Format("{0} kan inte vara null eller tom.", e.Column.ColumnName), e.Column.ColumnName);
                }
            }

        }


    }
}
