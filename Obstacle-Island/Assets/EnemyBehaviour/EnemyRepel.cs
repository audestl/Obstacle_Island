using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyRepel : MonoBehaviour
{
    
 /* THIS IS PART OF THE ENEMY BEHAVIOUR SERIES BY MARIE-ÈVE.

    The enemy is running away from the player when he gets too close.
     
     Please refer to the scene "EnemyBehaviourTest" in order to see this script in live action.
     
     HOW TO USE THIS SCRIPT :
      -- THIS SCRIPT HAS TO BE PUT ON AN ENEMY (NOT A PLAYER, THIS IS AN AI).
      -- THE LEVEL NEEDS A BAKED NAVMESH SURFACE.
      -- SET THE DISTANCE DETECTION VARIABLE TO DESIRED RADIUS.
      -- THE SCRIPT IS MADE TO RECOGNIZE ACTIVE PLAYER ACCORDING TO BECOME SCRIPT.

  IF YOU HAVE ANY QUESTIONS OR SEE ANY BUG PLEASE POST AN ISSUE IN THE GITHUB OR SEND ME A SLACK @Marie-Ève
  
    */
    
   // Values to access NavMesh
    private NavMeshController controller;
    private NavMeshAgent agent;
    
    private GameObject player;

    // Distance which enemy detects player
    public float DistanceDetect = 3.0f;

    
    private void Start() {
        
        // Find the active player defined by Become Script
        player = GameObject.FindWithTag("ActivePlayer");
        
        // Get the NavMeshAgent components
        controller = GetComponent<NavMeshController>(); 
        agent = controller.GetComponent<NavMeshAgent>();      
    }
    
    private void Update() {
    StartRepeling();
     // Constantly check if active player has changed
     player = GameObject.FindWithTag("ActivePlayer");
    } 
    
public void StartRepeling() {
    float distance = Vector3.Distance(transform.position, player.transform.position);
   
    if (distance < DistanceDetect) {
    Vector3 dirToPlayer = transform.position - player.transform.position;
    Vector3 newPos = transform.position + dirToPlayer;
    controller.NavMeshProvider(newPos);
    }
}    

}
