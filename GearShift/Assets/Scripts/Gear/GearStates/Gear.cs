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
    /// State manager for gears
    /// Handles state changes and sends out Click, Release, Activate/Deactivate messages to all the individual gears.
    /// </summary>
    public class Gear : MonoBehaviour
    {
		/**********************/
		/**    Model Data    **/
		/**********************/
        public GearState state;

		/**********************/
		/**   Initializers   **/
		/**********************/
        protected void Awake()
        {
            if (state == null)
            {
                // defaults to Toolbox on create.
                state = gameObject.GetComponent<ToolBoxGearState>();
            }

            //Make sure Activate is called for starting script.
            if (!state.enabled)
            { state.enabled = true; }
			
            state.Activate();
        }
		
		// Default Start Method.
		protected void Start() {}

		/**********************/
		/**     Updating     **/
		/**********************/
		// Update is called once per frame.
        protected void Update()
        {
            //Send out release message.
            if (Input.GetMouseButtonUp(0))
            { state.Release(); }
        }
		
		/**********************/
		/** Mutation Methods **/
		/**********************/
		//Change current state and enable/disable appropriate components.
        public void ChangeState(GearState newState)
        {
            state.Deactivate();
            state.enabled = false;
            state = newState;
            state.enabled = true;
            newState.Activate();
        }

		/**********************/
		/** Events / Drivers **/
		/**********************/
        protected void OnMouseOver()
        {
            //Send out click to the current state
            if (Input.GetMouseButtonDown(0))
            { state.Click(); }
        }
    }
}
