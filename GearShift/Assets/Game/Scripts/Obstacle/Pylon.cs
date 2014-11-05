using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace GearShift
{
    /// <summary>
    /// Transfer energy to another pylon
    /// </summary>
    public class Pylon : Obstacle
    {
        public TextMesh letter;

        protected string myText
        {
            get
            {
                return letter.text;
            }
        }

        public Rotater connectedRotater;

        public void ConnectToOtherRotater(Rotater rotater)
        {
            rotater.attachedGears.Add(connectedRotater);
            connectedRotater.attachedGears.Add(rotater);
        }

        protected static Dictionary<string, PylonPair> pylonPairs = new Dictionary<string,PylonPair>();
 
        protected void Awake()
        {
            connectedRotater = gameObject.GetComponent<Rotater>();
            if(!pylonPairs.Keys.Contains(myText))
            {
                pylonPairs[myText] = new PylonPair(this);
            }
            else
            {
                pylonPairs[myText].AddPylon(this);
                ConnectToOtherRotater(pylonPairs[myText].GetOtherPylon(this).connectedRotater);
            }
        }

        public override void PowerOn()
        {
            if(on)
            {
                //don't perform events when already powered on
                return;
            }
            base.PowerOn();
        }
    }

    public class PylonPair 
    {
        public Pylon pylon1;
        public Pylon pylon2;
        public PylonPair(Pylon pylon)
        {
            pylon1 = pylon;
        }

        public void AddPylon(Pylon pylon)
        {
            pylon2 = pylon;
        }

        public Pylon GetOtherPylon(Pylon pylon)
        {
            if(pylon1==pylon)
            {
                return pylon2;
            }
            else if(pylon2==pylon)
            {
                return pylon1;
            }
            else
            {
                Debug.LogError("There is no other pylon attached");
                return null;
            }
        }
    }
}
