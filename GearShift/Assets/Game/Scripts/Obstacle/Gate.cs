using System;
using System.Collections;
using System.Linq;
using System.Text;
using UnityEngine;

namespace GearShift
{
	public class Gate : Obstacle
	{
		public Wall linkedWall;

		//similar to on state from obstacle, but needs to be initialized to null
		protected bool? powered = null;
		protected bool flag = false;
		public float distance = 133;

		

		//change state to on and open/move the amount determined by distance
		public override void PowerOn()
		{

			float localDistance = 0;
			//make sure gear isn't already powered and extended
			if (powered != true) 
			{
				while (localDistance < distance)
				{
					ChangePositionOn();
					localDistance = localDistance + 1;
				}
				flag = true;
				powered = true;
				on = true;

			}

			
		}
		//change state to off and close/move
		public override void PowerOff()
		{

			float localDistance = 0;
			//make sure gear was previously powered/extended
			if (powered != null) 
				{
					while (localDistance < distance) 
					{
						ChangePositionOff();
						localDistance = localDistance + 1;
					}
					on = false;
					powered = false;
				}

		}
		
		protected void ChangePositionOn()
		{
			linkedWall.transform.Translate (Vector3.back * Time.deltaTime);
		}
		
		protected void ChangePositionOff()
		{
			linkedWall.transform.Translate (Vector3.forward * Time.deltaTime);
		}

		
		
	}
}

