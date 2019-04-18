using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ShieldTimer : MonoBehaviour
{
    Image fillImg;
    float timeAmt = 20;
    float time;
    private bool isTicking;
    private bool isTimeOver;


    // Use this for initialization
    void Start()
    {
        isTicking = false;
        fillImg = this.GetComponent<Image>();
        time = timeAmt;
        isTimeOver = false;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (isTicking)
        {
            if (time > 0)
            {
                time -= Time.deltaTime;
                fillImg.fillAmount = time / timeAmt;

            }
            else
                isTimeOver = true;
        }
       
    }
    
    public void resetTimer(){
        //time = 0;
        isTimeOver = false;
        time = timeAmt;
    }


    public void playTimer()
    {
        isTicking = true;
        FixedUpdate();
    }

    public void stopTimer()
    {
        isTicking = false;
    }

    public bool timeEqualZero()
    {
        return isTimeOver;
    }
}


// Code reuse from http://aarlangdi.blogspot.com/2016/02/making-health-bar-in-unity-5-radial.html
// Added playTimer(), stopTimer() and timeEqualZero()
