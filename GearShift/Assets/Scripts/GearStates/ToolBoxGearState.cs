using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace GearShift
{
    /// <summary>
    /// State of gear when gear is in ToolBox
    /// Changes layer to show up above the GUI
    /// </summary>
    public class ToolBoxGearState : GearState
    {
        //Find the states we need to switch to
        protected HeldGearState heldState;
        protected override void Awake()
        {
            heldState = gameObject.GetComponent<HeldGearState>();
            base.Awake();
        }
        public override void Click() 
        {
            master.ChangeState(heldState);
        }
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