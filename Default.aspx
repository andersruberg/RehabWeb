<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>V�lkommen till Doris Ruberg Rehab</title>
</head>
<body onload=body_onload()>
    <form id="form1" runat="server">
    <div align=center>
    
    <span style="font-size: 18pt; font-family: Arial">DORIS RUBERG REHAB AB</span><br /><br />
        <asp:Login ID="Login1" runat="server" BackColor="#EFF3FB" BorderColor="#B5C7DE" BorderPadding="4"
            BorderStyle="Solid" BorderWidth="1px" DestinationPageUrl="~/Secure/Bookings_pilot.aspx" DisplayRememberMe="False" FailureText="Inloggningen misslyckades. Kontrollera anv�ndarnamn och l�senord."
            Font-Names="Verdana" Font-Size="0.8em" ForeColor="#333333" Height="214px" Style="left: -4px;
            position: relative; top: -4px" Width="399px" LoginButtonText="Logga in" PasswordLabelText="L�senord:" TitleText="V�lkommen till det interna tidbokningssystemet" UserNameLabelText="Anv�ndarnamn:" PasswordRequiredErrorMessage="L�senord kr�vs" UserNameRequiredErrorMessage="Anv�ndarnamn kr�vs">
            <TitleTextStyle BackColor="#507CD1" Font-Bold="True" Font-Size="0.9em" ForeColor="White" />
            <InstructionTextStyle Font-Italic="True" ForeColor="Black" />
            <TextBoxStyle Font-Size="0.8em" />
            <LoginButtonStyle BackColor="White" BorderColor="#507CD1" BorderStyle="Solid" BorderWidth="1px"
                Font-Names="Verdana" Font-Size="0.8em" ForeColor="#284E98" />
            <LayoutTemplate>
                <table border="0" cellpadding="4" cellspacing="0" style="border-collapse: collapse">
                    <tr>
                        <td>
                            <table border="0" cellpadding="0" style="width: 399px; height: 300px">
                                <tr>
                                    <td align="center" colspan="2" style="font-weight: bold; font-size: 0.9em; color: white;
                                        background-color: #507cd1">
                                        V�lkommen till det interna tidbokningssystemet</td>
                                </tr>
                                <tr>
                                    <td align="right">
                                        <asp:Label ID="UserNameLabel" runat="server" AssociatedControlID="UserName">Anv�ndarnamn:</asp:Label></td>
                                    <td>
                                        <asp:TextBox ID="UserName" runat="server" Font-Size="0.8em"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="UserNameRequired" runat="server" ControlToValidate="UserName"
                                            ErrorMessage="Anv�ndarnamn kr�vs" ToolTip="Anv�ndarnamn kr�vs" ValidationGroup="Login1">*</asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right">
                                        <asp:Label ID="PasswordLabel" runat="server" AssociatedControlID="Password">L�senord:</asp:Label></td>
                                    <td>
                                        <asp:TextBox ID="Password" runat="server" Font-Size="0.8em" TextMode="Password"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="PasswordRequired" runat="server" ControlToValidate="Password"
                                            ErrorMessage="L�senord kr�vs" ToolTip="L�senord kr�vs" ValidationGroup="Login1">*</asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="center" colspan="2" style="color: red">
                                        <asp:Literal ID="FailureText" runat="server" EnableViewState="False"></asp:Literal>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right" colspan="2">
                                        <asp:Button ID="LoginButton" runat="server" BackColor="White" BorderColor="#507CD1"
                                            BorderStyle="Solid" BorderWidth="1px" CommandName="Login" Font-Names="Verdana"
                                            Font-Size="0.8em" ForeColor="#284E98" Text="Logga in" ValidationGroup="Login1" />
                                    </td>
                                </tr>
                                <tr>
                                <td colspan="2">
                                <asp:Label ID="JavaScriptEnabledLabel" runat="server" Style="position: relative"></asp:Label>
                                <br />
                                <asp:Label ID="CookiesEnabledLabel" runat="server" Style="position: relative"></asp:Label>
                                </td>
                                </tr>
                                <tr>
                                <td colspan="2">
                                <hr />
                                <span style="font-size:8pt; font-family:Arial; text-align:left">Observera att det h�r tidbokningssystemet endast �r f�r internt bruk. Om du �r <span style="font-weight:bold">PATIENT</span> och vill boka tid hos leg. sjukgymnast Doris Ruberg, var v�nlig ring 070-6617957. Tack!</span>
                                </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </LayoutTemplate>
        </asp:Login>
        
        
        
        </div>
    </form>
</body>
</html>
