using UnityEngine;
using System.Collections;

namespace GearShift
{
	public class SceneTransitionScript : MonoBehaviour
	{
		public int sceneNum;

		// Use this for initialization
		protected void Start () {}
		
		// Update is called once per frame
		protected void Update () {}

		void OnMouseDown()
		{
			Application.LoadLevel(sceneNum);
		}
	}
}