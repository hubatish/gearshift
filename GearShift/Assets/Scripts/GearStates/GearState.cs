using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace GearShift
{
    /// <summary>
    /// An abstract gear state
    /// All abstract methods are called by the Gear master - check that file out to see exactly when
    /// Child classes will spend a lot of time disabled, and generally shouldn't take any actions while disabled
    /// </summary>
    public abstract class GearState : MonoBehaviour
    {
        protected Gear master;
        //What happens when clicked on the gear - usually change state
        public virtual void Click() { }
        //What happens when mouse is released - usually change state
        public virtual void Release() { }
        //What happens when this state is activated/changed to.  A mirror of Deactivate, though really it's called at the same time as Start()
        public abstract void Activate();
        //What happens when state is deactivated/left.  A mirror of Activate
        public abstract void Deactivate();

        //Called the first time the component is enabled
        protected virtual void Awake()
        {
            master = gameObject.GetComponent<Gear>();
        }
    }
}
