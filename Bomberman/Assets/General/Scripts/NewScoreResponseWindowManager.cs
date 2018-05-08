using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewScoreResponseWindowManager : MonoBehaviour {

    public GameObject connectionFailed, scoreSaved;	
	
    public void CloseButton()
    {
        scoreSaved.SetActive(false);
        connectionFailed.SetActive(false);        
    }
}
