using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace GearShift
{
    /// <summary>
    /// An abstract gear state
    /// All methods are called by the Gear master
    /// </summary>
    public abstract class GearState : MonoBehaviour
    {
        protected Gear master;
        public abstract void Move();
        public abstract void Click();
        public abstract void Release();
        public abstract void Activate();
        public abstract void Deactivate();

        //This should maybe be Awake, since it seems Start is called at same time as Activate (ie when object is enabled)
        protected virtual void Start()
        {
            master = gameObject.GetComponent<Gear>();
        }
    }
}
