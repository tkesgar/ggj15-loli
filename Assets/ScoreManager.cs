using UnityEngine;
using System.Collections;

public class ScoreManager : MonoBehaviour {

	public static int score = 0;

	void Start() {
		score = 0;
	}

	public static void AddScore(int amount) {
		score += amount;
	}
}
