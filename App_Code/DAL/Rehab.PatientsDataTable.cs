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
    public partial class PatientsDataTable
    {
        public override void BeginInit()
        {
            base.BeginInit();
            this.ColumnChanging += new DataColumnChangeEventHandler(ValidateColumn);
        }

        void ValidateColumn(object sender, DataColumnChangeEventArgs e)
        {
            if (e.Column.Equals(this.personnumberColumn))
            {
                if (Convert.IsDBNull(e.ProposedValue) || ((string)e.ProposedValue).Length == 0)
                {
                    throw new ArgumentException(string.Format("{0} kan inte vara null eller tom.", e.Column.ColumnName), "personnumret");
                }
            }

            if (e.Column.Equals(this.freecarddateColumn))
            {
                if (!Convert.IsDBNull(e.ProposedValue))
                {
                    if (((DateTime.Parse((string)e.ProposedValue)).Year == DateTime.MinValue.Year) || ((DateTime.Parse((string)e.ProposedValue)).Year == DateTime.MaxValue.Year))
                    {
                        throw new ArgumentException(string.Format("{0} är inte ett giltigt datum.", e.Column.ColumnName), e.Column.ColumnName);
                    }
                }
            }

            if (e.Column.Equals(this.surnameColumn))
            {
                if (Convert.IsDBNull(e.ProposedValue) || ((string)e.ProposedValue).Length == 0)
                {
                    throw new ArgumentException(string.Format("{0} kan inte vara null eller tom.", e.Column.ColumnName), "efternamnet");
                }
            }

            if (e.Column.Equals(this.firstnameColumn))
            {
                if (Convert.IsDBNull(e.ProposedValue) || ((string)e.ProposedValue).Length == 0)
                {
                    throw new ArgumentException(string.Format("{0} kan inte vara null eller tom.", e.Column.ColumnName), "efternamnet");
                }
            }

        }


    }
}
