using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemActivatorV2 : MonoBehaviour
{

    // --------------------------------------------------
    // Variables:

    [SerializeField]
    
    //private int distanceFromPlayer;

    public GameObject player;
    //List, like arrays but more flexible, self sorting and easy to add and remove
    //
    private List<ActivatorItemV2> activatorItems;

    public List<ActivatorItemV2> addList;
    //temporary list 
    List<ActivatorItemV2> removeList = new List<ActivatorItemV2>();

    // --------------------------------------------------

    void Start()
    {
     //  player = GameObject.Find("Player");
        //initialize list
        
        activatorItems = new List<ActivatorItemV2>();
        addList = new List<ActivatorItemV2>();

        AddToList();
    }

    void AddToList()
    {
         if (addList.Count > 0)
         {
            foreach (ActivatorItemV2 item in addList)
            {
                if (item.item != null)
                {
                    activatorItems.Add(item);
                   
                }
            }

           addList.Clear();
        }
        //When you call a function, it runs to completion before returning. 
        //This effectively means that any action taking place in a function must happen within a single frame update
        StartCoroutine("CheckActivation");
      
    }



    IEnumerator CheckActivation()
    {
       // Debug.Log("running");

        //if theres something in the list
        if (activatorItems.Count > 0)
        {

           // Debug.Log(activatorItems.Count);
           //for every item in the list
            foreach (ActivatorItemV2 item in activatorItems)
            {

                // IF DISTANCE IS BIGGER THAN " DISTANCEFROMPLAYER" MAKE THE OBJECT INVISIBLE
                if ((Vector3.Distance(player.transform.position, item.item.transform.position) > item.item.GetComponent<DisableIfFarAwayV2>().distanceToDisable))
                        {
                    // if something has happened to that object since the last time we checked the list

                            if (item.item == null)
                            {
                        //we add it to a new list call removelist 
                                removeList.Add(item);
                                //Debug.Log("non exists");
                            }
                            else 
                            {
                        //if the item still exists
                        //but is not within the distance range
                        //we set it to false which does not show it
                                removeList.Add(item);
                                item.item.SetActive(false);
                              //  Debug.Log("here"+ item.item.tag);

                  
                           }
                               
                        }
              /* else if ((Vector3.Distance(player.transform.position, item.item.transform.position) > distanceFromPlayer+20) && item.item.tag.Equals("Medium"))
                {

                    if (item.item == null)
                    {
                        removeList.Add(item);
                        //Debug.Log("non exists");
                    }
                    else
                    {
                        removeList.Add(item);
                        item.item.SetActive(false);
                        Debug.Log("here" + item.item.tag);


                    }

                }

                else if ((Vector3.Distance(player.transform.position, item.item.transform.position) > distanceFromPlayer + 30) && item.item.tag.Equals("Large"))
                {

                    if (item.item == null)
                    {
                        removeList.Add(item);
                        //Debug.Log("non exists");
                    }
                    else
                    {
                        removeList.Add(item);
                        item.item.SetActive(false);
                      //  Debug.Log("here" + item.item.tag);


                    }

                }
                */

                // MAKE IT VISIBLE
                else
                        {
                    
                            if (item.item == null)
                            {
                                removeList.Add(item);
                                //Debug.Log("non exists");
                            }
                            else
                            {
                        //if it has not been destroyed and is within range
                        //we activate the item
                              //  removeList.Add(item);
                              item.item.SetActive(true);
                          //  Debug.Log("on");
                            }
                        }

                        yield return new WaitForSeconds(0.01f);
                    }
             
        }
        //a pause so it doesnt lag
        yield return new WaitForSeconds(0.1f);

        AddToList();


    } // end of Ienumerator




} //end of class




//class each instance of item will have the gameobject item within it 
public class ActivatorItemV2
{
    public GameObject item;
}

/*DOCUMENTATION*\
 * Base code was built upon https://www.youtube.com/watch?v=tAbEvzZVz5E&feature=youtu.be
 * HOW TO USE
 * Attach to all items in scene, then use the input to specify the distance that you want the object to be viewable from */