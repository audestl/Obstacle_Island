using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RigidBodyController : MonoBehaviour
{
    //following ironequals tut for reference -- https://medium.com/ironequal/unity-character-controller-vs-rigidbody-a1e243591483 and https://docs.huihoo.com/unity/3.3/Documentation/ScriptReference/Input.GetAxis.html and https://www.noob-programmer.com/unity3d/how-to-make-player-object-jump-in-unity-3d/

    private Rigidbody characterBod;
    public float speed = 2f;
    public float jumpSpeed = 5f;
     public Vector3 jump;

    private float rotateXAxis = 0.0f;
    private float rotateYAxis = 0.0f;

    public bool isGrounded;

    // Start is called before the first frame update
    void Start()
    {
        characterBod = GetComponent<Rigidbody>();
        rotateXAxis = characterBod.transform.eulerAngles.x;
        rotateYAxis = characterBod.transform.eulerAngles.y;
        jump = new Vector3(0.0f, 2.0f, 0.0f);
    }

    public void Locomote(Vector3 direction)
    {
        direction.y = 0;

        direction = direction.normalized;
      
        direction.z *= speed * Time.deltaTime;
        direction.x *= speed * Time.deltaTime;

        characterBod.transform.position += transform.right * direction.x;
        characterBod.transform.position += transform.forward * direction.z;

    }


    public void Rotate()
    {
        if (Input.GetMouseButton(0))
        {
            rotateYAxis += 3 * Input.GetAxis("Mouse X");
            rotateXAxis += 3 * Input.GetAxis("Mouse Y");
        }
        characterBod.transform.rotation = Quaternion.Euler(0, rotateYAxis, 0);

        Become cam = GameObject.Find("Camera_Become").GetComponent<Become>();

        if (cam.GetCamMode() == 1)
        {
            cam.gameObject.transform.rotation = Quaternion.Euler(rotateXAxis, rotateYAxis, 0);
        }
        else
        {
            cam.gameObject.transform.rotation = Quaternion.Euler(33+ rotateXAxis, rotateYAxis, 0);
        }
    }
    public void Jump()
    {
        if (isGrounded)
        {
            characterBod.AddForce(jump * jumpSpeed, ForceMode.Impulse);
            isGrounded = false;
        }
       
    }
    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == ("Ground") && isGrounded == false)
        {
            isGrounded = true;
        }
    }

}
