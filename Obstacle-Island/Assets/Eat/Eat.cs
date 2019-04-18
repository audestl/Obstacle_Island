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
    private Vector3 initialPosPlayer;
    public GameObject initialObj;
    public GameObject initialPlayer;
    
    //private GameObject shield;
//    private MeshRenderer render;
//    private Collider collider;
    
    private ObjDestroy script;
    
    private bool isChanging;
    
    private ShieldActivator actionShield;

    

    void Start() {
        isChanging = false;
        myFood = GetComponentInParent<Pickupper>();
        
//    shield = GameObject.FindWithTag("Shield");
//    render = shield.GetComponent<MeshRenderer>();
//    collider = shield.GetComponent<Collider>();
        
        actionShield = GetComponentInParent<ShieldActivator>();
        
        //playerSize = player.transform.localScale;
        small = false;
        //player = GameObject.FindWithTag("ActivePlayer");
        playerSize = this.transform.localScale;
        
        initialPos = initialObj.transform.position;
        initialPosPlayer = initialPlayer.transform.position;
        
        //initialPos = shroom.transform.position;
    }
    
//    void Update() {
//        print("isChanging " + isChanging);
//        print("isSmall " + small);
//    }

    public void EatFood()
        
    { 
    
        if (myFood.IsHoldingObject())
        {
            foodLoc = myFood.grabPoint;
            foreach (Transform child in foodLoc)
            {
                Eatable eatable = child.GetComponent<Eatable>();
                foodSize = child.gameObject.transform.localScale;
                if (child.gameObject != null && eatable != null)
                {
                
                    if (!small && !isChanging) StartCoroutine(EatTheObj(child, foodSize, playerSize, duration));
                }
            }
        }
    }
    
    private void OnCollisionEnter(Collision other) {
  script = other.gameObject.GetComponent<ObjDestroy>();
        
        if ((other.gameObject.tag == "Projectile" && script.objIsActive()) || other.gameObject.tag == "Blade") {
            
            if(!actionShield.shieldIsOn()) {
            if(small && !isChanging) StartCoroutine(ReturnNormal(playerSize, duration)); 
            else if (!small) SceneManager.LoadScene("MainScene");
             
            } else return;
        } 
    }
    
    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.name == "fireCollide") {
            print("collide fire");
            
            if(!actionShield.shieldIsOn()) {
            if(small && !isChanging) StartCoroutine(ReturnNormal(playerSize, duration)); 
            else if (!small) SceneManager.LoadScene("MainScene");
             
            } else return;
        }
        
    }
//    
//    private void OnCollisionEnter(Collision other) {
//        if(other.gameObject.tag == "Blade") {
//            if (render.enabled == false && collider.enabled == false) {
//            if(small && !isChanging) StartCoroutine(ReturnNormal(playerSize, duration));
//             else if (!small) this.transform.position = initialPosPlayer;        
//            }
//        }
//    }


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
            this.transform.localScale = Vector3.Lerp(transform.localScale, this.transform.localScale - _playerSize / 60, f);
            isChanging = true;
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
            this.transform.localScale = Vector3.Lerp(transform.localScale, this.transform.localScale + _playerSize / 58, i);
             
            isChanging = true;
            yield return null;
        } if (i >= 1.0f) {
             yield return StartCoroutine(FinishGrow());
//         _obj.gameObject.GetComponent<MeshRenderer>().enabled = true;
         } 
    }


    public IEnumerator FinishEating(Transform _obj, Vector3 _foodSize)
    {
        //_obj.GetComponent<Rigidbody>().useGravity = true;
        _obj.parent = null;
        myFood.DigestTheFood();
        isChanging = false;
        
        //Destroy(_obj.gameObject);
        //_obj.gameObject.enabled = false;
        resetShroom(_obj.gameObject, _foodSize);
        small = true;
        yield return null;
    }
    
    public IEnumerator FinishGrow() {
        small = false;
        isChanging = false;
        yield return null;
    }
    
    private void resetShroom(GameObject shroom, Vector3 size) {
        shroom.transform.position = initialPos;
        shroom.transform.localScale += new Vector3(0.86f, 0.86f, 0.86f);
        shroom.transform.rotation = Quaternion.Euler(0, 30, 0);
    }
    
        
    public bool getIsChanging() {
        return isChanging;
    }
    
    public bool isSmall() {
        return small;
    }
   
}
                                        


