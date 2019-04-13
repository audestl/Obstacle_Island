using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    void FixedUpdate()
    {
        player = GameObject.FindWithTag("ActivePlayer");
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
                if (child.gameObject != null && eatable != null)
                {
                    Vector3 foodSize = child.gameObject.transform.localScale;
                    Vector3 playerSize = player.transform.localScale;
                    print(playerSize);
                    StartCoroutine(EatTheObj(child, foodSize, playerSize, duration));
                }
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
//            player.transform.localScale = Vector3.Lerp(transform.localScale, player.transform.localScale - _playerSize / 100, i);
            yield return null;
        }
        if (i >= 1.0f)
        {       
            while (f <= 1.0f)
        {
            f += Time.deltaTime * playerRate;
//            _obj.transform.localScale = Vector3.Lerp(_foodSize, _foodSize / 2, i);
            player.transform.localScale = Vector3.Lerp(transform.localScale, player.transform.localScale - _playerSize / 50, f);
            yield return null;
        }
        if (f >= 1.0f)
        {  
            yield return StartCoroutine(FinishEating(_obj));
        }
        }

                }
                       
    
    //yield return StartCoroutine(FinishEating(_obj));


    public IEnumerator FinishEating(Transform _obj)
    {
        //_obj.GetComponent<Rigidbody>().useGravity = true;
        _obj.parent = null;
        //myFood.DigestTheFood();
        Destroy(_obj.gameObject, duration/3);
        yield return null;
    }
   
}
                                        


