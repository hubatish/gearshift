using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace GearShift
{
	public class Gate : MonoBehaviour
		{
			protected bool on;
			public Gear linkedGear;
			protected Rotater linkedRotation;
			protected List<Rotater> checkedGears = null;
			
			//change state to on and open/move
			public void PowerOn()
			{
				on = true;
				
				Gate.Destroy(this);
				
			}
			//change state to off and close/move
			public void PowerOff()
			{
				on = false;
			}
			//if the linked gear changes State (rotating/not) the obstacle must move
			public void ChangeState()
			{
				//check to see if the linked gear should rotate
				if(linkedRotation.CheckConnectionToRoot(checkedGears))
				{
					PowerOn();
				}
				else
				{
					PowerOff();
				}
			}

			
		}
}

