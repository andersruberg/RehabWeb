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
/// Summary description for Patients
/// </summary>
[Serializable]public class Patient
{
    public Patient()
    {
        freecardDate = "";
        firstname = "";
        surname = "";
        personnumber = "";
        street = "";
        zipcode = "";
        city = "";
        homePhone = "";
        workPhone = "";
        mobilePhone = "";
        info = "";
    }

    public Patient(Rehab.PatientsRow patientsRow)
    {
        patientId = patientsRow.patientid;
        surname = patientsRow.surname;
        firstname = patientsRow.firstname;
        personnumber = patientsRow.personnumber;
        street = patientsRow.street;
        zipcode = patientsRow.zipcode;
        city = patientsRow.city;
        homePhone = patientsRow.homephone;
        mobilePhone = patientsRow.mobilephone;
        workPhone = patientsRow.workphone;
        freecardDate = patientsRow.freecarddate;
        info = patientsRow.info;
    }

    

    #region Properties

    private string firstname, surname, personnumber, street, zipcode, city, homePhone, workPhone,
        mobilePhone, info, freecardDate;
    private int patientId;


    public string FreecardDate
    {
        get { return freecardDate; }
        set { freecardDate = value; }
    }

    public string Firstname
    {
        get { return firstname; }
        set { firstname = value; }
    }

    public string Surname
    {
        get { return surname; }
        set { surname = value; }
    }

    public string Street
    {
        get { return street; }
        set { street = value; }
    }

    public string Zipcode
    {
        get { return zipcode; }
        set { zipcode = value; }
    }

    public string City
    {
        get { return city; }
        set { city = value; }
    }

    public string WorkPhone
    {
        get { return workPhone; }
        set { workPhone = value; }
    }

    public string HomePhone
    {
        get { return homePhone; }
        set { homePhone = value; }
    }

    public string MobilePhone
    {
        get { return mobilePhone; }
        set { mobilePhone = value; }
    }

    public string Info
    {
        get { return info; }
        set { info = value; }
    }

    public string Personnumber
    {
        get { return personnumber; }
        set { personnumber = value; }
    }

    public int PatientId
    {
        get { return patientId; }
        set { patientId = value; }
    }
    #endregion
}
