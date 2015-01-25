using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScoreScript : MonoBehaviour {

	private Text text;

	void Start() {
		text = GetComponent<Text> ();
	}

	void Update() {
		text.text = "Score:\n" + ScoreManager.score;
	}
}
