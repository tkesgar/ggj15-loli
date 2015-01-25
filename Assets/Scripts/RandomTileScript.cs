using UnityEngine;
using System.Collections;

public class RandomTileScript : MonoBehaviour {

	void Start() {
		ClearTile ();
	}

	public void RandomTile() {
		((SpriteRenderer)renderer).sprite = Resources.Load<Sprite> (@"tile (" +  Random.Range(1, 11) + ")");
	}

	public void ClearTile() {
		((SpriteRenderer)renderer).sprite = null;
	}
}
