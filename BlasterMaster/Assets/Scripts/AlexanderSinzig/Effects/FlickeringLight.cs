using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlickeringLight : MonoBehaviour {

    // other object (to toggle emission)
    // set via Inspector
    public GameObject emissionObject;
    public FlickeringEmission flickerScript;

    private int flickerTimer, flickerAt, lightOffTimer, lightOffAt;
    // bool lightOn;
    private Light light;

    void Start () {
        light = this.GetComponent<Light>();
        lightOffAt = 3;
        FlickerOn();

        flickerScript = emissionObject.GetComponent<FlickeringEmission>();
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
        // turn light on
        light.enabled = true;
        flickerTimer = 0;
        flickerAt = Random.Range(5, 50);
        // turn Emission on
        flickerScript.on = true;
    }

    void FlickerOff()
    {
        // turn light off
        light.enabled = false;
        lightOffTimer = 0;
        // turn Emission ooff
        flickerScript.on = false;
    }
}
