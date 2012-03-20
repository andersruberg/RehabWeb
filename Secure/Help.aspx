<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Help.aspx.cs" Inherits="Secure_Help" Title="Hj�lp - Doris Ruberg REHAB AB" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<span style="font-size:16pt">Lathund f�r tidbokning och patientregister</span>
<table>
<tr>
<td colspan="2" class=tableHelpHeader>Tidbokning</td>
</tr>
<tr>
<td class=smallHeader>Boka en ny tid:</td>
<td>Klicka p� tiden (hel eller halv timme) i kalendern. I det nya f�nster som �ppnas �r det m�jligt att v�lja patient och �ven l�gga till bl.a. en kommentar f�r bokningen.  </td>
</tr>
<tr>
<td></td>
<td>
<table>
<tr>
<td colspan="2">Kortkommandon:</td>
</tr>
<tr>
<td>v</td><td>�ppna f�nstret f�r att v�lja patient.</td>
</tr>
<tr>
<td>b</td><td>Boka</td>
</tr>
<tr>
<td>ESC</td><td>St�ng f�nstret och �terg� till tidbokingskalendern.</td>
</tr>
</table>
</td>
</tr>
<tr>
<td class=smallHeader>Visa mer information om en bokning:</td>
<td>Klicka p� bokningen. Ett f�nster med detaljerad information om bokningen �ppnas. H�r g�r det att markera �terbud, �ndra tid och datum.</td>
</tr>
<tr>
<td class=smallHeader>�terbud:</td>
<td>Om en patient vill l�mna �terbud til en bokning, klicka p� bokningen f�r att visa bokningsinformationen. Klicka sedan p� "Patienten har l�mnat �terbud" och fyll i en orsak till �terbud. Klicka d�refter p� "Forts�tt" och f�nstret f�r "Ny bokning �ppnas". Bokningen �r markerad som ett �terbud och det �r m�jligt att direkt boka in en ny tid f�r patienten. Om en ny tid inte �r aktuell, klicka p� "Avbryt" f�r att komma tillbaka till tidbokningskalendern.</td>
</tr>
<tr>
<td></td>
<td>
<table>
<tr>
<td colspan="2" class=smallHeader>Kortkommandon f�r tidbokningskalendern:</td>
</tr>
<tr>
<td>t</td><td>G� till tidbokningskalendern. Om tidbokningskalendern redan �r �ppen s� v�xlas mellan dag- och veckovy.</td>
</tr>
<tr>
<td>p</td><td>G� till patientregistret.</td>
</tr>
<tr>
<td>d</td><td>Visa minikalendern f�r att v�lja ett datum att g� till. (Endast dagvy.)</td>
</tr>
<tr>
<td>pil h�ger</td><td>G� till n�sta dag. (Endast dagvy.)</td>
</tr>
<tr>
<td>pil v�nster</td><td>G� till f�reg�ende dag. (Endast dagvy.)</td>
</tr>
<tr>
<td>pil upp</td><td>Bl�ddra en vecka fram�t.</td>
</tr>
<tr>
<td>pil ned</td><td>Bl�ddra en vecka bak�t.</td>
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
<td class=smallHeader>S�ka efter en patient:</td>
<td>Skriv en del av eller hela efternamnet i s�krutan och tryck Enter. Alla matchande patienter visa. Observera att det g�r att ange en del av efternamet och en del av f�rnamnet f�r att begr�nsa s�kningen. </td>
</tr>
<tr>
<td></td>
<td>
<table>
<tr>
<td>Exempel 1:</td><td>"andersson" visar alla patienter som heter Andersson i efternamn.</td>
</tr>
<tr>
<td>Exempel 2:</td><td>"andersson p" visar alla patienter som heter Andersson i efternamn och har ett f�rnamn som b�rjar p� P.</td>
</tr>
<tr>
<td>Exempel 3:</td><td>"and p" visar alla patienter som har ett efternamn som b�rjar p� And och ett f�rnamn som b�rjar p� P.</td>
</tr>
<tr>
<td>Exempel 4:</td><td>"a p" visar alla patienter som har ett efternamn som b�rjar p� A och ett f�rnamn som b�rjar p� P.</td>
</tr>
<tr>
<td>Exempel 5:</td><td>"p" visar alla patienter som har ett efternamn som b�rjar p� P.</td>
</tr>
</table>
</td>
</tr>
<tr>
<td class=smallHeader>L�gga till en ny patient</td>
<td>Klicka p� "Ny patient...". Ett nytt f�nster �ppnas d�r det �r m�jligt att ange efternamn, f�rnamn, personnummer (dessa uppgifter �r obligatorsika), adress, telefonnummer och frikortsdatum.</td>
</tr>
<tr>
<td></td>
<td>
<table>
<tr>
<td colspan="2">Kortkommandon:</td>
</tr>
<tr>
<td>l</td><td>L�gg till patienten</td>
</tr>
<tr>
<td>ESC</td><td>St�ng f�nstret</td>
</tr>
</table>
</td>
</tr>
<tr>
<td class=smallHeader>Visa detaljerad patientinformation</td>
<td>Klicka p� "Ny patient...". Ett nytt f�nster �ppnas d�r det �r m�jligt att ange efternamn, f�rnamn, personnummer (dessa uppgifter �r obligatorsika), adress, telefonnummer och frikortsdatum.</td>
</tr>

</table>
</asp:Content>

