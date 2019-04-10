using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyThrow : MonoBehaviour
{

    // Test script for Marie-Eve and Audrey's milestone 3 project

    //private Throw npcthrow;
    private float DistanceDetect = 3.0f;
    private GameObject player;
    public GameObject objPrefab;
    private Transform target;

    public Transform throwPoint;
    private GameObject clonedObj;

    public float maxForce = 10;
    public float throwDistance = 10;
    private AudioSource throwSound;

    // Start is called before the first frame update
    void Start()
    {
        //npcthrow = GetComponent<Throw>();
        player = GameObject.FindGameObjectWithTag("ActivePlayer");
        target = player.transform;
        InvokeRepeating ("throwObj", 1, 1);
        //throwSound = GetComponent<AudioSource>();

    }

    // Update is called once per frame
    void Update()
    {
            float playerDistance = Vector3.Distance(transform.position, player.transform.position);

    //if (playerDistance < DistanceDetect)
        //throwObj();
    }

    private void throwObj() {

        clonedObj = Instantiate(objPrefab, throwPoint.transform.position, throwPoint.transform.rotation);

        float distance = Vector3.Distance(transform.position, target.position);

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
