using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace GearShift
{
    public class Rotater : MonoBehaviour
    {
        protected float rotationSpeed = 10f;

        //Are we attached to other rotating gears?
        public bool isRotating
        {
            get
            {
                return _isRotating;
            }
            set
            {
                //The root gear can never not rotate
                if(rootGear)
                {
                    _isRotating = true;
                }
                else
                {
                    _isRotating = value;
                }
            }
        }
        protected bool _isRotating;

        //Is this the root gear?
        public bool rootGear = false;

        protected void Start()
        {
            if(rootGear)
            {
                isRotating = true;
            }
        }

        //which direciton are we rotating?
        public bool clockwise = true;

        //What gears are we attached to?
        protected List<Rotater> attachedGears = new List<Rotater>();

        protected void Update()
        {
            if(!isRotating)
            {
                //we shouldn't be rotating, so don't rotate
                return;
            }
            float toRotate = rotationSpeed;
            if(!clockwise)
            {
                //clockwise is opposite direction of counter-clockwise
                toRotate = -toRotate;
            }
            //Rotate around the y axis
            transform.Rotate(Vector3.up * Time.deltaTime * toRotate);
        }

        protected void OnTriggerStay(Collider col)
        {
            //Debug.Log("collision between " + gameObject.name + " and " + col.gameObject.name);
        }

        protected void OnTriggerEnter(Collider col)
        {
            //Attach the next gear
            Rotater other = col.GetComponent<Rotater>();
            attachedGears.Add(other);
            //Start rotating if the other one is rotating
            if(other.isRotating)
            {
                isRotating = true;
                clockwise = !other.clockwise;
            }
            //Debug.Log("collision enter " + gameObject.name + " and " + col.gameObject.name + " attached to " + attachedGears.Count);
        }

        protected void OnTriggerExit(Collider col)
        {
            //Debug.Log("collision exit " + gameObject.name + " and " + col.gameObject.name + " attached to " + attachedGears.Count);
            //Unattach the gear
            attachedGears.Remove(col.GetComponent<Rotater>());
            if(attachedGears.Count==0)
            {
                isRotating = false;
            }
        }

    }
}
