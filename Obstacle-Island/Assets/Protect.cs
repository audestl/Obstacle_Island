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

    }

    // Update is called once per frame
    void Update()
    {

    }


    void OnCollisionEnter(Collision col)
    {
         if(col.gameObject.name == "Brick") 
        col.collider.attachedRigidbody.velocity += new Vector3(-col.relativeVelocity.x, col.relativeVelocity.y);
        print("Cold brick");

        }
//    }
//    

}
