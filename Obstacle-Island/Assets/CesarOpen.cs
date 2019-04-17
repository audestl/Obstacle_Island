using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CesarOpen : MonoBehaviour


{ 
    
    private Pickupper actionPickup;
    public GameObject player;

// Smothly open a door
    public float smooth = 2.0f; //Increasing this value will make the door open faster
    public float doorOpenAngle = -100.0f; //Set either positive or negative number to open the door inwards or outwards

    bool open = false;
    bool enter = false;

    float defaultRotationAngle;

    void Start()
    {
        //player = GameObject.FindWithTag("ActivePlayer");
        actionPickup = player.GetComponent<Pickupper>();
        
        defaultRotationAngle = transform.localEulerAngles.y;
    }

    //Main function
    void Update()
    {
        
        //print(actionPickup.hasKey());
        
        transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, Mathf.LerpAngle(transform.localEulerAngles.y, (open ? doorOpenAngle : defaultRotationAngle), Time.deltaTime * smooth), transform.localEulerAngles.z);

        if (Input.GetKeyDown(KeyCode.F) && enter && actionPickup.hasKey())
        {
            open = !open;
        } else return;
    }

    void OnGUI()
    {
        if (enter)
        {
            GUI.Label(new Rect(Screen.width / 2 - 75, Screen.height - 100, 150, 50), "Press 'F' to open the door if you have the key");
        }
    }

    //Activate the Main function when player is near the door
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("ActivePlayer"))
        {
            enter = true;
        }
    }

    //Deactivate the Main function when player is go away from door
    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("ActivePlayer"))
        {
            enter = false;
        }
    }
}