using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickupper : MonoBehaviour
{

    /*These are the requirements for this script to work:
     * 1 - You must create a trigger collider designating the reach of the grab action.
     * 2 - You must have an empty as a child to place the picked up object.
     * 3 - ButtonCheck() must be called to fire.
     * 4 - Pickupable object must have a rigidbody.
     * 5 - Pickupable object must have the tag "pickupable".
     */

    //The Grabpoint is the empty where the object is placed when it is picked up
    public Transform grabPoint;

    private GameObject pickup;
    private List<GameObject> pickups = new List<GameObject>();
    private bool inRange;
    private bool buttonDown;
    private bool isHolding;

    // Start is called before the first frame update
    void Start()
    {
        inRange = false;
        buttonDown = false;
    }

    // Update is called once per frame
    void Update()
    {
        //if someone presses the button this parents the pickupable to the selected empty.
        if (buttonDown)
        {
            if (inRange && pickup.gameObject != null)
            {
                pickup.GetComponent<Rigidbody>().useGravity = false;
                pickup.transform.position = grabPoint.transform.position;
                pickup.transform.parent = grabPoint.transform;
            }
        }   
    }

    //Passively checks for objects within the trigger's range.
    //NOTE: This is the trigger you should have set up.
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "pickupable")
        { 
            pickups.Add(other.gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
       if (other.transform.tag == "pickupable")
        {
            pickups.Remove(other.gameObject);
            if (pickups.Count == 0)
            {
                pickup = null;
                inRange = false;
            }    
        }
    }

    //This function will activate a pickup of the nearest object within range.
    //IE Input.GetButtonDown(myButtonName) {Pickupper1.buttonCheck()}
    public void ButtonCheck()
    {
        //print("bla");
        if (buttonDown)
        {
            buttonDown = false;
            pickup.GetComponent<Rigidbody>().useGravity = true;
            pickup.transform.parent = null;
            isHolding = false;
            return;
        }

        if(pickups.Count == 1 && buttonDown == false)
        {
            pickup = pickups[0];
            buttonDown = true;
            inRange = true;
            isHolding = true;
        }

        //If there are more than one pickupables in range,
        //This will determine and select the nearest one.
//        if (pickups.Count > 1 && buttonDown == false)
//        {
//            Vector3 currentPosition = this.transform.position;
//            float nearestDist = Mathf.Infinity;
//
//            foreach (GameObject obj in pickups){
//                if (obj.gameObject != null)
//                {
//                    Vector3 dist = obj.transform.position - currentPosition;
//                    float distSqr = dist.sqrMagnitude;
//                    if (distSqr < nearestDist)
//                    {
//                        nearestDist = distSqr;
//                        pickup = obj;
//                    }
//                }
//            }
//
//            buttonDown = true;
//            inRange = true;
//            isHolding = true;
//        }
     }

    //Debug button fire
    public void PickUp()
    {
        ButtonCheck();
    }

    public bool IsHoldingObject()
    {
        return isHolding;
    }

    public GameObject HeldObject()
    {
        return pickup;
    }
    //EAT added the function DigestTheFood() in order to keep things private
    public void DigestTheFood()
    {
        isHolding = false;
        buttonDown = false;
        pickup = null;
    }
}
