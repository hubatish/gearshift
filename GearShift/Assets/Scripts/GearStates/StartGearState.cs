using UnityEngine;
using System.Collections;

namespace GearShift
{
	public class StartGearState : GearState
    {
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

        protected void Start()
        {
            base.Start();
        }

        public void Move() {}
        public override void Click() {}
        public override void Release() { }
        public override void Activate() { }
        public override void Deactivate() { }
    }
}