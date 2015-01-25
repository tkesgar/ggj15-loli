using UnityEngine;
using System.Collections;

public class SwordSpriteScript : MonoBehaviour {

	private SwordScript sword;

	void Start() {
		sword = GetComponentInParent<SwordScript> ();
	}

	void OnTriggerEnter2D(Collider2D other) {
		if (other.gameObject.tag == "Enemy") {
			// TODO implement scoring
			Debug.Log("Hit " + other.gameObject.name);
			other.gameObject.SendMessage("Damage", sword.strength);
		}
		transform.parent.SendMessage ("DestroySelf");
	}
}
