using UnityEngine;
using System.Collections;

public class SwordScript : MonoBehaviour {
	
	public static bool hasSword = false;
	
	public float lifetime = 0.1f;

	public float strength = 25.0f;
	
	void Start () {
		hasSword = true;
		Invoke ("DestroySelf", lifetime);
	}
	
	void DestroySelf() {
		Destroy(gameObject);
		hasSword = false;
	}
}
