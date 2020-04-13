using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class NewHighscore : MonoBehaviour
{

    private string serverip = "https://bombermanscoremanager.herokuapp.com/";
    public string tableName;
    private string playerName, score;
    public static bool networkError;
    public GameObject text, window, connectionFailed, scoreSaved;

    private void Awake()
    {
        networkError = false;
    }

    IEnumerator InsertNewScore(string URL)
    {
        using (UnityWebRequest www = UnityWebRequest.Get(serverip + tableName + "/" + playerName.ToUpper() + "/" + score))
        {
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
                connectionFailed.SetActive(true);                        
            }else{
                scoreSaved.SetActive(true);          
            }            
        }
    }
  

    public void OkButton()
    {
        playerName = text.GetComponent<Text>().text;        
        score = Mathf.RoundToInt(ScoreManager.elapsedTime).ToString();
        StartCoroutine(InsertNewScore(serverip));
        window.SetActive(false);        
    }

    public void CancelButton()
    {
        window.SetActive(false);        
    }
}
