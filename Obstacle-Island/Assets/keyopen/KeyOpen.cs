/*
    KeyOpen task is based on use task. 
    become script and pickup script is involved.  at march 20th

    test scene is base on use test scene.
    player picks up a key and goes to the door using the key to open the door(rotation unfrozen)  

    use target door is available by now. box is coming. 
       
*/


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyOpen : MonoBehaviour
{
    public bool open = false;
    public float doorOpenAngle = 90f;
    public float doorCloseAngle = 0f;
    public float smooth =2.0f;
    private bool opendoor = false;
    public GameObject door,box;


    //public void changeDoorState() {
    //    open = !open;

    //}

    //public void Update()
    //{
    //    if (opendoor == true)
    //    {
    //        //door = GameObject.Find("door");
    //        Debug.Log("hello door");

    //        //UseTarget TargetUse = hit.collider.gameObject.GetComponent<UseTarget>();
    //        //if (TargetUse != null)
    //        //{

    //        // Rotate the cube by converting the angles into a quaternion.
    //        tiltY += 1;
    //        Quaternion target = Quaternion.Euler(0, tiltY, 0);

    //        // Dampen towards the target rotation
    //        door.transform.rotation = target;
    //    }
    //}


    public void KeyOpen_door()
    {
        opendoor = true;
        //Quaternion targetRotation = Quaternion.Euler(0, doorOpenAngle, 0);
        //transform.localRotation = Quaternion.Slerp(transform.localRotation, targetRotation, smooth * Time.deltaTime);

        door = GameObject.Find("door");
   //     door.GetComponent<Rigidbody>().constraints &= ~RigidbodyConstraints.FreezeRotationY;
        door.GetComponent<Rigidbody>().constraints &= ~RigidbodyConstraints.FreezeRotationX;
     //   door.GetComponent<Rigidbody>().constraints &= ~RigidbodyConstraints.FreezeRotationZ;

    }

    public void KeyOpen_box()
    {
        box = GameObject.Find("box");
        box.GetComponent<Rigidbody>().constraints &= ~RigidbodyConstraints.FreezeRotationZ;
        door.GetComponent<Rigidbody>().constraints &= ~RigidbodyConstraints.FreezeRotationY;
        door.GetComponent<Rigidbody>().constraints &= ~RigidbodyConstraints.FreezeRotationX;

    }
}