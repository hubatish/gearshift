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
    public class InputMouse : Singleton<InputMouse>
    {
        public LayerController layers;

        protected void Awake()
        {
            if(layers==null)
            {
                layers = gameObject.GetComponent<LayerController>();
            }
            base.Awake();
        }

        public Vector3 worldPosition
        {
            get
            {
                //Our camera is rotated so it's looking along the y axis at an xz plane
                float differenceFromCamera = Camera.main.transform.position.y - layers.getCurrentY();
                //ScreenToWorldPoint takes the two screen points as x,y, and a distance in the scene from the camera as z
                //  This has no real relation with normal Vector3/their relationship and position in Unity
                Vector3 worldPoint = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, differenceFromCamera));
                return worldPoint;
            }
        }
    }
}
