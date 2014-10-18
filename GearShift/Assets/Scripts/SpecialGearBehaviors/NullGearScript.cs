using UnityEngine;
using System.Collections;

public class NullGearScript : MonoBehaviour {

	public float destructionRadius = 2f;
	public float blackHoleRadius = 2.5f;
	public GameObject blackHole;
	public GameObject destructionField;
	public bool hasDestroyed = false;
	public bool isNullifying = false;
	void Start(){
	}

	void Update () {

		// Destroy Gears within a certain radius
		if (this.tag == "Placed Gear" && hasDestroyed == false) {
			// Colliders within destruction radius
			Collider[] nearObjects = Physics.OverlapSphere(this.transform.position, destructionRadius);
			// Eventually add check if game object connected to each collider is deletable
			foreach(Collider nearObj in nearObjects){
				if(nearObj.tag == "Placed Gear" && nearObj != this.collider){ // Temporary check/limiter{
					Destroy(nearObj.gameObject);
				}
			}
			hasDestroyed = true;
		}

		// Prevent placement within a certain radius
		if (hasDestroyed && !isNullifying) {
			CapsuleCollider capCollider = (CapsuleCollider)GetComponent(typeof(CapsuleCollider));
			capCollider.radius = capCollider.radius * blackHoleRadius;
			isNullifying = true;
		}

		// Prevent rotation from affect the larger collider -- freeze rotation on the object w/out removing the rotater script

	}


}
