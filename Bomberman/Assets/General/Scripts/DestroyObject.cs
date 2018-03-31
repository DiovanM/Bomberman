using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyObject : MonoBehaviour {

    public void DestroyMe()
    { //Utilizado no animation event ao final da animação
        Destroy(gameObject);
    }
}
