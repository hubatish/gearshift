using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace GearShift
{
    /// <summary>
    /// Causes the gear to rotate
    /// Also detects collisions with other gears and decides whether to rotate based on whether they're rotating or not
    /// Probably needs some abstraction to account for different gear teeth, though that could be as simple as an enum
    /// </summary>
    public class Rotater : MonoBehaviour
    {
        //How fast do the gears rotate when turning?
        public float rotationSpeed = 10f;

        //Different types of teeth
        public enum GearTeeth { Square, Holes, Spikes};

        //What teeth do I have and what teeth can I connect to?
        public List<GearTeeth> teeth;

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
                //we shouldn't be rotating, so don't rotate/do anything
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

        protected void OnTriggerEnter(Collider col)
        {
            //Attach the next gear
            Rotater other = col.GetComponent<Rotater>();
            if(other.teeth.Intersect(teeth).Count()>0)
            {
                bool newGear = (attachedGears.Count == 0);
                //we have at least some of the same teeth, so become connected
                attachedGears.Add(other);
                if(newGear)
                {
                    //Only check connection when new gears are added
                    CheckConnectionToRoot();
                }
            }
        }

        protected void OnTriggerExit(Collider col)
        {
            //Unattach the gear
            Rotater other = col.GetComponent<Rotater>();
            if(attachedGears.Contains(other))
            {
                //We were connected, break that connection
                attachedGears.Remove(other);

                CheckConnectionToRoot();
            }
        }

        /// <summary>
        /// Recursively alk the tree of connected gears until one is found that is the root gear or all gears in the chain are walked
        /// With side effects of starting to rotate if connected, or stopping if not
        /// </summary>
        /// <param name="gears">Rotater's in this list have already been checked - don't check again</param>
        /// <returns>true if the chain is connected to the root gear</returns>
        public bool CheckConnectionToRoot(List<Rotater> checkedGears = null)
        {
            if (checkedGears == null)
            {
                //Initialize default arguments (ie, checking starting with me)
                checkedGears = new List<Rotater>();
            }
            if (rootGear)
            {
                //I am the root, whatever called this is connected
                return true;
            }
            //Get all attached gears that haven't been checked yet
            IEnumerable<Rotater> unCheckedGears = attachedGears.Where(gear => !checkedGears.Contains(gear));

            //Add myself to checked gears and recurse
            checkedGears.Add(this);
            foreach (Rotater gear in unCheckedGears)
            {
                if (gear.CheckConnectionToRoot(checkedGears))
                {
                    //We are connected!
                    isRotating = true;
                    clockwise = !gear.clockwise;
                    return true;
                }
                else
                {
                    //Probably doing unnecessary work when 3 gears are connected to each other but...
                    //Add the gear we just checked to the list and try the other gear
                    checkedGears.Add(gear);
                }
            }
            //We aren't connected to the root gear
            isRotating = false;
            return false;
        }

    }
}
