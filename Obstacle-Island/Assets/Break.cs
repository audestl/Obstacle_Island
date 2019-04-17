// Simple break into primitive shapes
// No need to set rigidbody component to the object when this script is used
// Editables:
// - Size and quantity of pieces
// - Force and radius of explosion
// Code reused from Learning Game Creating at www.youtube.com/channel/UCm4rOGiKPBCVM8RunCeBNNQ


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Break : MonoBehaviour {

    public float cubeSize = 0.05f;
    public int cubesInRow = 5;

    float cubesPivotDistance;
    Vector3 cubesPivot;

    public float minExplosionForce = 1f;
    public float maxExplosionForce = 20f;
    public float explosionRadius = 4f;
    public float explosionUpward = 0.4f;
    public float removeDelay = 3f;
    
	// Use this for initialization
	void Start () {

        // Adding rigidbody component to the object
        gameObject.AddComponent<Rigidbody>();

        // Calculate pivot distance
        cubesPivotDistance = cubeSize * cubesInRow / 2;

        // Use this value to create pivot vector
        cubesPivot = new Vector3(cubesPivotDistance, cubesPivotDistance, cubesPivotDistance);


    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Ground") || other.gameObject.CompareTag("Breaker") || other.gameObject.CompareTag("Projectile"))
        {
            explode();
        }

    }

    //private void OnMouseDown()
    //{
    //    explode();
    //}

    // Makes the object disappear and calls cunction to create pieces
    public void explode(){
        gameObject.SetActive(false);

        // Loop to create multiple pieces
        for (int x = 0; x < cubesInRow; x++) {
            for (int y = 0; y < cubesInRow; y++) {
                for (int z = 0; z < cubesInRow; z++) {
                    createPiece(x, y, z);
                }
            }
        }

        // Get explosion position
        Vector3 explosionPos = transform.position;

        // Get collider in this position in radius
        Collider[] colliders = Physics.OverlapSphere(explosionPos, explosionRadius);

        // Add explosion force to all colliders in that overlap sphere
        foreach (Collider hit in colliders) {
            // get rigidbody from collider object
            Rigidbody rigbod = hit.GetComponent<Rigidbody>();
            if(rigbod != null) {
                // Add explosion force to this body with defined parameters
                rigbod.AddExplosionForce(Random.Range(minExplosionForce, maxExplosionForce), transform.position, explosionRadius, explosionUpward);
            }
        }
    }

    // Creating pieces
    void createPiece(int x, int y, int z){
        GameObject piece;
        piece = GameObject.CreatePrimitive(PrimitiveType.Cube);

        // Setting size and position of pieces
        piece.transform.position = transform.position + new Vector3(cubeSize * x, cubeSize * y, cubeSize * z) - cubesPivot;
        piece.transform.localScale = new Vector3(cubeSize, cubeSize, cubeSize);

        // Adding Rigidbody component
        piece.AddComponent<Rigidbody>();
        piece.GetComponent<Rigidbody>().mass = cubeSize;
        Destroy(piece, removeDelay);

    }
}
