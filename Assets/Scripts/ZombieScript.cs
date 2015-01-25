using UnityEngine;
using System.Collections;

public class ZombieScript : MonoBehaviour {

	public float health = 100.0f;

	public float zombieScale = 1.0f;

	public float size = 1.0f;
	public float speed = 1.0f;

	public float knockStrength = 500.0f;
	public float knockTime = 0.1f;
	private bool knocked = false;

	private Rigidbody2D body;
	private GameObject loli;
	private Animator animator;

	public AudioClip zombieTauntSound;
	public AudioClip zombieDamageSound;
	public AudioClip zombieDieSound;
	public AudioClip bossTauntSound;
	public AudioClip bossDamageSound;
	public AudioClip bossDieSound;

	// Use this for initialization
	void Start () {
		body = GetComponent<Rigidbody2D> ();
		animator = GetComponent<Animator> ();
		loli = GameObject.Find ("Loli");
	}
	
	// Update is called once per frame
	void Update () {
		transform.localScale = new Vector3 (size, size, transform.localScale.z);

		if (!knocked) {
			Vector3 direction = loli.transform.position - transform.position;
			body.velocity = new Vector2 (direction.x, direction.y).normalized * speed;
		}

		Vector2 velocity = body.velocity;
		bool walking = false;
		int face = 0;
		if (velocity.x < 0) {
			walking = true;
			face = 1;
			transform.localScale = new Vector3(-zombieScale, zombieScale, 1.0f);
		} else if (velocity.x > 0) {
			walking = true;
			face = 1;
			transform.localScale = new Vector3(-zombieScale, zombieScale, 1.0f);
		}
		if (velocity.y > 0) {
			walking = true;
			face = 2;
		} else if (velocity.y > 0) {
			walking = true;
			face = 0;
		}
		animator.SetBool("Walking", walking);
		animator.SetInteger("Face", face);
	}

	public void Damage (float amount) {
		health -= amount;
		Debug.Log("damageSound");
		if (zombieScale > 2.0f) {
			audio.PlayOneShot (bossDamageSound);
		} else {
			audio.PlayOneShot (zombieDamageSound);
		}
		if (health <= 0.0f) {
			Die ();
		} else {
			knocked = true;
			Invoke ("Unknock", knockTime);

			Vector3 knockDirection = (transform.position - loli.transform.position).normalized;
			body.AddForce (new Vector2 (knockDirection.x, knockDirection.y) * knockStrength);
		}
	}

	public void Die () {
		Debug.Log("dieSound");
		if (zombieScale > 2.0f) {
			audio.PlayOneShot (bossDieSound);
		} else {
			audio.PlayOneShot (zombieDieSound);
		}
		Invoke ("DestroySelf", 0.0f);
		GameObject.Find("Manager").GetComponent<LevelManager>().zombieKilled++;
		if (zombieScale > 2.0f) {
			ScoreManager.AddScore (30);
		} else {
			ScoreManager.AddScore (10);
		}
	}

	private void DestroySelf() {
		Destroy (gameObject);
	}

	private void Unknock() {
		if (Random.Range (0, 10) > 7) {
			Debug.Log("tauntSound");
			if (zombieScale > 2.0f) {
				audio.PlayOneShot (bossTauntSound);
			} else {
				audio.PlayOneShot (zombieTauntSound);
			}
		}
		knocked = false;
	}
}
