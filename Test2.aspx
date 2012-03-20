<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Test2.aspx.cs" Inherits="Test2" %>

<%@ Register Assembly="KMobile.Web" Namespace="KMobile.Web.UI.WebControls" TagPrefix="asp" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <asp:SimpleDateTimePicker id="DateTimePicker1" runat="server" AutoFormat="Colorful"></asp:SimpleDateTimePicker>
    <asp:LinkButton ID="Hej" runat=server />
    </div>
    </form>
</body>
</html>
