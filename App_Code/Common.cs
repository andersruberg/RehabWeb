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
/// Summary description for Common
/// </summary>
public static class Common
{
	
    public static void SetupRepeater(Repeater repeater)
    {

        DataTable dt = new DataTable();
        dt.Columns.Add("Letter", typeof(string));

        string[] Letters = { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K",
                     "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V",
                     "W", "X", "Y", "Z", "Å", "Ä", "Ö", "[ Visa alla ]"};

        for (int i = 0; i < Letters.Length; i++)
        {
            DataRow dr = dt.NewRow();
            dr[0] = Letters[i];
            dt.Rows.Add(dr);
        }

        repeater.DataSource = dt.DefaultView;
        repeater.DataBind();
    }

    
    public class CenteredDateTime
    {
        //private DateTime currentDate;

        //private StateBag ViewState;
        private System.Web.SessionState.HttpSessionState ViewState;

        public DateTime CurrentDate
        {
            get
            {
                DateTime date = (DateTime)ViewState["CurrentDate"];
                if (date != null)
                {
                    if (date == DateTime.MinValue)
                        return DateTime.Now;
                    else
                        return date;
                }
                else
                    return DateTime.Now;
            }

            set
            {
                ViewState["CurrentDate"] = value;
                

            }
        }

        //public CenteredDateTime(System.Web.UI.StateBag pageViewState, DateTime date)
        public CenteredDateTime(System.Web.SessionState.HttpSessionState pageViewState, DateTime date)
        {
            ViewState = pageViewState;
            CurrentDate = date;
        }

        public void SetNextDay()
        {
            CurrentDate = CurrentDate.AddDays(1.0);
        }

        public void SetPreviousDay()
        {
            CurrentDate = CurrentDate.AddDays(-1.0);
        }

        public void SetNextWeek()
        {
            CurrentDate = CurrentDate.AddDays(7.0);
        }

        public void SetPreviousWeek()
        {
            CurrentDate = CurrentDate.AddDays(-7.0);
        }
        
        public DateTime GetCurrentStartDate()
        {
            return GetWeekStartDate(CurrentDate);
            
        }

        public DateTime GetNextWeekStartDate()
        {
            return GetWeekStartDate(CurrentDate.AddDays(7.0));

        }

        public DateTime GetPreviousWeekStartDate()
        {
            return GetWeekStartDate(CurrentDate.AddDays(-7.0));

        }

        public int GetNextWeek()
        {
            return GetWeekNumber(CurrentDate.AddDays(7.0));
        }

        public int GetPreviousWeek()
        {
            return GetWeekNumber(CurrentDate.AddDays(-7.0));
        }

        public int GetCurrentWeek()
        {
            return GetWeekNumber(CurrentDate);
        }

        public string GetPreviousMonth()
        {
            return (CurrentDate.AddMonths(-1)).ToString("MMMM");
        }

        public string GetNextMonth()
        {
            return (CurrentDate.AddMonths(1)).ToString("MMMM");
        }

        public void SetNextMonth()
        {
              CurrentDate = CurrentDate.AddMonths(1);
        }

        public void SetPreviousMonth()
        {
            CurrentDate = CurrentDate.AddMonths(-1);
        }

        private DateTime GetWeekStartDate(DateTime dateTime)
        {
            switch (dateTime.DayOfWeek)
            {
                case DayOfWeek.Monday: return dateTime;
                case DayOfWeek.Tuesday: return dateTime.AddDays(-1.0);
                case DayOfWeek.Wednesday: return dateTime.AddDays(-2.0);
                case DayOfWeek.Thursday: return dateTime.AddDays(-3.0);
                case DayOfWeek.Friday: return dateTime.AddDays(-4.0);
                case DayOfWeek.Saturday: return dateTime.AddDays(-5.0);
                case DayOfWeek.Sunday: return dateTime.AddDays(-6.0);
                default: return dateTime;
            }

        }

    }

    public static int GetWeekNumber(DateTime date)
    {
        // Updated 2004.09.27. Cleaned the code and fixed a bug. Compared the algorithm with
        // code published here . Tested code successfully against the other algorithm 
        // for all dates in all years between 1900 and 2100.
        // Thanks to Marcus Dahlberg for pointing out the deficient logic.
        // Calculates the ISO 8601 Week Number
        // In this scenario the first day of the week is monday, 
        // and the week rule states that:
        // [...] the first calendar week of a year is the one 
        // that includes the first Thursday of that year and 
        // [...] the last calendar week of a calendar year is 
        // the week immediately preceding the first 
        // calendar week of the next year.
        // The first week of the year may thus start in the 
        // preceding year

        const int JAN = 1;
        const int DEC = 12;
        const int LASTDAYOFDEC = 31;
        const int FIRSTDAYOFJAN = 1;
        const int THURSDAY = 4;
        bool ThursdayFlag = false;

        // Get the day number since the beginning of the year
        int DayOfYear = date.DayOfYear;

        // Get the numeric weekday of the first day of the 
        // year (using sunday as FirstDay)
        int StartWeekDayOfYear =
             (int)(new DateTime(date.Year, JAN, FIRSTDAYOFJAN)).DayOfWeek;
        int EndWeekDayOfYear =
             (int)(new DateTime(date.Year, DEC, LASTDAYOFDEC)).DayOfWeek;

        // Compensate for the fact that we are using monday
        // as the first day of the week
        if (StartWeekDayOfYear == 0)
            StartWeekDayOfYear = 7;
        if (EndWeekDayOfYear == 0)
            EndWeekDayOfYear = 7;

        // Calculate the number of days in the first and last week
        int DaysInFirstWeek = 8 - (StartWeekDayOfYear);
        int DaysInLastWeek = 8 - (EndWeekDayOfYear);

        // If the year either starts or ends on a thursday it will have a 53rd week
        if (StartWeekDayOfYear == THURSDAY || EndWeekDayOfYear == THURSDAY)
            ThursdayFlag = true;

        // We begin by calculating the number of FULL weeks between the start of the year and
        // our date. The number is rounded up, so the smallest possible value is 0.
        int FullWeeks = (int)Math.Ceiling((DayOfYear - (DaysInFirstWeek)) / 7.0);

        int WeekNumber = FullWeeks;

        // If the first week of the year has at least four days, then the actual week number for our date
        // can be incremented by one.
        if (DaysInFirstWeek >= THURSDAY)
            WeekNumber = WeekNumber + 1;

        // If week number is larger than week 52 (and the year doesn't either start or end on a thursday)
        // then the correct week number is 1. 
        if (WeekNumber > 52 && !ThursdayFlag)
            WeekNumber = 1;

        // If week number is still 0, it means that we are trying to evaluate the week number for a
        // week that belongs in the previous year (since that week has 3 days or less in our date's year).
        // We therefore make a recursive call using the last day of the previous year.
        if (WeekNumber == 0)
            WeekNumber = GetWeekNumber(
                 new DateTime(date.Year - 1, DEC, LASTDAYOFDEC));
        return WeekNumber;
    }



    public static void ExceptionHandling(Exception e, Label labelError)
    {
        if (e != null)
        {
            labelError.Visible = true;
            labelError.Text = "Det har uppstått ett problem. \n";

            if (e.InnerException != null)
            {
                Exception innerException = e.InnerException;

                if (innerException is System.Data.Common.DbException)
                    labelError.Text += "Databasen fungerar för närvarande inte som den ska.";
                else if (innerException is System.ArgumentException)
                {
                    string paramName = ((ArgumentException)innerException).ParamName;
                    labelError.Text += string.Concat("Värdet ", paramName, " är inte giltigt. Var god kontrollera inmatningen.");
                }
                else if (innerException is NoNullAllowedException)
                    labelError.Text += "Det saknas en uppgift. Var god kontrollera inmatningen.";
                else if (innerException is ApplicationException)
                    labelError.Text += innerException.Message;
                

            }
            else
                labelError.Text += e.Message;

            
        }

    }

    public static void CheckPersonnumber(string aPersonnumber)
    {
        string shortPersonnumber;

        if (aPersonnumber.Length == 11)
        {
            //Just remove the - sign
            shortPersonnumber = aPersonnumber.Remove(8, 1);
        }
        else if (aPersonnumber.Length == 13)
        {
            //Remove the - sign and the "sekel"
            shortPersonnumber = (aPersonnumber.Remove(8, 1)).Remove(0, 2);
        }
        else
            throw new ApplicationException("Personnumret har ett felaktigt format.");

        

        //Check that the date is a valid date
        //Here we assume that the "sekel" is either 1900 or 2000
        try
        {
            string date = "19" + shortPersonnumber.Substring(0, 2) + "-" + shortPersonnumber.Substring(2, 2) + "-" + shortPersonnumber.Substring(4, 2);
            System.DateTime dt = System.DateTime.Parse(date);
        }
        catch (FormatException exception1)
        {
            //The date was not correct, try with the 2100 century
            try
            {
                string date = "19" + shortPersonnumber.Substring(0, 2) + "-" + shortPersonnumber.Substring(2, 2) + "-" + shortPersonnumber.Substring(4, 2);
                System.DateTime dt = System.DateTime.Parse(date);
            }
            catch (FormatException exception2)
            {
                //The 2100 century was not correct either
                throw new ApplicationException("Personnumret innehåller ett datum som ej är giltigt.");
            }
        }


        int checkNumber = int.Parse(shortPersonnumber[9].ToString());

        string personnumberNoCheck = shortPersonnumber.Substring(0, 9);
        

        int[] result = new int[personnumberNoCheck.Length];
        for (int i = 0; i < personnumberNoCheck.Length; i++)
        {
            try
            {
                result[i] = int.Parse(personnumberNoCheck[i].ToString());
                if ((i % 2) == 0)
                    result[i] *= 2;
            }
            catch (Exception e)
            {
                throw new ApplicationException("Kunde ej parsa personnumret. \n" + e.Message);
            }
        }

        /*int[] result = numbers;
        for (int i = 0; i < result.Length; i+=2)
        {
            result[i] *=2;
        }*/

        int sum = 0;
        for (int i = 0; i < result.Length; i++)
        {
            int l = (int)(result[i] / 10);
            int r = result[i] % 10;

            int numberSum = l + r;
            sum += numberSum;
        }
        

        int lastNumber = sum % 10;
        //Do % 10 because if the lastNumber equals 0 then checksum is 0
        int calculatedCheckNumber = (10 - lastNumber) % 10;

        

        if (calculatedCheckNumber != checkNumber)
        {
            throw new ApplicationException("Personnumrets kontrollsiffra stämmer ej.");
        }

    }

    public class CollidingBookingException : ApplicationException
    {
        private DateTime _originalStartdatetime;
        private DateTime _originalEnddatetime;
        private DateTime _proposedStartdatetime;
        private DateTime _proposedEnddatetime;
        
        public CollidingBookingException() : base("")
        {
            _originalStartdatetime = new DateTime((long)0);
            _originalEnddatetime = new DateTime((long)0);
            _proposedStartdatetime = new DateTime((long)0);
            _proposedEnddatetime = new DateTime((long)0);
        }

        public CollidingBookingException(DateTime originalStartdatetime, DateTime originalEnddatetime, DateTime proposedStartdatetime, DateTime proposedEnddatetime) : base("")
        {
            _originalStartdatetime = originalStartdatetime;
            _originalEnddatetime = originalEnddatetime;
            _proposedStartdatetime = proposedStartdatetime;
            _proposedEnddatetime = proposedEnddatetime;
            
        }

        public CollidingBookingException(string message) : base(message)
        {
            _originalStartdatetime = new DateTime((long)0);
            _originalEnddatetime = new DateTime((long)0);
            _proposedStartdatetime = new DateTime((long)0);
            _proposedEnddatetime = new DateTime((long)0);
        }

        public CollidingBookingException(string message, DateTime originalStartdatetime, DateTime originalEnddatetime, DateTime proposedStartdatetime, DateTime proposedEnddatetime) : base(message)
        {
            _originalStartdatetime = originalStartdatetime;
            _originalEnddatetime = originalEnddatetime;
            _proposedStartdatetime = proposedStartdatetime;
            _proposedEnddatetime = proposedEnddatetime;
        }

        public DateTime OrignalStartdatetime
        {
            get { return _originalStartdatetime; }
            set { _originalStartdatetime = value; }
        }

        public DateTime OriginalEnddatetime
        {
            get { return _originalEnddatetime; }
            set { _originalEnddatetime = value; }
        }

        public DateTime ProposedEnddatetime
        {
            get { return _proposedEnddatetime; }
            set { _proposedEnddatetime = value; }
        }
        public DateTime ProposedStartdatetime
        {
            get { return _proposedStartdatetime; }
            set { _proposedStartdatetime = value; }
        }
       

    }


}
