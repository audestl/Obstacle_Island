using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableIfFarAwayV2 : MonoBehaviour
{
    public GameObject itemActivatorObject;
    private ItemActivatorV2 activationScript;
    public float distanceToDisable = 10;

    // --------------------------------------------------

    void Start()
    {
        //itemActivatorObject = GameObject.Find("ItemActivatorObject");
        activationScript = itemActivatorObject.GetComponent<ItemActivatorV2>();

        StartCoroutine("AddToList");
    }

    IEnumerator AddToList()
    {
        yield return new WaitForSeconds(0.01f);
       // Debug.Log(this.gameObject);

        activationScript.addList.Add(new ActivatorItemV2 { item = this.gameObject });
    }
}