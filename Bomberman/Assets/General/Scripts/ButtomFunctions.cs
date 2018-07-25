using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtomFunctions : MonoBehaviour {

    public GameObject startButton, exitButton, scoreboardButton, returnMenu, levelOne, levelTwo, boss, scoreboard, errorWindow;

    public void Start()
    {
        ScoreboardUpdate.onNetworkError += EnableNetworkErrorWindow;
    }

    public void Load(int sceneNumber){
		Time.timeScale = 1;
		SceneManager.LoadScene(sceneNumber);
	}

	public void Quit(){
		Application.Quit();
	}

    public void Scores()
    {
        startButton.SetActive(false);
        exitButton.SetActive(false);
        scoreboardButton.SetActive(false);
        scoreboard.SetActive(true);        
    }

    public void CloseErrorWindow()
    {
        errorWindow.SetActive(false);
    }

    public void Return()
    {
        startButton.SetActive(true);
        exitButton.SetActive(true);
        scoreboardButton.SetActive(true);
        scoreboard.SetActive(false);
    }

    public void MenuButtons() {
        startButton.SetActive(false);
        levelOne.SetActive(true);
        levelTwo.SetActive(true);
        boss.SetActive(true);
        scoreboardButton.SetActive(false);
        returnMenu.SetActive(true);
    }

    public void ReturnMenu()
    {
        startButton.SetActive(true);
        levelOne.SetActive(false);
        levelTwo.SetActive(false);
        boss.SetActive(false);
        scoreboardButton.SetActive(true);
        returnMenu.SetActive(false);
    }

    public void EnableNetworkErrorWindow()
    {
        errorWindow.SetActive(true);
    }

}
