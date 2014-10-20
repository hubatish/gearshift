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
    public class NullGearState : GearState
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