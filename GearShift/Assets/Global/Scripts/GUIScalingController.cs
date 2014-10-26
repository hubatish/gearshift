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
    /// GUI Scaling Controller
	/// Revises the scaling of GUI elements based on window size.
    /// </summary>
	public class GUIScalingController : MonoBehaviour
	{
		/**********************/
		/**    Model Data    **/
		/**********************/
		private float defaultWidth;
		private float defaultHeight;
		
		private Vector3 screenScale;

		/**********************/
		/**   Initializers   **/
		/**********************/
		// Initialization Code
		protected void Start ()
		{
			// define here the original resolution
			// you used to create the GUI contents
			defaultWidth = 1024.0f;
			defaultHeight = 768.0f;
			
			screenScale = Vector3.zero;
		}
		
		/**********************/
		/**     Updating     **/
		/**********************/
		// Default Update Method.
		protected void Update () { }
		
		/**********************/
		/**       Draw       **/
		/**********************/
		protected void OnGUI ()
		{
			// Calculate Scaling
			screenScale.x = Screen.width / defaultWidth;
			screenScale.y = Screen.height / defaultHeight;
			
			// Z Scaling is left to default. We only care about scaling x and y for screen resolution.
			screenScale.z = 1.0f;
			
			// Get the Current Default Matrix of position, rotation, and scaling for the GUI.
			Matrix4x4 cdMatrix = GUI.matrix;
			
			// Perform the scale revision on the GUI Matrix.
			GUI.matrix = Matrix4x4.TRS(Vector3.zero, Quaternion.identity, screenScale);

			// Restore Matrix to the Current Default Matrix.
			GUI.matrix = cdMatrix;
		}
	}
}