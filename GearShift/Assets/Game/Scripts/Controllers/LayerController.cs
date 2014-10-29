using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace GearShift
{
    /// <summary>
    /// Control moving between and placing gears between layers
    /// </summary>
    public class LayerController : MonoBehaviour
    {
        public List<Transform> layerPositions;

        //All the objects (camera, GUI, etc) that need to move with the layers should be parented under the commonObjectParent
        public Transform commonObjectParent;

        //public float moveDistance = 11f;

        public Transform curLayerTransform
        {
            get
            {
                return layerPositions[curLayer];
            }
        }

        protected Vector3 cameraOffset;

        public int curLayer = 0;

        protected void Start()
        {
            cameraOffset = Camera.main.transform.position - curLayerTransform.position;
        }

        protected bool isPressed = false;

        protected void Update()
        {
            //Get vertical scroll input and move layers based on it
            float scrollWheel = Input.GetAxis("Mouse ScrollWheel");
            float upDown = Input.GetAxis("Vertical");
            float error = 0.0001f;
            int newLayer = 0;
            if (scrollWheel > error || upDown > error)
            {
                newLayer -= 1;
            }
            else if (scrollWheel < -error || upDown < -error)
            {
                newLayer += 1;
            }
            //We've actually made a change, and this is the first change made
            if(newLayer!=0 && !isPressed)
            {
                MoveToLayer(newLayer+curLayer);
                isPressed = true;
            }
            //no input was taken this step
            if(newLayer == 0)
            {
                //get ready to accept more input
                isPressed = false;
            }
        }

        protected void MoveToLayer(int newLayer)
        {
            //make sure the layer is in bounds
            if(newLayer<0)
            {
                newLayer = layerPositions.Count + newLayer;
            }
            curLayer = newLayer % (layerPositions.Count);
            commonObjectParent.position = new Vector3(0, getCurrentY(), 0);
        }

        // Our game is aligned on the y axis right now
        // ie, gears are placed on the xz axis, and layers and camera move along the y axis
        public float getCurrentY()
        {
            return layerPositions[curLayer].position.y;
        }
    }
}
