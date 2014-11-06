using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace GearShift
{
    /// <summary>
    /// State that is active while the gear is being dragged around
    /// Handles placement of gear, and checking to see if every location is valid
    /// </summary>
    public class HeldGearState : GearState
    {
        //The state that we're going to change to
        protected InPlaceGearState inPlace;
		protected LockedGearState locked;
        //The CapsuleCollider attached to this gameobject
        //Using a getter to only find the collider if/when we need it
        protected CapsuleCollider capsule
        {
            get
            {
                if (_capsule == null)
                {
                    _capsule = gameObject.GetComponent<CapsuleCollider>();
                }
                return _capsule;
            }
        }
        private CapsuleCollider _capsule;
        //When activated, make the collider smaller to squeeze in next to other gears
        public float bigger = 0.75f;

        public override void Release()
        {
            GameObject.FindWithTag("GearCounter").GetComponent<GearCounter>().addGear(inPlace);
            //check for numCollisions
			if (this.tag == "Null Gear")
			{ master.ChangeState(locked); }
			else
            { master.ChangeState(inPlace); }
        }
        public override void Activate()
        {
            //Make my collider smaller
            capsule.radius *= bigger;
            prevColor = renderer.material.color;
        }
        public override void Deactivate()
        {
            //resize myself to previous size
            capsule.radius /= bigger;
            // Change to Deselected Graphics
            this.renderer.material.color = prevColor;
            //warp to last locked position
            transform.position = lastPosition;
        }

        /**********************/
        /** Internal Classes **/
        /**********************/

        /**********************/
        /**    Model Data    **/
        /**********************/
        private int numCollisions;

        private Vector3 offset;

        protected Vector3 lastPosition;

        protected float boundaryDistance = 5f;

        protected Color prevColor;

        /**********************/
        /** Operator Methods **/
        /**********************/
        private bool isValidLocation()
        {
            return (numCollisions == 0) ;
        }

        /**********************/
        /** Mutation Methods **/
        /**********************/
        void setXPosition(float value)
        { this.transform.position = new Vector3(value, this.transform.position.y, this.transform.position.z); }

        void setYPosition(float value)
        { this.transform.position = new Vector3(this.transform.position.x, value, this.transform.position.z); }

        void setZPosition(float value)
        { this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, value); }

        /**********************/
        /**   Constructors   **/
        /**********************/

        /**********************/
        /**   Initializers   **/
        /**********************/
        protected void Start()
        {
            //Using this rather than Activate for clarity
            numCollisions = 0;
            lastPosition = transform.position;
            Vector3 fullOffset = transform.position - InputMouse.Instance.worldPosition;
            offset = new Vector3(fullOffset.x, 0, fullOffset.z);
        }

        protected override void Awake()
        {
            //Find needed scripts
            inPlace = gameObject.GetComponent<InPlaceGearState>();
			locked = gameObject.GetComponent<LockedGearState>();
            base.Awake();
        }

        /**********************/
        /**     Updating     **/
        /**********************/
        // Update is called once per frame
        void Update() { }

        /**********************/
        /** Events / Drivers **/
        /**********************/
        // Called when the selected gear is translated on the screen.
        void OnMouseDrag()
        {
            // Apply Mouse Coordinates of the Gear.
            Vector3 worldPoint = InputMouse.Instance.worldPosition;
            this.transform.position = worldPoint + this.offset;

            // Check if location is valid.
            if (isValidLocation())
            {
                // Lock Coordinates of Gear.
                lastPosition = transform.position;
                renderer.material.color = Color.green;
            }
            else
            {
                renderer.material.color = Color.red;
            }
        }

        ///***********************************
        /// Collision Events
        ///     Get called even when component is disabled
        ///     For trigger enter/exit this desired
        ///***********************************

        // Called when two gears collide.
        // Record the number of gears we're hitting
        void OnTriggerEnter(Collider col)
        {
            numCollisions = numCollisions + 1;
            //Debug.Log("we collided with "+col.gameObject.name);
        }

        // Called when two gears separate.
        // Decrease number of gears hitting
        void OnTriggerExit(Collider col)
        {
            numCollisions = numCollisions - 1;
            if(numCollisions<0)
            {
                numCollisions = 0;
            }
            //Debug.Log("we just left" + col.gameObject.name);
        }
    }
}
