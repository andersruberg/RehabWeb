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
/// Summary description for PatientsBLL
/// </summary>

[System.ComponentModel.DataObject]
public class PatientsBLL
{
    private PatientsTableAdapter _patientsTableAdapter = null;

    protected PatientsTableAdapter Adapter
	{
        get
        {
            if (_patientsTableAdapter == null)
                return new PatientsTableAdapter();
            else
                return _patientsTableAdapter;
        }
	}

    [System.ComponentModel.DataObjectMethodAttribute
    (System.ComponentModel.DataObjectMethodType.Select, true)]
    public Rehab.PatientsDataTable GetPatients()
    {
        return Adapter.GetPatients();
    }

    [System.ComponentModel.DataObjectMethodAttribute
    (System.ComponentModel.DataObjectMethodType.Select, false)]
    public Rehab.PatientsDataTable GetPatientsByName(string surname, string firstname)
    {
        if (String.IsNullOrEmpty(surname) && string.IsNullOrEmpty(firstname))
            return Adapter.GetPatients();
        else if (!String.IsNullOrEmpty(firstname))
            return Adapter.GetDataByName(surname, firstname);
        else
            return Adapter.GetDataBySurname(surname);
    }

    [System.ComponentModel.DataObjectMethodAttribute
    (System.ComponentModel.DataObjectMethodType.Select, false)]
    public Rehab.PatientsDataTable GetPatientsByID(int patientid)
    {
        return Adapter.GetDataByPatientID(patientid);
    }

    


    [System.ComponentModel.DataObjectMethodAttribute
    (System.ComponentModel.DataObjectMethodType.Select, false)]
    public Rehab.PatientsDataTable GetPatientsByPersonnumber(string personnumber)
    {
        return Adapter.GetDataByPersonnumber(personnumber);
    }

    [System.ComponentModel.DataObjectMethodAttribute
    (System.ComponentModel.DataObjectMethodType.Select, false)]
    public Rehab.PatientsDataTable GetPatientsBySurname(string surname)
    {
        if (String.IsNullOrEmpty(surname))
            return Adapter.GetPatients();
        else
            return Adapter.GetDataBySurname(surname);
    }

    [System.ComponentModel.DataObjectMethodAttribute
    (System.ComponentModel.DataObjectMethodType.Insert, true)]
    public int AddPatient(string surname, string firstname, string personnumber,
        string street, string zipcode, string city, string homephone,
        string workphone, string mobilephone, string info, string freecarddate)
    {
        Rehab.PatientsDataTable patients = new Rehab.PatientsDataTable();
        Rehab.PatientsRow patient = patients.NewPatientsRow();

        if ((surname == string.Empty) || (firstname == string.Empty) || (personnumber == string.Empty))
            throw new ApplicationException("Eternamn, förnamn och personnummer måste anges för att patienten ska kunna läggas till i patientregistret");

        patient.surname = surname;
        patient.firstname = firstname;

        IsPatientAlreadyExisting(personnumber, -1);

        //This will not work in hosted environment
        /*System.Configuration.Configuration rootWebConfig = System.Web.Configuration.WebConfigurationManager.OpenWebConfiguration(null);
        if (0 < rootWebConfig.AppSettings.Settings.Count)
        {
            System.Configuration.KeyValueConfigurationElement validatePersonnumber =
                rootWebConfig.AppSettings.Settings["validatePersonnumber"];
            if (null != validatePersonnumber)
            {
                if (validatePersonnumber.Value == true.ToString())
                    Common.CheckPersonnumber(personnumber);
            }
        }
        else
            Common.CheckPersonnumber(personnumber);*/

        Common.CheckPersonnumber(personnumber);

        patient.personnumber = personnumber;
        if (street == null)
            patient.SetstreetNull();
        else
            patient.street = street;
        if (zipcode == null)
            patient.SetzipcodeNull();
        else
            patient.zipcode = zipcode;
        if (city == null)
            patient.SetcityNull();
        else
            patient.city = city;
        if (homephone == null)
            patient.SethomephoneNull();
        else
            patient.homephone = homephone;
        if (workphone == null)
            patient.SetworkphoneNull();
        else
            patient.workphone = workphone;
        if (mobilephone == null)
            patient.SetmobilephoneNull();
        else
            patient.mobilephone = mobilephone;
        if (freecarddate == null)
            patient.SetfreecarddateNull();
        else
            patient.freecarddate = freecarddate;

        //patients.AddPatientsRow(patient);
        //int id = Adapter.Update(patients);

        

        int id = Convert.ToInt32(Adapter.InsertPatient(patient.surname, patient.firstname, patient.street, patient.zipcode, patient.city, patient.homephone, patient.workphone, patient.mobilephone, patient.personnumber, patient.info, patient.freecarddate));


        return id;
    }

    [System.ComponentModel.DataObjectMethodAttribute
    (System.ComponentModel.DataObjectMethodType.Update, true)]
    public int UpdatePatient(int patientid, string surname, string firstname, string personnumber,
        string street, string zipcode, string city, string homephone,
        string workphone, string mobilephone, string info, string freecarddate)
    {
        Rehab.PatientsDataTable patients = Adapter.GetDataByPatientID(patientid);
        if (patients.Count == 0)
            throw new ApplicationException("Internt fel vid uppdatering av patientinformation. En patient med det givna id:et finns ej i patientregistret");

        Rehab.PatientsRow patient = patients[0];

        if ((surname == string.Empty) || (firstname == string.Empty) || (personnumber == string.Empty))
            throw new ApplicationException("Internt fel vid uppdatering av patientinformation. Förnamn, efternamn och personnummer måste anges för att patienten ska kunna finnas i patientregistret");

        patient.surname = surname;
        patient.firstname = firstname;

        IsPatientAlreadyExisting(personnumber, patientid);

        /*System.Configuration.Configuration rootWebConfig = System.Web.Configuration.WebConfigurationManager.OpenWebConfiguration(null);
        if (0 < rootWebConfig.AppSettings.Settings.Count)
        {
            System.Configuration.KeyValueConfigurationElement validatePersonnumber =
                rootWebConfig.AppSettings.Settings["validatePersonnumber"];
            if (null != validatePersonnumber)
            {
                if (validatePersonnumber.Value == true.ToString())
                    Common.CheckPersonnumber(personnumber);
            }
        }
        else
            Common.CheckPersonnumber(personnumber);*/

        Common.CheckPersonnumber(personnumber);

        patient.personnumber = personnumber;

        if (street == null)
            patient.SetstreetNull();
        else
            patient.street = street;
        if (zipcode == null)
            patient.SetzipcodeNull();
        else
            patient.zipcode = zipcode;
        if (city == null)
            patient.SetcityNull();
        else
            patient.city = city;
        if (homephone == null)
            patient.SethomephoneNull();
        else
            patient.homephone = homephone;
        if (workphone == null)
            patient.SetworkphoneNull();
        else
            patient.workphone = workphone;
        if (mobilephone == null)
            patient.SetmobilephoneNull();
        else
            patient.mobilephone = mobilephone;
        if (freecarddate == null)
            patient.SetfreecarddateNull();
        else
            patient.freecarddate = freecarddate;

        //int affectedRows = Adapter.Update(patient.surname, patient.firstname, patient.street, patient.zipcode, patient.city, patient.homephone, patient.workphone, patient.mobilephone, patient.personnumber, patient.info, patient.freecarddate, patientid);
        int affectedRows = Adapter.Update(patients);

        
        return patientid;
    }

    [System.ComponentModel.DataObjectMethodAttribute
    (System.ComponentModel.DataObjectMethodType.Delete, true)]
    public bool DeletePatient(int patientid)
    {
        BookingsTableAdapter bookingsTableAdapter = new BookingsTableAdapter();
        Rehab.BookingsDataTable bookings = bookingsTableAdapter.GetBookingsByPatientID(patientid);

        int nrofBookings = bookings.Count;
        if (nrofBookings > 0)
            throw new ApplicationException(string.Format("Patienten kan inte tas bort. Det finns {0} bokningar för patienten som måste tas bort först", nrofBookings));

        int affectedRows = Adapter.Delete(patientid);

        return affectedRows == 1;
    }

    private void IsPatientAlreadyExisting(string personnumber, int patientid)
    {
        PatientsTableAdapter patientsTableAdapter = new PatientsTableAdapter();
        Rehab.PatientsDataTable patients = patientsTableAdapter.GetDataByPersonnumber(personnumber);

        int nrofPatients = patients.Count;
        if (nrofPatients > 0)
        {
            if (patientid == -1)
                throw new ApplicationException(string.Format("Patienten kan inte läggas till. Det finns redan en patient, {0} {1}, med personnummer {2} i patientregistret.", patients[0].surname, patients[0].firstname, personnumber));
            else if (patientid != patients[0].patientid)
                throw new ApplicationException(string.Format("Patienten kan inte uppdateras. Det finns redan en patient, {0} {1}, med personnummer {2} i patientregistret.", patients[0].surname, patients[0].firstname, personnumber));
        }
    }

}
