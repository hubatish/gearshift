using UnityEngine;
using System.Collections;

namespace GearShift
{
	public class SceneTransitionScript : MonoBehaviour
	{
		public Object scene;

		// Use this for initialization
		protected void Start () {}
		
		// Update is called once per frame
		protected void Update () {}

		void OnMouseDown()
		{
			if (scene != null)
			{ Application.LoadLevel((scene).name); }
		}
	}
}