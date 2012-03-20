<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Test.aspx.cs" Inherits="Secure_Test" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
    <link href="../StyleSheet.css" rel="stylesheet" type="text/css" />
</head>

   


<body>
    <form id="form1" runat="server">
    
        <asp:Label runat=server ID="protectionInfoLabel" Text="ConnectionStrings är ej krypterad"></asp:Label>        
        <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Style="left: -203px;
            position: relative; top: 40px" Text="Kryptera" />
    </form>
</body>
</html>
