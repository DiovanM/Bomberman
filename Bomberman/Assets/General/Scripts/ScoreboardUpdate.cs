using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class ScoreboardUpdate : MonoBehaviour {

    public string ServerIP, tableName;
    private string URL;
    public static bool NetworkError;
    public GameObject[] level1Names = new GameObject[10];
    public GameObject[] level1Scores = new GameObject[10];
    public GameObject[] level2Names = new GameObject[10];
    public GameObject[] level2Scores = new GameObject[10];
    public GameObject[] level3Names = new GameObject[10];
    public GameObject[] level3Scores = new GameObject[10];
    public static Scores[] levelScore = new Scores[3];
    
    void Start () {
        NetworkError = true;
        URL = (ServerIP + tableName);
        StartCoroutine(GetLevel1Score(URL));
        StartCoroutine(GetLevel2Score(URL));
        StartCoroutine(GetLevel3Score(URL));
    }

    private void Awake()
    {
        DontDestroyOnLoad(transform.gameObject);
    }

    //Cada IEnumerator faz o request dos dados de score para cada fase e os aloca na tabela

    IEnumerator GetLevel1Score(string URL)
    {
        using (UnityWebRequest www = UnityWebRequest.Get(URL + '1'))
        {
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
                NetworkError = true;
            }
            else
            {
                levelScore[0] = JsonUtility.FromJson<Scores>(www.downloadHandler.text);
                for (int i = 0; i < levelScore[0].resp.Length; i++)
                {                    
                    level1Names[i].GetComponent<Text>().text = levelScore[0].resp[i].nome;  
                    level1Scores[i].GetComponent<Text>().text = levelScore[0].resp[i].score;
                   
                }
            }
        }
    }

    IEnumerator GetLevel2Score(string URL)
    {
        using (UnityWebRequest www = UnityWebRequest.Get(URL + '2'))
        {
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
                NetworkError = true;
            }
            else
            {
                levelScore[1] = JsonUtility.FromJson<Scores>(www.downloadHandler.text);
                for (int i = 0; i < levelScore[1].resp.Length; i++)
                {                    
                    level2Names[i].GetComponent<Text>().text = levelScore[1].resp[i].nome;
                    level2Scores[i].GetComponent<Text>().text = levelScore[1].resp[i].score;
                }
            }
        }
    }

    IEnumerator GetLevel3Score(string URL)
    {
        using (UnityWebRequest www = UnityWebRequest.Get(URL + '3'))
        {
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
                NetworkError = true;
            }
            else
            {
                NetworkError = false;
                levelScore[2] = JsonUtility.FromJson<Scores>(www.downloadHandler.text);
                for (int i = 0; i < levelScore[2].resp.Length; i++)
                {                    
                    level3Names[i].GetComponent<Text>().text = levelScore[2].resp[i].nome;
                    level3Scores[i].GetComponent<Text>().text = levelScore[2].resp[i].score;
                }
            }
        }
    }
}
