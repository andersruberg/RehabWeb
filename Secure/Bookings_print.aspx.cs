using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

public partial class _Default : System.Web.UI.Page 
{
    
    private int _totalNrofBookings;
    private int _totalNrofNotShown;
    private int _totalNrofCancelled;

    public System.Collections.Hashtable bookingtypesHashTable;
    
    protected void Page_Load(object sender, EventArgs e)
    {

        SetupJavaScript();
        
        if (!Page.IsPostBack)
        {
            string dateString = "";
            try
            {
                string tmpString = (string)Request.QueryString["Date"];
                dateString = (DateTime.Parse(tmpString)).ToString(@"dddden \den d MMMM yyyy");
            }
            catch (Exception exception)
            {
                //Could not get the date from the query string..
                dateString = "[datum saknas]";
            }

            bookingtypesHashTable = new System.Collections.Hashtable();

            DateLabel.Text = "Tidbokning " + dateString;
            GridView1.DataBind();

            PrintButton.Attributes.Add("onclick", "javascript:PrintWindow();return false;");
            
            //Response.Write("<script language=JavaScript>PrintWindow();</script>");

            
        }
        
    }

    protected void SetupJavaScript()
    {
        string scriptString = "";

        scriptString += "<script language=JavaScript> ";
        
        scriptString += " function PrintWindow(){";
        scriptString += " window.print(); }";
        
        
       
        //scriptString += " function CheckWindowState() {";
        //scriptString += " if(document.readyState=='complete') {";
        //scriptString += " window.close();  }";
        //scriptString += " else {";
        //scriptString += " setTimeout('CheckWindowState()', 2000); }";
        //scriptString += " } ";
        scriptString += " </script>";
        RegisterStartupScript("PrintScript", scriptString);
}


    protected void ObjectDataSource1_Selecting(object sender, ObjectDataSourceSelectingEventArgs e)
    {
            e.InputParameters["date"] = Page.Request.QueryString["date"];
        
    }

    protected void CloseButton_Click(object sender, EventArgs e)
    {
        Response.Write("<script>window.opener='x';window.close();</script>");
    }
    
    protected void GridView1_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row == null)
            return;

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            
            Rehab.BookingsRow booking = (Rehab.BookingsRow)((System.Data.DataRowView)e.Row.DataItem).Row;

            MyWebControls.NullLabel label = (MyWebControls.NullLabel)GridView1.FindControl("lblPhone");

            if (label != null)
            {
                if (!String.IsNullOrEmpty(booking.mobilephone))
                    label.Text = booking.mobilephone;
                else if (!String.IsNullOrEmpty(booking.homephone))
                    label.Text = booking.homephone;
                else if (!String.IsNullOrEmpty(booking.workphone))
                    label.Text = booking.workphone;
                else
                    label.Text = "";
            }

            if (!bookingtypesHashTable.Contains(booking.bookingtype))
                bookingtypesHashTable.Add(booking.bookingtype, 1);
            else
            {
                int value = (int)bookingtypesHashTable[booking.bookingtype];
                value ++;
                bookingtypesHashTable[booking.bookingtype] = value;
            }

            _totalNrofBookings++;

            if (booking.notshown)
                _totalNrofNotShown++;

            if (booking.cancelled)
                _totalNrofCancelled++;
        }
        if (e.Row.RowType == DataControlRowType.Footer)
        {
            /*int cols = e.Row.Cells.Count;
            e.Row.Cells.Clear();
            e.Row.Cells.Add(new TableCell());
            e.Row.Cells[0].Style.Add("colspan", cols.ToString() );
            
            e.Row.Cells[0].Text = "Totalt " + _totalNrofBookings + " bokingar (varav " + _totalNrofNotShown + " uteblivna besök).";
             * */

            NrofBookingsLabel.Text = String.Format("Totalt {0} bokningar (varav {1} uteblivna besök och {2} återbud)", _totalNrofBookings, _totalNrofNotShown, _totalNrofCancelled);

            //This is to show the different bookingtypes.
            //BookingtypesGridView.DataSource = bookingtypesHashTable;
            //BookingtypesGridView.DataBind();
        }
    }
    
}

