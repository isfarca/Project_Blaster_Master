using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndingScript : MonoBehaviour {

    private bool beginningOfTheEnd = false;
    private int endCounter = 0;

    AudioSource audioData;
    private Feedback _feedbackScriptLeft, _feedbackScriptRight;
    playerMovement
    public GameObject player;
    public Movement movementScript;
    //light
    public GameObject hallLight1, hallLight2, hallLight3, hallLight4;
    private Light light1, light2, light3, light4;

    public GameObject emissionObject1, emissionObject2, emissionObject3, emissionObject4;
    private TurnOffEmission turnOffScript1, turnOffScript2, turnOffScript3, turnOffScript4;


    private void Awake()
    {
        audioData = GetComponent<AudioSource>();

        _feedbackScriptLeft = GameObject.Find("FeedbackLeft").GetComponent<Feedback>();
        _feedbackScriptRight = GameObject.Find("FeedbackRight").GetComponent<Feedback>();

        movementScript = player.GetComponent<Movement>();

        light1 = hallLight1.GetComponent<Light>();
        light2 = hallLight2.GetComponent<Light>();
        light3 = hallLight3.GetComponent<Light>();
        light4 = hallLight4.GetComponent<Light>();

        turnOffScript1 = emissionObject1.GetComponent<TurnOffEmission>();
        turnOffScript2 = emissionObject2.GetComponent<TurnOffEmission>();
        turnOffScript3 = emissionObject3.GetComponent<TurnOffEmission>();
        turnOffScript4 = emissionObject4.GetComponent<TurnOffEmission>();
    }

    void OnTriggerEnter()
    {
        StartCoroutine(BeginningOfTheEnd());
    }

    private IEnumerator BeginningOfTheEnd()
    {
        // disable lights and movement
        light1.enabled = false;
        light2.enabled = false;
        light3.enabled = false;
        light4.enabled = false;
        movementScript.enabled = false;

        turnOffScript1.on = false;
        turnOffScript2.on = false;
        turnOffScript3.on = false;
        turnOffScript4.on = false;

        yield return new WaitForSeconds(2);
        // roar and controller vibration
        audioData.Play(0);
        _feedbackScriptLeft.Vibrate(VibrationForce.Hard);
        _feedbackScriptRight.Vibrate(VibrationForce.Hard);


        yield return new WaitForSeconds(5);
        //load 'Game Over' screen
        SceneManager.LoadScene("GameOver");
        StopAllCoroutines();
    }
}
