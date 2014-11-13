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

using UnityEngine;
using System.Collections;

namespace GearShift
{
	/// <summary>
	/// 
	/// </summary>
	public class NullGearScript : MonoBehaviour
	{
		/**********************/
		/**   Enumerations   **/
		/**********************/
		
		/**********************/
		/**  Reference Data  **/
		/**********************/
		private DragDropDestroy dragDropDestroy;
		
		/**********************/
		/**  Internals Data  **/
		/**********************/
		private bool hasDestroyed = false;
		private bool isNullifying = false;
		
		private GearCounter counter;
		
		/**********************/
		/**  Externals Data  **/
		/**********************/
		public float destructionRadius = 2f;
		public float blackHoleRadius = 2.5f;
		public GameObject blackHole;
		public GameObject destructionField;

		/**********************/
		/**   Initializers   **/
		/**********************/
		protected void Start()
		{ dragDropDestroy = gameObject.GetComponent<DragDropDestroy>(); }

		/**********************/
		/**     Updating     **/
		/**********************/
        protected void Update()
		{
			// Trigger if Null Gear has not destroyed anything yet
			if ((!hasDestroyed) && (dragDropDestroy.getGearLocation() == DragDropDestroy.GearLocation.Layer))
			{
				// Get all Colliders within range of Null Gear's destruction effect
				Collider[] colObjects = Physics.OverlapSphere(this.transform.position, destructionRadius);
				
				// Iterate through the Colliders, destroying any placed gears
				foreach (Collider colObject in colObjects)
				{
					GameObject obj = colObject.transform.gameObject;
					DragDropDestroy script = obj.GetComponent<DragDropDestroy>();
					
					// Call when target does not have Drag, Drop, Destroy
					if (script != null)
					{
						// Do not let the Power Gears get destroyed
						if ((script.getGearLocation() != DragDropDestroy.GearLocation.Toolbox) && (script.getGearLocation() != DragDropDestroy.GearLocation.Obstacle))
						{ Destroy(obj); }
					}
				}
				
				hasDestroyed = true;
			}

			// Prevent placement within a certain radius
			if (hasDestroyed && !isNullifying)
			{
				CapsuleCollider capCollider = (CapsuleCollider)GetComponent(typeof(CapsuleCollider));
				capCollider.radius = capCollider.radius * blackHoleRadius;
				isNullifying = true;
			}

			//transform.rotation = Quaternion.AngleAxis(30, Vector3.up);
		}
		
		/**********************/
        /** Accessor Methods **/
        /**********************/
		
		/**********************/
        /** Operator Methods **/
        /**********************/

		/**********************/
        /** Events / Drivers **/
        /**********************/
	}
}