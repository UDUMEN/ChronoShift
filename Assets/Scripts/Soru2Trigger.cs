using UnityEngine;

public class Soru2Trigger : MonoBehaviour
{
    private bool triggered;

    private readonly string[] messages = new string[]
    {
        "<b>— Level 2: Kırık Zaman —</b>\n\nHoş geldin. Bu yer, geçmiş ile geleceğin üst üste çöktüğü bir kırılma noktası.",

        "Elindeki en güçlü silah <color=#BB88FF><b>Q tuşu</b></color>.\n\nGeçmişe geç — gizli yollar açılır, engeller ortadan kalkar.\nGeleceğe dön — tehlikeler değişir, dünya yeniden şekillenir.",

        "Her iki zaman da kendi sırlarını saklar.\nHangisinde ne bulacağını bilmek için <b>denemekten</b> korkma.",

        "Portalı bulmalısın.\n\n<color=#FF8888>Ama dikkat —</color> bu bölümde geçmiş her zaman güvenli değil.\nVe gelecek... hiç güvenli olmadı.",
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
