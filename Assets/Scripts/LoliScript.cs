using UnityEngine;
using System.Collections;

public class LoliScript : MonoBehaviour {

	public GameObject sword;

	public float speed = 3.0f;

	public int health = 3;

	public float knockStrength = 500.0f;
	public float knockTime = 0.1f;
	private bool knocked = false;

	private Rigidbody2D body;
	private Animator animator;

	public bool died = false;
	
	public AudioClip loliDieSound;
	public AudioClip loliAttackSound;
	public AudioClip loliAttackSound2;
	public AudioClip loliDamageSound;

	private GameObject gameOver;
	
	private bool returnToTitle = false;

	void Start() {
		body = GetComponent<Rigidbody2D> ();
		animator = GetComponentInChildren<Animator> ();
		gameOver = GameObject.Find ("gameover");
		gameOver.SetActive (false);
	}

	void Update() {

		if (returnToTitle) {
			if (Input.anyKeyDown) {
				Application.LoadLevel("TitleScene");
			}
		}

		if (!died && !knocked) {
			bool walking = false;
			int face = 0;
			if (Input.GetKey (KeyCode.LeftArrow)) {
					body.velocity = new Vector2 (-speed, body.velocity.y);
					walking = true;
					face = 1;
					animator.gameObject.transform.localScale = new Vector3 (-1.0f, 1.0f, 1.0f);
			} else if (Input.GetKey (KeyCode.RightArrow)) {
					body.velocity = new Vector2 (speed, body.velocity.y);
					walking = true;
					face = 1;
					animator.gameObject.transform.localScale = new Vector3 (1.0f, 1.0f, 1.0f);
			}
			if (Input.GetKey (KeyCode.UpArrow)) {
					body.velocity = new Vector2 (body.velocity.x, speed);
					walking = true;
					face = 2;
			} else if (Input.GetKey (KeyCode.DownArrow)) {
					body.velocity = new Vector2 (body.velocity.x, -speed);
					walking = true;
					face = 0;
			}
			animator.SetBool ("Walking", walking);
			animator.SetInteger ("Face", face);
			
			if (Input.GetKeyDown (KeyCode.W)) {
				Attack("north");
			} else if (Input.GetKeyDown (KeyCode.S)) {
				Attack("south");
			} else if (Input.GetKeyDown (KeyCode.A)) {
				Attack("west");
			} else if (Input.GetKeyDown (KeyCode.D)) {
				Attack("east");
			}
		}
	}

	public float swordSpeed = 0.5f;
	private bool canSword = true;

	public void Attack(string direction) {
		if (!SwordScript.hasSword && canSword) {
			float rotation = 0.0f;
			switch (direction) {
			case "south":
				rotation = 0.0f;
				break;
			case "east":
				rotation = 90.0f;
				break;
			case "north":
				rotation = 180.0f;
				break;
			case "west":
				rotation = 270.0f;
				break;
			default:
				break;
			}
			GameObject o = (GameObject) Instantiate (sword, transform.position, transform.rotation);
			o.transform.parent = transform;
			o.transform.Rotate (new Vector3 (0.0f, 0.0f, rotation));
			audio.PlayOneShot (Random.Range(0, 100) > 50 ? loliAttackSound : loliAttackSound2);

			canSword = false;
			Invoke("ReloadSword", swordSpeed);
		}
	}

	public void Damage(Vector2 origin) {
		health--;
		if (health <= 0) {
			collider2D.enabled = false;
			Die ();
		} else {
			knocked = true;
			Invoke ("Unknock", knockTime);
			audio.PlayOneShot (loliDamageSound);
			
			animator.SetTrigger ("Damage");
			Vector2 knockDirection = (new Vector2(transform.position.x, transform.position.y) - origin).normalized;
			body.AddForce (new Vector2 (knockDirection.x, knockDirection.y) * knockStrength);
		}
	}

	public void Die() {
		body.isKinematic = true;
		body.velocity = Vector2.zero;

		animator.SetTrigger ("Die");
		audio.PlayOneShot (loliDieSound);
		died = true;
		Invoke ("GameOverScene", 2.0f);
	}

	private void GameOverScene() {
		gameOver.SetActive (true);
		gameOver.audio.Play ();
		returnToTitle = true;
	}
	
	void OnCollisionEnter2D(Collision2D coll) {
		if (coll.gameObject.tag == "Enemy") {
			Damage(new Vector2(coll.gameObject.transform.position.x, coll.gameObject.transform.position.y));
		}
	}
	
	private void Unknock() {
		knocked = false;
	}

	private void ReloadSword() {
		canSword = true;
	}

	private void Heal() {
		health++;
	}
}
