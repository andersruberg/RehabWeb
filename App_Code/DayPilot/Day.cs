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
using System.Runtime.Serialization;

namespace DayPilot.Web.Ui
{
	/// <summary>
	/// Day handles events of a single day.
	/// </summary>
	internal class Day : ISerializable
	{
		/// <summary>
		/// List of all events.
		/// </summary>
		internal ArrayList Events = new ArrayList();

		private ArrayList blocks = new ArrayList();
		internal DateTime date;

		/// <summary>
		/// 
		/// </summary>
		/// <param name="date">The day we are showing. The time part of DateTime doesn't matter.</param>
		public Day(DateTime date)
		{
			this.date = extractDay(date);
		}

		

		private void stripAndAddEvent(Event e)
		{
			// the event happens before this day
			if (e.Start < date && e.End <= date)
				return;
	
			// the event happens after this day
			if (e.Start >= date.AddDays(1) && e.End >= date.AddDays(1))
				return;
	
			// this is invalid event that does have no duration
			if (e.Start >= e.End)
				return;
	
			// fix the starting time
			if (e.Start < date)
				e.Start = date;
	
	
			// fix the ending time
			if (e.End > date.AddDays(1))
				e.End = date.AddDays(1);
	
			Events.Add(e);
		}

		/// <summary>
		/// Loads events from ArrayList of Events.
		/// </summary>
		/// <param name="events">ArrayList that contains the Events.</param>
		public void Load(ArrayList events)
		{
			foreach (Event e in events)
			{
				stripAndAddEvent(e);
			}
			putIntoBlocks();
		}

		private void putIntoBlocks()
		{
			foreach (Event e in Events)
			{
				// if there is no block, create the first one
				if (lastBlock == null)
				{
					blocks.Add(new Block());
				}
					// or if the event doesn't overlap with the last block, create a new block
				else if (!lastBlock.OverlapsWith(e))
				{
					blocks.Add(new Block());
				}

				// any case, add it to some block
				lastBlock.Add(e);

			}
		}

		private Block lastBlock
		{
			get
			{
				if (blocks.Count == 0)
					return null;
				return (Block) blocks[blocks.Count - 1];
			}
		}

		/// <summary>
		/// Extracts the day part from the DateTime (i.e. resets the minute and smaller units to 0).
		/// </summary>
		/// <param name="dt">The source DateTime.</param>
		/// <returns></returns>
		private DateTime extractDay(DateTime dt)
		{
			return new DateTime(dt.Year, dt.Month, dt.Day);
		}

		/// <summary>
		/// The start of the box of the first event.
		/// </summary>
		public DateTime BoxStart
		{
			get
			{
				DateTime min = DateTime.MaxValue;

				foreach(Block block in blocks)
				{
					if (block.BoxStart < min)
						min = block.BoxStart;
				}

				return min;
			}
		}

		/// <summary>
		/// The end of the box of the last event.
		/// </summary>
		public DateTime BoxEnd
		{
			get
			{
				DateTime max = DateTime.MinValue;

				foreach(Block block in blocks)
				{
					if (block.BoxEnd > max)
						max = block.BoxEnd;
				}

				return max;
			}
		}


		public void GetObjectData(SerializationInfo info, StreamingContext context)
		{
		}

	}
}
