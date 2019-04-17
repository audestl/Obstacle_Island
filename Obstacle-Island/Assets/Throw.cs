using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Throw : MonoBehaviour
{
    //This script requires a Pickupper to work. 

    public float maxForce = 10;
    public float throwDistance = 10;
    private AudioSource throwSound;

    Pickupper pickupper;
    GameObject heldObject;

    // Start is called before the first frame update
    void Start()
    {
        pickupper = GetComponent<Pickupper>();
        throwSound = GetComponent<AudioSource>();
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
            throwSound.Play();
            heldObject = null;
        }
        else
        {
            heldObject = null;
        }
    }

    //Call this for NPC throw. Add the player as target.
    public void ThrowObject(Transform target)
    {
        float distance = Vector3.Distance(transform.position, target.position);

        Vector3 heading = target.position - transform.position;
        Vector3 direction = heading / heading.magnitude;

        if (pickupper.IsHoldingObject())
        {
            heldObject = pickupper.HeldObject();
            var throwRb = heldObject.GetComponent<Rigidbody>();
            pickupper.ButtonCheck();
            var vel = Projectile.GetProjectileVelocity(maxForce, distance, transform.up, direction);
            throwRb.AddForce(vel, ForceMode.VelocityChange);
            throwSound.Play();
            heldObject = null;
        }
        else
        {
            heldObject = null;
        }
    }

  
}
