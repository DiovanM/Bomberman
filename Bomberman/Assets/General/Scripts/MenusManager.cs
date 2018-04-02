using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenusManager : MonoBehaviour {

	public static bool playerIsAlive, isPauseable;
	public static int enemyAmount;
	public GameObject stageClear, youDied, winLoseMenu, pauseMenu;
	public AudioSource levelMusic, VictoryMusic;
	GameObject[] enemies, bombEater, boss;

	void Start(){
		playerIsAlive = true;
		isPauseable = true;
		enemies = GameObject.FindGameObjectsWithTag ("Enemy");
        bombEater = GameObject.FindGameObjectsWithTag("BombEater");
        boss = GameObject.FindGameObjectsWithTag("Boss");
        enemyAmount = bombEater.Length + enemies.Length + boss.Length;
    }

	void FixedUpdate(){        
        if (enemyAmount == 0) {
			levelMusic.Stop ();
			VictoryMusic.Play ();
			isPauseable = false;
			stageClear.SetActive (true);
			winLoseMenu.SetActive(true);
			Time.timeScale = 0;
		}
		if (playerIsAlive == false) {
			levelMusic.Stop ();
			isPauseable = false;
			youDied.SetActive (true);
			winLoseMenu.SetActive(true);
			Time.timeScale = 0;
		}

	}

	void Update(){
		if (Input.GetKeyDown(KeyCode.Escape)) {			
			if (isPauseable) {
				isPauseable = false;
				pauseMenu.SetActive (true);
				Time.timeScale = 0;
				levelMusic.Pause ();
			}
		}
	}

	public void Resume(){
		MenusManager.isPauseable = true;
		pauseMenu.SetActive (false);
		Time.timeScale = 1;
		levelMusic.Play ();
	}

    public void Load(int sceneNumber)
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(sceneNumber);
    }

}
