using UnityEngine;

public class Lock : MonoBehaviour 
{
    // Declare variables.
    [SerializeField] private Transform _offset;
    private Feedback _feedbackScriptLeft, _feedbackScriptRight;
    private Door _leftDoor, _rightDoor;

    /// <summary>
    /// Get the references.
    /// </summary>
    private void Awake()
    {
        // Get the scripts by name in hierarchy.
        _feedbackScriptLeft = GameObject.Find("FeedbackLeft").GetComponent<Feedback>();
        _feedbackScriptRight = GameObject.Find("FeedbackRight").GetComponent<Feedback>();
        _leftDoor = GameObject.Find("LeftDoor").GetComponent<Door>();
        _rightDoor = GameObject.Find("RightDoor").GetComponent<Door>();
    }

    /// <summary>
    /// Usable key.
    /// </summary>
    /// <param name="other">Key.</param>
    private void OnTriggerStay(Collider other)
    {   
        // The game object match, then continue usable logic.
        if (!other.gameObject.CompareTag("Key"))
            return;

        // Press the hand trigger, than can rotate the key, otherwise key plug in the hole.
        if (OVRInput.Get(OVRInput.Button.PrimaryHandTrigger) || OVRInput.Get(OVRInput.Button.SecondaryHandTrigger))
            return;

        other.GetComponent<Rigidbody>().isKinematic = true;
        other.transform.GetChild(0).position = _offset.position;

        _feedbackScriptLeft.Vibrate(VibrationForce.Hard);
        _feedbackScriptRight.Vibrate(VibrationForce.Hard);

        _leftDoor.LockCount++;
        _rightDoor.LockCount++;

        Destroy(this); 
    }
}