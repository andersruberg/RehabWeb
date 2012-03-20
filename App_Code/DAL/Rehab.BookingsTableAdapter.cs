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
    public partial class BookingsTableAdapter
    {

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Insert, true)]
        public virtual int CopyBooking(int bookingid, System.Nullable<int> patientid, System.Nullable<System.DateTime> createdatetime, string title, System.Nullable<System.DateTime> startdatetime, System.Nullable<System.DateTime> enddatetime, string note, System.Nullable<int> bookingtypeid, bool arrived, bool notshown, bool cancelled, string cancellednote, string tableName)
        {


            string cmdText = "INSERT INTO `" + tableName + "` (`bookingid`, `patientid`, `createdatetime`, `title`, `startdatetime`, `enddatetime`, `note`, `bookingtypeid`, `arrived`, `notshown`, `cancelled`, `cancellednote`) VALUES (?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?)";
            System.Data.OleDb.OleDbCommand cmd = new System.Data.OleDb.OleDbCommand(cmdText, this.Adapter.InsertCommand.Connection);

            cmd.Parameters.Add(new System.Data.OleDb.OleDbParameter("bookingid", System.Data.OleDb.OleDbType.Integer, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "bookingid", System.Data.DataRowVersion.Current, false, null));
            cmd.Parameters.Add(new System.Data.OleDb.OleDbParameter("patientid", System.Data.OleDb.OleDbType.Integer, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "patientid", System.Data.DataRowVersion.Current, false, null));
            cmd.Parameters.Add(new System.Data.OleDb.OleDbParameter("createdatetime", System.Data.OleDb.OleDbType.Date, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "createdatetime", System.Data.DataRowVersion.Current, false, null));
            cmd.Parameters.Add(new System.Data.OleDb.OleDbParameter("title", System.Data.OleDb.OleDbType.VarWChar, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "title", System.Data.DataRowVersion.Current, false, null));
            cmd.Parameters.Add(new System.Data.OleDb.OleDbParameter("startdatetime", System.Data.OleDb.OleDbType.Date, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "startdatetime", System.Data.DataRowVersion.Current, false, null));
            cmd.Parameters.Add(new System.Data.OleDb.OleDbParameter("enddatetime", System.Data.OleDb.OleDbType.Date, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "enddatetime", System.Data.DataRowVersion.Current, false, null));
            cmd.Parameters.Add(new System.Data.OleDb.OleDbParameter("note", System.Data.OleDb.OleDbType.VarWChar, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "note", System.Data.DataRowVersion.Current, false, null));
            cmd.Parameters.Add(new System.Data.OleDb.OleDbParameter("bookingtypeid", System.Data.OleDb.OleDbType.Integer, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "bookingtypeid", System.Data.DataRowVersion.Current, false, null));
            cmd.Parameters.Add(new System.Data.OleDb.OleDbParameter("arrived", System.Data.OleDb.OleDbType.Boolean, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "arrived", System.Data.DataRowVersion.Current, false, null));
            cmd.Parameters.Add(new System.Data.OleDb.OleDbParameter("notshown", System.Data.OleDb.OleDbType.Boolean, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "notshown", System.Data.DataRowVersion.Current, false, null));
            cmd.Parameters.Add(new System.Data.OleDb.OleDbParameter("cancelled", System.Data.OleDb.OleDbType.Boolean, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "cancelled", System.Data.DataRowVersion.Current, false, null));
            cmd.Parameters.Add(new System.Data.OleDb.OleDbParameter("cancellednote", System.Data.OleDb.OleDbType.VarWChar, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "cancellednote", System.Data.DataRowVersion.Current, false, null));

            cmd.Parameters[0].Value = (int)(bookingid);

            if ((patientid.HasValue == true))
            {
                cmd.Parameters[1].Value = ((int)(patientid.Value));
            }
            else
            {
                cmd.Parameters[1].Value = System.DBNull.Value;
            }
            if ((createdatetime.HasValue == true))
            {
                cmd.Parameters[2].Value = ((System.DateTime)(createdatetime.Value));
            }
            else
            {
                cmd.Parameters[2].Value = System.DBNull.Value;
            }
            if ((title == null))
            {
                cmd.Parameters[3].Value = System.DBNull.Value;
            }
            else
            {
                cmd.Parameters[3].Value = ((string)(title));
            }
            if ((startdatetime.HasValue == true))
            {
                cmd.Parameters[4].Value = ((System.DateTime)(startdatetime.Value));
            }
            else
            {
                cmd.Parameters[5].Value = System.DBNull.Value;
            }
            if ((enddatetime.HasValue == true))
            {
                cmd.Parameters[5].Value = ((System.DateTime)(enddatetime.Value));
            }
            else
            {
                cmd.Parameters[5].Value = System.DBNull.Value;
            }
            if ((note == null))
            {
                cmd.Parameters[6].Value = System.DBNull.Value;
            }
            else
            {
                cmd.Parameters[6].Value = ((string)(note));
            }
            if ((bookingtypeid.HasValue == true))
            {
                cmd.Parameters[7].Value = ((int)(bookingtypeid.Value));
            }
            else
            {
                cmd.Parameters[7].Value = System.DBNull.Value;
            }
            cmd.Parameters[8].Value = ((bool)(arrived));
            cmd.Parameters[9].Value = ((bool)(notshown));
            cmd.Parameters[10].Value = ((bool)(cancelled));
            if ((cancellednote == null))
            {
                cmd.Parameters[11].Value = System.DBNull.Value;
            }
            else
            {
                cmd.Parameters[11].Value = ((string)(cancellednote));
            }
            System.Data.ConnectionState previousConnectionState = cmd.Connection.State;
            if (((cmd.Connection.State & System.Data.ConnectionState.Open)
                        != System.Data.ConnectionState.Open))
            {
                cmd.Connection.Open();
            }
            try
            {
                
                
                int returnValue = cmd.ExecuteNonQuery();
                
                return returnValue;
            }
            finally
            {
                if ((previousConnectionState == System.Data.ConnectionState.Closed))
                {
                    cmd.Connection.Close();
                }
            }
        }
    }
}