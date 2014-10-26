using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace GearShift
{
    public class Obstacle : MonoBehaviour
    {
        protected bool on;
        public virtual void PowerOn()
        {
            on = true;
        }
        public virtual void PowerOff()
        {
            on = false;
        }
    }
}
