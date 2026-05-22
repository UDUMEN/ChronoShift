using UnityEngine;

public class PlayerSoundController : MonoBehaviour
{
    CharacterController cc;
    StarterAssets.StarterAssetsInputs inputs;

    AudioSource runSource;
    float stepTimer;
    bool wasGrounded;
    bool wasRunning;

    void Start()
    {
        cc = GetComponent<CharacterController>();
        inputs = GetComponent<StarterAssets.StarterAssetsInputs>();
        wasGrounded = cc != null && cc.isGrounded;

        // Dedicated looping source for running sound
        runSource = gameObject.AddComponent<AudioSource>();
        runSource.loop = true;
        runSource.spatialBlend = 0f;
        runSource.volume = 0.7f;
        runSource.playOnAwake = false;
    }

    void Update()
    {
        if (AudioManager.Instance == null || cc == null) return;

        bool grounded = cc.isGrounded;
        float speed = new Vector3(cc.velocity.x, 0f, cc.velocity.z).magnitude;
        bool moving = speed > 0.3f;
        bool sprinting = inputs != null && inputs.sprint && moving && grounded;

        // Jump sound
        if (wasGrounded && !grounded)
            AudioManager.Play(AudioManager.Instance.jumpClip, 0.8f);
        wasGrounded = grounded;

        // Running sound — looping, start/stop on sprint change
        if (AudioManager.Instance.runClip != null)
        {
            runSource.clip = AudioManager.Instance.runClip;
            if (sprinting && !wasRunning)
                runSource.Play();
            else if (!sprinting && wasRunning)
                runSource.Stop();
        }
        wasRunning = sprinting;

        // Walk footstep sounds (only when not sprinting)
        if (grounded && moving && !sprinting)
        {
            stepTimer -= Time.deltaTime;
            if (stepTimer <= 0f)
            {
                AudioManager.Play(AudioManager.Instance.walkClip, 0.65f);
                stepTimer = 0.48f;
            }
        }
        else if (!moving)
        {
            stepTimer = 0f;
        }
    }

    void OnDisable()
    {
        if (runSource != null) runSource.Stop();
        wasRunning = false;
    }
}
