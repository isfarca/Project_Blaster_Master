using UnityEngine;

public class Jetpack : MonoBehaviour
{
    // Declare variables.
    [SerializeField] private Transform _comparisonTransform;
    [SerializeField] private Transform _leftHandAnchorTransform;
    [SerializeField] private Transform _rightHandAnchorTransform;
    private Vector3 _moveForceLeft, _moveForceRight, _moveForce;
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
        // Movement in space.
        if (OVRInput.Get(OVRInput.Button.Three)) // Left jetpack.
            _moveForceLeft += (_comparisonTransform.position - _leftHandAnchorTransform.position) / 80;
        else if (OVRInput.Get(OVRInput.Button.One)) // Right jetpack.
            _moveForceRight += (_comparisonTransform.position - _rightHandAnchorTransform.position) / 80;

        // Set move force.
        _moveForce = _moveForceLeft + _moveForceRight;

        // Move the player with move force values.
        Controller.Move(_moveForce);

        // 0.5 per cent lost movement speed.
        _moveForceLeft *= 0.995f;
        _moveForceRight *= 0.995f;
    }
}
