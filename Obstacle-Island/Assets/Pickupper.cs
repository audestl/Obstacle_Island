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
    private bool isHoldingKey;
    
    private ObjDestroy script;
    //private GameObject shield;
//    private MeshRenderer render;
//    private Collider collider;
//    
    private ShieldActivator actionShield;
    
    // Start is called before the first frame update
    void Start()
    {
//    shield = GameObject.FindWithTag("Shield");
//    render = shield.GetComponent<MeshRenderer>();
//    collider = shield.GetComponent<Collider>();
        
        actionShield = GetComponentInParent<ShieldActivator>();
        
        inRange = false;
        buttonDown = false;
        isHoldingKey = false;
        isHolding = false;
        
    }

    // Update is called once per frame
    void Update()
    {
        
        //print(pickups.Count);
        //print("holds key: " + isHoldingKey);
        //print("holds obj: " + isHolding);
     //Debug.Log(pickup);
        
        if (isHolding) {
            foreach (Transform child in grabPoint)
        if (child.name == "key") isHoldingKey = true;
        } else isHoldingKey = false;
        
//       if (isHolding && render.enabled == true && collider.enabled == true) { pickup.transform.parent = null;
//    isHolding = false;
//                                                                            }
//        print(buttonDown);

        //if someone presses the button this parents the pickupable to the selected empty.
        if (buttonDown)
        {
            
            if (inRange && pickup.gameObject != null)
            {
                pickup.transform.position = grabPoint.transform.position;
                pickup.transform.parent = grabPoint.transform;
            }
        }         
        
    }
    

    
    //Passively checks for objects within the trigger's range.
    //NOTE: This is the trigger you should have set up.
    private void OnCollisionEnter(Collision other)
    {
          script = other.gameObject.GetComponent<ObjDestroy>();
        
        if (other.transform.tag == "pickupable")
        { 
            pickups.Add(other.gameObject);
        }
        
        if (other.transform.tag == "Blade" || other.transform.tag == "Projectile" && script.objIsActive()) {
//            if (render.enabled == false && collider.enabled == false) {
            if (isHolding) {
            //foreach (Transform child in grabPoint)
            pickup.transform.parent = null;
                
            print("lost obj");
            isHolding = false;
            //buttonDown = false;
                }
            
        }
    }
    
    private void OnTriggerEnter(Collider other) {
        if(other.gameObject.name == "fireCollide") {
                       if (isHolding) {
            //foreach (Transform child in grabPoint)
            pickup.transform.parent = null;
                
            print("lost obj");
            isHolding = false;
            //buttonDown = false;
                } 
        }
        
    }

    private void OnCollisionExit(Collision other)
    {
       if (other.transform.tag == "pickupable")
        {
            pickups.Remove(other.gameObject);
           
            if (pickups.Count == 0)
            {
                //pickup = null;
                inRange = false;
            }    
        }
    }

    public void dropIt() {
        if (buttonDown) {    
            buttonDown = false;
            pickup.transform.parent = null;
            isHolding = false;
            return;
        }
    }
     //This function will activate a pickup of the nearest object within range.   
    public void ButtonCheck()
    {
        if (buttonDown)
        {
            buttonDown = false;
            pickup.transform.parent = null;
            isHolding = false;
            return;
        }

        if(pickups.Count == 1 && buttonDown == false)
        {
            if(!actionShield.shieldIsOn()) {         
            pickup = pickups[0];
            buttonDown = true;
            inRange = true;
            isHolding = true;
            //isHoldingKey = false;
            }
            
        }

//        If there are more than one pickupables in range,
//        This will determine and select the nearest one.
        if (pickups.Count > 1 && buttonDown == false)
        {
            Vector3 currentPosition = this.transform.position;
            float nearestDist = Mathf.Infinity;

            foreach (GameObject obj in pickups){
                if (obj.gameObject != null)
                {
                    Vector3 dist = obj.transform.position - currentPosition;
                    float distSqr = dist.sqrMagnitude;
                    if (distSqr < nearestDist)
                    {
                        nearestDist = distSqr;
                        pickup = obj;
                    }
                }
            }

            buttonDown = true;
            inRange = true;
            isHolding = true;
        }
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
    
    public bool hasKey() {
        return isHoldingKey;
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
