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
        protected Rotater rotater;

        //When activated, make the collider smaller to squeeze in next to other gears
        public float bigger = 1.25f;
        //The CapsuleCollider attached to this gameobject
        //Using a getter to only find the collider if/when we need it
        protected CapsuleCollider capsule
        {
            get
            {
                if (_capsule == null)
                {
                    _capsule = gameObject.GetComponent<CapsuleCollider>();
                }
                return _capsule;
            }
        }
        private CapsuleCollider _capsule;

        protected override void Awake()
        {
            //Find components we need
            holder = gameObject.GetComponent<HeldGearState>();
            rotater = gameObject.GetComponent<Rotater>();
            base.Awake();
        }

        public override void Click()
        {
            master.ChangeState(holder);
        }
        public override void Activate()
        {
            //resize my collider and let the gear pass through other objects
            rigidbody.isKinematic = true;
            capsule.radius *= bigger;
            //Start the rotation scripts
            rotater.enabled = true;
            //Only let it check connection when placed
            rotater.Invoke("BePlaced",2*Time.deltaTime);

			// Eventually, the "Placed Gear" tag definition should be moved to LockedGearState
			this.tag = "Placed Gear";
        }
        public override void Deactivate()
        {
            //set collider back to original size and make it solid again
            rigidbody.isKinematic = false;
            capsule.radius /= bigger;
            //Deactivate the rotation scripts
            rotater.enabled = false;
        }
    }
}
