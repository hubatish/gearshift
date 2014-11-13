//
// Copyright © 2014 GearShift Studios, All Rights Reserved
//
// THIS SOFTWARE IS PROVIDED BY THE AUTHORS "AS IS" AND ANY EXPRESS OR
// IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED WARRANTIES
// OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE DISCLAIMED.
// IN NO EVENT SHALL THE AUTHORS BE LIABLE FOR ANY DIRECT, INDIRECT,
// INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING, BUT
// NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES; LOSS OF USE,
// DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND ON ANY
// THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT
// (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF
// THIS SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.
//

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace GearShift
{
    /// <summary>
    ///
    /// </summary>
    public class DragDropDestroy : MonoBehaviour
    {
		/**********************/
		/**   Enumerations   **/
		/**********************/
		public enum GearLocation{Toolbox, Layer, Obstacle};
		
		/**********************/
		/**  Reference Data  **/
		/**********************/
		private Color matColor;
		private GearCounter gearCounter;
		private CapsuleCollider gearCollider;
		private Rotation rotationController;
		private LayerController layerController;
		
		/**********************/
		/**  Internals Data  **/
		/**********************/
        private Vector3 mouseOffset;
		private Vector3 pointWorldScreen;
        private Vector3 lastPosition;
		
		private int collisionCount;
		private float collisionModifier;
		
		/**********************/
		/**  Externals Data  **/
		/**********************/
		public GearLocation location;
		public bool lockedPosition = false;

		/**********************/
		/**   Initializers   **/
		/**********************/
		protected void Start()
		{
			// Locate References
			matColor = renderer.material.color;
			rotationController = gameObject.GetComponent<Rotation>();
			gearCollider = gameObject.GetComponent<CapsuleCollider>();
			gearCounter = GameObject.FindWithTag("GearCounter").GetComponent<GearCounter>();
			layerController = GameObject.FindWithTag("LayerController").GetComponent<LayerController>();
			
			// Set Base Variables
			lastPosition = transform.position;
			collisionCount = 0;
			collisionModifier = -0.02f;
		}

		/**********************/
		/**     Updating     **/
		/**********************/
        protected void Update() {}
		
		/**********************/
        /** Accessor Methods **/
        /**********************/
		public GearLocation getGearLocation()
		{ return this.location; }
		
		/**********************/
        /** Operator Methods **/
        /**********************/
		// Returns true if the gear is not colliding with anything
        private bool isValidLocation()
        { return (collisionCount == 0); }

		/**********************/
        /** Events / Drivers **/
        /**********************/
		// Called when the Left-Mouse Button is Pressed
		protected void OnMouseDown()
        {
			if (lockedPosition == false)
			{
				// Factor in the layer's height when placing the gear
				float layerFactor = -layerController.layerList[layerController.getCurrentLayer()].LayerCoordinates.position.y;
				
				// Get the Offset of the Mouse on selection
				// X = Input Mouse X
				// Y = Input Mouse Y
				// Z = Distance from Camera to Layer
				mouseOffset = transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, layerFactor + 10.75f));
				
				// Disable Forces, Motions, and Collisions temporarily
				rigidbody.isKinematic = true;
				
				// Adjust Collision Bounds temporarily
				gearCollider.radius = gearCollider.radius + collisionModifier;
				
				// Stop the Rotation of the Gear temporarily
				if (rotationController != null)
				{ rotationController.enabled = false; }
			}
			else if (location != GearLocation.Obstacle)
			{
				gearCounter.removeGear(this);
				Destroy(this.gameObject);
			}
        }
		
        // Called when the Mouse is Dragged
        protected void OnMouseDrag()
        {
			// Factor in the layer's height when placing the gear
			float layerFactor = -layerController.layerList[layerController.getCurrentLayer()].LayerCoordinates.position.y;
			
            // Apply Mouse Coordinates of the Gear
			// X = Input Mouse X
			// Y = Input Mouse Y
			// Z = Distance from Camera to Layer
            this.transform.position = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, layerFactor + 10.75f)) + mouseOffset;
			
			// Fix the Y Component
			this.transform.position = new Vector3(this.transform.position.x, -layerFactor, this.transform.position.z);

            // Check if location is valid
            if (isValidLocation())
            { renderer.material.color = Color.green; }
            else
            { renderer.material.color = Color.red; }
			
			// Stop the Rotation of the Gear temporarily
			if (rotationController != null)
			{ rotationController.enabled = false; }
        }
		
		// Called when the Left-Mouse Button is Released
		protected void OnMouseUp()
        {
			// Check if location is valid
			if (isValidLocation())
			{
				lastPosition = transform.position;
				location = GearLocation.Layer;
				
				if (rotationController != null)
				{ rotationController.enabled = true; }
				gearCounter.addGear(this);
				this.transform.parent = layerController.layerList[layerController.getCurrentLayer()].GearContainer.transform;
			}
			else
			{
				transform.position = lastPosition;
			}
			
			// Reset Original Material Color
			renderer.material.color = matColor;
			
			// Enable Forces, Motions, and Collisions
			rigidbody.isKinematic = false;
			
			// Reset Collision Bounds
			gearCollider.radius = gearCollider.radius - collisionModifier;
        }
		
        // Called when the gear collides with any collider
        protected void OnTriggerEnter(Collider col)
        { collisionCount = collisionCount + 1; }
		
        // Called when the gear separates from a collider
        protected void OnTriggerExit(Collider col)
        { collisionCount = collisionCount - 1; }
    }
}
