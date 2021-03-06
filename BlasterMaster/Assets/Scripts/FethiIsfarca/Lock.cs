﻿using UnityEngine;

public class Lock : MonoBehaviour 
{
    // Declare variables.
    [SerializeField] private Transform _offset;
    private Feedback _feedbackScriptLeft, _feedbackScriptRight;
    [SerializeField] private Door _leftDoor, _rightDoor;

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
        other.transform.position = _offset.position;
        other.transform.rotation = Quaternion.identity;
        other.transform.parent = transform;
        other.GetComponent<SphereCollider>().enabled = false;
        other.GetComponent<OVRGrabbable>().enabled = false;
        other.GetComponent<Grabber>().enabled = false;
        other.transform.GetChild(0).GetComponent<BoxCollider>().enabled = false;
        other.transform.GetChild(0).GetChild(0).gameObject.SetActive(false);

        _feedbackScriptLeft.Vibrate(VibrationForce.Hard);
        _feedbackScriptRight.Vibrate(VibrationForce.Hard);

        _leftDoor.LockCount++;
        _rightDoor.LockCount++;

        Destroy(this); 
    }
}