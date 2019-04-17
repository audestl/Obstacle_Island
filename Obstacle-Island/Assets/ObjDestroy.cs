using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjDestroy : MonoBehaviour
{
    private bool active;
    private GameObject shield;
    private MeshRenderer renderer;
    private Collider collider;
    
    private void Start() {
    active = true; 
    shield = GameObject.FindWithTag("Shield");
    renderer = shield.GetComponent<MeshRenderer>();
    collider = shield.GetComponent<Collider>();
    }
    
    private void Update() {
        print(active);
    }
    
        public void OnCollisionEnter(Collision col) {
         //print("aouch");        
        if (col.gameObject.tag == "ActivePlayer") {
            if (active && collider.enabled == false) Destroy(this.gameObject);
        } 
            if (col.gameObject.tag == "Ground") {
            active = false;
        }
        
    }
    
    private IEnumerator CountDown() {

        yield return new WaitForSeconds(2.0f);
        Destroy(this.gameObject);
    }
    
    public bool objIsActive() {
        return active;
    }
}
