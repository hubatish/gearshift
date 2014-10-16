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
        //The CapsuleCollider attached to this gameobject
        //Using a getter to only find the collider if/when we need it
        protected CapsuleCollider capsule
        {
            get
            {
                if(_capsule==null)
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
            //check for collisions
            master.ChangeState(inPlace);
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
        }

        /**********************/
        /** Internal Classes **/
        /**********************/

        /**********************/
        /**    Model Data    **/
        /**********************/
        private int collisions;

        private Vector3 pointOnScreen;
        private Vector3 offset;

        protected Vector3 lastPosition;

        public float boundaryDistance = 5f;

        protected Color prevColor;

        /**********************/
        /** Operator Methods **/
        /**********************/
        private bool isValidLocation()
        {
            if (this.collisions > 0)
            { return false; }
            else
            { return true; }
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
            collisions = 0;
            lastPosition = transform.position;
            pointOnScreen = Camera.main.WorldToScreenPoint(transform.position);
            offset = transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, pointOnScreen.z));
        }

        protected override void Awake()
        {
            //Find needed scripts
            inPlace = gameObject.GetComponent<InPlaceGearState>();
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
            Vector3 screenPoint = new Vector3(Input.mousePosition.x,Input.mousePosition.y,pointOnScreen.z);

            // Translate Mouse to Screen Coordinates.
            Vector3 worldPoint = Camera.main.ScreenToWorldPoint(screenPoint);
            this.transform.position = worldPoint + this.offset;

            // Apply Level Boundaries
            if (transform.position.x < -boundaryDistance)
            { this.setXPosition(-boundaryDistance); }

            if (transform.position.x > boundaryDistance)
            { this.setXPosition(boundaryDistance); }

            if (transform.position.z < -boundaryDistance)
            { this.setZPosition(-boundaryDistance); }

            if (transform.position.z > boundaryDistance)
            { this.setZPosition(boundaryDistance); }

            // Check if location is valid.
            if (this.isValidLocation())
            {
                // Lock Coordinates of Gear.
                lastPosition = transform.position;
                this.renderer.material.color = Color.green; 
            }
            else
            { 
                this.renderer.material.color = Color.red; 
            }
        }

        ///***********************************
        /// Collision Events
        ///     Get called even when component is disabled
        ///     For trigger enter/exit this desired
        ///***********************************

        // Called when two gears collide.
        // Record the number of gears we're hitting
        void OnTriggerEnter()
        { collisions = collisions + 1; }

        void OnTriggerStay()
        {
            // Shouldn't warp back when this state isn't enabled
            if(!enabled)
            {
                return;
            }
            //Otherwise, warp back previous position
            transform.position = lastPosition;
        }

        // Called when two gears separate.
        // Decrease number of gears hitting
        void OnTriggerExit()
        { collisions = collisions - 1; }
    }
}