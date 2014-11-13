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
	public class GearCounter : MonoBehaviour
	{
		/**********************/
		/**  Internals Data  **/
		/**********************/
		// Used Gears include all gears that were placed and destroyed.
		private int gearsUsed;
		
		// Placed Gears includes only the gears that are currently active on the board.
		private int gearsPlaced;
		
		// Removed Gears includes only the gears that were removed by the player.
		private int gearsRemoved;
		
		// The Last In Place Gear. This script is used to lock gears.
		private DragDropDestroy lastDragDropGear = null;
		
		private Rect counterPanel;
		private string counterText;
		private GUIStyle counterStyle;
		
		private float defaultWidth;
		private float defaultHeight;
		
		private Vector3 screenScale;
		
		/**********************/
		/**  Externals Data  **/
		/**********************/
		public float xPosition;
		public float yPosition;
		
		public int FontSize;
		public Font FontType;
		public TextAnchor FontJustification;
		
		/**********************/
		/**   Initializers   **/
		/**********************/
		// Initialization Code
		protected void Start ()
		{
			// Set Base Variables
			counterPanel = new Rect(xPosition * defaultWidth, yPosition * defaultHeight, 1.0f, 1.0f);
			defaultWidth = 960.0f;
			defaultHeight = 540.0f;
			screenScale = Vector3.zero;
			
			gearsPlaced = 0;
			gearsUsed = 0;
			gearsRemoved = 0;
			
			// Generate Styling
			counterStyle = GUIStyle.none;
			counterStyle.font = FontType;
			counterStyle.fontSize = FontSize;
			counterStyle.alignment = FontJustification;
			counterStyle.normal.textColor = Color.white;
		}
		
		/**********************/
		/**     Updating     **/
		/**********************/
		protected void Update()
		{
			// Update Panel
			counterPanel = new Rect(xPosition * defaultWidth, yPosition * defaultHeight, 1.0f, 1.0f);
			
			// Update Styling
			counterStyle.font = FontType;
			counterStyle.fontSize = FontSize;
			counterStyle.alignment = FontJustification;
			counterStyle.normal.textColor = Color.white;
			
			// Update Text
			counterText = "Destroyed: " + getGearsRemoved().ToString();
		}
		
		protected void OnGUI()
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

			// Draw the Modified GUI Label here.
			GUI.Label(counterPanel, counterText, counterStyle);
			
			// Restore Matrix to the Current Default Matrix.
			GUI.matrix = cdMatrix;
		}
		
		/**********************/
		/** Accessor Methods **/
		/**********************/
		public int getGearsUsed()
		{ return gearsUsed; }
		
		public int getGearsPlaced()
		{ return gearsPlaced; }
		
		public int getGearsRemoved()
		{ return gearsRemoved; }
		
		/**********************/
		/** Operator Methods **/
		/**********************/
		public void addGear(DragDropDestroy gear)
		{
			if ((lastDragDropGear == null) || (lastDragDropGear != gear))
			{
				gearsPlaced = gearsPlaced + 1;
				gearsUsed = gearsUsed + 1;
			}

			if ((lastDragDropGear != null) && (lastDragDropGear != gear))
			{ lastDragDropGear.lockedPosition = true; }
			
			lastDragDropGear = gear;
		}
		
		public void removeGear(DragDropDestroy gear)
		{
			gearsPlaced = gearsPlaced - 1;
			gearsRemoved = gearsRemoved + 1;
			
			if (lastDragDropGear == gear)
			{ lastDragDropGear = null; }
		}
	}
}