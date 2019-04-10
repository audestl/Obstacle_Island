using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyPatrol : MonoBehaviour
{
    
 /* THIS IS PART OF THE ENEMY BEHAVIOUR SERIES BY MARIE-ÈVE.

    The enemy is patrolling different waypoints.
     
     Please refer to the scene "EnemyBehaviourTest" in order to see this script in live action.
     
     HOW TO USE THIS SCRIPT :
      -- THIS SCRIPT HAS TO BE PUT ON AN ENEMY (NOT A PLAYER, THIS IS AN AI)
      -- THE LEVEL NEEDS TO HAVE A BAKED NAV MESH SURFACE
      -- THE LEVEL NEEDS TO HAVE AN ARRAY OF EMPTY OBJECTS THAT ARE PATROL POINTS, WHICH EACH NEED THE TAG "PatrolPoints"
      -- FOR NOW YOU NEED TO LINK THOSE WAYPOINTS MANUALLY TO THE SCRIPT

  IF YOU HAVE ANY QUESTIONS OR SEE ANY BUG PLEASE POST AN ISSUE IN THE GITHUB OR SEND ME A SLACK @Marie-Ève
  
    */
    
    
    private NavMeshAgent agent;
    private NavMeshController controller;
    
    // Array of patrol points
    public Transform[] PatrolPoints;
    private int currentPatrolPoint;
    
    void Start() {
        
        currentPatrolPoint = 0;
        
        // Get the NavMeshAgent component
        //agent = this.GetComponent<NavMeshAgent>();
        controller = GetComponentInParent<NavMeshController>(); 
        agent = controller.GetComponent<NavMeshAgent>();
        
        
        // Enemy starts his round
        StartPatrol();
    }
    
    public void StartPatrol() {
        
    // Making the array dynamic by calculating how many waypoints are on the sceme
//    GameObject[] patrolObjects = GameObject.FindGameObjectsWithTag ("PatrolPoints");
//     PatrolPoints = new Transform[patrolObjects.Length];
// 
//     // Transforming 'GameObject' instance into 'Transform'
//        for ( int i = 0; i < PatrolPoints.Length; ++i )
//         PatrolPoints[i] = patrolObjects[i].transform;
        
        GoToNextPoint();       
    }
    
    // Enemy starts his round
    public void GoToNextPoint() {
        
            // Returns if no points have been set up
            if (PatrolPoints.Length == 0)
                return;
   
        // Function from NavMeshController script, we are feeding it the PatrolPoints' Vector3 position
        controller.NavMeshProvider(PatrolPoints[currentPatrolPoint].position);

            // Choose the next point in the array as the destination, cycling to the start if necessary.
            currentPatrolPoint = (currentPatrolPoint + 1) % PatrolPoints.Length;
        //print(PatrolPoints.Length);
    }
  
    
    void Update() {
        
        Debug.Log("pathpending : " + agent.pathPending);
Debug.Log("path status: " + agent.pathStatus);

        
        //print(currentPatrolPoint);
            // When enemy has reached one patrol point, go to the next one
          if (!agent.pathPending && agent.remainingDistance < 0.5f) GoToNextPoint(); 
        //Debug.Log("go to next in update"); }
   }    
}
