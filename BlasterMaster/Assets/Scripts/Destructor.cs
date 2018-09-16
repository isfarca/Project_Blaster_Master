using UnityEngine;

public class Destructor : MonoBehaviour
{
    // Declare variables.
    private Feedback _feedbackScriptLeft, _feedbackScriptRight;
    [SerializeField] private Rigidbody _physic;

    /// <summary>
    /// Get the references.
    /// </summary>
    private void Awake()
    {
        // Get the scripts by name in hierarchy.
        _feedbackScriptLeft = GameObject.Find("FeedbackLeft").GetComponent<Feedback>();
        _feedbackScriptRight = GameObject.Find("FeedbackRight").GetComponent<Feedback>();
    }

    /// <summary>
    /// Trigger only by destructible game objects with weapon.
    /// </summary>
    /// <param name="other">Destructible game object.</param>
    private void OnTriggerEnter(Collider other)
    {
        // When the other game object, not destructible, than quit this function.
        if (!other.gameObject.CompareTag("Destructible"))
            return;

        // Activate the vibration by pressing hand trigger button from current hand.
        if (OVRInput.Get(OVRInput.Button.PrimaryHandTrigger))
            _feedbackScriptLeft.Vibrate(VibrationForce.Medium);
        else if (OVRInput.Get(OVRInput.Button.SecondaryHandTrigger))
            _feedbackScriptRight.Vibrate(VibrationForce.Medium);

        // Destroy the destructible game object.
        if (_physic.velocity.magnitude > 10.0f)
            Destroy(other.gameObject);
    }
}