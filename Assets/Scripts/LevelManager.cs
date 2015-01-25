using UnityEngine;
using System.Collections;

public class LevelManager : MonoBehaviour {
	
	public float zombieMinX;
	public float zombieMaxX;
	public float zombieMinY;
	public float zombieMaxY;
	public float zombieZ;

	public float minDistanceFromLoli = 1.5f;

	public GameObject zombie;
	public GameObject wall;
	private GameObject loli;
	
	public int zombieKilled = 0;
	public int zombieTarget = 0;
	public int zombieCount = 4;

	public float chanceForMegaZombie = 0.0f;

	public bool challengeCompleted = false;
	
	public Vector2 loliStartNorth;
	public Vector2 loliStartSouth;
	public Vector2 loliStartWest;
	public Vector2 loliStartEast;

	private GameObject doorblock;

	void Start() {
		loli = GameObject.Find ("Loli");
		wall = GameObject.Find ("Wall");
		doorblock = GameObject.Find ("DoorblockCollision");
		SetupLevel ();
	}

	public void NewLevel(string from) {
		// TODO fadeout-fadein
		if (from == "north") {
			loli.transform.position = loliStartSouth;
		} else if (from == "south") {
			loli.transform.position = loliStartNorth;
		} else if (from == "east") {
			loli.transform.position = loliStartWest;
		} else if (from == "west") {
			loli.transform.position = loliStartEast;
		}
		SetupLevel ();
	}

	public void SetupLevel() {

		wall.SendMessage ("Close");
		doorblock.SetActive(true);
		challengeCompleted = false;
		
		GameObject.Find("NorthTile").SendMessage("ClearTile");
		GameObject.Find("SouthTile").SendMessage("ClearTile");
		GameObject.Find("WestTile").SendMessage("ClearTile");
		GameObject.Find("EastTile").SendMessage("ClearTile");

		float dice = Random.Range (0, 1000) / 1000.0f;
		if (dice < chanceForMegaZombie) {
			int megaZombieCount = zombieCount / 3;
			if (megaZombieCount == 0) {
				megaZombieCount = 1;
			}
			PopulateMegaZombies (megaZombieCount);
			chanceForMegaZombie = 0.0f;
			wall.GetComponent<AudioSource>().pitch = 2.0f;
		} else {
			PopulateZombies (zombieCount);
			wall.GetComponent<AudioSource>().pitch = 1.0f;
		}
	}

	public void Update() {
		if (zombieKilled == zombieTarget) {
			challengeCompleted = true;
			loli.SendMessage("Heal");
			ScoreManager.AddScore(100);
			zombieCount++;
			chanceForMegaZombie += 0.2f;
			GameObject.Find("NorthTile").SendMessage("RandomTile");
			GameObject.Find("SouthTile").SendMessage("RandomTile");
			GameObject.Find("WestTile").SendMessage("RandomTile");
			GameObject.Find("EastTile").SendMessage("RandomTile");
			zombieTarget = 999;
		}

		if (challengeCompleted) {
			wall.SendMessage("Open");
			doorblock.SetActive(false);
		}
	}

	void PopulateZombies(int count) {
		for (int i = 0; i < count; i++) {
			float loliX = loli.transform.position.x;
			float loliY = loli.transform.position.y;
			float randomX = Random.Range(zombieMinX, zombieMaxX);
			float randomY = Random.Range(zombieMinY, zombieMaxY);
			while (Vector2.Distance(new Vector2(randomX, randomY), new Vector2(loliX, loliY)) < minDistanceFromLoli) {
				randomX = Random.Range(zombieMinX, zombieMaxX);
				randomY = Random.Range(zombieMinY, zombieMaxY);
			}
			Instantiate(zombie, new Vector3(randomX, randomY, zombieZ), Quaternion.identity);
		}
		zombieKilled = 0;
		zombieTarget = count;
	}
	
	void PopulateMegaZombies(int count) {
		Debug.Log ("Mega Zombie!");
		for (int i = 0; i < count; i++) {
			float loliX = loli.transform.position.x;
			float loliY = loli.transform.position.y;
			float randomX = Random.Range(zombieMinX, zombieMaxX);
			float randomY = Random.Range(zombieMinY, zombieMaxY);
			while (Vector2.Distance(new Vector2(randomX, randomY), new Vector2(loliX, loliY)) < minDistanceFromLoli) {
				randomX = Random.Range(zombieMinX, zombieMaxX);
				randomY = Random.Range(zombieMinY, zombieMaxY);
			}
			GameObject o = (GameObject) Instantiate(zombie, new Vector3(randomX, randomY, zombieZ), Quaternion.identity);
			o.GetComponent<ZombieScript>().zombieScale = 3.0f;
			o.GetComponent<ZombieScript>().speed /= 1.25f;
			o.GetComponent<ZombieScript>().health *= 3.0f;
		}
		zombieKilled = 0;
		zombieTarget = count;
	}
}
