using UnityEngine;
using UnityEngine.SceneManagement;

public class EndingScript : MonoBehaviour {

    private bool beginningOfTheEnd = false;
    private int endCounter = 0;
    private int CounterSeconds = 0;

    AudioSource audioData;
    private Feedback _feedbackScriptLeft, _feedbackScriptRight;
    //playerMovement
    public GameObject player;
    public Movement movementScript;
    //light
    public GameObject hallLight1, hallLight2, hallLight3, hallLight4;
    [SerializeField] private Light light1, light2, light3, light4;

    public GameObject emissionObject1, emissionObject2, emissionObject3, emissionObject4;
    private TurnOffEmission turnOffScript1, turnOffScript2, turnOffScript3, turnOffScript4;


    private void Awake()
    {
        audioData = GetComponent<AudioSource>();

        _feedbackScriptLeft = GameObject.Find("FeedbackLeft").GetComponent<Feedback>();
        _feedbackScriptRight = GameObject.Find("FeedbackRight").GetComponent<Feedback>();

        player = GameObject.FindGameObjectWithTag("Player");
        movementScript = player.GetComponent<Movement>();

        turnOffScript1 = emissionObject1.GetComponent<TurnOffEmission>();
        turnOffScript2 = emissionObject2.GetComponent<TurnOffEmission>();
        turnOffScript3 = emissionObject3.GetComponent<TurnOffEmission>();
        turnOffScript4 = emissionObject4.GetComponent<TurnOffEmission>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("Ending trigger");
            beginningOfTheEnd = true;
        }
    }

    private void Update()
    {
        if (!beginningOfTheEnd)
            return;

        endCounter++;
        if (endCounter % 60 == 0)
            CounterSeconds++;

        if (CounterSeconds == 1)
        {
            Debug.Log("After 1 second");
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
        }
        else if (CounterSeconds == 3)
        {
            Debug.Log("Start");
            // roar and controller vibration
            audioData.Play(0);
            _feedbackScriptLeft.Vibrate(VibrationForce.Hard);
            _feedbackScriptRight.Vibrate(VibrationForce.Hard);
            Debug.Log("After 2 seconds");
        }
        else if (CounterSeconds == 8)
        {
            //load 'Game Over' screen
            Debug.Log("After 8 seconds.");
            SceneManager.LoadScene("GameOver");
            StopAllCoroutines();
        }
    }
}
