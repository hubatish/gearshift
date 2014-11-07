using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace GearShift
{
    /// <summary>
    /// State of gear when placed on the field
    /// Makes its colliders bigger when placed
    /// </summary>
    public class InPlaceGearState : GearState
    {
        //The state that we're going to change to
        protected HeldGearState holder;
		protected LockedGearState locked;
        protected Rotater rotater;

        protected override void Awake()
        {
            //Find components we need
            holder = gameObject.GetComponent<HeldGearState>();
			locked = gameObject.GetComponent<LockedGearState>();
            rotater = gameObject.GetComponent<Rotater>();
            base.Awake();
        }

        public override void Click()
        {
			master.ChangeState(holder);
        }
		
		public void Lock()
        {
			if (this.tag != "Null Gear")
			{
				master.ChangeState(locked);
				renderer.material.color = Color.gray;
			}
        }

        public override void Activate()
        {
            //resize my collider and let the gear pass through other objects
            rigidbody.isKinematic = true;
            //Start the rotation scripts
            rotater.enabled = true;
            //Only let it check connection when placed
            rotater.Invoke("BePlaced",2*Time.deltaTime);

			// Eventually, the "Placed Gear" tag definition should be moved to LockedGearState
			this.tag = "Placed Gear";
            //organize myself in the scene
            transform.parent = GearParent.Instance.gearParent;
        }
        public override void Deactivate()
        {
            //set collider back to original size and make it solid again
            rigidbody.isKinematic = false;
            //Deactivate the rotation scripts
            rotater.enabled = false;
        }
    }
}
