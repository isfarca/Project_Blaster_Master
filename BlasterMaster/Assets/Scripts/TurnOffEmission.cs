using UnityEngine;

public class TurnOffEmission : MonoBehaviour
{

    public Material material;
    public bool on = true;

    void Update()
    {
        if (on)
            material.EnableKeyword("_EMISSION");
        else
            material.DisableKeyword("_EMISSION");
    }
}

