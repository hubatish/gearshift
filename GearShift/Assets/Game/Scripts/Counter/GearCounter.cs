//
// Copyright © 2014 GearShift Studios, All Rights Reserved
//
// THIS SOFTWARE IS PROVIDED BY THE AUTHORS "AS IS" AND ANY EXPRESS OR
// IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED WARRANTIES
// OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE DISCLAIMED.
// IN NO EVENT SHALL THE AUTHORS BE LIABLE FOR ANY DIRECT, INDIRECT,
// INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING, BUT
// NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES; LOSS OF USE,
// DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND ON ANY
// THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT
// (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF
// THIS SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.
//

using UnityEngine;
using System.Collections;

namespace GearShift
{
    /// <summary>
    /// State manager for gears
    /// Handles state changes and sends out Click, Release, Activate/Deactivate messages to all the individual gears.
    /// </summary>
	public class GearCounter : MonoBehaviour
	{
		/**********************/
		/**    Model Data    **/
		/**********************/
		// Used Gears include all gears that were placed and destroyed.
		public int gearsUsed;
		
		// Placed Gears includes only the gears that are currently active on the board.
		public int gearsPlaced;
		
		// Removed Gears includes only the gears that were removed by the player.
		public int gearsRemoved;
		
		// The Last In Place Gear. This script is used to lock gears.
		private InPlaceGearState lastGear = null;
		
		/**********************/
		/**   Initializers   **/
		/**********************/
		// Initialization Code
		protected void Start ()
		{
			gearsPlaced = 0;
			gearsUsed = 0;
			gearsRemoved = 0;
		}
		
		/**********************/
		/**     Updating     **/
		/**********************/
		// Default Update Method.
		protected void Update () { }
		
		/**********************/
		/** Accessor Methods **/
		/**********************/
		public int getGearsPlaced()
		{ return gearsPlaced; }
		
		public int getGearsUsed()
		{ return gearsUsed; }
		
		public int getGearsRemoved()
		{ return gearsRemoved; }
		
		/**********************/
		/** Mutation Methods **/
		/**********************/
		public void setGearsPlaced(int placed)
		{ gearsPlaced = placed; }
		
		public void setGearsUsed(int used)
		{ gearsUsed = used; }
		
		public void setGearsRemoved(int removed)
		{ gearsRemoved = removed; }
		
		/**********************/
		/** Operator Methods **/
		/**********************/
		public void addGear(InPlaceGearState gear)
		{
			gearsPlaced = gearsPlaced + 1;
			gearsUsed = gearsUsed + 1;
			
			if (gear == null)
			{ lastGear = null; }
			else if ((lastGear != null) && (lastGear != gear))
			{
				lastGear.Lock();
				lastGear = gear;
			}
			else
			{ lastGear = gear; }
		}
		
		public void removeGear()
		{
			gearsPlaced = gearsPlaced - 1;
			gearsRemoved = gearsRemoved + 1;
		}
		
		public void setLastGear(InPlaceGearState gear)
		{
			lastGear = gear;
		}
	}
}