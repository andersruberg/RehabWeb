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

namespace DayPilot.Web.Ui
{
	/// <summary>
	/// Delegate for passing an event primary key.
	/// </summary>
	public delegate void EventClickDelegate(object sender, EventClickEventArgs e);

	/// <summary>
	/// Delegate for passing a starting time.
	/// </summary>
	public delegate void FreeClickDelegate(object sender, FreeClickEventArgs e);


    public class EventClickEventArgs : EventArgs
    {
        private string value;

        public string Value
        {
            get { return value; }
        }

        public EventClickEventArgs(string value)
        {
            this.value = value;
        }
    }

    public class FreeClickEventArgs : EventArgs
    {
        private DateTime start;


        public DateTime Start
        {
            get { return start; }
        }

        public FreeClickEventArgs(DateTime start)
        {
            this.start = start;
        }
    }

}
