<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Maintainance.aspx.cs" Inherits="Secure_Maintainance" Title="Underhåll - Doris Ruberg REHAB AB" %>



<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:ObjectDataSource ID="ObjectDataSourceArchive" SelectCountMethod="CountBookings" runat="server" DeleteMethod="DeleteBooking" InsertMethod="CopyBooking" OldValuesParameterFormatString="original_{0}" SelectMethod="GetBookingsByOldDate" TypeName="BookingsBLL" OnDeleting="ObjectDataSourceArchive_Deleting" OnInserting="ObjectDataSourceArchive_Inserting" OnInserted="ObjectDataSourceArchive_Inserted">
    <DeleteParameters>
        <asp:Parameter Name="bookingid" Type="Int32" />
    </DeleteParameters>  
        <SelectParameters>
            <asp:ControlParameter ControlID="TextBoxDate" Name="date" PropertyName="Text" Type="DateTime" />
        </SelectParameters>
        <InsertParameters>
            <asp:Parameter Name="bookingid" Type="Int32" />
            <asp:Parameter Name="patientid" Type="Int32" />
            <asp:Parameter Name="title" Type="String" />
            <asp:Parameter Name="createdatetime" Type="DateTime" />
            <asp:Parameter Name="startdatetime" Type="DateTime" />
            <asp:Parameter Name="enddatetime" Type="DateTime" />
            <asp:Parameter Name="note" Type="String" />
            <asp:Parameter Name="bookingtypeid" Type="Int32" />
            <asp:Parameter Name="arrived" Type="Boolean" />
            <asp:Parameter Name="notshown" Type="Boolean" />
            <asp:Parameter Name="cancellednote" Type="String" />
            <asp:Parameter Name="cancelled" Type="Boolean" />
            <asp:Parameter Name="tableName" Type="String" />
        </InsertParameters>
    </asp:ObjectDataSource>
    
    <asp:ObjectDataSource ID="ObjectDataSourceAllBookings" runat="server" SelectMethod="GetBookings" TypeName="BookingsBLL">
    </asp:ObjectDataSource>
    <br />
    <asp:Label ID="LabelNrofBookings" runat=server></asp:Label>
    <table>
    <tr>
    <td>1.</td>
    <td>Välj datum att arkivera från</td>
    </tr>
    <tr>
    <td></td>
    <td><asp:TextBox ID="TextBoxDate" Columns="10" MaxLength="10" runat="server"></asp:TextBox></td>
    </tr>
    
    <tr>
    <td>2.</td>
    <td>Välj år att arkivera till</td>
    </tr>
    <tr>
    <td></td>
    <td><asp:DropDownList ID=DropDownListYeartoArchive runat=server>
    <asp:ListItem Selected=True Value="2008">2008</asp:ListItem>
    <asp:ListItem Value="2009">2009</asp:ListItem>
    <asp:ListItem Value="2010">2010</asp:ListItem>
    <asp:ListItem Value="2011">2011</asp:ListItem>
    </asp:DropDownList></td>
    </tr>
    </table>
    <asp:Button ID="ButtonArchive"
        runat="server" Text="Arkivera" OnClick="ButtonArchive_Click" Enabled=false />
        <br />
        <asp:Label ID="LabelResult" runat=server></asp:Label>
        <br />
        <asp:Label ID="LabelErrorInfo"  Visible=false CssClass="warning" runat=server></asp:Label>
</asp:Content>

