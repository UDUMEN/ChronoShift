using StarterAssets;
using UnityEngine;

public class DisableJump : MonoBehaviour
{
    StarterAssetsInputs inputs;

    void Awake() => inputs = GetComponent<StarterAssetsInputs>();

    void LateUpdate()
    {
        if (inputs != null) inputs.jump = false;
    }
}
