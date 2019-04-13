using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    private bool shrinking = true;
    public float minSize = 0.4f;
    public float shrinkSpeed = 0.4f;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("Starvation", 1f);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (shrinking)
        {
            transform.localScale -= Vector3.one * Time.deltaTime * shrinkSpeed;
            shrinking = false;
        }
    }

    IEnumerator Starvation(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        //code executes after the waitTime is elapsed 
        if (transform.localScale.z < minSize)
        {
            shrinking = false;
            Destroy(gameObject);
        }
        else
        {
            shrinking = true;
        }
        StartCoroutine("Starvation", 1f);
    }
}
