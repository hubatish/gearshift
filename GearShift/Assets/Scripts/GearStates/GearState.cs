using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace MathDash
{
    public abstract class GearState : MonoBehaviour
    {
        protected Gear master;
        public abstract void Move();
        public abstract void Click();
        public abstract void Release();
        public abstract void Activate();
        public abstract void Deactivate();

        protected virtual void Start()
        {
            master = gameObject.GetComponent<Gear>();
        }
    }
}
