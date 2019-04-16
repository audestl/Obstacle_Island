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
    
    public float rotationSpeed = 10f;

    private Vector3 destination;

    private void Start()
    {
        agent = this.GetComponent<NavMeshAgent>();
//        locomotionCtl = GetComponent<RigidBodyController>();
//        rigidb = GetComponent<Rigidbody>();
        //agent.Warp(this.transform.position);
       //agent.SetDestination(agent.transform.position);
//        destination = agent.transform.position;
//            agent.updateRotation = false;
    }



    public void NavMeshProvider(Transform target)
    {

        //agent.transform.LookAt(target);
        
        RotateTowards(target);
        
//        if (agent.velocity.sqrMagnitude > Mathf.Epsilon)
//{
//    transform.rotation = Quaternion.LookRotation(agent.velocity.normalized);
//}

//
//        if (Vector3.Distance(this.transform.position, destination) > 0.1f)
//            locomotionCtl.Locomote(Vector3.Normalize(agent.steeringTarget - this.transform.position));

    }
    
         private void RotateTowards (Transform _target) {
            Vector3 direction = (_target.position - transform.position).normalized;
            Quaternion lookRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * rotationSpeed);
     }

//
//    void FixedUpdate()
//    {
//        agent.velocity = rigidb.velocity;
//
//
//        if (Input.GetMouseButtonDown(0)) {
//
//            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
//
//            RaycastHit hit;
//
//            if (Physics.Raycast(ray, out hit))
//            {
//                agent.SetDestination(hit.point);
//                destination = hit.point;
//
//            }
//        }
//
//        //if (Input.GetMouseButtonDown(0)) {
//
//        //    Ray ray = cam.ScreenPointToRay(Input.mousePosition);
//
//        //    RaycastHit hit;
//
//        //    if (Physics.Raycast(ray, out hit))
//        //    {
//        //        agent.SetDestination(hit.point);
//        //        destination = hit.point;
//
//        //    }
//        //}
//
//        if (Vector3.Distance(this.transform.position, destination) > 0.1f)
//       locomotionCtl.Locomote(Vector3.Normalize(agent.steeringTarget - this.transform.position));
//
//     //  this.transform.position += Vector3.Normalize(agent.steeringTarget - this.transform.position) * 0.1f;
//       
//    }

}

