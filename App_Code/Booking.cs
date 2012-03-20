using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.OleDb;

/// <summary>
/// Summary description for Bookings
/// </summary>
[Serializable]
public class Booking
{
    public Booking()
    {
        title = "";
        bookingtype = "";
        note = "";
        mobilephone = "";
        workphone = "";
        homephone = "";
        startdatetime = new System.DateTime(((long)(0)));
        enddatetime = new System.DateTime(((long)(0)));
    }

    public Booking(Rehab.BookingsRow booking)
    {
        patientid = booking.patientid;
        bookingid = booking.bookingid;
        
        title = booking.title;
        startdatetime = booking.startdatetime;
        enddatetime = booking.enddatetime;

        arrived = booking.arrived;
        notshown = booking.notshown;
        cancelled = booking.cancelled;
        
        note = booking.note;
        cancellednote = booking.cancellednote;

        mobilephone = booking.mobilephone;
        homephone = booking.homephone;
        workphone = booking.workphone;

        freecarddate = booking.freecarddate;
        personnumber = booking.personnumber;
        bookingtype = booking.bookingtype;
    }

    #region Properties

    private string title, bookingtype, note, mobilephone, homephone, workphone, personnumber, cancellednote, freecarddate;
    private DateTime startdatetime, enddatetime;
    private int bookingid, patientid;
    private bool arrived, notshown, cancelled;

    public enum Bookingtypes {Behandling=1, Akupunktur, Träning, Lunch, Ledig, Semester, Kurs};

    public string Title
    {
        get { return title; }
        set { title = value; }
    }

    public string Bookingtype
    {
        get { return bookingtype; }
        set { bookingtype = value; }
    }

    public string Note
    {
        get { return note; }
        set { note = value; }
    }

    public DateTime Startdatetime
    {
        get { return startdatetime; }
        set {startdatetime = value;}
    }

    public DateTime Enddatetime
    {
        get { return enddatetime; }
        set { enddatetime = value; }
    }

    public int Bookingid
    {
        get { return bookingid; }
        set { bookingid = value; }
    }

    public int Patientid
    {
        get { return patientid; }
        set { patientid = value; }
    }

    public string Cancellednote
    {
        get { return cancellednote; }
        set { cancellednote = value; }
    }

    public bool Arrived
    {
        get { return arrived; }
        set { arrived = value; }
    }

    public bool Notshown
    {
        get { return notshown; }
        set { notshown = value; }
    }

    public bool Cancelled
    {
        get { return cancelled; }
        set { cancelled = value; }
    }

    public string Mobilephone
    {
        get { return mobilephone; }
        set { mobilephone = value; }
    }

    public string Homephone
    {
        get { return homephone; }
        set { homephone = value; }
    }

    public string Workphone
    {
        get { return workphone; }
        set { workphone = value; }
    }

    public string Personnumber
    {
        get { return personnumber; }
        set { personnumber = value; }
    }

    public string Freecarddate
    {
        get { return freecarddate; }
        set { freecarddate = value; }
    }

    #endregion

    
}
