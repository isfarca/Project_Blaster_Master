using UnityEngine;

public class LayDown : MonoBehaviour
{
    // Declare variables.
    [SerializeField] private Transform _offset;

    /// <summary>
    /// Storable game object.
    /// </summary>
    /// <param name="other">Game object with storable tag.</param>
    private void OnTriggerStay(Collider other)
    {
        // The game object match, then continue storable logic.
        if (gameObject.CompareTag("Storable") != other.gameObject.CompareTag("Storable"))
            return;

        // If no grab button pressed, than storable game object.
        if (!OVRInput.Get(OVRInput.Button.PrimaryHandTrigger) || !OVRInput.Get(OVRInput.Button.SecondaryHandTrigger))
        {
            other.GetComponent<Rigidbody>().isKinematic = true;
            other.transform.position = _offset.position;
            other.transform.rotation = Quaternion.identity;
            other.transform.parent = transform;
        }
    }
}