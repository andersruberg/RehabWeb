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
/// Summary description for BookingtypeBLL
/// </summary>
[System.ComponentModel.DataObject]
public class DailymessagesBLL
{
    private DailymessagesTableAdapter _dailymessagesTableAdapter = null;

    protected DailymessagesTableAdapter Adapter
    {
        get
        {
            if (_dailymessagesTableAdapter == null)
                return new DailymessagesTableAdapter();
            else
                return _dailymessagesTableAdapter;
        }
    }

    [System.ComponentModel.DataObjectMethodAttribute
    (System.ComponentModel.DataObjectMethodType.Select, true)]
    public Rehab.DailymessagesDataTable GetAllDailymessages()
    {
        return Adapter.GetAllDailymessages();
    }

    [System.ComponentModel.DataObjectMethodAttribute
    (System.ComponentModel.DataObjectMethodType.Insert, true)]
    public bool AddDailymessage(string message)
    {
        Rehab.DailymessagesDataTable dailymessagesTable = new Rehab.DailymessagesDataTable();
        Rehab.DailymessagesRow dailymessagesRow = dailymessagesTable.NewDailymessagesRow();


        dailymessagesRow.message = message;

        dailymessagesTable.AddDailymessagesRow(dailymessagesRow);
        int affectedRows = Adapter.Update(dailymessagesTable);
        return affectedRows == 1;
    }



}
