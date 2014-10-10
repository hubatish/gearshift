using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace GearShift
{
    /// <summary>
    /// State manager for gears
    /// </summary>
    public class Gear : MonoBehaviour
    {
        public GearState state;

        protected void Start()
        {
            if(state!=null)
            {
                //By default select toolbox, but which GearState to use can also be dragged in
                state = gameObject.GetComponent<ToolBoxGearState>();
            }
            //Change to that state to ensure Activate messages are properly called
            ChangeState(state);
        }
        
        //Change current state and enable/disable appriopriate components?
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
            //really, only the held state needs a release button
            if (Input.GetMouseButtonUp(0))
            {
                state.Release();
            }
        }

        protected void OnMouseOver()
        {
            if(Input.GetMouseButtonDown(0))
            {
                state.Click();
            }
        }
    }
}
