/* How to use Navmesh script from zero base
 * 1. download navmesh component framework  https://github.com/Unity-Technologies/NavMeshComponents (already in github project assets folder)
 * 2. create an empty, add navmesh surface component on it (scene had it already)
 * 3. build your scene and setup your non walkable objects (by adding navmesh modifier-override-nonwalkable) (scene had it alreay)
 * 4. on navmesh surface comonent ,click bake (scene had it already)
 *    for making scene run, on navmesh object, you need to add two navmesh surface script component. one for humanoid, the other for giant. 
 * 5. humanoid navmesh map is created , same as giant navmesh is created. (scene had it alreay)
 * 6. add navmesh agent on your player, choose humanoid or giant navmesh map on agent type dropdown menu of player.(you have to do it by yourself)
 * 7. add navmeshcontroller script on your player (do it by yourself)
 * 8. add rigidbody component on your player (scene had it already)
 * 9. add rigidbodycontroller script on your player (do it by yourself)
 * 10. run
 * 11. questions? slack @Emily or @Marie-Ève
 reference url: https://www.youtube.com/watch?v=CHV1ymlw-P8&feature=youtu.be
*/
using UnityEngine;
using UnityEngine.AI;
public class NavMeshController : MonoBehaviour
{
    public Camera cam;
    private NavMeshAgent agent;
    // public LocomotionController locomotionCtl;
    private RigidBodyController locomotionCtl;
    private Rigidbody rigidb;
    Vector3 direction;

    private Vector3 destination;

    private void Start()
    {
        agent = this.GetComponent<NavMeshAgent>();
        locomotionCtl = GetComponent<RigidBodyController>();
        rigidb = GetComponent<Rigidbody>();
        agent.Warp(this.transform.position);
       //agent.SetDestination(agent.transform.position);
        destination = agent.transform.position;
    }

    /* NavMeshProvider() function is for leading player to an assigned destination. this destination
     parameter is passed by the one who call this function. a position(not direction) is requaired in this function.  
     by now, it is not calling locomote function in order to make movement going the right way. 
   */

    public  void NavMeshProvider(Vector3 destination)
    {
        //agent.velocity = rigidb.velocity;

        agent.SetDestination(destination);


//        this.transform.position += Vector3.Normalize(agent.steeringTarget - this.transform.position) * 0.1f;

//        if (Vector3.Distance(this.transform.position, destination) > 0.1f)
//            locomotionCtl.Locomote(Vector3.Normalize(agent.steeringTarget - this.transform.position));

        //if (Vector3.Distance(this.transform.position, destination) > 0.1f)
        //    locomotionCtl.Locomote(Vector3.Normalize(agent.steeringTarget - this.transform.position));

    }


    /* FixedUpdate() function is for standalone testing navmesh function. Hit mouse left button(MLB) to set
       up a destination for navmesh agent. if you want to test, uncomment
       "locomotionCtl.Locomote(Vector3.Normalize(agent.steeringTarget - this.transform.position), false)"
        and comment the line blew. by now, it is not calling locomote function in order to make movement
        going the right way. -Feb 22th

        March 16th on friday, Locomotion and navmesh works well together,this is the newest update of navmesh script
    */


    void FixedUpdate()
    {
        agent.velocity = rigidb.velocity;


        if (Input.GetMouseButtonDown(0)) {

            Ray ray = cam.ScreenPointToRay(Input.mousePosition);

            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                agent.SetDestination(hit.point);
                destination = hit.point;

            }
        }

        //if (Input.GetMouseButtonDown(0)) {

        //    Ray ray = cam.ScreenPointToRay(Input.mousePosition);

        //    RaycastHit hit;

        //    if (Physics.Raycast(ray, out hit))
        //    {
        //        agent.SetDestination(hit.point);
        //        destination = hit.point;

        //    }
        //}

        if (Vector3.Distance(this.transform.position, destination) > 0.1f)
       locomotionCtl.Locomote(Vector3.Normalize(agent.steeringTarget - this.transform.position));

     //  this.transform.position += Vector3.Normalize(agent.steeringTarget - this.transform.position) * 0.1f;
       
    }

}

