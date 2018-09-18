using UnityEngine;

public class Hand : MonoBehaviour
{
    // Declare variables.
    private SphereCollider _sphereCollider;

    /// <summary>
    /// Get the references.
    /// </summary>
    private void Awake()
    {
        // Get the collider from hand.
        _sphereCollider = GetComponent<SphereCollider>();
    }

    /// <summary>
    /// Collider from hand permanently activate, if it turns off.
    /// </summary>
    private void FixedUpdate()
    {
        _sphereCollider.enabled = true;
    }
}