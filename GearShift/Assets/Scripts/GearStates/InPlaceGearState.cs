using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace MathDash
{
    public class InPlaceGearState : GearState
    {
        protected HeldGearState holder;

        protected override void Start()
        {
            holder = gameObject.GetComponent<HeldGearState>();
            base.Start();
        }

        public override void Move()
        {
        }

        public override void Click() 
        {
            master.ChangeState(holder);
        }
        public override void Release() { }
        public override void Activate() 
        {
            rigidbody.isKinematic = true;
        }
        public override void Deactivate() 
        {
            rigidbody.isKinematic = false;
        }

    }
}
