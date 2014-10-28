using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace GearShift
{
    /// <summary>
    /// Helps get the position of the mouse in the world
    /// Will need to communicate with layers to make sure getting correct z value
    /// </summary>
    public class InputMouse : Singleton<MonoBehaviour>
    {
        protected Vector3 pointOnScreen;

        public override void Awake()
        {
            pointOnScreen = Camera.main.WorldToScreenPoint(transform.position);
            base.Awake();
        }

        public Vector3 worldPosition
        {
            get
            {
                return Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, pointOnScreen.z));
            }
        }
    }
}
