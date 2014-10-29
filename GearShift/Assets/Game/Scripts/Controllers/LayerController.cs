using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace GearShift
{
    public class LayerController : MonoBehaviour
    {
        public List<Transform> layerPositions;

        protected Vector3 cameraOffset;

        public int curLayer = 0;

        protected void Start()
        {
            cameraOffset = Camera.main.transform.position - layerPositions[curLayer].position;
        }

        protected void Update()
        {
            float scrollWheel = Input.GetAxis("Mouse ScrollWheel");
            float upDown = Input.GetAxis("Vertical");
            if (scrollWheel > 0 || upDown > 0)
            {
                curLayer -= 1;
            }
            else if (scrollWheel < 0 || upDown < 0)
            {
                curLayer += 1;
            }
            curLayer = curLayer % (layerPositions.Count);
        }

        public float getCurrentZ()
        {
            return layerPositions[curLayer].position.z;
        }

        public float getCurrentY()
        {
            return layerPositions[curLayer].position.y;
        }
    }
}
