using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Protect : MonoBehaviour
{
    //private GameObject shield;
    private MeshRenderer render;
    private Collider collider;
    private bool shieldActivated;
    
    // Start is called before the first frame update
    void Start()
    {
    render = this.GetComponent<MeshRenderer>();
    collider = this.GetComponent<Collider>();
    shieldActivated = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (shieldActivated) {
        render.enabled = true;
        collider.enabled = true;
        } else {
        render.enabled = false;
        collider.enabled = false; 
            }
    }

//
//    void OnCollisionEnter(Collision col)
//    {
//        // force is how forcefully we will push the player away from the enemy.
//       // col.collider.attachedRigidbody.velocity += new Vector3(-col.relativeVelocity.x, col.relativeVelocity.y);
//
//        //}
//    }
//    
    public bool shieldActivate() {
        return shieldActivated;
    }
}
