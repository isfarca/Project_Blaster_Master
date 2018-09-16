using UnityEngine;

public class Lock : MonoBehaviour 
{
    // Declare variables.
    [SerializeField] private Transform _offset;

    /// <summary>
    /// Usable key.
    /// </summary>
    /// <param name="other">Key.</param>
    private void OnTriggerStay(Collider other)
    {   
        // The game object match, then continue usable logic.
        if (!other.gameObject.CompareTag("Key1") && !other.gameObject.CompareTag("Key2"))
            return;
        
        // Press the hand trigger, than can rotate the key, otherwise key plug in the hole.
        if (!OVRInput.Get(OVRInput.Button.PrimaryHandTrigger) || !OVRInput.Get(OVRInput.Button.SecondaryHandTrigger))
        {
            other.transform.GetChild(0).position = _offset.position;
            other.transform.parent = transform;
        }
        else if (OVRInput.Get(OVRInput.Button.PrimaryHandTrigger) || OVRInput.Get(OVRInput.Button.SecondaryHandTrigger))
        {
            if (gameObject.CompareTag("Lock1") != other.gameObject.CompareTag("Key1") &&
                gameObject.CompareTag("Lock2") != other.gameObject.CompareTag("Key2"))
                return;
            
            if (other.gameObject.CompareTag("Key1") && other.transform.eulerAngles.x >= 180.0f)
                LockKey(other);
            else if (other.gameObject.CompareTag("Key2") && other.transform.eulerAngles.x <= 0.0f)
                LockKey(other);
        }
    }

    /// <summary>
    /// Lock the key in the hole and open the door..
    /// </summary>
    /// <param name="other">Key.</param>
    private static void LockKey(Component other)
    {
        // Lock the key in the hole.
        other.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
        other.GetComponent<OVRGrabbable>().enabled = false;
    }
}