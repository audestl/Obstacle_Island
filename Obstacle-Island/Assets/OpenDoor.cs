using System.Collections;
using System.Collections.Generic;


//Make an empty game object and call it "Door"
//Rename your 3D door model to "Body"
//Make sure that the "Door" object is at the side of the "Body" object. The place where a Door Hinge should be
//Move the "Body" object inside "Door"
//Add a box collider to "Door" object and make it much bigger then the "Body" model, mark it as Trigger
//Assign this script to a "Door" game object that have box collider with trigger enabled
//Make sure the main character is tagged "Player"
//Upon walking into trigger area press "F" to open / close the door

using UnityEngine;

public class OpenableDoor : MonoBehaviour
{

    // Smothly open a door
    public float smooth = 2.0f; //Increasing this value will make the door open faster
    public float doorOpenAngle = 90.0f; //Set either positive or negative number to open the door inwards or outwards

    bool open = false;
    bool enter = false;

    float defaultRotationAngle;

    void Start()
    {
        defaultRotationAngle = transform.localEulerAngles.y;
    }

    //Main function
    void Update()
    {
        transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, Mathf.LerpAngle(transform.localEulerAngles.y, (open ? doorOpenAngle : defaultRotationAngle), Time.deltaTime * smooth), transform.localEulerAngles.z);

        if (Input.GetKeyDown(KeyCode.F) && enter)
        {
            open = !open;
        }
    }

    void OnGUI()
    {
        if (enter)
        {
            GUI.Label(new Rect(Screen.width / 2 - 75, Screen.height - 100, 150, 30), "Press 'F' to open the door");
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
