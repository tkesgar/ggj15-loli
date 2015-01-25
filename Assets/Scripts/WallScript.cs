using UnityEngine;
using System.Collections;

public class WallScript : MonoBehaviour {

	public Sprite spriteClosed;
	public Sprite spriteOpened;

	private bool actuallyOpen = false;
	public bool open = false;

	// Use this for initialization
	void Start () {
		Close ();
	}

	public void Update() {
		if (open != actuallyOpen) {
			if (open) {
				Open ();
			} else {
				Close ();
			}
			actuallyOpen = open;
		}
	}

	public void Open() {
		((SpriteRenderer) renderer).sprite = spriteOpened;
	}

	public void Close() {
		((SpriteRenderer) renderer).sprite = spriteClosed;
	}
}
