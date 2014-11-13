using System;
using System.Collections;
using System.Linq;
using System.Text;
using UnityEngine;

namespace GearShift
{
	/// <summary>
    /// 
    /// </summary>
	public class RGate : MonoBehaviour
	{
		/**********************/
		/**  Internals Data  **/
		/**********************/
		// Value between 0.0 and 1.0 marking fully inactive and fully active
		private float progress;
		
		private float baseXRot;
		private float baseYRot;
		private float baseZRot;
		
		/**********************/
		/**  Externals Data  **/
		/**********************/
		// Rotation Controller From the Gear linked to this obstacle
		public Rotation rotationController;
		
		// Gate Door to be rotated during the Update
		public GameObject controlledDoor;
		
		// Maximum Angle to rotate the Door
		public float Angle;

		/**********************/
		/**   Initializers   **/
		/**********************/
		protected void Start()
		{
			progress = 0.0f;
			
			baseXRot = controlledDoor.transform.eulerAngles.x;
			baseYRot = controlledDoor.transform.eulerAngles.y;
			baseZRot = controlledDoor.transform.eulerAngles.z;
		}

		/**********************/
		/**     Updating     **/
		/**********************/
        protected void Update()
        {
			if (rotationController.getRotationStatus() != Rotation.GearRotation.NoRotation)
			{
				progress = progress + Time.deltaTime;
				
				if (progress > 1.0f)
				{ progress = 1.0f; }
			}
			else
			{
				progress = progress - Time.deltaTime;
				
				if (progress < 0.0f)
				{ progress = 0.0f; }
			}
			
			controlledDoor.transform.eulerAngles = new Vector3(baseXRot, baseYRot + (Angle * progress), baseZRot);
        }
	}
}

