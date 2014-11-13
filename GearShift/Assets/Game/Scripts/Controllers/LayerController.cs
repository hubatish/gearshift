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
    public class LayerController : MonoBehaviour
    {
		/**********************/
		/** Custom Datatypes **/
		/**********************/
		[System.Serializable]
		public class Layer
		{
			public Transform LayerCoordinates;
			public GameObject GearContainer;
			public GameObject ObstacleContainer;
			public Renderer LayerSplash;
		}
		
		[System.Serializable]
		public class ViewPort
		{
			public GameObject View;
			public float Offset;
		}
		
		/**********************/
		/**  Internals Data  **/
		/**********************/
		private int curLayer;
		private bool wasToggled;
		
		/**********************/
		/**  Externals Data  **/
		/**********************/
		public ViewPort viewPort;
		public List<Layer> layerList;
		public int startLayerElement;

		/**********************/
		/**   Initializers   **/
		/**********************/
		protected void Start()
		{
			wasToggled = false;
			curLayer = startLayerElement;
		}

		/**********************/
		/**     Updating     **/
		/**********************/
        protected void Update()
        {
			// Mouse / Keyboard Inputs to change Layers.
            float mWheelAxes = Input.GetAxis("Mouse ScrollWheel");
            float upDownAxes = Input.GetAxis("Vertical");
			
			// Delta blocks accidental axes movements.
            float delta = 0.0001f;
			
			// Occurs when there was a break between level switches, and the user initiated a switch
            if (((mWheelAxes > delta) || (upDownAxes > delta)) && (!wasToggled))
            {
				// Disable the previous Layer Splash
				layerList[curLayer].LayerSplash.enabled = false;
                curLayer = ToggleLayer(curLayer - 1);
                wasToggled = true;
            }
            else if (((mWheelAxes < -delta) || (upDownAxes < -delta)) && (!wasToggled))
            {
				// Disable the previous Layer Splash
				layerList[curLayer].LayerSplash.enabled = false;
                curLayer = ToggleLayer(curLayer + 1);
                wasToggled = true;
            }
			else // User is no longer attempting to switch. Stops rapid level switches.
			{ wasToggled = false; }
			
			// Enable the new Layer Splash
			layerList[curLayer].LayerSplash.enabled = true;
			viewPort.View.transform.position = layerList[curLayer].LayerCoordinates.transform.position;
        }
		
		/**********************/
        /** Accessor Methods **/
        /**********************/
		public int getCurrentLayer()
		{ return curLayer; }
		
		/**********************/
        /** Operator Methods **/
        /**********************/
		public int ToggleLayer(int newLayer)
		{
			if (newLayer >= layerList.Count)
			{ return 0; }
			else if (newLayer < 0)
			{ return layerList.Count - 1; }
			else
			{ return newLayer; }
		}
    }
}
