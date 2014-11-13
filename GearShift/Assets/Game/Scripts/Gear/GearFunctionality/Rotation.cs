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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using GearShift;

namespace GearShift
{
	/// <summary>
	///
	/// </summary>
	public class Rotation : MonoBehaviour
	{
		/**********************/
		/**   Enumerations   **/
		/**********************/
		public enum GearRotation{ForceCounterclockwise,Counterclockwise,NoRotation,Clockwise,ForceClockwise};
		
		/**********************/
		/**  Reference Data  **/
		/**********************/
		
		/**********************/
		/**  Internals Data  **/
		/**********************/
		// Measured in Degrees per Second
		private float angularVelocity;
		
		/**********************/
		/**  Externals Data  **/
		/**********************/
		public int numberOfTeeth;
		public GearRotation rotationStatus;
		
		// A list that contains Rotation Scripts
		public List<Rotation> rotationsList = new List<Rotation>();

		/**********************/
		/**   Initializers   **/
		/**********************/
		protected void Start()
		{
			// Set Base Variables
			angularVelocity = 360.0f / (numberOfTeeth * 1.0f);
		}

		/**********************/
		/**     Updating     **/
		/**********************/
        protected void Update()
		{
			if (this.enabled == true)
			{
				// Update the Rotation of the Gear
				updateRotationStatus();
				
				// Apply the Rotation on this Gear
				if ((rotationStatus == GearRotation.ForceCounterclockwise) || (rotationStatus == GearRotation.Counterclockwise))
				{ this.gameObject.transform.Rotate(Vector3.down * Time.deltaTime * angularVelocity); }
				else if ((rotationStatus == GearRotation.ForceClockwise) || (rotationStatus == GearRotation.Clockwise))
				{ this.gameObject.transform.Rotate(Vector3.up * Time.deltaTime * angularVelocity); }
			}
		}
		
		/**********************/
        /** Accessor Methods **/
        /**********************/
		public GearRotation getRotationStatus()
		{ return rotationStatus; }
		
		/**********************/
        /** Mutation Methods **/
        /**********************/
		public void updateRotationStatus()
		{
			List<Rotation> banList = new List<Rotation>();
			banList.Add(this);
			
			if (traceRotationSource(banList) == true)
			{
				bool clockwiseFlag = false;
				bool counterclockwiseFlag = false;
				
				int curCounter = 0;
				
				while (curCounter < rotationsList.Count)
				{
					Rotation rotControl = this.rotationsList[curCounter];
					curCounter = curCounter + 1;
					if ((rotControl != null) && (rotControl.enabled = true))
					{
						GearRotation rot = rotControl.getRotationStatus();
						
						if ((rot == GearRotation.ForceCounterclockwise) || (rot == GearRotation.Counterclockwise))
						{ counterclockwiseFlag = true; }
						else if ((rot == GearRotation.ForceClockwise) || (rot == GearRotation.Clockwise))
						{ clockwiseFlag = true; }
					}
					else
					{ this.rotationsList.Remove(rotControl); }
				}
				
				if ((rotationStatus != GearRotation.ForceCounterclockwise) && (rotationStatus != GearRotation.ForceClockwise))
				{
					if ((clockwiseFlag == true) && (counterclockwiseFlag == true))
					{
						foreach (Rotation rotControl in this.rotationsList)
						{ rotControl.rotationsList.Remove(this); }
						
						Destroy(this.gameObject);
					}
					else if (clockwiseFlag == true)
					{ rotationStatus = GearRotation.Counterclockwise; }
					else if (counterclockwiseFlag == true) 
					{ rotationStatus = GearRotation.Clockwise; }
					else
					{ rotationStatus = GearRotation.NoRotation; }
				}
			}
			else
			{ rotationStatus = GearRotation.NoRotation; }
		}
		
		/**********************/
        /** Operator Methods **/
        /**********************/
		public bool traceRotationSource(List<Rotation> banList)
		{
			if ((this.rotationStatus == GearRotation.ForceCounterclockwise) || (this.rotationStatus == GearRotation.ForceClockwise))
			{ return true; }
			else
			{
				int curCounter = 0;
				while (curCounter < rotationsList.Count)
				{
					Rotation rotControl = this.rotationsList[curCounter];
					curCounter = curCounter + 1;
					if ((rotControl != null) && (rotControl.enabled = true))
					{
						if ((this.rotationsList.Contains(rotControl)) && (!banList.Contains(rotControl)))
						{
							banList.Add(rotControl);
							if (rotControl.traceRotationSource(banList))
							{ return true; }
						}
					}
					else
					{ this.rotationsList.Remove(rotControl); }
				}
			}
			
			return false;
		}

		/**********************/
        /** Events / Drivers **/
        /**********************/
        // Called when the gear collides with any collider
        protected void OnTriggerEnter(Collider col)
		{
			rotationsList.Add(col.gameObject.GetComponent<Rotation>());
		}
		
        // Called when the gear separates from a collider
        protected void OnTriggerExit(Collider col)
		{
			rotationsList.Remove(col.gameObject.GetComponent<Rotation>());
		}
	}
}