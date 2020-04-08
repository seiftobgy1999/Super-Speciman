using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerScript : MonoBehaviour
{
    public Text Timer;
    public int time;

    private float nextActionTime = 0.0f;
    // Start is called before the first frame update
    void Start()
    {
        time = 0;
        Timer = GameObject.Find("Timer").GetComponent<Text>();
        Timer.text = time.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
        if (Time.time > nextActionTime)
        {
            nextActionTime += 1;
            time++;
            Timer.text = time.ToString();
        }
    }
}
