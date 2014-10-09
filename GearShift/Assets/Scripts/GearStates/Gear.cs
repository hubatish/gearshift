using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace GearShift
{
    public class Gear : MonoBehaviour
    {
        public GearState state;

        protected void Start()
        {
            //Start in wanderer state by default
            state = gameObject.GetComponent<ToolBoxGearState>();
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
            state.Move();

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
