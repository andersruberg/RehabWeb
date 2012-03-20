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

namespace DayPilot.Web.Ui
{
	/// <summary>
	/// Behavior of the non-business hours.
	/// </summary>
	public enum NonBusinessHoursBehavior
	{
		/// <summary>
		/// Hides the non-business hours if there is no event in that time.
		/// </summary>
		HideIfPossible,


        /// <summary>
        /// Always hides the non-business hours.
        /// </summary>
        Hide,
		
		/// <summary>
		/// Always shows the non-business hours.
		/// </summary>
		Show
	}
}
