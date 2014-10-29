using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace GearShift
{
    /// <summary>
    /// Let the world know which game object is the current parent of all the gears
    ///     it changes whenever we switch layers
    /// </summary>
    public class GearParent : Singleton<GearParent>
    {
        public LayerController layers;

        protected string parentName = "GearParent";

        public Transform gearParent
        {
            get
            {
                Transform curParent = layers.curLayerTransform.FindChild(parentName);
                if(curParent!=null)
                {
                    return curParent;
                }
                //we couldn't find it under the layer, create an empty one!
                curParent = (new GameObject()).transform;
                curParent.gameObject.name = parentName;
                curParent.transform.parent = layers.curLayerTransform;
                curParent.transform.position = new Vector3(0,0,0);
                return curParent;
            }
        }
    }
}
