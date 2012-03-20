<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Help.aspx.cs" Inherits="Secure_Help" Title="Hjälp - Doris Ruberg REHAB AB" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<span style="font-size:16pt">Lathund för tidbokning och patientregister</span>
<table>
<tr>
<td colspan="2" class=tableHelpHeader>Tidbokning</td>
</tr>
<tr>
<td class=smallHeader>Boka en ny tid:</td>
<td>Klicka på tiden (hel eller halv timme) i kalendern. I det nya fönster som öppnas är det möjligt att välja patient och även lägga till bl.a. en kommentar för bokningen.  </td>
</tr>
<tr>
<td></td>
<td>
<table>
<tr>
<td colspan="2">Kortkommandon:</td>
</tr>
<tr>
<td>v</td><td>Öppna fönstret för att välja patient.</td>
</tr>
<tr>
<td>b</td><td>Boka</td>
</tr>
<tr>
<td>ESC</td><td>Stäng fönstret och återgå till tidbokingskalendern.</td>
</tr>
</table>
</td>
</tr>
<tr>
<td class=smallHeader>Visa mer information om en bokning:</td>
<td>Klicka på bokningen. Ett fönster med detaljerad information om bokningen öppnas. Här går det att markera återbud, ändra tid och datum.</td>
</tr>
<tr>
<td class=smallHeader>Återbud:</td>
<td>Om en patient vill lämna återbud til en bokning, klicka på bokningen för att visa bokningsinformationen. Klicka sedan på "Patienten har lämnat återbud" och fyll i en orsak till återbud. Klicka därefter på "Fortsätt" och fönstret för "Ny bokning öppnas". Bokningen är markerad som ett återbud och det är möjligt att direkt boka in en ny tid för patienten. Om en ny tid inte är aktuell, klicka på "Avbryt" för att komma tillbaka till tidbokningskalendern.</td>
</tr>
<tr>
<td></td>
<td>
<table>
<tr>
<td colspan="2" class=smallHeader>Kortkommandon för tidbokningskalendern:</td>
</tr>
<tr>
<td>t</td><td>Gå till tidbokningskalendern. Om tidbokningskalendern redan är öppen så växlas mellan dag- och veckovy.</td>
</tr>
<tr>
<td>p</td><td>Gå till patientregistret.</td>
</tr>
<tr>
<td>d</td><td>Visa minikalendern för att välja ett datum att gå till. (Endast dagvy.)</td>
</tr>
<tr>
<td>pil höger</td><td>Gå till nästa dag. (Endast dagvy.)</td>
</tr>
<tr>
<td>pil vänster</td><td>Gå till föregående dag. (Endast dagvy.)</td>
</tr>
<tr>
<td>pil upp</td><td>Bläddra en vecka framåt.</td>
</tr>
<tr>
<td>pil ned</td><td>Bläddra en vecka bakåt.</td>
</tr>
<tr>
<td>ESC</td><td>Logga ut.</td>
</tr>
</table>
</td>
</tr>
<tr>
<td colspan="2" class=tableHelpHeader>Patientregister</td>
</tr>
<tr>
<td class=smallHeader>Söka efter en patient:</td>
<td>Skriv en del av eller hela efternamnet i sökrutan och tryck Enter. Alla matchande patienter visa. Observera att det går att ange en del av efternamet och en del av förnamnet för att begränsa sökningen. </td>
</tr>
<tr>
<td></td>
<td>
<table>
<tr>
<td>Exempel 1:</td><td>"andersson" visar alla patienter som heter Andersson i efternamn.</td>
</tr>
<tr>
<td>Exempel 2:</td><td>"andersson p" visar alla patienter som heter Andersson i efternamn och har ett förnamn som börjar på P.</td>
</tr>
<tr>
<td>Exempel 3:</td><td>"and p" visar alla patienter som har ett efternamn som börjar på And och ett förnamn som börjar på P.</td>
</tr>
<tr>
<td>Exempel 4:</td><td>"a p" visar alla patienter som har ett efternamn som börjar på A och ett förnamn som börjar på P.</td>
</tr>
<tr>
<td>Exempel 5:</td><td>"p" visar alla patienter som har ett efternamn som börjar på P.</td>
</tr>
</table>
</td>
</tr>
<tr>
<td class=smallHeader>Lägga till en ny patient</td>
<td>Klicka på "Ny patient...". Ett nytt fönster öppnas där det är möjligt att ange efternamn, förnamn, personnummer (dessa uppgifter är obligatorsika), adress, telefonnummer och frikortsdatum.</td>
</tr>
<tr>
<td></td>
<td>
<table>
<tr>
<td colspan="2">Kortkommandon:</td>
</tr>
<tr>
<td>l</td><td>Lägg till patienten</td>
</tr>
<tr>
<td>ESC</td><td>Stäng fönstret</td>
</tr>
</table>
</td>
</tr>
<tr>
<td class=smallHeader>Visa detaljerad patientinformation</td>
<td>Klicka på "Ny patient...". Ett nytt fönster öppnas där det är möjligt att ange efternamn, förnamn, personnummer (dessa uppgifter är obligatorsika), adress, telefonnummer och frikortsdatum.</td>
</tr>

</table>
</asp:Content>

