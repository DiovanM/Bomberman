using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtomFunctions : MonoBehaviour {

    public GameObject startButton, levelOne, levelTwo;

	public void Load(int sceneNumber){
		Time.timeScale = 1;
		SceneManager.LoadScene(sceneNumber);
	}

	public void Quit(){
		Application.Quit();
	}

    public void MenuButtons() {
        startButton.SetActive(false);
        levelOne.SetActive(true);
        levelTwo.SetActive(true);
    }

}
