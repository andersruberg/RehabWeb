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
using System.ComponentModel;
using System.Drawing;
using System.Web.UI.WebControls;

namespace DayPilot.Web.Ui
{
    public partial class DayPilotCalendar
    {

        /// <summary>
        /// Gets or sets the start of the business day (in hours).
        /// </summary>
        [Description("Start of the business day (hour from 0 to 23).")]
        [Category("Appearance")]
        [DefaultValue(9)]
        public int BusinessBeginsHour
        {
            get
            {
                if (ViewState["BusinessBeginsHour"] == null)
                    return 9;
                return (int)ViewState["BusinessBeginsHour"];
            }
            set
            {
                if (value < 0)
                    ViewState["BusinessBeginsHour"] = 0;
                else if (value > 23)
                    ViewState["BusinessBeginsHour"] = 23;
                else
                    ViewState["BusinessBeginsHour"] = value;
            }
        }

        /// <summary>
        /// Gets or sets the end of the business day (hours).
        /// </summary>
        [Description("End of the business day (hour from 1 to 24).")]
        [Category("Appearance")]
        [DefaultValue(18)]
        public int BusinessEndsHour
        {
            get
            {
                if (ViewState["BusinessEndsHour"] == null)
                    return 18;
                return (int)ViewState["BusinessEndsHour"];
            }
            set
            {
                if (value < BusinessBeginsHour)
                    ViewState["BusinessEndsHour"] = BusinessBeginsHour + 1;
                else if (value > 24)
                    ViewState["BusinessEndsHour"] = 24;
                else
                    ViewState["BusinessEndsHour"] = value;
            }
        }

        /// <summary>
        /// Gets or sets the height of the hour cell in pixels. Must be even.
        /// </summary>
        [Description("Height of the hour cell in pixels. Must be even.")]
        [Category("Layout")]
        [DefaultValue(40)]
        public int HourHeight
        {
            get
            {
                if (ViewState["HourHeight"] == null)
                    return 40;
                return (int)ViewState["HourHeight"];
            }
            set
            {
                if (value % 2 == 0)
                    ViewState["HourHeight"] = value;
                else
                    ViewState["HourHeight"] = value - 1;
            }
        }

        /// <summary>
        /// Gets or sets the width of the hour cell in pixels.
        /// </summary>
        [Description("Width of the hour cell in pixels.")]
        [Category("Layout")]
        [DefaultValue(40)]
        public int HourWidth
        {
            get
            {
                if (ViewState["HourWidth"] == null)
                    return 40;
                return (int)ViewState["HourWidth"];
            }
            set
            {
                ViewState["HourWidth"] = value;
            }
        }

        /// <summary>
        /// Gets or sets the Javascript code that is executed when the users clicks on an event. '{0}' will be replaced by the primary key of the event.
        /// </summary>
        [Description("Javascript code that is executed when the users clicks on an event. '{0}' will be replaced by the primary key of the event.")]
        [Category("Behavior")]
        [DefaultValue("alert('{0}');")]
        public string JavaScriptEventAction
        {
            get
            {
                if (ViewState["JavaScriptEventAction"] == null)
                    return "alert('{0}');";
                return (string)ViewState["JavaScriptEventAction"];
            }
            set
            {
                ViewState["JavaScriptEventAction"] = value;
            }
        }

        /// <summary>
        /// Gets or sets the Javascript code that is executed when the users clicks on a free time slot. '{0}' will be replaced by the starting time of that slot (i.e. '9:00'.
        /// </summary>
        [Description("Javascript code that is executed when the users clicks on a free time slot. '{0}' will be replaced by the starting time of that slot (i.e. '9:00'.")]
        [Category("Behavior")]
        [DefaultValue("alert('{0}');")]
        public string JavaScriptFreeAction
        {
            get
            {
                if (ViewState["JavaScriptFreeAction"] == null)
                    return "alert('{0}');";
                return (string)ViewState["JavaScriptFreeAction"];
            }
            set
            {
                ViewState["JavaScriptFreeAction"] = value;
            }
        }

        /// <summary>
        /// Gets or sets the first day to be shown. Default is DateTime.Today.
        /// </summary>
        [Description("The first day to be shown. Default is DateTime.Today.")]
        public DateTime StartDate
        {
            get
            {
                if (ViewState["StartDate"] == null)
                {
                    return DateTime.Today;
                }

                return (DateTime)ViewState["StartDate"];

            }
            set
            {
                ViewState["StartDate"] = new DateTime(value.Year, value.Month, value.Day);
            }
        }

        /// <summary>
        /// Gets or sets the number of days to be displayed. Default is 1.
        /// </summary>
        [Description("The number of days to be displayed on the calendar. Default value is 1.")]
        [DefaultValue(1)]
        public int Days
        {
            get
            {
                if (ViewState["Days"] == null)
                    return 1;
                return (int)ViewState["Days"]; 
            }
            set
            {
                int daysCount = value;

                if (daysCount < 1)
                    daysCount = 1;

                ViewState["Days"] = daysCount;
            }
        }

        /// <summary>
        /// Gets the last day to be shown.
        /// </summary>
        public DateTime EndDate
        {
            get
            {
                return StartDate.AddDays(Days - 1);
            }
        }

        /// <summary>
        /// Gets or sets the name of the column that contains the event starting date and time (must be convertible to DateTime).
        /// </summary>
        [Description("The name of the column that contains the event starting date and time (must be convertible to DateTime).")]
        [Category("Data")]
        public string DataStartField
        {
            get
            {
                return dataStartField;
            }
            set
            {
                dataStartField = value;

                if (Initialized)
                {
                    OnDataPropertyChanged();
                }

            }
        }

        /// <summary>
        /// Gets or sets the name of the column that contains the event starting date and time (must be convertible to DateTime).
        /// </summary>
        [Description("The name of the column that contains the personnumber.")]
        [Category("Data")]
        public string PersonnumberField
        {
            get
            {
                return personnumberField;
            }
            set
            {
                personnumberField = value;

                if (Initialized)
                {
                    OnDataPropertyChanged();
                }

            }
        }

        /// <summary>
        /// Gets or sets the name of the column that contains the event starting date and time (must be convertible to DateTime).
        /// </summary>
        [Description("The name of the column that contains the event bookingtype.")]
        [Category("Data")]
        public string BookingtypeField
        {
            get
            {
                return bookingtypeField;
            }
            set
            {
                bookingtypeField = value;

                if (Initialized)
                {
                    OnDataPropertyChanged();
                }

            }
        }

        /// <summary>
        /// Gets or sets the name of the column that contains the event starting date and time (must be convertible to DateTime).
        /// </summary>
        [Description("The name of the column that contains the event arrived status.")]
        [Category("Data")]
        public string ArrivedField
        {
            get
            {
                return arrivedField;
            }
            set
            {
                arrivedField = value;

                if (Initialized)
                {
                    OnDataPropertyChanged();
                }

            }
        }

        /// <summary>
        /// Gets or sets the name of the column that contains the event starting date and time (must be convertible to DateTime).
        /// </summary>
        [Description("The name of the column that contains the event not shown status.")]
        [Category("Data")]
        public string NotshownField
        {
            get
            {
                return notshownField;
            }
            set
            {
                notshownField = value;

                if (Initialized)
                {
                    OnDataPropertyChanged();
                }

            }
        }

        /// <summary>
        /// Gets or sets the name of the column that contains the event starting date and time (must be convertible to DateTime).
        /// </summary>
        [Description("The name of the column that contains the event mobilephone.")]
        [Category("Data")]
        public string MobilephoneField
        {
            get
            {
                return mobilephoneField;
            }
            set
            {
                mobilephoneField = value;

                if (Initialized)
                {
                    OnDataPropertyChanged();
                }

            }
        }

        /// <summary>
        /// Gets or sets the name of the column that contains the event starting date and time (must be convertible to DateTime).
        /// </summary>
        [Description("The name of the column that contains the event note.")]
        [Category("Data")]
        public string NoteField
        {
            get
            {
                return noteField;
            }
            set
            {
                noteField = value;

                if (Initialized)
                {
                    OnDataPropertyChanged();
                }

            }
        }

        /// <summary>
        /// Gets or sets the name of the column that contains the event starting date and time (must be convertible to DateTime).
        /// </summary>
        [Description("The name of the column that contains the event freecarddate.")]
        [Category("Data")]
        public string FreecarddateField
        {
            get
            {
                return freecarddateField;
            }
            set
            {
                freecarddateField = value;

                if (Initialized)
                {
                    OnDataPropertyChanged();
                }

            }
        }



        /// <summary>
        /// Gets or sets the name of the column that contains the event ending date and time (must be convertible to DateTime).
        /// </summary>
        [Description("The name of the column that contains the event ending date and time (must be convertible to DateTime).")]
        [Category("Data")]
        public string DataEndField
        {
            get
            {
                return dataEndField;
            }
            set
            {
                dataEndField = value;
                if (Initialized)
                {
                    OnDataPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Gets or sets the  name of the column that contains the name of an event.
        /// </summary>
        [Category("Data")]
        [Description("The name of the column that contains the name of an event.")]
        public string DataTextField
        {
            get
            {
                return dataTextField;
            }
            set
            {
                dataTextField = value;

                if (Initialized)
                {
                    OnDataPropertyChanged();
                }

            }
        }

        /// <summary>
        /// Gets or sets the name of the column that contains the primary key. The primary key will be used for rendering the custom JavaScript actions.
        /// </summary>
        [Category("Data")]
        [Description("The name of the column that contains the primary key. The primary key will be used for rendering the custom JavaScript actions.")]
        public string DataValueField
        {
            get
            {
                return dataValueField;
            }
            set
            {
                dataValueField = value;

                if (Initialized)
                {
                    OnDataPropertyChanged();
                }

            }
        }

        /// <summary>
        /// Gets or sets whether the hour numbers should be visible.
        /// </summary>
        [Category("Appearance")]
        [Description("Should the hour numbers be visible?")]
        [DefaultValue(true)]
        public bool ShowHours
        {
            get
            {
                if (ViewState["ShowHours"] == null)
                    return true;
                return (bool)ViewState["ShowHours"];
            }
            set
            {
                ViewState["ShowHours"] = value;
            }
        }

        /// <summary>
        /// Gets or sets whether sensitive data should be hidden or not
        /// </summary>
        [Category("Appearance")]
        [Description("Should sensitive data be hidden?")]
        [DefaultValue(false)]
        public bool HideSensitiveData
        {
            get
            {
                if (ViewState["HideSensitiveData"] == null)
                    return true;
                return (bool)ViewState["HideSensitiveData"];
            }
            set
            {
                ViewState["HideSensitiveData"] = value;
            }
        }

        /// <summary>
        /// Gets or sets the time-format for hour numbers (on the left).
        /// </summary>
        [Category("Appearance")]
        [Description("The time-format that will be used for the hour numbers.")]
        [DefaultValue(TimeFormat.Clock12Hours)]
        public TimeFormat TimeFormat
        {
            get
            {
                if (ViewState["TimeFormat"] == null)
                    return TimeFormat.Clock12Hours;
                return (TimeFormat)ViewState["TimeFormat"];
            }
            set
            {
                ViewState["TimeFormat"] = value;
            }
        }

        /// <summary>
        /// Gets or sets the drawing mode of non-business hours.
        /// </summary>
        [Category("Appearance")]
        [Description("Determines whether the control shows the non-business hours.")]
        [DefaultValue(NonBusinessHoursBehavior.HideIfPossible)]
        public NonBusinessHoursBehavior NonBusinessHours
        {
            get
            {
                if (ViewState["NonBusinessHours"] == null)
                    return NonBusinessHoursBehavior.HideIfPossible;
                return (NonBusinessHoursBehavior)ViewState["NonBusinessHours"];
            }
            set
            {
                ViewState["NonBusinessHours"] = value;
            }
        }

        /// <summary>
        /// Handling of user action (clicking an event).
        /// </summary>
        [Category("Behavior")]
        [Description("Whether clicking an event should do a postback or run a javascript action. By default, it calls the javascript code specified in JavaScriptEventAction property.")]
        [DefaultValue(UserActionHandling.JavaScript)]
        public UserActionHandling EventClickHandling
        {
            get
            {
                if (ViewState["EventClickHandling"] == null)
                    return UserActionHandling.JavaScript;
                return (UserActionHandling)ViewState["EventClickHandling"];
            }
            set
            {
                ViewState["EventClickHandling"] = value;
            }
        }

        /// <summary>
        /// Handling of user action (clicking a free-time slot).
        /// </summary>
        [Category("Behavior")]
        [Description("Whether clicking a free-time slot should do a postback or run a javascript action. By default, it calls the javascript code specified in JavaScriptFreeAction property.")]
        [DefaultValue(UserActionHandling.JavaScript)]
        public UserActionHandling FreetimeClickHandling
        {
            get
            {
                if (ViewState["FreetimeClickHandling"] == null)
                    return UserActionHandling.JavaScript;
                return (UserActionHandling)ViewState["FreetimeClickHandling"];
            }
            set
            {
                ViewState["FreetimeClickHandling"] = value;
            }
        }

        //headerDateFormat
        /// <summary>
        /// Gets or sets the format of the date display in the header columns.
        /// </summary>
        [Description("Format of the date display in the header columns.")]
        [Category("Appearance")]
        [DefaultValue("d")]
        public string HeaderDateFormat
        {
            get
            {
                if (ViewState["HeaderDateFormat"] == null)
                    return "d";
                return (string)ViewState["HeaderDateFormat"];
            }
            set
            {
                ViewState["HeaderDateFormat"] = value;
            }
        }

        /// <summary>
        /// Gets or sets whether the header should be visible.
        /// </summary>
        [Category("Appearance")]
        [Description("Should the header be visible?")]
        [DefaultValue(true)]
        public bool ShowHeader
        {
            get
            {
                return showHeader;
            }
            set
            {
                showHeader = value;
            }
        }


        /// <summary>
        /// Gets or sets whether the header should be visible.
        /// </summary>
        [Category("Layout")]
        [Description("Header height in pixels.")]
        [DefaultValue(21)]
        public int HeaderHeight
        {
            get
            {
                return headerHeight;
            }
            set
            {
                headerHeight = value;
            }
        }

        public override Color BackColor
        {
            get
            {
                if (ViewState["BackColor"] == null)
                    return ColorTranslator.FromHtml("#FFFFD5");
                return (Color)ViewState["BackColor"];
            }
            set
            {
                ViewState["BackColor"] = value;
            }
        }

        public Color EventArrivedBackColor
        {
            get
            {
                if (ViewState["EventArrivedBackColor"] == null)
                    return ColorTranslator.FromHtml("#FFFFD5");
                return (Color)ViewState["EventArrivedBackColor"];
            }
            set
            {
                ViewState["EventArrivedBackColor"] = value;
            }
        }

        public Color EventNotShownBackColor
        {
            get
            {
                if (ViewState["EventNotShownBackColor"] == null)
                    return ColorTranslator.FromHtml("#FFFFD5");
                return (Color)ViewState["EventNotShownBackColor"];
            }
            set
            {
                ViewState["EventNotShownBackColor"] = value;
            }
        }

        public Color EventFreecardExpiredBackColor
        {
            get
            {
                if (ViewState["EventFreecardExpiredBackColor"] == null)
                    return ColorTranslator.FromHtml("#FFFFD5");
                return (Color)ViewState["EventFreecardExpiredBackColor"];
            }
            set
            {
                ViewState["EventFreecardExpiredBackColor"] = value;
            }
        }


        public override Color BorderColor
        {
            get
            {
                if (ViewState["BorderColor"] == null)
                    return ColorTranslator.FromHtml("#000000");
                return (Color)ViewState["BorderColor"];
            }
            set
            {
                ViewState["BorderColor"] = value;
            }
        }

        [Category("Appearance")]
        [TypeConverter(typeof(WebColorConverter))]
        public Color HoverColor
        {
            get
            {
                if (ViewState["HoverColor"] == null)
                    return ColorTranslator.FromHtml("#FFED95");
                return (Color)ViewState["HoverColor"];

            }
            set
            {
                ViewState["HoverColor"] = value;
            }
        }

        [Category("Appearance")]
        [TypeConverter(typeof(WebColorConverter))]
        public Color HourBorderColor
        {
            get
            {
                if (ViewState["HourBorderColor"] == null)
                    return ColorTranslator.FromHtml("#EAD098");
                return (Color)ViewState["HourBorderColor"];

            }
            set
            {
                ViewState["HourBorderColor"] = value;
            }
        }

        [Category("Appearance")]
        [TypeConverter(typeof(WebColorConverter))]
        public Color HourHalfBorderColor
        {
            get
            {
                if (ViewState["HourHalfBorderColor"] == null)
                    return ColorTranslator.FromHtml("#F3E4B1");
                return (Color)ViewState["HourHalfBorderColor"];

            }
            set
            {
                ViewState["HourHalfBorderColor"] = value;
            }
        }

        [Category("Appearance")]
        [TypeConverter(typeof(WebColorConverter))]
        public Color HourNameBorderColor
        {
            get
            {
                if (ViewState["HourNameBorderColor"] == null)
                    return ColorTranslator.FromHtml("#ACA899");
                return (Color)ViewState["HourNameBorderColor"];
            }
            set
            {
                ViewState["HourNameBorderColor"] = value;
            }
        }

        [Category("Appearance")]
        [TypeConverter(typeof(WebColorConverter))]
        public Color HourNameBackColor
        {
            get
            {
                if (ViewState["HourNameBackColor"] == null)
                    return ColorTranslator.FromHtml("#ECE9D8");
                return (Color)ViewState["HourNameBackColor"];
            }
            set
            {
                ViewState["HourNameBackColor"] = value;
            }
        }


        [Category("Appearance")]
        [TypeConverter(typeof(WebColorConverter))]
        public Color EventBackColor
        {
            get
            {
                if (ViewState["EventBackColor"] == null)
                    return ColorTranslator.FromHtml("#FFFFFF");
                return (Color)ViewState["EventBackColor"];
            }
            set
            {
                ViewState["EventBackColor"] = value;
            }
        }


        [Category("Appearance")]
        [TypeConverter(typeof(WebColorConverter))]
        public Color EventHoverColor
        {
            get
            {
                if (ViewState["EventHoverColor"] == null)
                    return ColorTranslator.FromHtml("#DCDCDC");
                return (Color)ViewState["EventHoverColor"];
            }
            set
            {
                ViewState["EventHoverColor"] = value;
            }
        }


        [Category("Appearance")]
        [TypeConverter(typeof(WebColorConverter))]
        public Color EventBorderColor
        {
            get
            {
                if (ViewState["EventBorderColor"] == null)
                    return ColorTranslator.FromHtml("#000000");
                return (Color)ViewState["EventBorderColor"];
            }
            set
            {
                ViewState["EventBorderColor"] = value;
            }
        }

        [Category("Appearance")]
        [TypeConverter(typeof(WebColorConverter))]
        public Color EventLeftBarColor
        {
            get
            {
                if (ViewState["EventLeftBarColor"] == null)
                    return ColorTranslator.FromHtml("blue");
                return (Color)ViewState["EventLeftBarColor"];
            }
            set
            {
                ViewState["EventLeftBarColor"] = value;
            }
        }   

        [Category("Appearance")]
        [TypeConverter(typeof(WebColorConverter))]
        public Color NonBusinessBackColor
        {
            get
            {
                if (ViewState["NonBusinessBackColor"] == null)
                    return ColorTranslator.FromHtml("#FFF4BC");

                return (Color)ViewState["NonBusinessBackColor"];
            }
            set
            {
                ViewState["NonBusinessBackColor"] = value;
            }
        }

        [Category("Appearance")]
        public string EventFontFamily
        {
            get
            {
                if (ViewState["EventFontFamily"] == null)
                    return "Tahoma";

                return (string)ViewState["EventFontFamily"];
            }
            set
            {
                ViewState["EventFontFamily"] = value;
            }
        }


        [Category("Appearance")]
        public string HourFontFamily
        {
            get
            {
                if (ViewState["HourFontFamily"] == null)
                    return "Tahoma";

                return (string)ViewState["HourFontFamily"];
            }
            set
            {
                ViewState["HourFontFamily"] = value;
            }
        }
        
        [Category("Appearance")]
        public string DayFontFamily
        {
            get
            {
                if (ViewState["DayFontFamily"] == null)
                    return "Tahoma";

                return (string)ViewState["DayFontFamily"];
            }
            set
            {
                ViewState["DayFontFamily"] = value;
            }
        }

        [Category("Appearance")]
        public string EventFontSize
        {
            get
            {
                if (ViewState["EventFontSize"] == null)
                    return "8pt";

                return (string)ViewState["EventFontSize"];
            }
            set
            {
                ViewState["EventFontSize"] = value;
            }
        }
        
        [Category("Appearance")]
        public string HourFontSize
        {
            get
            {
                if (ViewState["HourFontSize"] == null)
                    return "16pt";

                return (string)ViewState["HourFontSize"];
            }
            set
            {
                ViewState["HourFontSize"] = value;
            }
        }

        [Category("Appearance")]
        public string DayFontSize
        {
            get
            {
                if (ViewState["DayFontSize"] == null)
                    return "10pt";

                return (string)ViewState["DayFontSize"];
            }
            set
            {
                ViewState["DayFontSize"] = value;
            }
        }

        /// <summary>
        /// Determines whether the event tooltip is active.
        /// </summary>
        [Description("Determines whether the event tooltip is active.")]
        [Category("Appearance")]
        [DefaultValue(true)]
        public bool ShowToolTip
        {
            get
            {
                if (ViewState["ShowToolTip"] == null)
                    return true;
                return (bool)ViewState["ShowToolTip"];
            }
            set
            {
                ViewState["ShowToolTip"] = value;
            }
        }

        /// <summary>
        /// Width of the right margin inside a column (in pixels).
        /// </summary>
        [Description("Width of the right margin inside a column (in pixels).")]
        [Category("Appearance")]
        [DefaultValue(5)]
        public int ColumnMarginRight
        {
            get
            {
                if (ViewState["ColumnMarginRight"] == null)
                    return 5;
                return (int)ViewState["ColumnMarginRight"];
            }
            set
            {
                ViewState["ColumnMarginRight"] = value;
            }
        }



    }
}
