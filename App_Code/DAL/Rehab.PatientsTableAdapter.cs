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
namespace RehabTableAdapters 
{
	public partial class PatientsTableAdapter
	{
        public int InsertPatient(string surname, string firstname, string street, string zipcode, string city, string homephone, string workphone, string mobilephone, string personnumber, string info, string freecarddate)
        {
            if ((surname == null))
            {
                this.Adapter.InsertCommand.Parameters[0].Value = System.DBNull.Value;
            }
            else
            {
                this.Adapter.InsertCommand.Parameters[0].Value = ((string)(surname));
            }
            if ((firstname == null))
            {
                this.Adapter.InsertCommand.Parameters[1].Value = System.DBNull.Value;
            }
            else
            {
                this.Adapter.InsertCommand.Parameters[1].Value = ((string)(firstname));
            }
            if ((street == null))
            {
                this.Adapter.InsertCommand.Parameters[2].Value = System.DBNull.Value;
            }
            else
            {
                this.Adapter.InsertCommand.Parameters[2].Value = ((string)(street));
            }
            if ((zipcode == null))
            {
                this.Adapter.InsertCommand.Parameters[3].Value = System.DBNull.Value;
            }
            else
            {
                this.Adapter.InsertCommand.Parameters[3].Value = ((string)(zipcode));
            }
            if ((city == null))
            {
                this.Adapter.InsertCommand.Parameters[4].Value = System.DBNull.Value;
            }
            else
            {
                this.Adapter.InsertCommand.Parameters[4].Value = ((string)(city));
            }
            if ((homephone == null))
            {
                this.Adapter.InsertCommand.Parameters[5].Value = System.DBNull.Value;
            }
            else
            {
                this.Adapter.InsertCommand.Parameters[5].Value = ((string)(homephone));
            }
            if ((workphone == null))
            {
                this.Adapter.InsertCommand.Parameters[6].Value = System.DBNull.Value;
            }
            else
            {
                this.Adapter.InsertCommand.Parameters[6].Value = ((string)(workphone));
            }
            if ((mobilephone == null))
            {
                this.Adapter.InsertCommand.Parameters[7].Value = System.DBNull.Value;
            }
            else
            {
                this.Adapter.InsertCommand.Parameters[7].Value = ((string)(mobilephone));
            }
            if ((personnumber == null))
            {
                this.Adapter.InsertCommand.Parameters[8].Value = System.DBNull.Value;
            }
            else
            {
                this.Adapter.InsertCommand.Parameters[8].Value = ((string)(personnumber));
            }
            if ((info == null))
            {
                this.Adapter.InsertCommand.Parameters[9].Value = System.DBNull.Value;
            }
            else
            {
                this.Adapter.InsertCommand.Parameters[9].Value = ((string)(info));
            }
            if ((freecarddate == null))
            {
                this.Adapter.InsertCommand.Parameters[10].Value = System.DBNull.Value;
            }
            else
            {
                this.Adapter.InsertCommand.Parameters[10].Value = ((string)(freecarddate));
            }
            System.Data.ConnectionState previousConnectionState = this.Adapter.InsertCommand.Connection.State;
            if (((this.Adapter.InsertCommand.Connection.State & System.Data.ConnectionState.Open)
                        != System.Data.ConnectionState.Open))
            {
                this.Adapter.InsertCommand.Connection.Open();
            }
            try
            {
                int returnValue = this.Adapter.InsertCommand.ExecuteNonQuery();
                System.Data.OleDb.OleDbConnection conn = this.Adapter.InsertCommand.Connection;
                System.Data.OleDb.OleDbCommand cmd = new System.Data.OleDb.OleDbCommand("SELECT @@IDENTITY FROM Patients", conn );
                returnValue = Convert.ToInt32(cmd.ExecuteScalar());
                return returnValue;
            }
            finally
            {
                if ((previousConnectionState == System.Data.ConnectionState.Closed))
                {
                    this.Adapter.InsertCommand.Connection.Close();
                }
            }
        }
	}
}
