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

public partial class Secure_Maintainance : System.Web.UI.Page
{
    Rehab.BookingsDataTable bookings;
    Rehab.BookingsRow booking;
    
    int nrofBookingsArchived;

    protected void Page_Load(object sender, EventArgs e)
    {
        
        if (!IsPostBack)
        {
            DateTime oldDate = DateTime.Now.AddDays(-7);
            TextBoxDate.Text = oldDate.ToShortDateString();
          
        }

        LabelErrorInfo.Visible = false;


        
    }

    protected void Page_PreRender(object sender, EventArgs e)
    {

        DataView dv = (DataView)ObjectDataSourceArchive.Select();
        bookings = (Rehab.BookingsDataTable)dv.Table;
        int nrofOldBookings = bookings.Count;


        dv = (DataView)ObjectDataSourceAllBookings.Select();
        bookings = (Rehab.BookingsDataTable)dv.Table;
        int nrofBookings = bookings.Count;

        LabelNrofBookings.Text = "Det finns totalt " + nrofBookings + " bokningar i den primära tabellen, varav " + nrofOldBookings + " är äldre än " + TextBoxDate.Text + ".";

    }

    protected void ButtonArchive_Click(object sender, EventArgs e)
    {
        DataView dv = (DataView) ObjectDataSourceArchive.Select();
        bookings = (Rehab.BookingsDataTable)dv.Table;

        foreach (DataRow dr in bookings)
        {
            booking = (Rehab.BookingsRow)dr;

            if (booking.startdatetime.Year.ToString() != DropDownListYeartoArchive.SelectedValue)
            {
                LabelErrorInfo.Text = "En eller flera bokningar tillhör ett annat år än det som valdes att arkivera till. De/dessa bokningar arkiverades inte.";
                LabelErrorInfo.Visible = true;
            }
            else
            {
                

                ObjectDataSourceArchive.Insert();

                ObjectDataSourceArchive.Delete();
            }
        }

        LabelResult.Text = nrofBookingsArchived + " bokningar arkiverades!";
    }
    protected void ObjectDataSourceArchive_Inserting(object sender, ObjectDataSourceMethodEventArgs e)
    {
        e.InputParameters["bookingid"] = booking.bookingid;
        e.InputParameters["patientid"] = booking.patientid;
        e.InputParameters["title"] = booking.title;
        e.InputParameters["startdatetime"] = booking.startdatetime;
        e.InputParameters["enddatetime"] = booking.enddatetime;
        e.InputParameters["note"] = booking.note;
        e.InputParameters["bookingtypeid"] = booking.bookingtypeid;
        e.InputParameters["notshown"] = booking.notshown;
        e.InputParameters["arrived"] = booking.arrived;
        e.InputParameters["cancelled"] = booking.cancelled;
        e.InputParameters["cancellednote"] = booking.cancellednote;
        e.InputParameters["createdatetime"] = booking.createdatetime;
        e.InputParameters["tableName"] = "Bookings2008";
    }

    protected void ObjectDataSourceArchive_Deleting(object sender, ObjectDataSourceMethodEventArgs e)
    {
        e.InputParameters["bookingid"] = booking.bookingid;
    }
    protected void ObjectDataSourceArchive_Inserted(object sender, ObjectDataSourceStatusEventArgs e)
    {
        if (e.Exception == null)
        {
            if (e.AffectedRows == -1)
                nrofBookingsArchived++;
        }

    }
}
