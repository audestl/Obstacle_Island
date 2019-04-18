using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShieldActivator : MonoBehaviour
{
    //private bool shieldOn; 
    public Image timer;
    public Text shieldText;
    private string keyText;
    private float currCountdownValue;
    private int index;
    
    private ShieldTimer stimer;
    //public GameObject st;
    
   private bool shieldOn; 
   private bool shieldActivated;
    
    private GameObject icon;
    private Pickupper actionPickup;
    
    
    private GameObject shield;
    private MeshRenderer render;
    private Collider collider;
    
    private GameObject clone;
    public GameObject prefab;
    public GameObject pos;
    
    private CesarOpen door;
    
    
        private void Start() {
               // timer
            
    shield = GameObject.FindWithTag("Shield");
    render = shield.GetComponent<MeshRenderer>();
    collider = shield.GetComponent<Collider>();
    actionPickup = GetComponent<Pickupper>();
            
        //finished = GameObject.Find("Portal").GetComponent<GameOver>();
        door = GameObject.Find("Door").GetComponent<CesarOpen>();
            
        shieldActivated = false;
        index = 0;
        icon = GameObject.Find("shieldToken");
            
        shieldText.enabled = false;
        //keyText.enabled = false;
        shieldText.text = "";
            
                   
        //st = GameObject.FindWithTag("ShieldTimer");
        stimer = timer.GetComponent<ShieldTimer>();
        timer.enabled = false;
    }
    
    private void Update() {
            
           if(actionPickup.hasKey()) keyText = "Press 'F' to open the door";
            else keyText = "You need a key to open this door";
        

                if (!stimer.timeEqualZero()) {
                if (shieldOn)
                {
                    render.enabled = true;
                    collider.enabled = true;
                    stimer.playTimer();

                }
                else
                {
                    render.enabled = false;
                    collider.enabled = false;
                    stimer.stopTimer();
                }
        }
        else {
            stimer.resetTimer();
            shieldActivated = false;
            render.enabled = false;
            collider.enabled = false;
        }
        
        
                if(shieldActivated) {
                    timer.enabled = true;
                    icon.GetComponent<MeshRenderer>().enabled = false;
                    icon.GetComponent<Collider>().enabled = false;
                    
        if (Input.GetKeyDown(KeyCode.M))
        {
            if (!shieldOn){
            shieldOn = true;
            actionPickup.dropIt();          
            } else {
            shieldOn = false;
    }
        }    
            
              } else {
        
                    shieldOn = false;
                    timer.enabled = false;
                    icon.GetComponent<MeshRenderer>().enabled = true;
                    icon.GetComponent<Collider>().enabled = true;                 
                }                            
       }
    
    
    
public bool shieldIsOn() {
    return shieldOn;
}
//    
//    public void iconAvailable(GameObject _icon) {  
//        if(!shieldOn){
//        _icon.SetActive(true);    
//        _icon.GetComponent<MeshRenderer>().enabled = true;
//        _icon.GetComponent<Collider>().enabled = true;
//        timer.enabled = false;
//        shieldOn = true;
//        } else {
//            shieldOn = false;
//            _icon.gameObject.SetActive(false);
//            timer.enabled = true;
//             if (index == 1) StartCoroutine(StartCountdown(8, "Press 'm' on your keyboard to activate your shield. Be careful, its lifespan is very limited!"));
//        }
//    }
        
            private void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag == "ShieldToken") {
            index++;
            shieldActivated = true;
            if (index == 1) StartCoroutine(StartCountdown(8, "Press 'm' on your keyboard to activate your shield. Be careful, its lifespan is very limited!"));
        }  else if (other.name == "eatCollide") {
//                StartCoroutine(StartCountdown(8, "Press 'p' on your keyboard to pick-up the mushroom, and 'e' to eat it.")); 
                    shieldText.enabled = true;
                    shieldText.text = "Press 'p' on your keyboard to pick-up the mushroom, and 'e' to eat it.";
                } else if (other.name == "Portal") {
                    shieldText.enabled = true;
                    shieldText.text = "Congratulations! You have finished this level.";
                } else if (other.name == "keyCollide") {
                    shieldText.enabled = true;
                    shieldText.text = "Be careful, you cannot activate your shield when you are holding an object...";
                }  else if (other.name == "doorCollide") {
            // stops nearby text from changing
            if(!door.isOpen()) {
                    shieldText.enabled = true;
                    shieldText.text = keyText;
            }
        }
    }
    
    private void OnTriggerExit(Collider other) {
         if (other.name == "eatCollide" || other.name == "keyCollide" || other.name == "doorCollide") {
             shieldText.enabled = false;
         }
    }
    
    
    public IEnumerator StartCountdown(float countdownValue, string textString)
 {
          currCountdownValue = countdownValue;
     while (currCountdownValue > 0)
     {
        shieldText.enabled = true;
        shieldText.text = textString;
         yield return new WaitForSeconds(1.0f);
         currCountdownValue--;
         shieldText.enabled = false;
     }   
    }

}
