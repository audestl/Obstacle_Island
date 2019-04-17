using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/*====================================================================================
EAT Script
by
Ebrahim Badawi, Martin-John Hearty, Catherine Weng

Description:
Player can eat the items it pickes up, which have "Eatable" script attached to them, 
by Pressing "E" on keyboard!
while eatenig, the food decreases in size and when it's finished, player drops its leftovers. 


====================================================================================*/

public class Eat : MonoBehaviour
{
    public float speed = 4f;
    public float playerSpeed = 4f;
    public float duration = 5f;
    private Pickupper myFood;
    Transform foodLoc;
    private GameObject player;
    Vector3 foodSize;
    Vector3 playerSize;
    bool small;
    
    private Vector3 initialPos;
    public GameObject initialObj;
    
    //Vector3 initialPos;
    

    void Start() {
        //playerSize = player.transform.localScale;
        small = false;
        player = GameObject.FindWithTag("ActivePlayer");
        playerSize = player.transform.localScale;
        
        initialPos = initialObj.transform.position;
        
        //initialPos = shroom.transform.position;
    }

    public void EatFood()
        
    { 
        myFood = GetComponentInParent<Pickupper>();
        

        if (myFood.IsHoldingObject())
        {
            foodLoc = myFood.grabPoint;
            foreach (Transform child in foodLoc)
            {
                Eatable eatable = child.GetComponent<Eatable>();
                foodSize = child.gameObject.transform.localScale;
                if (child.gameObject != null && eatable != null)
                {
                
                    StartCoroutine(EatTheObj(child, foodSize, playerSize, duration));
                }
            }
        }
    }
    
    private void OnCollisionEnter(Collision other) {
        if (other.gameObject.tag == "Blade" || other.gameObject.tag == "Projectile") {
            if(small) {
            //print("hit blade");
            StartCoroutine(ReturnNormal(playerSize, duration));
            } else {
                SceneManager.LoadScene("ObstacleIsland");
            }
        }
        
    }


    public IEnumerator EatTheObj(Transform _obj, Vector3 _foodSize, Vector3 _playerSize, float _time)
   {

        
        float i = 0.0f;
        float f= 0.0f;
        float eatingRate = (1.0f / _time) * speed;
        float playerRate = (1.0f / _time) * playerSpeed;

        while (i <= 1.0f)
        {
            i += Time.deltaTime * eatingRate;
            _obj.transform.localScale = Vector3.Lerp(_foodSize, _foodSize / 100, i);

            yield return null;
        }
        if (i >= 1.0f)
        {       
            while (f <= 1.0f)
        {
            f += Time.deltaTime * playerRate;
//            _obj.transform.localScale = Vector3.Lerp(_foodSize, _foodSize / 2, i);
            if (!small) player.transform.localScale = Vector3.Lerp(transform.localScale, player.transform.localScale - _playerSize / 60, f);
            yield return null;
        }
        if (f >= 1.0f)
            {  
            yield return StartCoroutine(FinishEating(_obj, _foodSize));
            }
        }
    }
    
        public IEnumerator ReturnNormal(Vector3 _playerSize, float _time) {
            float i = 0.0f;
            float playerRate = (1.0f / _time) * playerSpeed;
            
         while (i <= 1.0f) {
            i +=Time.deltaTime * playerRate;
            player.transform.localScale = Vector3.Lerp(transform.localScale, player.transform.localScale + _playerSize / 100, i);
            yield return null;
        } if (i >= 1.0f) {
             small = false;
//         _obj.gameObject.GetComponent<MeshRenderer>().enabled = true;
         }
    }


    public IEnumerator FinishEating(Transform _obj, Vector3 _foodSize)
    {
        //_obj.GetComponent<Rigidbody>().useGravity = true;
        _obj.parent = null;
        myFood.DigestTheFood();
        
        //Destroy(_obj.gameObject);
        //_obj.gameObject.enabled = false;
        resetShroom(_obj.gameObject, _foodSize);
        
        yield return null;
        small = true;
    }
    
    public IEnumerator FinishGrow() {
        //small = false;
        yield return null;
    }
    
    private void resetShroom(GameObject shroom, Vector3 size) {
        shroom.transform.position = initialPos;
        shroom.transform.localScale += new Vector3(0.5f, 0.5f, 0.5f);
    }
    
    public bool isSmall() {
        return small;
    }
   
}
                                        


