using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace GearShift
{
    public class ToolBoxGearState : GearState
    {
        protected HeldGearState heldState;
        protected override void Start()
        {
            heldState = gameObject.GetComponent<HeldGearState>();
            base.Start();
        }
        public override void Move()
        {
            //do nothing
        }
        public override void Click() 
        {
            master.ChangeState(heldState);
        }
        public override void Release() { }
        public override void Activate()
        {
            //Collisions with this object should never actually happen but...
            rigidbody.isKinematic = true;
			gameObject.layer = LayerMask.NameToLayer ("Gearbox Gears");
        }
        public override void Deactivate()
        {
            rigidbody.isKinematic = false;
			gameObject.layer = LayerMask.NameToLayer ("Default");
        }
    }
}