using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOver : MonoBehaviour
{
    bool endOfGame;
    
    void Start() {
        endOfGame = false;
    }
    // Start is called before the first frame update
            private void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag == "ActivePlayer") {
            endOfGame = true;
        }
    }
    
    public bool gameIsOver() {
        return endOfGame;
    }
}
