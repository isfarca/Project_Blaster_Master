﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlickeringLight : MonoBehaviour {

    private int flickerTimer, flickerAt, lightOffTimer, lightOffAt;
    //bool lightOn;
    private Light light;

    void Start () {
        light = this.GetComponent<Light>();
        lightOffAt = 3;
        FlickerOn();
	}
	
	void Update () {
        flickerTimer++;

        if (flickerTimer >= flickerAt)
        {
            FlickerOn();
        }

        if (light.enabled == true && lightOffTimer <= lightOffAt)
        {
            lightOffTimer++;
        }
        else if (light.enabled == true && lightOffTimer > lightOffAt)
        {
            FlickerOff();
        }
	}

    void FlickerOn()
    {
        //turn light on
        light.enabled = true;
        flickerTimer = 0;
        flickerAt = Random.Range(5, 50);
    }

    void FlickerOff()
    {
        //turn light off
        light.enabled = false;
        lightOffTimer = 0;
    }
}