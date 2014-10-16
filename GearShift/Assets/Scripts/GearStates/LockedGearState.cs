using UnityEngine;
using System.Collections;

namespace GearShift
{
	public class LockedGearState : GearState
    {
        protected CapsuleCollider capsule
        {
            get
            {
                if (_capsule == null)
                {
                    _capsule = gameObject.GetComponent<CapsuleCollider>();
					this.tag = "Placed Gear";
                }
                return _capsule;
            }
        }
		
        protected CapsuleCollider _capsule;

        protected void Start()
        {
            base.Start();
        }

        public void Move() {}

        public void Click()
        {
            Destroy(this);
        }
		
        public override void Release() { }
        public override void Activate() { }
        public override void Deactivate() { }
    }
}