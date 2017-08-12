using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenusBehaviour : MonoBehaviour {

	public static bool playerIsAlive, isPauseable;
	public static int enemyAmount;
	public GameObject stageClear, youDied, winLoseMenu, pauseMenu;
	GameObject[] enemies;

	void Start(){
		playerIsAlive = true;
		isPauseable = true;
		enemies = GameObject.FindGameObjectsWithTag ("Enemy");
		enemyAmount = enemies.Length;
	}

	void FixedUpdate(){		
		
		if (enemyAmount == 0) {
			isPauseable = false;
			stageClear.SetActive (true);
			winLoseMenu.SetActive(true);
			Time.timeScale = 0;
		}
		if (playerIsAlive == false) {
			isPauseable = false;
			youDied.SetActive (true);
			winLoseMenu.SetActive(true);
			Time.timeScale = 0;
		}

		if (Input.GetKeyDown(KeyCode.Escape)) {			
			if (isPauseable) {
				isPauseable = false;
				pauseMenu.SetActive (true);
				Time.timeScale = 0;
			}
		}

	}

}
