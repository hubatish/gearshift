using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace MathDash
{
    public class HeldGearState : GearState
    {
        protected InPlaceGearState inPlace;

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
        }
        public override void Deactivate()
        {
            lastPosition = transform.position;
        }

        

        /**********************/
        /** Internal Classes **/
        /**********************/

        /**********************/
        /**    Model Data    **/
        /**********************/
        private bool isMoveable;
        private int collisions;

        private Vector3 pointOnScreen;
        private Vector3 offset;

        protected Vector3 lastPosition;

        float boundaryDistance;

        /**********************/
        /**   Constructors   **/
        /**********************/

        /**********************/
        /**   Initializers   **/
        /**********************/
        // Use this for initialization
        void Start()
        {
            isMoveable = true;

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
        bool getMoveable()
        { return this.isMoveable; }

        float getXPosition()
        { return this.transform.position.x; }

        float getYPosition()
        { return this.transform.position.y; }

        float getZPosition()
        { return this.transform.position.z; }

        /**********************/
        /** Mutation Methods **/
        /**********************/
        // Needed for later "finalizing" gear placement
        void setMoveable(bool flag)
        { this.isMoveable = flag; }

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

            if (this.collisions == 0)
            {
                // Lock Coordinates of Gear.
                lastPosition = transform.position;
            }
            else
            {
                Debug.Log("there are collisions");
                // Revert to Old Position.
/*                this.setXPosition(this.xLastPosition);
                this.setYPosition(this.yLastPosition);
                this.setZPosition(this.zLastPosition);

                // Translate Mouse to Screen Coordinates.
                this.transform.position = Camera.main.ScreenToWorldPoint(this.transform.position) + this.offset;*/
                
            }
        
        }

        // Called when two gears collide.
        void OnCollisionEnter()
        { collisions = collisions + 1; }

        void OnCollisionStay()
        {
            //This gets called even when component is disabled, so return
            if(!enabled)
            {
                return;
            }
            transform.position = lastPosition;
        }

        // Called when two gears separate.
        void OnCollisionExit()
        { collisions = collisions - 1; }
    }
}