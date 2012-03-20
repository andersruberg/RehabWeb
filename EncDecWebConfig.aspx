<%@ Page Language="C#" AutoEventWireup="True" CodeFile="EncDecWebConfig.aspx.cs" Inherits="management_EncDecWebConfig" validateRequest="false" Title="Encrypting Configuration Information" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title/>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <h2>Encrypting Configuration Information</h2>
            <b>Contents of Web.config:</b>
            <asp:Button ID="btnRefreshWebConfig" runat="server" Text="Refresh" OnClick="btnRefreshWebConfig_Click" />
        <asp:TextBox ID="webConfig" TextMode="MultiLine" Width="97%" Rows="20" runat="server" ReadOnly="True"></asp:TextBox>

        <table>
            <tr style=" padding: 5px">
                <td style="width: 280px; padding: 5px 5px 5px 5px;"><asp:Button ID="btnEncrypt" runat="server" Text="Encrypt Connection Strings" OnClick="btnEncrypt_Click" /></td>
                <td style="padding: 5px 5px 5px 5px; width: 275px;"><asp:Button ID="btnDescrypt" runat="server" Text="Decrypt Connection Strings" OnClick="btnDescrypt_Click" /></td>
            </tr>
            <tr >
                <td style="width: 280px; padding: 5px 5px 5px 5px;"><asp:Button ID="btnEncAuthentication" runat="server" OnClick="btnEncAuthentication_Click" Text="Encrypt Authentication Section" /></td>
                <td style="padding: 5px 5px 5px 5px;width: 275px;"><asp:Button ID="btnDecAuthentication" runat="server" OnClick="btnDecAuthentication_Click" Text="Decrypt Authentication Section" /></td>
            </tr>
            <tr >
                <td style="width: 280px; padding: 5px 5px 5px 5px;"><asp:Button ID="btnEncAppSettings" runat="server" OnClick="btnEncAppSettings_Click" Text="Encrypt AppSettings Section" /></td>
                <td style="padding: 5px 5px 5px 5px;width: 275px;"><asp:Button ID="btnDecAppSettings" runat="server" OnClick="btnDecAppSettings_Click" Text="Decrypt AppSettings Section" /></td>
            </tr>
            <tr>
                <td colspan="2" valign="middle"><hr /></td>
            </tr>
            <tr >
                <td style="width: 280px; padding: 5px 5px 5px 5px;"><asp:Button ID="btnEncAll" runat="server" OnClick="btnEncAll_Click" Text="Encrypt All Above Sections" /></td>
                <td style="padding: 5px 5px 5px 5px;width: 275px;"><asp:Button ID="btnDecAll" runat="server" OnClick="btnDecAll_Click" Text="Decrypt All Above Sections" /></td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
