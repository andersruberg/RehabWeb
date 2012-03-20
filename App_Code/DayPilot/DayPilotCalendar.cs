/*
Copyright © 2005 - 2007 Annpoint, s.r.o.

Licensed under the Apache License, Version 2.0 (the "License");
you may not use this file except in compliance with the License.
You may obtain a copy of the License at

http://www.apache.org/licenses/LICENSE-2.0

Unless required by applicable law or agreed to in writing, software
distributed under the License is distributed on an "AS IS" BASIS,
WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
See the License for the specific language governing permissions and
limitations under the License.

-------------------------------------------------------------------------

NOTE: Reuse requires the following acknowledgement (see also NOTICE):
This product includes DayPilot (http://www.daypilot.org) developed by Annpoint, s.r.o.
*/

using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing; 
using System.Security.Permissions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DayPilot.Web.Ui.Design;

namespace DayPilot.Web.Ui
{
    /// <summary>
    /// DayPilot is a component for showing a day schedule.
    /// </summary>
    [ToolboxBitmap(typeof(Calendar))]
    [Designer(typeof(DayPilotCalendarDesigner))]
    [AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal), AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
    public partial class DayPilotCalendar : DataBoundControl, IPostBackEventHandler
    {
        internal Day[] days;

        private string dataStartField;
        private string dataEndField;
        private string dataTextField;
        private string dataValueField;

        private string noteField;
        private string personnumberField;
        private string bookingtypeField;
        private string mobilephoneField;
        private string freecarddateField;
        private string arrivedField;
        private string notshownField;

        // day header
        private bool showHeader = true;
        private int headerHeight = 21;
        private string headerDateFormat = "d";

        private ArrayList items;

        /// <summary>
        /// Event called when the user clicks an event in the calendar. It's only called when DoPostBackForEvent is true.
        /// </summary>
        public event EventClickDelegate EventClick;

        /// <summary>
        /// Event called when the user clicks a free space in the calendar. It's only called when DoPostBackForFreeTime is true.
        /// </summary>
        public event FreeClickDelegate FreeTimeClick;


        #region Viewstate

        /// <summary>
        /// Loads ViewState.
        /// </summary>
        /// <param name="savedState"></param>
        protected override void LoadViewState(object savedState)
        {
            if (savedState == null)
                return;

            object[] vs = (object[])savedState;

            if (vs.Length != 2)
                throw new ArgumentException("Wrong savedState object.");

            if (vs[0] != null)
                base.LoadViewState(vs[0]);

            if (vs[1] != null)
                items = (ArrayList)vs[1];

        }

        /// <summary>
        /// Saves ViewState.
        /// </summary>
        /// <returns></returns>
        protected override object SaveViewState()
        {
            object[] vs = new object[2];
            vs[0] = base.SaveViewState();
            vs[1] = items;

            return vs;
        }

        #endregion

        #region PostBack


        /// <summary>
        /// 
        /// </summary>
        /// <param name="eventArgument"></param>
        public void RaisePostBackEvent(string eventArgument)
        {

            if (eventArgument.StartsWith("PK:"))
            {
                string pk = eventArgument.Substring(3, eventArgument.Length - 3);
                if (EventClick != null)
                    EventClick(this, new EventClickEventArgs(pk));
            }
            else if (eventArgument.StartsWith("TIME:"))
            {
                DateTime time = Convert.ToDateTime(eventArgument.Substring(5, eventArgument.Length - 5));
                if (FreeTimeClick != null)
                    FreeTimeClick(this, new FreeClickEventArgs(time));
            }
            else
            {
                throw new ArgumentException("Bad argument passed from postback event.");
            }
        }

        #endregion

        #region Rendering

        /// <summary>
        /// Renders the component HTML code.
        /// </summary>
        /// <param name="output"></param>
        protected override void Render(HtmlTextWriter output)
        {
            loadEventsToDays();

            // <table>
            output.AddAttribute("id", ClientID);
            output.AddAttribute("cellpadding", "0");
            output.AddAttribute("cellspacing", "0");
            output.AddAttribute("border", "0");
            output.AddAttribute("width", Width.ToString());
            output.AddStyleAttribute("border-bottom", "1px solid " + ColorTranslator.ToHtml(BorderColor));
            output.AddStyleAttribute("text-align", "left");
            output.RenderBeginTag("table");

            // <tr>
            output.RenderBeginTag("tr");

            // <td>
            output.AddAttribute("valign", "top");
            output.RenderBeginTag("td");

            if (ShowHours)
                renderHourNamesTable(output);

            // </td>
            output.RenderEndTag();

            // <td>
            output.AddAttribute("width", "100%");
            output.AddAttribute("valign", "top");
            output.RenderBeginTag("td");

            renderEventsAndCells(output);

            // </td>
            output.RenderEndTag();
            // </tr>
            output.RenderEndTag();
            // </table>
            output.RenderEndTag();
        }


        private void renderHourNamesTable(HtmlTextWriter output)
        {
            //			output.WriteLine("<!-- hours table  -->");
            output.AddAttribute("cellpadding", "0");
            output.AddAttribute("cellspacing", "0");
            output.AddAttribute("border", "0");
            output.AddAttribute("width", this.HourWidth.ToString());

            output.AddStyleAttribute("border-left", "1px solid " + ColorTranslator.ToHtml(BorderColor));
            output.RenderBeginTag("table");

            // <tr> first emtpy
            output.AddStyleAttribute("height", "1px");
            output.AddStyleAttribute("background-color", ColorTranslator.ToHtml(BorderColor));
            output.RenderBeginTag("tr");

            output.RenderBeginTag("td");
            output.RenderEndTag();

            // </tr> first empty
            output.RenderEndTag();

            if (this.ShowHeader)
                renderHourHeader(output);

            for (DateTime i = visibleStart; i < visibleEnd; i = i.AddHours(1))
            {
                renderHourTr(output, i);
            }

            // </table>
            output.RenderEndTag();

        }


        private void renderHourTr(HtmlTextWriter output, DateTime i)
        {

            // <tr>
            output.AddStyleAttribute("height", HourHeight + "px");
            output.RenderBeginTag("tr");

            // <td>
            output.AddAttribute("valign", "bottom");
            output.AddStyleAttribute("background-color", ColorTranslator.ToHtml(HourNameBackColor));
            output.AddStyleAttribute("cursor", "default");
            output.RenderBeginTag("td");

            // <div> block
//            DivWriter divBlock = new DivWriter();
            output.AddStyleAttribute("display", "block");
            output.AddStyleAttribute("border-bottom", "1px solid " + ColorTranslator.ToHtml(HourNameBorderColor));
            output.AddStyleAttribute("height", (HourHeight - 1) + "px");
            output.AddStyleAttribute("text-align", "right");
            output.RenderBeginTag("div");
//            output.Write(divBlock.BeginTag());

            // <div> text
//            DivWriter divText = new DivWriter();
            output.AddStyleAttribute("padding-top", "4px");
            output.AddStyleAttribute("padding-right", "4px");
            output.AddStyleAttribute("padding-left", "2px");
            output.AddStyleAttribute("font-family", HourFontFamily);
            output.AddStyleAttribute("font-size", HourFontSize);
            output.RenderBeginTag("div");
//            output.Write(divText.BeginTag());

            int hour = i.Hour;
            bool am = (i.Hour / 12) == 0;
            if (TimeFormat == TimeFormat.Clock12Hours)
            {
                hour = i.Hour % 12;
                if (hour == 0)
                    hour = 12;
            }

            output.Write(hour);
            output.Write("<span style='font-size:10px; vertical-align: super; '>&nbsp;");
            if (TimeFormat == TimeFormat.Clock24Hours)
            {
                output.Write("00");
            }
            else
            {
                if (am)
                    output.Write("AM");
                else
                    output.Write("PM");
            }
            output.Write("</span>");
            
            output.RenderEndTag();
            
            output.AddStyleAttribute("padding-top", "12px");
            output.AddStyleAttribute("padding-right", "4px");
            output.AddStyleAttribute("padding-left", "2px");
            output.AddStyleAttribute("font-family", HourFontFamily);
            output.AddStyleAttribute("font-size", HourFontSize);
            output.AddStyleAttribute("vertical-align", "bottom");
            
            output.RenderBeginTag("div");
            

            output.Write(hour);
            

            output.Write("<span style='font-size:10px; vertical-align: super; '>&nbsp;");
            if (TimeFormat == TimeFormat.Clock24Hours)
            {
                output.Write("30");
            }
            else
            {
                if (am)
                    output.Write("AM");
                else
                    output.Write("PM");
            }
            output.Write("</span>");

            
            output.RenderEndTag();
            output.RenderEndTag();
//            output.Write(divText.EndTag());
//            output.Write(divBlock.EndTag());
            output.RenderEndTag(); // </td>
            output.RenderEndTag(); // </tr>
        }


        private void renderHourHeader(HtmlTextWriter output)
        {

            // <tr>
            output.AddStyleAttribute("height", (this.HeaderHeight) + "px");
            output.RenderBeginTag("tr");

            // <td>
            output.AddAttribute("valign", "bottom");
            output.AddStyleAttribute("background-color", ColorTranslator.ToHtml(HourNameBackColor));
            output.AddStyleAttribute("cursor", "default");
            output.RenderBeginTag("td");

            // <div> block
//            DivWriter divBlock = new DivWriter();
            output.AddStyleAttribute("display", "block");
            output.AddStyleAttribute("border-bottom", "1px solid " + ColorTranslator.ToHtml(BorderColor));
            output.AddStyleAttribute("text-align", "right");
            output.RenderBeginTag("div");
//            output.Write(divBlock.BeginTag());

            // <div> text
//            DivWriter divText = new DivWriter();
            output.AddStyleAttribute("padding", "2px");
            output.AddStyleAttribute("font-size", "14pt");
            output.RenderBeginTag("div");
//            output.Write(divText.BeginTag());

            output.Write("v. " + Common.GetWeekNumber(StartDate).ToString());

            output.RenderEndTag();
            output.RenderEndTag();
//            output.Write(divText.EndTag());
//            output.Write(divBlock.EndTag());
            output.RenderEndTag(); // </td>
            output.RenderEndTag(); // </tr>
        }

        private void renderEventsAndCells(HtmlTextWriter output)
        {
            //			output.WriteLine("<!-- cells table -->");

            if (days != null)
            {
                // <table>
                output.AddAttribute("cellpadding", "0");
                output.AddAttribute("cellspacing", "0");
                output.AddAttribute("border", "0");
                output.AddAttribute("width", "100%");
                output.AddStyleAttribute("border-left", "1px solid " + ColorTranslator.ToHtml(BorderColor));
                output.RenderBeginTag("table");

                // <tr> first
                output.AddStyleAttribute("height", "1px");
                output.AddStyleAttribute("background-color", ColorTranslator.ToHtml(BorderColor));
                output.RenderBeginTag("tr");

                renderEventTds(output);

                // </tr> first
                output.RenderEndTag();

                // header
                if (this.ShowHeader)
                {
                    renderDayHeaders(output);
                }
            }

            output.WriteLine("<!-- empty cells -->");

            // render all cells

            for (DateTime i = visibleStart; i < visibleEnd; i = i.AddHours(1))
            {

                // <tr> first half-hour
                output.RenderBeginTag("tr");

                addHalfHourCells(output, i, true, false);

                // </tr>
                output.RenderEndTag();

                // <tr> second half-hour
                output.AddStyleAttribute("height", (HourHeight / 2) + "px");
                output.RenderBeginTag("tr");

                bool isLastRow = (i == visibleEnd.AddHours(-1));
                addHalfHourCells(output, i, false, isLastRow);

                // </tr>
                output.RenderEndTag();
            }

            // </table>
            output.RenderEndTag();

        }

        private void renderDayHeaders(HtmlTextWriter output)
        {
            output.AddStyleAttribute("background-color", ColorTranslator.ToHtml(HourNameBackColor));
            output.AddStyleAttribute("height", this.HeaderHeight + "px");
            output.RenderBeginTag("tr");

            foreach (Day d in days)
            {
                DateTime h = new DateTime(d.date.Year, d.date.Month, d.date.Day, 0, 0, 0);

                // <td>
                output.AddAttribute("valign", "bottom");
                output.AddStyleAttribute("background-color", ColorTranslator.ToHtml(HourNameBackColor));
                output.AddStyleAttribute("cursor", "default");
                output.AddStyleAttribute("border-right", "1px solid " + ColorTranslator.ToHtml(BorderColor));
                output.RenderBeginTag("td");

                // <div> block
//                DivWriter divBlock = new DivWriter();
                output.AddStyleAttribute("display", "block");
                output.AddStyleAttribute("border-bottom", "1px solid " + ColorTranslator.ToHtml(BorderColor));
                output.AddStyleAttribute("text-align", "center");
                output.RenderBeginTag("div");
//                output.Write(divBlock.BeginTag());

                // <div> text
//                DivWriter divText = new DivWriter();
                output.AddStyleAttribute("padding", "2px");
                output.AddStyleAttribute("font-family", DayFontFamily);
                output.AddStyleAttribute("font-size", DayFontSize);
                output.RenderBeginTag("div");
//                output.Write(divText.BeginTag());

                output.Write(h.ToString(HeaderDateFormat));
                //				output.Write("&nbsp;");

                output.RenderEndTag();
                output.RenderEndTag();
//                output.Write(divText.EndTag());
//                output.Write(divBlock.EndTag());
                output.RenderEndTag(); // </td>


            }

            output.RenderEndTag();
        }



        private void renderEventTds(HtmlTextWriter output)
        {

            int dayPctWidth = 100 / days.Length;

            for (int i = 0; i < days.Length; i++)
            {
                Day d = days[i];


                // <td>
                output.AddStyleAttribute("height", "1px");
                output.AddStyleAttribute("text-align", "left");
                output.AddAttribute("width", dayPctWidth + "%");
                output.RenderBeginTag("td");

                // <div> position
//                DivWriter divPosition = new DivWriter();
                output.AddStyleAttribute("display", "block");
                output.AddStyleAttribute("margin-right", ColumnMarginRight + "px"); 
                output.AddStyleAttribute("position", "relative");
                output.AddStyleAttribute("height", "1px");
                output.AddStyleAttribute("font-size", "1px");
                output.AddStyleAttribute("margin-top", "-1px");
                output.RenderBeginTag("div");
//                output.Write(divPosition.BeginTag());

                foreach (Event e in d.Events)
                {
                    renderEvent(output, e, d);
                }

                // </div> position
//                output.Write(divPosition.EndTag());
                output.RenderEndTag();

                // </td>
                output.RenderEndTag();
            }
        }

        private void renderEvent(HtmlTextWriter output, Event e, Day d)
        {
            string displayTime = e.booking.Startdatetime.ToShortTimeString() + " - " + e.booking.Enddatetime.ToShortTimeString();
            string displayTitle = " " + e.booking.Title;
            string displayPersonnumber = " " + e.booking.Personnumber;
            string displayType = " (" + e.booking.Bookingtype + ")"; 
            string displayNote = " " + e.booking.Note ;
            
            string displayMobilephone = " ";
            if (!String.IsNullOrEmpty(e.booking.Mobilephone))
                displayMobilephone += "tel: " + e.booking.Mobilephone;
            else if (!String.IsNullOrEmpty(e.booking.Homephone))
                displayMobilephone += "tel: " + e.booking.Homephone;
            else if (!String.IsNullOrEmpty(e.booking.Workphone))
                displayMobilephone += "tel: " + e.booking.Workphone;
            else
                displayMobilephone += "tel: -";

            string displayFreecarddate = " F: " + e.booking.Freecarddate;

            Color eventLeftBarColor = EventLeftBarColor;
            Color eventBackColor = EventBackColor;
            Color eventFreecardExpiredColor = Color.Black;
            Color eventCancellednoteColor = Color.Red;

            try
            {
                DateTime freecarddate = DateTime.Parse(e.booking.Freecarddate);
                if ((freecarddate == DateTime.MinValue) || (freecarddate == DateTime.MaxValue))
                    throw new Exception("Frikortsdatumet existerar inte eller är inte giltigt");
                if ((freecarddate < e.booking.Startdatetime) && (!e.booking.Cancelled))
                {
                    eventLeftBarColor = EventFreecardExpiredBackColor;
                    eventFreecardExpiredColor = EventFreecardExpiredBackColor;
                }
            }
            catch (Exception exception)
            {
            }


            if (e.booking.Arrived)
                eventLeftBarColor = EventArrivedBackColor;
            if (e.booking.Notshown)
                eventLeftBarColor = EventNotShownBackColor;
            if (e.booking.Cancelled)
            {
                eventBackColor = Color.LightGray;
                eventLeftBarColor = Color.Black;
                displayNote = " Återbud:" + e.booking.Cancellednote;
            }
            
            
            // real box dimensions and position
            DateTime dayVisibleStart = new DateTime(d.date.Year, d.date.Month, d.date.Day, visibleStart.Hour, 0, 0);
            DateTime realBoxStart = e.BoxStart < dayVisibleStart ? dayVisibleStart : e.BoxStart;

            DateTime dayVisibleEnd;
            if (visibleEnd.Day == 1)
                dayVisibleEnd = new DateTime(d.date.Year, d.date.Month, d.date.Day, visibleEnd.Hour, 0, 0);
            else if (visibleEnd.Day == 2)
                dayVisibleEnd = new DateTime(d.date.Year, d.date.Month, d.date.Day, visibleEnd.Hour, 0, 0).AddDays(1);
            else
                throw new ArgumentOutOfRangeException("Unexpected time for dayVisibleEnd.");

            DateTime realBoxEnd = e.BoxEnd > dayVisibleEnd ? dayVisibleEnd : e.BoxEnd;

            // top
            double top = (realBoxStart - dayVisibleStart).TotalHours * HourHeight + 1;
            if (ShowHeader)
                top += this.HeaderHeight;

            // height
            double height = ((realBoxEnd - realBoxStart).TotalHours * HourHeight - 2);

            // It's outside of visible area (for NonBusinessHours set to Hide).
            // Don't draw it in that case.
            if (height <= 0)
            {
                return;
            }

            // MAIN BOX
            output.AddAttribute("onselectstart", "return false;"); // prevent text selection in IE

            if (EventClickHandling == UserActionHandling.PostBack)
            {
                output.AddAttribute("onclick", "javascript:event.cancelBubble=true;" + Page.ClientScript.GetPostBackEventReference(this, "PK:" + e.PK));
            }
            else
            {
                output.AddAttribute("onclick", "javascript:event.cancelBubble=true;" + String.Format(JavaScriptEventAction, e.PK));
            }

            //To get some more space to the right
            int newColumnWidth = e.Column.WidthPct - 1;

            output.AddStyleAttribute("-moz-user-select", "none"); // prevent text selection in FF
            output.AddStyleAttribute("-khtml-user-select", "none"); // prevent text selection
            output.AddStyleAttribute("user-select", "none"); // prevent text selection
            output.AddStyleAttribute("cursor", "pointer");
            //output.AddStyleAttribute("cursor", "hand");
            output.AddStyleAttribute("position", "absolute");
            output.AddStyleAttribute("font-family", EventFontFamily);
            output.AddStyleAttribute("font-size", EventFontSize);
            output.AddStyleAttribute("white-space", "no-wrap");
            output.AddStyleAttribute("left", e.Column.StartsAtPct + "%");
            output.AddStyleAttribute("top", top + "px");
            output.AddStyleAttribute("width", newColumnWidth + "%"); 
            output.AddStyleAttribute("height", (realBoxEnd - realBoxStart).TotalHours * HourHeight + "px");
            output.AddStyleAttribute("background-color", ColorTranslator.ToHtml(EventBorderColor));
            
            output.RenderBeginTag("div");
            //output.Write(divMain.BeginTag());

            // FIX BOX - to fix the outer/inner box differences in Mozilla/IE (to create border)
//            DivWriter divFix = new DivWriter();
            output.AddAttribute("onmouseover", "this.style.backgroundColor='" + ColorTranslator.ToHtml(EventHoverColor) + "';event.cancelBubble=true;");
            output.AddAttribute("onmouseout", "this.style.backgroundColor='" + ColorTranslator.ToHtml(eventBackColor) + "';event.cancelBubble=true;");

            if (ShowToolTip)
            {
                output.AddAttribute("title", displayTime + displayTitle + displayType +displayNote + "( " + displayMobilephone + displayFreecarddate + " )");
            }

            output.AddStyleAttribute("margin-top", "1px");
            output.AddStyleAttribute("display", "block");
            output.AddStyleAttribute("height", height + "px");
            output.AddStyleAttribute("background-color", ColorTranslator.ToHtml(eventBackColor));
            output.AddStyleAttribute("border-left", "1px solid " + ColorTranslator.ToHtml(EventBorderColor));
            output.AddStyleAttribute("border-right", "1px solid " + ColorTranslator.ToHtml(EventBorderColor));
            output.AddStyleAttribute("overflow", "hidden");
            output.RenderBeginTag("div");
//            output.Write(divFix.BeginTag());

            // blue column
            if (e.Start > realBoxStart)
            {
                
            }

            int startDelta = (int) Math.Floor((e.Start - realBoxStart).TotalHours * HourHeight);
            int endDelta = (int) Math.Floor((realBoxEnd - e.End).TotalHours * HourHeight);

//            DivWriter divBlue = new DivWriter();
            output.AddStyleAttribute("float", "left");
            output.AddStyleAttribute("width", "7px");
            output.AddStyleAttribute("height", height - startDelta - endDelta + "px");
            output.AddStyleAttribute("margin-top", startDelta + "px");
            output.AddStyleAttribute("background-color", ColorTranslator.ToHtml(eventLeftBarColor));
            output.AddStyleAttribute("font-size", "1px");
            output.RenderBeginTag("div");
            output.RenderEndTag();
//            output.Write(divBlue.BeginTag());
//            output.Write(divBlue.EndTag());

            // right border of blue column
//            DivWriter divBorder = new DivWriter();
            output.AddStyleAttribute("float", "left");
            output.AddStyleAttribute("width", "1px");
            output.AddStyleAttribute("background-color", ColorTranslator.ToHtml(EventBorderColor));
            output.AddStyleAttribute("height", "100%");
            output.RenderBeginTag("div");
            output.RenderEndTag();
//            output.Write(divBorder.BeginTag());
//            output.Write(divBorder.EndTag());

            // space
//            DivWriter divSpace = new DivWriter();
            output.AddStyleAttribute("float", "left");
            output.AddStyleAttribute("width", "2px");
            output.AddStyleAttribute("height", "100%");
            output.RenderBeginTag("div");
            output.RenderEndTag();
//            output.Write(divSpace.BeginTag());
//            output.Write(divSpace.EndTag());

            // PADDING BOX
//            DivWriter divPadding = new DivWriter();
            output.AddStyleAttribute("padding", "1px");
            output.RenderBeginTag("div");
//            output.Write(divPadding.BeginTag());



            if (Days < 5)
            {
                //output.Write("<div id='sensitivePartOne'>");
                output.Write("<span id='sensitiveTitle' style='font-weight:bold; '>" + displayTitle + "</span>");
                output.Write("<span id='sensitivePersonnumber'>" + displayPersonnumber + "</span>");
                //output.Write("</div>");

                output.Write(displayType);

                //output.Write("<div id='sensitivePartTwo'>");
                if (e.booking.Cancelled)
                    output.Write("<span id='sensitiveNote' style='color:" + ColorTranslator.ToHtml(eventCancellednoteColor) + ";font-weight:bold; '>" + displayNote + "</span>");
                else
                    output.Write("<span id='sensitiveNote' style='font-weight:bold; '>" + displayNote + "</span>");

                output.Write("<span id='sensitiveMobilephone' style='font-size:12px;font-family:" + EventFontFamily + "; '>" + " (" + displayMobilephone + "</span>");

                output.Write("<span id='sensitiveFreecarddate' style='color:" + ColorTranslator.ToHtml(eventFreecardExpiredColor) + ";font-size:12px;font-family:" + EventFontFamily + "; '>" + displayFreecarddate + "</span>");

                output.Write("<span id='sensitiveEnd' style='font-size:12px;font-family:" + EventFontFamily + "; '>" + ")" + "</span>");
                //output.Write("</div>");
            }
            else
            {
                output.Write("<span id='sensitiveTitle' style='font-size:10px;font-family:" + EventFontFamily + "; '>" + displayTitle + "</span>");

                output.Write("<span style='font-size:10px;font-family:" + EventFontFamily + "; '>" + displayType + "</span>");
            }
             

            // closing the PADDING BOX
            output.RenderEndTag();
//            output.Write(divPadding.EndTag());

            // closing the FIX BOX
            output.RenderEndTag();
//            output.Write(divFix.EndTag());

            // closing the MAIN BOX
//            output.Write(divMain.EndTag());
            output.RenderEndTag();
        }

        private void addHalfHourCells(HtmlTextWriter output, DateTime hour, bool hourStartsHere, bool isLast)
        {
            foreach (Day d in days)
            {
                DateTime h = new DateTime(d.date.Year, d.date.Month, d.date.Day, hour.Hour, 0, 0);
                addHalfHourCell(output, h, hourStartsHere, isLast);
            }
        }

        private void addHalfHourCell(HtmlTextWriter output, DateTime hour, bool hourStartsHere, bool isLast)
        {
            string cellBgColor;
            if (hour.Hour < BusinessBeginsHour || hour.Hour >= BusinessEndsHour || hour.DayOfWeek == DayOfWeek.Saturday || hour.DayOfWeek == DayOfWeek.Sunday)
                cellBgColor = ColorTranslator.ToHtml(NonBusinessBackColor);
            else
                cellBgColor = ColorTranslator.ToHtml(BackColor);

            string borderBottomColor;
            if (hourStartsHere)
                borderBottomColor = ColorTranslator.ToHtml(HourHalfBorderColor);
            else
                borderBottomColor = ColorTranslator.ToHtml(HourBorderColor);

            DateTime startingTime = hour;
            if (!hourStartsHere)
                startingTime = hour.AddMinutes(30);

            if (FreetimeClickHandling == UserActionHandling.PostBack)
            {
                output.AddAttribute("onclick", "javascript:" + Page.ClientScript.GetPostBackEventReference(this, "TIME:" + startingTime.ToString("s")));
            }
            else
            {
                output.AddAttribute("onclick", "javascript:" + String.Format(JavaScriptFreeAction, startingTime.ToShortDateString(), startingTime.ToShortTimeString()));
            }
            output.AddAttribute("onmouseover", "this.style.backgroundColor='" + ColorTranslator.ToHtml(HoverColor) + "';");
            output.AddAttribute("onmouseout", "this.style.backgroundColor='" + cellBgColor + "';");
            output.AddAttribute("valign", "bottom");
            output.AddStyleAttribute("background-color", cellBgColor);
            output.AddStyleAttribute("cursor", "pointer");
            output.AddStyleAttribute("cursor", "hand");
            output.AddStyleAttribute("border-right", "1px solid " + ColorTranslator.ToHtml(BorderColor));
            output.AddStyleAttribute("height", (HourHeight / 2) + "px");
            output.RenderBeginTag("td");

            // FIX BOX - to fix the outer/inner box differences in Mozilla/IE (to create border)
//            DivWriter divFix = new DivWriter();
            output.AddStyleAttribute("display", "block");
            output.AddStyleAttribute("height", "14px");
            if (!isLast)
                output.AddStyleAttribute("border-bottom", "1px solid " + borderBottomColor);
            output.RenderBeginTag("div");
//            output.Write(divFix.BeginTag());

            // required
            output.Write("<span style='font-size:1px'>&nbsp;</span>");

            // closing the FIX BOX
            output.RenderEndTag();
//            output.Write(divFix.EndTag());

            // </td>
            output.RenderEndTag();

        }


        #endregion

        #region Calculations


        /// <summary>
        /// This is only a relative time. The date part should be ignored.
        /// </summary>
        private DateTime visibleStart
        {
            get
            {
                DateTime date = new DateTime(1900, 1, 1);

                if (NonBusinessHours == NonBusinessHoursBehavior.Show)
                    return new DateTime(date.Year, date.Month, date.Day, 0, 0, 0);

                DateTime start = new DateTime(date.Year, date.Month, date.Day, BusinessBeginsHour, 0, 0);

                if (NonBusinessHours == NonBusinessHoursBehavior.Hide)
                    return start;

                if (days == null)
                    return start;

                if (totalEvents == 0)
                    return start;

                foreach (Day d in days)
                {
                    DateTime boxStart = new DateTime(date.Year, date.Month, date.Day, d.BoxStart.Hour, d.BoxStart.Minute, d.BoxStart.Second);
                    if (boxStart < start)
                        start = boxStart;
                }

                return new DateTime(start.Year, start.Month, start.Day, start.Hour, 0, 0);


            }
        }

        /// <summary>
        /// This is only a relative time. The date part should be ignored.
        /// </summary>
        private DateTime visibleEnd
        {
            get
            {
                DateTime date = new DateTime(1900, 1, 1);

                if (NonBusinessHours == NonBusinessHoursBehavior.Show)
                    return new DateTime(date.Year, date.Month, date.Day, 0, 0, 0).AddDays(1);

                DateTime end;
                if (BusinessEndsHour == 24)
                    end = new DateTime(date.Year, date.Month, date.Day, 0, 0, 0).AddDays(1);
                else
                    end = new DateTime(date.Year, date.Month, date.Day, BusinessEndsHour, 0, 0);

                if (NonBusinessHours == NonBusinessHoursBehavior.Hide)
                    return end;

                if (days == null)
                    return end;

                if (totalEvents == 0)
                    return end;

                foreach (Day d in days)
                {

                    bool addDay = false;
                    if (d.BoxEnd > DateTime.MinValue && d.BoxEnd.AddDays(-1) >= d.date)
                        addDay = true;

                    DateTime boxEnd = new DateTime(date.Year, date.Month, date.Day, d.BoxEnd.Hour, d.BoxEnd.Minute, d.BoxEnd.Second);

                    if (addDay)
                        boxEnd = boxEnd.AddDays(1);

                    if (boxEnd > end)
                        end = boxEnd;
                }

                if (end.Minute != 0)
                    end = end.AddHours(1);

                return new DateTime(end.Year, end.Month, end.Day, end.Hour, 0, 0);
            }
        }


        private int totalEvents
        {
            get
            {
                int ti = 0;
                foreach (Day d in days)
                    ti += d.Events.Count;

                return ti;
            }
        }
        #endregion

        #region Data binding


        protected override void PerformSelect()
        {
            // Call OnDataBinding here if bound to a data source using the
            // DataSource property (instead of a DataSourceID), because the
            // databinding statement is evaluated before the call to GetData.       
            if (!IsBoundUsingDataSourceID)
            {
                this.OnDataBinding(EventArgs.Empty);
            }

            // The GetData method retrieves the DataSourceView object from  
            // the IDataSource associated with the data-bound control.            
            GetData().Select(CreateDataSourceSelectArguments(),
                this.OnDataSourceViewSelectCallback);

            // The PerformDataBinding method has completed.
            RequiresDataBinding = false;
            MarkAsDataBound();

            // Raise the DataBound event.
            OnDataBound(EventArgs.Empty);
        }

        private void OnDataSourceViewSelectCallback(IEnumerable retrievedData)
        {
            // Call OnDataBinding only if it has not already been 
            // called in the PerformSelect method.
            if (IsBoundUsingDataSourceID)
            {
                OnDataBinding(EventArgs.Empty);
            }
            // The PerformDataBinding method binds the data in the  
            // retrievedData collection to elements of the data-bound control.
            PerformDataBinding(retrievedData);
        }

        protected override void PerformDataBinding(IEnumerable retrievedData)
        {
            // don't load events in design mode
            if (DesignMode)
            {
                return;
            }

            base.PerformDataBinding(retrievedData);

            if (DataStartField == null || DataStartField == String.Empty)
                throw new NullReferenceException("DataStartField property must be specified.");

            if (DataEndField == null || DataEndField == String.Empty)
                throw new NullReferenceException("DataEndField property must be specified.");

            if (DataTextField == null || DataTextField == String.Empty)
                throw new NullReferenceException("DataTextField property must be specified.");

            if (DataValueField == null || DataValueField == String.Empty)
                throw new NullReferenceException("DataValueField property must be specified.");


            // Verify data exists.
            if (retrievedData != null)
            {
                items = new ArrayList();

                foreach (object dataItem in retrievedData)
                {
                    
                    System.Data.DataRowView drv = (System.Data.DataRowView)dataItem;
                    Rehab.BookingsRow bookingsRow = (Rehab.BookingsRow)drv.Row;
                    Booking booking = new Booking(bookingsRow);

                    /*DateTime start = Convert.ToDateTime(DataBinder.GetPropertyValue(dataItem, DataStartField, null));
                    DateTime end = Convert.ToDateTime(DataBinder.GetPropertyValue(dataItem, DataEndField, null));
                    string name = Convert.ToString(DataBinder.GetPropertyValue(dataItem, DataTextField, null));
                    string pk = Convert.ToString(DataBinder.GetPropertyValue(dataItem, DataValueField, null));
                    
                    string personnumber = Convert.ToString(DataBinder.GetPropertyValue(dataItem, PersonnumberField, null));
                    string note = Convert.ToString(DataBinder.GetPropertyValue(dataItem, NoteField, null));
                    string bookingtype = Convert.ToString(DataBinder.GetPropertyValue(dataItem, BookingtypeField, null));
                    string mobilephone = Convert.ToString(DataBinder.GetPropertyValue(dataItem, MobilephoneField, null));
                    
                    string freecarddate = Convert.ToString(DataBinder.GetPropertyValue(dataItem, FreecarddateField, null));
                    bool arrived = Convert.ToBoolean(DataBinder.GetPropertyValue(dataItem, ArrivedField, null));
                    bool notshown = Convert.ToBoolean(DataBinder.GetPropertyValue(dataItem, NotshownField, null));*/

                    //items.Add(new Event(pk, start, end, name, personnumber, note, bookingtype, freecarddate, mobilephone, arrived, notshown));

                    items.Add(new Event(booking));

                }

                items.Sort(new EventComparer());

//                loadEventsToDays();
            }
        }

        private void loadEventsToDays()
        {

            if (EndDate < StartDate)
                throw new ArgumentException("EndDate must be equal to or greater than StartDate.");

            int dayCount = (int)(EndDate - StartDate).TotalDays + 1;
            days = new Day[dayCount];

            for (int i = 0; i < days.Length; i++)
            {
                days[i] = new Day(StartDate.AddDays(i));

                if (items != null)
                    days[i].Load(items);

            }
        }

        #endregion


        
    }

}
