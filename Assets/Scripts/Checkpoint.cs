using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    [Tooltip("0 = başlangıç, 1 = orta, 2 = sona yakın")]
    public int index = 0;

    [Tooltip("Oyuncu bu noktada spawn olur — boş bırakılırsa transform.position kullanılır")]
    public Transform spawnAnchor;

    bool activated;

    // Orb referansı — aktif olunca rengi değişir
    Renderer orbRenderer;

    static readonly Color InactiveColor  = new Color(0.55f, 0.65f, 0.80f, 1f); // soğuk mavi-gri
    static readonly Color ActiveColor    = new Color(0.20f, 1.00f, 0.55f, 1f); // parlak yeşil
    static readonly Color StartColor     = new Color(0.20f, 1.00f, 0.55f, 1f); // start zaten aktif

    void Awake()
    {
        // CheckpointManager yoksa oluştur
        if (CheckpointManager.Instance == null)
            new GameObject("CheckpointManager").AddComponent<CheckpointManager>();
    }

    void Start()
    {
        // Orb child'ını bul
        var orbTr = transform.Find("Orb");
        if (orbTr != null) orbRenderer = orbTr.GetComponent<Renderer>();

        // Start checkpoint (index 0) oyun başladığında otomatik aktif
        if (index == 0)
            Activate();
        else
            SetOrbColor(InactiveColor);
    }

    void OnTriggerEnter(Collider other) => TryActivate(other);
    void OnTriggerStay(Collider other)  => TryActivate(other);

    void TryActivate(Collider other)
    {
        if (activated) return;
        if (!other.CompareTag("Player")) return;
        Activate();
    }

    void Activate()
    {
        activated = true;
        Vector3 spawnPos = spawnAnchor != null ? spawnAnchor.position : transform.position;
        CheckpointManager.Instance.SetCheckpoint(index, spawnPos, force: index == 0);
        SetOrbColor(ActiveColor, glow: true);
    }

    void SetOrbColor(Color c, bool glow = false)
    {
        if (orbRenderer == null) return;

        // renderer.material ile unique instance oluştur — her shader tipinde çalışır
        var mat = orbRenderer.material;
        mat.color = c;

        if (mat.HasProperty("_BaseColor"))
            mat.SetColor("_BaseColor", c);

        if (glow && mat.HasProperty("_EmissionColor"))
        {
            mat.EnableKeyword("_EMISSION");
            mat.SetColor("_EmissionColor", new Color(0f, 0.8f, 0.3f, 1f) * 2.0f);
        }
    }
}
