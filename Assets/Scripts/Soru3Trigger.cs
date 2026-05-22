using UnityEngine;

public class Soru3Trigger : MonoBehaviour
{
    private bool triggered;

    private readonly string[] messages = new string[]
    {
        "<b>— Bulmaca Odası —</b>\n\nBu odada zaman ikiye bölünmüş. Geçmişte var olan şey gelecekte yok, gelecekte var olan şey geçmişte kayıp.",

        "Toplar rastgele dizilmemiş — her birinin <b>doğru bir sırası</b> var.\n\n<color=#BB88FF><b>Q tuşunu</b></color> kullan. Zamanlar arasında geç, topların hangi çağda göründüğünü gözlemle.",

        "Eşleştirme sırasını <b>atlarsan</b> bulmaca sıfırlanır.\n\nAcele etme. Önce <i>gözlemle</i>, sonra hareket et.",

        "İpucu: Renk her şeyi söyler.\n\n<color=#FF8888>Yanlış bir hamle</color> seni başa döndürür.\n<color=#88FF88>Doğru sıra</color> ise kapıyı açar.",
    };

    void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player") || triggered) return;
        triggered = true;

        if (NarrativePanel.Instance == null)
            new GameObject("NarrativePanel").AddComponent<NarrativePanel>();

        NarrativePanel.Instance.ShowSequence(messages);
        gameObject.SetActive(false);
    }
}
