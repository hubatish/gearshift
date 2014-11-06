using UnityEngine;
using System.Collections;

namespace GearShift
{
	public class ApplicationExitScript : MonoBehaviour
	{
		// Use this for initialization
		protected void Start () {}
		
		// Update is called once per frame
		protected void Update () {}

		void OnMouseDown()
		{
			Application.Quit();
		}
	}
}