using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace GearShift
{
	public class Gate : Obstacle
		{
			public Wall linkedWall;
			
			//change state to on and open/move
			public override void PowerOn()
			{
				on = true;
                //Debug.Log("i tried to kill it0");
				if(linkedWall!=null)
                {
                    GameObject.Destroy(linkedWall.gameObject);	
                }
			}
			//change state to off and close/move
			public void PowerOff()
			{
				on = false;
			}
			
		}
}

