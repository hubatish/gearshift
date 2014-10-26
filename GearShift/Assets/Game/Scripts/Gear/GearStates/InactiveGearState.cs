using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace GearShift
{
    // State for Null gears that have been placed, and will no longer be moveable or deleteable
    public class InactiveGearState : GearState
    {
        protected override void Awake()
        { base.Awake(); }
		
        public override void Click()
        {}
		
        public override void Activate()
        {}
		
        public override void Deactivate()
        {}
    }
}