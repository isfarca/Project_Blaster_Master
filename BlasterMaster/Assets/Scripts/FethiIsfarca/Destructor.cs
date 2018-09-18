using System.Collections;
using UnityEngine;

public class Destructor : MonoBehaviour
{
    // Declare variables.
    private Feedback _feedbackScriptLeft, _feedbackScriptRight;
    [SerializeField] private GameObject _offset;
    private BoxCollider _boxCollider;
    [SerializeField] private Transform _destructibleGameObjects;
    private OVRGrabbable _pluginGrabScript;
    private int _cutDuration;
    private bool _isDestroyable;

    /// <summary>
    /// Get the references.
    /// </summary>
    private void Awake()
    {
        // Get the scripts by name in hierarchy.
        _feedbackScriptLeft = GameObject.Find("FeedbackLeft").GetComponent<Feedback>();
        _feedbackScriptRight = GameObject.Find("FeedbackRight").GetComponent<Feedback>();

        // Get the physics.
        _boxCollider = GetComponent<BoxCollider>();

        // Get the script.
        _pluginGrabScript = _offset.GetComponent<OVRGrabbable>();
    }

    /// <summary>
    /// Set default values.
    /// </summary>
    private void Start()
    {
        // Initializing.
        _cutDuration = 0;
        _isDestroyable = false;
    }

    /// <summary>
    /// Physic.
    /// </summary>
    private void FixedUpdate()
    {
        // Have you not destructible game objects, than exit this function.
        if (_destructibleGameObjects.childCount <= 0)
            return;

        // When a player grabbed, than activate the trigger for destructible game objects and weapon.
        if (_pluginGrabScript.isGrabbed)
        {
            _boxCollider.isTrigger = true;

            for (int i = 0; i < _destructibleGameObjects.childCount; i++)
                _destructibleGameObjects.GetChild(i).GetComponent<BoxCollider>().isTrigger = true;
        }
        else
        {
            _boxCollider.isTrigger = false;

            for (int i = 0; i < _destructibleGameObjects.childCount; i++)
                _destructibleGameObjects.GetChild(i).GetComponent<BoxCollider>().isTrigger = false;
        }
    }

    /// <summary>
    /// Trigger only by destructible game objects with weapon.
    /// </summary>
    /// <param name="other">Destructible game object.</param>
    private void OnTriggerStay(Collider other)
    {
        // When the other game object, not destructible, than quit this function.
        if (!other.gameObject.CompareTag("Destructible"))
            return;

        // Activate the vibration by pressing hand trigger button from current hand.
        if (OVRInput.Get(OVRInput.Button.PrimaryHandTrigger))
            _feedbackScriptLeft.Vibrate(VibrationForce.Hard);
        else if (OVRInput.Get(OVRInput.Button.SecondaryHandTrigger))
            _feedbackScriptRight.Vibrate(VibrationForce.Hard);

        // When a player have over ten cuts in the game objetc, then is this destroyable.
        _cutDuration++;
        _isDestroyable = _cutDuration > 10 ? true : false;

        // Destroy the destructible game object.
        if (_isDestroyable)
        {
            Destroy(other.gameObject);
            StartCoroutine(CoolDown());
        }
    }

    /// <summary>
    /// Reset the cut amount from weapon.
    /// </summary>
    /// <returns>Seconds.</returns>
    private IEnumerator CoolDown()
    {
        yield return new WaitForSeconds(3);

        _cutDuration = 0;
        StopAllCoroutines();
    }
}