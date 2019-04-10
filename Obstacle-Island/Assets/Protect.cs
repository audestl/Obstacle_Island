using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Protect : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }


    void OnCollisionEnter(Collision col)
    {
        // force is how forcefully we will push the player away from the enemy.
       // col.collider.attachedRigidbody.velocity += new Vector3(-col.relativeVelocity.x, col.relativeVelocity.y);
        Vector3 vec = new Vector3(-col.relativeVelocity.x, -col.relativeVelocity.y);
        col.collider.attachedRigidbody.AddForce(vec * 50);
        //}
    }
}
