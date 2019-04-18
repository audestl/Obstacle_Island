using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Throw : MonoBehaviour
{
    //This script requires a Pickupper to work. 

    public float maxForce = 10;
    public float throwDistance = 10;

    Pickupper pickupper;
    GameObject heldObject;

    // Start is called before the first frame update
    void Start()
    {
        pickupper = GetComponent<Pickupper>();
    }

    //Call this for player throw. This will throw the object where the player is facing.
    public void ThrowObject()
    {
        if (pickupper.IsHoldingObject())
        {
            heldObject = pickupper.HeldObject();
            var throwRb = heldObject.GetComponent<Rigidbody>();
            pickupper.ButtonCheck();
            var vel = Projectile.GetProjectileVelocity(maxForce, throwDistance, transform.up, transform.forward);
            throwRb.AddForce(vel, ForceMode.VelocityChange);
            heldObject = null;
        }
        else
        {
            heldObject = null;
        }
    }
  
}
