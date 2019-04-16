using UnityEngine;

public class NavMeshController : MonoBehaviour
{
    
    public float rotationSpeed = 10f;
// We kept the original NavMeshController title but the enemy does not use the AI engine anymore.

    public void Rotation(Transform target)
    {
        
        RotateTowards(target);

    }
    
         private void RotateTowards (Transform _target) {
            Vector3 direction = (_target.position - transform.position).normalized;
            Quaternion lookRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * rotationSpeed);
     }

}

