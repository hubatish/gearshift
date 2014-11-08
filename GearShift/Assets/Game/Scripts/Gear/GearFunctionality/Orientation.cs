using UnityEngine;
using System.Collections;

namespace GearShift
{
	public class Orientation : MonoBehaviour
	{
		//private float Radius = .12f;
		private int TeethCount = 20;

		// Use this for initialization
		void Start()
		{

		}

		public float GetDegreeOffset(GameObject otherGear)
		{
			/*float circumfrence = 2 * Mathf.PI * Radius;
			float toothWidth = circumfrence / TeethCount;
			float OtherAngle = otherGear.GetComponent<Rotater>().currentRotation;

			Vector3 RelativeVector = otherGear.transform.position - transform.position;
			float RelativeAngle;
			if(RelativeVector.x==0)
			{
			 //oh no, bad!
			 RelativeAngle = 0;
			}
			else
			{
			 RelativeAngle = Mathf.Rad2Deg * Mathf.Atan(RelativeVector.z/RelativeVector.x);  
			}*/

			float TeethAngle = 360f / (TeethCount * 2);
			float OtherAngle = otherGear.GetComponent<Rotater>().currentRotation;
			float Remainder = OtherAngle % TeethAngle;

			return (Remainder + TeethAngle);

		}
	}
}