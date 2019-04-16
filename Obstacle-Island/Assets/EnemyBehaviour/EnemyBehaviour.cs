using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    
    /* ENEMY BEHAVIOUR BY MARIE-ÈVE.
    This is one example of a behaviour tree.
    This script determines enemies behaviour.
     The following functions are implemented in this script:
     - If player has picked up object, enemy will run away from player.
     - If player hasn't picked up object, enemy will chase him.
     - When player is too far from enemy, enemy patrols a certain area infinitely.
     
    THIS SCRIPT IS AN OLD VERSION. THESE BEHAVIOURS HAVE BEEN SPLIT IN THREE.
    - EnemyPatrol
    - EnemyChase
    - EnemyRepel
  
    */
    

    private NavMeshAgent agent;
    public GameObject player;
    
    // Values to access other scripts
    private Pickupper pickUpFunction;
    private NavMeshController controller;
    
    // Array of patrol points
    public Transform[] PatrolPoints;
    private int currentPatrolPoint = 0;
    
    // Distance which player is detected
    private float enemyDistanceDetect = 3.0f;
    
    void Start() {
        
        // Get the NavMeshAgent component
        agent = this.GetComponent<NavMeshAgent>();
        controller = GetComponentInParent<NavMeshController>();
        
       // Calls the "Pickupper" script on the player (if you follow the Pickupper script's instructions, the player should have the pickupper script on him)
        pickUpFunction = player.GetComponent<Pickupper>();
        
        // Enemy starts his round
        GoToNextPoint();
    }
    
    void GoToNextPoint() {
            // Returns if no points have been set up
            if (PatrolPoints.Length == 0)
                return;
   
        // Function from NavMeshController script, we are feeding it the PatrolPoints' Vector3 position
        //controller.NavMeshProvider(PatrolPoints[currentPatrolPoint].position);

            // Choose the next point in the array as the destination, cycling to the start if necessary.
            currentPatrolPoint = (currentPatrolPoint + 1) % PatrolPoints.Length;
    }
  
    
    void Update() {
        
        Debug.Log(currentPatrolPoint);
        
        //Actual distance between player and enemy.
        float distance = Vector3.Distance(transform.position, player.transform.position);
    
        
        if (distance < enemyDistanceDetect) // If the player is too close to the enemy...
        {   
            // If player has picked up object, enemy will run away from player 
            if (pickUpFunction.IsHoldingObject()) // We are calling the IsHoldingObject() function from the Pickupper script which returns a boolean called isHolding.   
            { 
                Vector3 dirToPlayer = transform.position - player.transform.position;
                Vector3 newPos = transform.position + dirToPlayer;
                //controller.NavMeshProvider(newPos);
            } else // If player hasn't picked up object, enemy will run towards player and chase him
            {                     
                //controller.NavMeshProvider(player.transform.position);
            }
              
        } else // If the player is too far from enemy, he patrols
        {
            // When enemy has reached one patrol point, go to the next one 
            if (!agent.pathPending && agent.remainingDistance < 0.5f) GoToNextPoint();                           
        }
    }    
}
