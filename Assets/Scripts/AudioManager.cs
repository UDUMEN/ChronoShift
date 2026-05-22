using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }

    [Header("Music")]
    public AudioClip backgroundMusic;

    [Header("Player")]
    public AudioClip walkClip;
    public AudioClip runClip;
    public AudioClip jumpClip;

    [Header("World")]
    public AudioClip questionMarkClip;
    public AudioClip timeTransitionClip;
    public AudioClip levelTransitionClip;

    AudioSource bgSource;
    AudioSource sfxSource;

    void Awake()
    {
        if (Instance != null) { Destroy(gameObject); return; }
        Instance = this;
        DontDestroyOnLoad(gameObject);

        bgSource = gameObject.AddComponent<AudioSource>();
        bgSource.loop = true;
        bgSource.volume = 0.06f;
        bgSource.spatialBlend = 0f;
        bgSource.pitch = 1f;
        bgSource.priority = 0;
        bgSource.playOnAwake = false;
        bgSource.ignoreListenerVolume = false;

        sfxSource = gameObject.AddComponent<AudioSource>();
        sfxSource.playOnAwake = false;
        sfxSource.spatialBlend = 0f;
        sfxSource.volume = 1f;

        if (backgroundMusic != null)
        {
            bgSource.clip = backgroundMusic;
            bgSource.Play();
            bgSource.time = 3f; // mp3 dosyasındaki fade-in'i atla
            bgSource.volume = 0.06f; // Play sonrası tekrar set et
        }
    }

    public void PlaySFX(AudioClip clip, float volume = 1f)
    {
        if (clip == null || sfxSource == null) return;
        sfxSource.PlayOneShot(clip, volume);
    }

    public static void Play(AudioClip clip, float volume = 1f)
    {
        if (Instance != null) Instance.PlaySFX(clip, volume);
    }
}
