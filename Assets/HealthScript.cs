using UnityEngine;
using System.Collections;

public class HealthScript : MonoBehaviour {

	private LoliScript loli;

	private int lastHealth;

	public GameObject heart;

	private GameObject[] hearts;

	void Start() {
		loli = GameObject.Find ("Loli").GetComponent<LoliScript> ();
		lastHealth = loli.health;

		hearts = new GameObject[10];
		for (int i = 0; i < 10; i++) {
			hearts[i] = (GameObject) Instantiate(heart);

			int row = i / 5;
			int col = i % 5;
			hearts[i].transform.position = new Vector3(-4.5f + col * 0.3f, -4.125f + row * 0.3f, 0.0f);
		}
		UpdateHealth();
	}

	void Update () {
		if (lastHealth != loli.health) {
			UpdateHealth();
		}
	}

	void UpdateHealth() {
		lastHealth = loli.health;
		foreach (GameObject o in hearts) {
			o.SetActive(false);
		}
		for (int i = 0; i < lastHealth; i++) {
			hearts[i].SetActive(true);
		}
	}
}
