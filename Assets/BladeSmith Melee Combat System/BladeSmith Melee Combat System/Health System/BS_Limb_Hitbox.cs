using UnityEngine;
using System.Collections;

public class BS_Limb_Hitbox : MonoBehaviour {

	[Tooltip("For this limb to work, all you need to do is to attach any collider to it (set as NOT trigger!). Fill in the below variable and it's good to go! Just remember to adjust the Tag and Layer of this limb to be readable as a Target for your desired weapons!    [This is an Info bool - it does nothing, just holds the Tooltip.]")]
	public bool Info;
	[Tooltip("What is this Limb attached to? Select the desired BS Health script which stores the Health of this Object (for example in one of the limb's parents.")]
	public BS_Main_Health MainHealth;
	[Tooltip("Should this Limb's collider be disabled twhen the MainHealth dies?")]
	public bool DisableColliderOnDeath = true;

	private Collider TheCollider;

	void Start()
	{
		if (MainHealth == null) {
			Debug.LogError("Excuse me, but it looks like the object called "+transform.name+" is missing it's Main Health parent! Please, assign it before we can continue!");
		}
		if (GetComponent<Collider>() == null) {
			Debug.LogError("I'm truely sorry but the BS Limb on "+transform.name +" raports that it has no collider attached!");
		}

		TheCollider = GetComponent<Collider> ();

	}
	void LateUpdate()
	{
		if (MainHealth._health <= 0) {
			if(DisableColliderOnDeath)
			{
				TheCollider.enabled = false;
			}
			enabled = false;
		}


	}

}
