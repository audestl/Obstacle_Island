using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyThrow : MonoBehaviour
{

    // Test script for Marie-Eve and Audrey's milestone 3 project

    //private Throw npcthrow;
    public float meleeRange = 5f;
    
    public GameObject player;
    public GameObject objPrefab;
    private Transform target;

    public Transform throwPoint;
    private GameObject clonedObj;

    public float maxForce = 10;
    public float throwDistance = 10;
    
    private NavMeshController navmesh;
    //private float DistanceDetect = 3.5f;

    // Start is called before the first frame update
    void Start()
    {
        //player = GameObject.FindWithTag("ActivePlayer");
        target = player.transform;
        InvokeRepeating("throwObj", 1, 1);
        navmesh = this.GetComponent<NavMeshController>();
    }

    // Update is called once per frame
    void Update()
    {   
         if (IsInMeleeRangeOf(target)) navmesh.Rotation(target);
    }

    private void throwObj() {

     if (IsInMeleeRangeOf(target)) {
         
         float distance = Vector3.Distance(transform.position, target.position);
         
        clonedObj = Instantiate(objPrefab, throwPoint.transform.position, throwPoint.transform.rotation);

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
        
     private bool IsInMeleeRangeOf (Transform target) {
         float distance = Vector3.Distance(transform.position, target.position);
         return distance < meleeRange;
     }

}
