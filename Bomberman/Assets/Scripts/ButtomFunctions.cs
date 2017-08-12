using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtomFunctions : MonoBehaviour {

	public GameObject pauseMenu;

	public void Load(int sceneNumber){
		Time.timeScale = 1;
		SceneManager.LoadScene(sceneNumber);
	}

	public void Quit(){
		Application.Quit();
	}

	public void Resume(){
		MenusBehaviour.isPauseable = true;
		pauseMenu.SetActive (false);
		Time.timeScale = 1;
	}
}
