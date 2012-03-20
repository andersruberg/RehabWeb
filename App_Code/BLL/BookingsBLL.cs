using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using RehabTableAdapters;

/// <summary>
/// Summary description for BookingsBLL
/// </summary>
[System.ComponentModel.DataObject]
public class BookingsBLL
{
    private BookingsTableAdapter _bookingsTableAdapter = null;

    protected BookingsTableAdapter Adapter
    {
        get
        {
            if (_bookingsTableAdapter == null)
                return new BookingsTableAdapter();
            else
                return _bookingsTableAdapter;
        }
    }

    [System.ComponentModel.DataObjectMethodAttribute
    (System.ComponentModel.DataObjectMethodType.Select, false)]
    public Rehab.BookingsDataTable GetBookings()
    {
        return Adapter.GetBookings();
    }

    [System.ComponentModel.DataObjectMethodAttribute
    (System.ComponentModel.DataObjectMethodType.Select, true)]
    public Rehab.BookingsDataTable GetBookingsByDate(System.DateTime date)
    {
        return Adapter.GetBookingsByDate(date.ToShortDateString());
    }

    [System.ComponentModel.DataObjectMethodAttribute
    (System.ComponentModel.DataObjectMethodType.Select, false)]
    public Rehab.BookingsDataTable GetBookingsByDates(System.DateTime startdate, System.DateTime enddate)
    {
        
        return Adapter.GetBookingsByDates(startdate.ToShortDateString(), enddate.ToShortDateString());
    }


    [System.ComponentModel.DataObjectMethodAttribute
    (System.ComponentModel.DataObjectMethodType.Select, false)]
    public Rehab.BookingsDataTable GetBookingsByBookingID(int? bookingID)
    {
        Rehab.BookingsDataTable bookings = new Rehab.BookingsDataTable();
        
        if (bookingID != null)
        {
            if (bookingID.Value > -1)
                bookings = Adapter.GetBookingsByBookingID(bookingID.Value);
                
        }
        
        return bookings;
    }

    [System.ComponentModel.DataObjectMethodAttribute
    (System.ComponentModel.DataObjectMethodType.Select, false)]
    public Rehab.BookingsDataTable GetBookingsByPatientID(int patientID)
    {
        return Adapter.GetBookingsByPatientID(patientID);
    }

    [System.ComponentModel.DataObjectMethodAttribute
    (System.ComponentModel.DataObjectMethodType.Select, false)]
    //Useful e.g. when getting all coming bookings for a certain patient.
    public Rehab.BookingsDataTable GetFutureBookingsByPatientID(int patientID)
    {
        return Adapter.GetFutureBookingsByPatientID(patientID, DateTime.Now.ToShortDateString());
    }

    [System.ComponentModel.DataObjectMethodAttribute
    (System.ComponentModel.DataObjectMethodType.Select, false)]
    public Rehab.BookingsDataTable GetBlockingBookingsByDate(DateTime date)
    {
        return Adapter.GetBlockingBookingsByDate(date);
    }

    [System.ComponentModel.DataObjectMethodAttribute
    (System.ComponentModel.DataObjectMethodType.Select, false)]
    public Rehab.BookingsDataTable GetBookingsByOldDate(DateTime date)
    {
        return Adapter.GetBookingsByOldDate(date.ToShortDateString());
    }

    [System.ComponentModel.DataObjectMethodAttribute
    (System.ComponentModel.DataObjectMethodType.Select, false)]
    public int? CountBookings()
    {
        return Adapter.CountBookings();
    }

    [System.ComponentModel.DataObjectMethodAttribute
    (System.ComponentModel.DataObjectMethodType.Select, false)]
    public int? CountBookingsOlderThanDate(DateTime date)
    {
        return Adapter.CountBookingsOlderThanDate(date.ToShortDateString());
    }

    [System.ComponentModel.DataObjectMethodAttribute
    (System.ComponentModel.DataObjectMethodType.Insert, false)]
    public bool CopyBooking(int bookingid, int patientid, string title, DateTime createdatetime, DateTime startdatetime, 
        DateTime enddatetime, string note, int bookingtypeid, bool arrived, bool notshown, string cancellednote, bool cancelled, string tableName)
    {
        
        int affectedRows = Adapter.CopyBooking(bookingid, patientid, createdatetime,  title, startdatetime, enddatetime, note, bookingtypeid, arrived, notshown, cancelled, cancellednote, tableName);
        
        return (affectedRows == 1);
    }

    [System.ComponentModel.DataObjectMethodAttribute
    (System.ComponentModel.DataObjectMethodType.Insert, true)]
    public bool AddBooking(int patientid, string title, DateTime startdatetime, 
        DateTime enddatetime, string note, int bookingtypeid)
    {
        Rehab.BookingsDataTable bookings = new Rehab.BookingsDataTable();
        Rehab.BookingsRow booking = bookings.NewBookingsRow();

        if ((startdatetime == null) || (enddatetime == null))
            throw new ApplicationException("Start-och sluttid för bokningen måste vara angivet.");

        //Do business logic validation here
        PatientsTableAdapter patientsTableAdapter = new PatientsTableAdapter();
        Rehab.PatientsDataTable patients = patientsTableAdapter.GetDataByPatientID(patientid);
        if (patients.Count == 0)
            throw new ApplicationException("Bokningen försöker göras med en patient som inte finns.");

        ValidateBookingDateTime(startdatetime, enddatetime);

        //Get other bookings this day and see if the interfere with the new booking
        Rehab.BookingsDataTable otherBookings = GetBookingsByDate(startdatetime);
        
        bool isBookingPossible = false;
        DateTime newStartdatetime = startdatetime;
        DateTime newEnddatetime = enddatetime;
        
        while ((!isBookingPossible) && (startdatetime < new DateTime(startdatetime.Year, startdatetime.Month, startdatetime.Day, 19, 0 , 0)))
        {
            isBookingPossible = CheckBlockingBookings(newStartdatetime, newEnddatetime, bookingtypeid, otherBookings);

            if (!isBookingPossible)
            {
                newStartdatetime =  newStartdatetime.AddMinutes((double)30);
                newEnddatetime = newEnddatetime.AddMinutes((double)30);
            }
        }

        if (isBookingPossible)
        {
            if (newStartdatetime != startdatetime)
                throw new Common.CollidingBookingException(startdatetime, enddatetime, newStartdatetime, newEnddatetime);
        }
        else
            throw new ApplicationException("Det finns ingen ledig tid idag från det angivna klockslaget.");
        
                    

        booking.startdatetime = startdatetime;
        booking.enddatetime = enddatetime;
        booking.patientid = patientid;
        booking.title = title;
        booking.bookingtypeid = bookingtypeid;
        
        booking.arrived = false;
        booking.notshown = false;

        booking.cancelled = false;
        booking.SetcancellednoteNull();

        if (note == null)
            booking.SetnoteNull();
        else
            booking.note = note;
        
        booking.createdatetime = DateTime.Now;

        bookings.AddBookingsRow(booking);
        int affectedRows = Adapter.Update(bookings);

        return affectedRows == 1;
            
    }


    [System.ComponentModel.DataObjectMethodAttribute
    (System.ComponentModel.DataObjectMethodType.Insert, false)]
    public bool AddBookingWithRestrictions(int patientid, string title, DateTime startdatetime,
        DateTime enddatetime, string note, int bookingtypeid)
    {
        Rehab.BookingsDataTable bookings = new Rehab.BookingsDataTable();
        Rehab.BookingsRow booking = bookings.NewBookingsRow();

        if ((startdatetime == null) || (enddatetime == null))
            return false;

        //Do business logic validation here
        PatientsTableAdapter patientsTableAdapter = new PatientsTableAdapter();
        Rehab.PatientsDataTable patients = patientsTableAdapter.GetDataByPatientID(patientid);
        if (patients.Count == 0)
            throw new ApplicationException("Bokningen försöker göras med en patient som inte finns");

        ValidateBookingDateTime(startdatetime, enddatetime);

        //Get other bookings this day and see if the interfere with the new booking
        Rehab.BookingsDataTable otherBookings = GetBookingsByDate(startdatetime);

        bool isBookingPossible = false;
        string message = "";
        DateTime newStartdatetime = startdatetime;
        DateTime newEnddatetime = enddatetime;

        while ((!isBookingPossible) && (newStartdatetime < new DateTime(startdatetime.Year, startdatetime.Month, startdatetime.Day, 19, 0, 0)))
        {
            
            isBookingPossible = CheckBlockingBookings(newStartdatetime, newEnddatetime, bookingtypeid, otherBookings);

            if (!isBookingPossible)
            {
                newStartdatetime = newStartdatetime.AddMinutes((double)30);
                newEnddatetime = newEnddatetime.AddMinutes((double)30);
            }
            else
            {
                try
                {
                    CheckUserRestrictions(newStartdatetime, newEnddatetime);
                }
                catch (ApplicationException applicationException)
                {
                    message = applicationException.Message;
                    //It wasn't possible to book after all
                    isBookingPossible = false;

                    newStartdatetime = newStartdatetime.AddMinutes((double)30);
                    newEnddatetime = newEnddatetime.AddMinutes((double)30);
                }
            }
        }

        if (isBookingPossible)
        {
            if (newStartdatetime != startdatetime)
                throw new Common.CollidingBookingException(message, startdatetime, enddatetime, newStartdatetime, newEnddatetime);
        }
        else
            throw new ApplicationException(message + " Det finns ingen ledig tid idag från det angivna klockslaget.");


        booking.startdatetime = startdatetime;
        booking.enddatetime = enddatetime;
        booking.patientid = patientid;
        booking.title = title;
        booking.bookingtypeid = bookingtypeid;

        booking.arrived = false;
        booking.notshown = false;

        booking.cancelled = false;
        booking.SetcancellednoteNull();


        if (note == null)
            booking.SetnoteNull();
        else
            booking.note = note;

        booking.createdatetime = DateTime.Now;

        bookings.AddBookingsRow(booking);
        int affectedRows = Adapter.Update(bookings);

        return affectedRows == 1;

    }

    [System.ComponentModel.DataObjectMethodAttribute
    (System.ComponentModel.DataObjectMethodType.Update, true)]
    public bool UpdateBooking(int bookingid, DateTime startdatetime,
        DateTime enddatetime, string note, int bookingtypeid, bool arrived, bool notshown, bool cancelled,
        string cancellednote)
    {
        Rehab.BookingsDataTable bookings = Adapter.GetBookingsByBookingID(bookingid);

        if (bookings.Count == 0)
            return false;

        Rehab.BookingsRow booking = bookings[0];

        ValidateBookingDateTime(startdatetime, enddatetime);

        //Get other bookings this day and see if the interfere with the new booking
        Rehab.BookingsDataTable otherBookings = GetBookingsByDate(startdatetime);

        bool isBookingPossible = false;
        DateTime newStartdatetime = startdatetime;
        DateTime newEnddatetime = enddatetime;

        while ((!isBookingPossible) && (startdatetime < new DateTime(startdatetime.Year, startdatetime.Month, startdatetime.Day, 19, 0, 0)))
        {
            isBookingPossible = CheckBlockingBookings(newStartdatetime, newEnddatetime, bookingtypeid, otherBookings);

            if (!isBookingPossible)
            {
                newStartdatetime = newStartdatetime.AddMinutes((double)30);
                newEnddatetime = newEnddatetime.AddMinutes((double)30);
            }
        }

        if (isBookingPossible)
        {
            if (newStartdatetime != startdatetime)
                throw new Common.CollidingBookingException(startdatetime, enddatetime, newStartdatetime, newEnddatetime);
        }
        else
            throw new ApplicationException("Det finns ingen ledig tid idag från det angivna klockslaget.");

        booking.startdatetime = startdatetime;
        booking.enddatetime = enddatetime;
        booking.bookingtypeid = bookingtypeid;
        booking.arrived = arrived;
        booking.notshown = notshown;
        booking.cancelled = cancelled;
        
        
        if (note == null)
            booking.SetnoteNull();
        else
            booking.note = note;

        if (cancellednote == null)
            booking.SetcancellednoteNull();
        else
            booking.cancellednote = note;


        int affectedRows = Adapter.Update(bookings);

        return affectedRows == 1;
    }

    [System.ComponentModel.DataObjectMethodAttribute
    (System.ComponentModel.DataObjectMethodType.Update, true)]
    public bool UpdateBookingWithRestrictions(int bookingid, DateTime startdatetime,
        DateTime enddatetime, string note, int bookingtypeid, bool arrived, bool notshown,
        bool cancelled, string cancellednote)
    {
        Rehab.BookingsDataTable bookings = Adapter.GetBookingsByBookingID(bookingid);

        if (bookings.Count == 0)
            return false;

        Rehab.BookingsRow booking = bookings[0];

        ValidateBookingDateTime(startdatetime, enddatetime);

        //Get other bookings this day and see if the interfere with the new booking
        Rehab.BookingsDataTable otherBookings = GetBookingsByDate(startdatetime);

        bool isBookingPossible = false;
        string message = "";
        DateTime newStartdatetime = startdatetime;
        DateTime newEnddatetime = enddatetime;

        while ((!isBookingPossible) && (newStartdatetime < new DateTime(startdatetime.Year, startdatetime.Month, startdatetime.Day, 19, 0, 0)))
        {

            isBookingPossible = CheckBlockingBookings(newStartdatetime, newEnddatetime, bookingtypeid, otherBookings);

            if (!isBookingPossible)
            {
                newStartdatetime = newStartdatetime.AddMinutes((double)30);
                newEnddatetime = newEnddatetime.AddMinutes((double)30);
            }
            else
            {
                try
                {
                    CheckUserRestrictions(newStartdatetime, newEnddatetime);
                }
                catch (ApplicationException applicationException)
                {
                    message = applicationException.Message;
                    //It wasn't possible to book after all
                    isBookingPossible = false;

                    newStartdatetime = newStartdatetime.AddMinutes((double)30);
                    newEnddatetime = newEnddatetime.AddMinutes((double)30);
                }
            }
        }

        booking.startdatetime = startdatetime;
        booking.enddatetime = enddatetime;
        booking.bookingtypeid = bookingtypeid;
        booking.arrived = arrived;
        booking.notshown = notshown;

        booking.cancelled = cancelled;

        if (note == null)
            booking.SetnoteNull();
        else
            booking.note = note;

        if (cancellednote == null)
            booking.SetcancellednoteNull();
        else
            booking.cancellednote = note;

        int affectedRows = Adapter.Update(bookings);

        return affectedRows == 1;
    }


    [System.ComponentModel.DataObjectMethodAttribute
   (System.ComponentModel.DataObjectMethodType.Update, false)]
    public bool SetBookingArrived(int bookingid, bool arrived)
    {
        Rehab.BookingsDataTable bookings = Adapter.GetBookingsByBookingID(bookingid);

        if (bookings.Count == 0)
            return false;

        Rehab.BookingsRow booking = bookings[0];

        if (booking.cancelled)
            throw new ApplicationException("Patienten har lämnat återbud till den här bokningen. Den kan inte sättas som ankommen.");


        //Only update the arrived field
        booking.arrived = arrived;

        int affectedRows = Adapter.Update(bookings);

        return affectedRows == 1;
    }

    [System.ComponentModel.DataObjectMethodAttribute
   (System.ComponentModel.DataObjectMethodType.Update, false)]
    public bool SetBookingNotShown(int bookingid, bool notshown)
    {
        Rehab.BookingsDataTable bookings = Adapter.GetBookingsByBookingID(bookingid);

        if (bookings.Count == 0)
            return false;

        Rehab.BookingsRow booking = bookings[0];

        if (booking.cancelled)
            throw new ApplicationException("Patienten har lämnat återbud till den här bokningen. Den kan inte sättas som utebliven.");


        //Only update the arrived field
        booking.notshown = notshown;

        int affectedRows = Adapter.Update(bookings);

        return affectedRows == 1;
    }

    [System.ComponentModel.DataObjectMethodAttribute
    (System.ComponentModel.DataObjectMethodType.Update, false)]
    public bool SetBookingCancelled(int bookingid, bool cancelled, string cancellednote)
    {
        Rehab.BookingsDataTable bookings = Adapter.GetBookingsByBookingID(bookingid);

        if (bookings.Count == 0)
            return false;

        Rehab.BookingsRow booking = bookings[0];

        //Only update the arrived field
        booking.cancelled = cancelled;
        if (!String.IsNullOrEmpty(cancellednote))
            booking.cancellednote = cancellednote;
        else
            return false;

        int affectedRows = Adapter.Update(bookings);

        return affectedRows == 1;
    }


    [System.ComponentModel.DataObjectMethodAttribute
    (System.ComponentModel.DataObjectMethodType.Delete, true)]
    public bool DeleteBooking(int bookingid)
    {
        int affectedRows = Adapter.Delete(bookingid);

        return affectedRows == 1;
    }

    private void ValidateBookingDateTime(DateTime startdatetime, DateTime enddatetime)
    {
        if (startdatetime > enddatetime)
            throw new ApplicationException("Starttiden för bokningen måste vara mindre än sluttiden");

        if (startdatetime.DayOfYear != enddatetime.DayOfYear)
            throw new ApplicationException("Start- och sluttiden måste infalla på samma dag");

        if ((startdatetime.DayOfWeek == DayOfWeek.Saturday) || (startdatetime.DayOfWeek == DayOfWeek.Sunday))
            throw new ApplicationException("Det går ej att boka lördag eller söndag");

        if (startdatetime.Year < 2007)
            throw new ApplicationException("Bokningen måste inträffa år 2007 eller senare");
    }

    private void CheckUserRestrictions(DateTime startdatetime, DateTime enddatetime)
    {
        //Restrictions for some users

        DateTime startlunch = new DateTime(startdatetime.Year, startdatetime.Month, startdatetime.Day, 12, 0, 0);
        DateTime endlunch = new DateTime(startdatetime.Year, startdatetime.Month, startdatetime.Day, 13, 0, 0);
        DateTime startday = new DateTime(startdatetime.Year, startdatetime.Month, startdatetime.Day, 8, 30, 0);
        DateTime endday = new DateTime(startdatetime.Year, startdatetime.Month, startdatetime.Day, 16, 0, 0);
        DateTime endfriday = new DateTime(startdatetime.Year, startdatetime.Month, startdatetime.Day, 16, 00, 0);

        if ((startdatetime >= startlunch) && (startdatetime < endlunch))
            throw new ApplicationException("Det går ej att boka mellan 12.00 och 13.00.");

        if ((enddatetime > startlunch) && (enddatetime <= endlunch))
            throw new ApplicationException("Det går ej att boka mellan 12.00 och 13.00.");

        if (startdatetime < startday)
            throw new ApplicationException("Det går ej att boka innan 8.30.");

        if (startdatetime > endday)
            throw new ApplicationException("Sista tiden att boka är 16.00.");

        //Not possible to book wednsday
        //if (startdatetime.DayOfWeek == DayOfWeek.Wednesday)
        //    throw new ApplicationException("Det går ej att boka onsdagar.");

        //Last time on Friday is 15.30
        if ((startdatetime > endfriday) && (startdatetime.DayOfWeek == DayOfWeek.Friday))
            throw new ApplicationException("Det går ej att boka kl. 16.00 och senare på en fredag.");

        if ((enddatetime > endfriday) && (enddatetime.DayOfWeek == DayOfWeek.Friday))
            throw new ApplicationException("Det går ej att boka kl. 16.00 och senare på en fredag.");

    }

    private bool CheckBlockingBookings(DateTime startdatetime, DateTime enddatetime, int bookingtypeid, Rehab.BookingsDataTable otherBookings)
    {
        //Find out if the new booking is a blocking booking
        bool isNewBookingBlocking = false;
        BookingtypeTableAdapter bookingtypeTableAdapter = new BookingtypeTableAdapter();
        int? blocking = bookingtypeTableAdapter.IsBookingtypeBlocking(bookingtypeid);
        if (blocking.HasValue)
        {
            if (blocking.Value == 1)
                isNewBookingBlocking = true;
            else if (blocking.Value == 0)
                isNewBookingBlocking = false;
            else
                throw new ApplicationException("Ett fel uppstod vid kontrollen om bokningen kolliderar med någon annan bokning på samma tidpunkt.");
        }

        
        foreach (DataRow dr in otherBookings)
        {
            
            bool isOtherBookingBlocking = false;
            
            Rehab.BookingsRow otherBooking = (Rehab.BookingsRow)dr;
            int? otherBookingBlocking = bookingtypeTableAdapter.IsBookingtypeBlocking(otherBooking.bookingtypeid);
            if (otherBookingBlocking.HasValue)
            {
                if (otherBookingBlocking.Value == 1)
                    isOtherBookingBlocking = true;
                else if (otherBookingBlocking.Value == 0)
                    isOtherBookingBlocking = false;
                else
                    throw new ApplicationException("Ett fel uppstod vid kontrollen om bokningen kolliderar med någon annan bokning på samma tidpunkt.");
            }



            if (IsBookingsColliding(startdatetime, enddatetime, otherBooking.startdatetime, otherBooking.enddatetime))
            {
                //If the new booking isn't blocking and the other booking isn't blocking either, then it's ok.
                //But if the new bocking is blocking the other booking isn't
                //or if the new booking is blocking and the other is too
                //or finally if the new booking isn't blocking but the other booking is
                //then we need to check so the start and end time don't collide
                if (!((isNewBookingBlocking == false) && (isOtherBookingBlocking == false)))
                {

                    return false;
                    //throw new ApplicationException("Det finns redan en bokning:\n" + otherBooking.title + " den " + otherBooking.startdatetime.ToShortDateString() + " mellan kl." + otherBooking.startdatetime.ToShortTimeString() + " och " + otherBooking.enddatetime.ToShortTimeString() + "\nVar vänlig avboka den tiden först");
                }

                if ((otherBooking.bookingtypeid == (int)Booking.Bookingtypes.Behandling) && (bookingtypeid == (int)Booking.Bookingtypes.Behandling))
                {
                    if (otherBooking.cancelled)
                        return true;
                    else
                        return false;
                    //throw new ApplicationException("Det finns redan en behandling inbokad:\n" + otherBooking.title + " den " + otherBooking.startdatetime.ToShortDateString() + " mellan kl." + otherBooking.startdatetime.ToShortTimeString() + " och " + otherBooking.enddatetime.ToShortTimeString() + "\nDet går ej att boka två behandlingar samtidigt.");
                }

                if ((otherBooking.bookingtypeid == (int)Booking.Bookingtypes.Akupunktur) && (bookingtypeid == (int)Booking.Bookingtypes.Akupunktur))
                {
                    if (otherBooking.cancelled)
                        return true;
                    else
                        return false;
                    //throw new ApplicationException("Det finns redan en akupunktur inbokad:\n" + otherBooking.title + " den " + otherBooking.startdatetime.ToShortDateString() + " mellan kl." + otherBooking.startdatetime.ToShortTimeString() + " och " + otherBooking.enddatetime.ToShortTimeString() + "\nDet går ej att boka två akupunkturer samtidigt.");
                }
            }



        }

        //There was no colliding booking.
        return true;
    }

    private bool IsBookingsColliding(DateTime startdatetime, DateTime enddatetime, DateTime otherBookingStartTime, DateTime otherBookingEndTime)
    {

        if ((startdatetime < otherBookingStartTime) && (enddatetime > otherBookingStartTime))
            return true;

        if ((startdatetime >= otherBookingStartTime) && (enddatetime <= otherBookingEndTime))
            return true;

        if ((startdatetime < otherBookingEndTime) && (enddatetime > otherBookingEndTime))
            return true;

        return false;
    }
}
