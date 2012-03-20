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
/// Summary description for DailyinfoBLL
/// </summary>
[System.ComponentModel.DataObject]
public class DailyinfoBLL
{
    private DailyinfoTableAdapter _dailyinfoTableAdapter = null;

    protected DailyinfoTableAdapter Adapter
    {
        get
        {
            if (_dailyinfoTableAdapter == null)
                return new DailyinfoTableAdapter();
            else
                return _dailyinfoTableAdapter;
        }
    }

    [System.ComponentModel.DataObjectMethodAttribute
    (System.ComponentModel.DataObjectMethodType.Select, true)]
    public Rehab.DailyinfoDataTable GetAllDailyinfo()
    {
        return Adapter.GetAllDailyinfo();
    }

    [System.ComponentModel.DataObjectMethodAttribute
    (System.ComponentModel.DataObjectMethodType.Select, false)]
    public Rehab.DailyinfoDataTable GetDailyinfoByDate(DateTime date)
    {
        return Adapter.GetDailyinfoByDate(date);
    }

    [System.ComponentModel.DataObjectMethodAttribute
    (System.ComponentModel.DataObjectMethodType.Insert, true)]
    public bool AddDailyinfo(DateTime date, int dailymessageid)
    {
        DeleteDailyinfo(date);

        Rehab.DailyinfoDataTable dailyinfoTable = new Rehab.DailyinfoDataTable();
        Rehab.DailyinfoRow dailyinfoRow = dailyinfoTable.NewDailyinfoRow();

        dailyinfoRow.date = date;
        dailyinfoRow.dailymessageid = dailymessageid;

        dailyinfoTable.AddDailyinfoRow(dailyinfoRow);
        int affectedRows = Adapter.Update(dailyinfoTable);
        return affectedRows == 1;
    }

    [System.ComponentModel.DataObjectMethodAttribute
    (System.ComponentModel.DataObjectMethodType.Delete, true)]
    public bool DeleteDailyinfo(DateTime date)
    {
        Rehab.DailyinfoDataTable existingDailyinfoTable = GetDailyinfoByDate(date);
        int affectedRows = 0;
        foreach (System.Data.DataRow row in existingDailyinfoTable.Rows)
        {
            Rehab.DailyinfoRow dailyInfoRow = (Rehab.DailyinfoRow)row;
            affectedRows = Adapter.Delete(dailyInfoRow.dailyinfoid);
        }
        
        return affectedRows == 1;
    }



}
