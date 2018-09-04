using UnityEngine;

public class Jetpack : MonoBehaviour
{
    // Declare variables.
    [SerializeField] private Transform _comparisonTransform;
    [SerializeField] private Transform _leftHandAnchorTransform;
    [SerializeField] private Transform _rightHandAnchorTransform;
    private Vector3 _moveForceLeft, _moveForceRight;
    protected CharacterController Controller;

	/// <summary>
    /// Calling before start.
    /// </summary>
    private void Awake()
    {
		// Get the scripts and components.
        Controller = GetComponent<CharacterController>();
    }

    /// <summary>
    /// Calling per frame.
    /// </summary>
    private void Update()
    {
		// Subtract the rib cage vector with the hand vector and divide by four.
        _moveForceLeft = (_comparisonTransform.position - _leftHandAnchorTransform.position) / 4;
        _moveForceRight = (_comparisonTransform.position - _rightHandAnchorTransform.position) / 4;

        // Left jetpack.
        if (OVRInput.Get(OVRInput.Button.Three))
            Controller.Move(_moveForceLeft);

        // Right jetpack.
        if (OVRInput.Get(OVRInput.Button.One))
            Controller.Move(_moveForceRight);
    }
}