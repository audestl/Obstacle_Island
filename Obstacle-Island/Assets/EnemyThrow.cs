using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyThrow : MonoBehaviour
{

    // Test script for Marie-Eve and Audrey's milestone 3 project

    //private Throw npcthrow;
    private float DistanceDetect = 5.0f;
    private GameObject player;
    public GameObject objPrefab;
    private Transform target;

    public Transform throwPoint;
    private GameObject clonedObj;

    public float maxForce = 10;
    public float throwDistance = 10;
    private AudioSource throwSound;
    
    private float distance;
    
    private NavMeshController navmesh;
    //private float DistanceDetect = 3.5f;

    // Start is called before the first frame update
    void Start()
    {
        //npcthrow = GetComponent<Throw>();
        player = GameObject.FindGameObjectWithTag("ActivePlayer");
        target = player.transform;
        InvokeRepeating ("throwObj", 1, 1);
        navmesh = this.GetComponent<NavMeshController>();
        //throwSound = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
            distance = Vector3.Distance(transform.position, target.position);
        
//         float playerPos = new Vector3(player.transform.position, player.transform.rotation)
        
        if (distance < DistanceDetect) navmesh.NavMeshProvider(target);
        
    //if (playerDistance < DistanceDetect)
        //throwObj();
    }

    private void throwObj() {

    //float distance = Vector3.Distance(transform.position, target.position);
   
    if (distance < DistanceDetect) {
        clonedObj = Instantiate(objPrefab, throwPoint.transform.position, throwPoint.transform.rotation);

        //float distance = Vector3.Distance(transform.position, target.position);

        Vector3 heading = target.position - transform.position;
        Vector3 direction = heading / heading.magnitude;

            //heldObject = pickupper.HeldObject();
            var throwRb = clonedObj.GetComponent<Rigidbody>();
            //pickupper.ButtonCheck();
            var vel = Projectile.GetProjectileVelocity(maxForce, distance, transform.up, direction);
            throwRb.AddForce(vel, ForceMode.VelocityChange);
            //throwSound.Play();
        }


    }

}
