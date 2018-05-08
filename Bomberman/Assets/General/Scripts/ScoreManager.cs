using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour {

    public static float elapsedTime = 0;
    private float lastTime = 0;
    public GameObject time, score;
	
	void Update () {
        if (MenusManager.playerIsAlive)
        {                               
            elapsedTime += Time.deltaTime;
            lastTime += Time.deltaTime;
            if (lastTime > 1)
                lastTime -= lastTime;
                time.GetComponent<Text>().text = Mathf.RoundToInt(elapsedTime).ToString();          
        }
	}
}
