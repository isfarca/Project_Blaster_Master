using UnityEngine;

public class Door : MonoBehaviour 
{
    // Declare variables.
    private Feedback _feedbackScriptLeft, _feedbackScriptRight;
    private Animator _animator;

    /// <summary>
    /// Get the references.
    /// </summary>
    private void Awake()
    {
        // Get the scripts by name in hierarchy.
        _feedbackScriptLeft = GameObject.Find("FeedbackLeft").GetComponent<Feedback>();
        _feedbackScriptRight = GameObject.Find("FeedbackRight").GetComponent<Feedback>();

        // Get the animator.
        _animator = GetComponent<Animator>();
    }

    /// <summary>
    /// Initializing
    /// </summary>
    private void Start()
    {
        // Set default values.
        LockCount = 0;
    }

    /// <summary>
    /// Open the door and destroy this script.
    /// </summary>
    private void Update()
    {
        if (LockCount < 2) 
            return;

        _feedbackScriptLeft.Vibrate(VibrationForce.Hard);
        _feedbackScriptRight.Vibrate(VibrationForce.Hard);
        _animator.enabled = true;
        _animator.Play(gameObject.name);

        Destroy(this);
    }

    // Auto-property.
    public int LockCount { get; set; }
}