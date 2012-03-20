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
public class BookingtypeBLL
{
    private BookingtypeTableAdapter _bookingtypeTableAdapter = null;

    protected BookingtypeTableAdapter Adapter
    {
        get
        {
            if (_bookingtypeTableAdapter == null)
                return new BookingtypeTableAdapter();
            else
                return _bookingtypeTableAdapter;
        }
    }

    [System.ComponentModel.DataObjectMethodAttribute
    (System.ComponentModel.DataObjectMethodType.Select, false)]
    public Rehab.BookingtypeDataTable GetAllBookingtypes()
    {
        return Adapter.GetAllBookingtypes();
    }

    [System.ComponentModel.DataObjectMethodAttribute
    (System.ComponentModel.DataObjectMethodType.Select, false)]
    public int? IsBookingtypeBlocking(int bookingtypeid)
    {
        return Adapter.IsBookingtypeBlocking(bookingtypeid);
    }


    [System.ComponentModel.DataObjectMethodAttribute
    (System.ComponentModel.DataObjectMethodType.Select, true)]
    public Rehab.BookingtypeDataTable GetBookingtypesByAccess()
    {
        return Adapter.GetBookingtypesByAccess("all");
    }

}
