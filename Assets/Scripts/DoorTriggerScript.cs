using UnityEngine;
using System.Collections;

public class DoorTriggerScript : MonoBehaviour {

	public string triggerTag;

	void OnTriggerEnter2D(Collider2D coll) {
		GameObject.Find ("Manager").SendMessage ("NewLevel", triggerTag);
	}
}
