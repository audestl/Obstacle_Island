using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjDestroy : MonoBehaviour
{
        public void OnCollisionEnter(Collision col) {
            //print("aouch");
        
        if (col.gameObject.tag == "ActivePlayer") {
           // print("activeplayer");
            Destroy(this.gameObject);
        }
        
    }
    
    private IEnumerator CountDown() {

        yield return new WaitForSeconds(2.0f);
        Destroy(this.gameObject);
    }
}
