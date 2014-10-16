using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace GearShift
{
    public class InPlaceGearState : GearState
    {
        protected HeldGearState holder;

        protected float bigger = 1.25f;

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
        protected CapsuleCollider _capsule;

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
            capsule.radius *= bigger;
        }
        public override void Deactivate()
        {
            rigidbody.isKinematic = false;
            capsule.radius /= bigger;
        }
    }
}
