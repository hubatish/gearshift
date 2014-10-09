using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace GearShift
{
    public class HeldGearState : GearState
    {
        protected InPlaceGearState inPlace;
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
        protected CapsuleCollider _capsule;
        protected float bigger = 0.75f;

        public override void Move()
        {
        }
        public override void Click() 
        {
           
        }
        public override void Release() 
        {
            //check for collisions
            master.ChangeState(inPlace);
        }
        public override void Activate() 
        {
            pointOnScreen = Camera.main.WorldToScreenPoint(transform.position);
            offset = transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, pointOnScreen.z));
            capsule.radius *= bigger;
        }
        public override void Deactivate()
        {
            lastPosition = transform.position;
            capsule.radius /= bigger;
            // Change to Deselected Graphics
            this.renderer.material.color = new Color(0.43529411764705882352941176470588f, 0.42352941176470588235294117647059f, 0.55686274509803921568627450980392f, 1.0f);
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

        float boundaryDistance;

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
        /**   Constructors   **/
        /**********************/

        /**********************/
        /**   Initializers   **/
        /**********************/
        // Use this for initialization
        protected override void Start()
        {
            collisions = 0;

            lastPosition = transform.position;

            inPlace = gameObject.GetComponent<InPlaceGearState>();
            base.Start();
        }

        /**********************/
        /**     Updating     **/
        /**********************/
        // Update is called once per frame
        void Update() { }

        /**********************/
        /** Accessor Methods **/
        /**********************/
        float getXPosition()
        { return this.transform.position.x; }

        float getYPosition()
        { return this.transform.position.y; }

        float getZPosition()
        { return this.transform.position.z; }

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
        /** Operator Methods **/
        /**********************/

        /**********************/
        /** Events / Drivers **/
        /**********************/
        // Called when the gear is selected.

        // Called when the selected gear is translated on the screen.
        void OnMouseDrag()
        {
            // Apply Mouse Coordinates of the Gear.
            Vector3 screenPoint = new Vector3(Input.mousePosition.x,Input.mousePosition.y,pointOnScreen.z);

            // Translate Mouse to Screen Coordinates.
            Vector3 worldPoint = Camera.main.ScreenToWorldPoint(screenPoint);
            this.transform.position = worldPoint + this.offset;

            // Apply Level Boundaries
/*            if (this.getXPosition() < -boundaryDistance)
            { this.setXPosition(-boundaryDistance); }

            if (this.getXPosition() > boundaryDistance)
            { this.setXPosition(boundaryDistance); }

            if (this.getZPosition() < -boundaryDistance)
            { this.setZPosition(-boundaryDistance); }

            if (this.getZPosition() > boundaryDistance)
            { this.setZPosition(boundaryDistance); }*/

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

        // Called when two gears collide.
        void OnTriggerEnter()
        { collisions = collisions + 1; }

        void OnTriggerStay()
        {
            //This gets called even when component is disabled, so return
            if(!enabled)
            {
                return;
            }
            transform.position = lastPosition;
        }

        // Called when two gears separate.
        void OnTriggerExit()
        { collisions = collisions - 1; }
    }
}