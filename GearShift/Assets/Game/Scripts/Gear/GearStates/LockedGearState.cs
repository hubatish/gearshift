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
                }
                return _capsule;
            }
        }
		
        protected CapsuleCollider _capsule;
		protected Rotater rotater;
		
		protected override void Awake()
        {
            //Find components we need
            rotater = gameObject.GetComponent<Rotater>();
            base.Awake();
        }

        protected override void Start()
        {
            base.Start();
        }

        public void Move() {}

        public override void Click()
        {
			GearCounter counter = GameObject.FindWithTag("GearCounter").GetComponent<GearCounter>();
			counter.removeGear();
			counter.setLastGear(null);
            Destroy(this.gameObject);
        }
		
        public override void Release() { }
		
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
		
        public override void Deactivate() { }
    }
}