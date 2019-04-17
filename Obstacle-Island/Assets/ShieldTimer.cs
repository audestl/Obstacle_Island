using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ShieldTimer : MonoBehaviour
{
    Image fillImg;
    float timeAmt = 10;
    float time;
    private bool isTicking;

    // Use this for initialization
    void Start()
    {
        isTicking = false;
        fillImg = this.GetComponent<Image>();
        time = timeAmt;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (time > 0)
        {
            time -= Time.deltaTime;
            fillImg.fillAmount = time / timeAmt;
            isTicking = true;
        }
        else
            isTicking = false;
    }


    void playTimer()
    {
        FixedUpdate();
    }
}


// Code reuse from http://aarlangdi.blogspot.com/2016/02/making-health-bar-in-unity-5-radial.html
// Added isTicking variable to keep track of the Timer being on.