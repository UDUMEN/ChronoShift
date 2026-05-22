using UnityEngine;

public class GoalTrigger : MonoBehaviour
{
    public float rotateSpeed = 60f;
    public float bobAmplitude = 0.25f;
    public float bobSpeed = 2f;

    const string Narrative =
        "Burada zaman, tek bir düzlemde akmaz.\n" +
        "Adımlarının altında <i>geçmiş</i> uyur — başının üstünde <i>gelecek</i> döner.\n\n" +
        "<color=#D966FF><b>[ Q ]</b></color>  tuşuna her bastığında dünya nefesini tutar... ve değişir.\n" +
        "İki çağın sırrını birlikte çöz. Katmanları aşındır, yolu bul.\n\n" +
        "<size=18><color=#C090E0>Level 2 Portalı, zamanı eğebilenler için açılır.</color></size>";

    [TextArea(3, 10)]
    public string narrativeOverride = "";

    Vector3 startPos;

void Start()
    {
        startPos = transform.position;

        if (NarrativePanel.Instance == null)
            new GameObject("NarrativePanelManager").AddComponent<NarrativePanel>();
    }

    void Update()
    {
        transform.Rotate(0f, rotateSpeed * Time.deltaTime, 0f, Space.World);
        var p = startPos;
        p.y += Mathf.Sin(Time.time * bobSpeed) * bobAmplitude;
        transform.position = p;
    }

    void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;
        string text = string.IsNullOrEmpty(narrativeOverride) ? Narrative : narrativeOverride;
        NarrativePanel.Instance.Show(text);
    }
}
