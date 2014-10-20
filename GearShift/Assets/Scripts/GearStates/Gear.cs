using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace GearShift
{
    /// <summary>
    /// State manager for gears
    /// Handles state changes
    ///     and sends out Click, Release, Activate/Deactivate messages to all the individual gears
    /// </summary>
    public class Gear : MonoBehaviour
    {
        public GearState state;

        protected void Awake()
        {
            if (state == null)
            {
                // Set to Null if none exists. Prefab Gear defaults to Toolbox on create.
                state = gameObject.GetComponent<NullGearState>();
            }

            //Make sure Activate is called for starting script
            if (!state.enabled)
            {
                state.enabled = true;
            }
            state.Activate();

        }
		
		protected void Start()
        { }
        
        //Change current state and enable/disable appropriate components.
        public void ChangeState(GearState newState)
        {
            state.Deactivate();
            state.enabled = false;
            state = newState;
            state.enabled = true;
            newState.Activate();
        }

        protected void Update()
        {
            //Send out release message
            if (Input.GetMouseButtonUp(0))
            {
                state.Release();
            }
        }

        protected void OnMouseOver()
        {
            //Send out click to the current state
            if(Input.GetMouseButtonDown(0))
            {
                state.Click();
            }
        }
    }
}
