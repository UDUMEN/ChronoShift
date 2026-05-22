using UnityEngine;
using UnityEngine.AI;

public class MummyCatch : MonoBehaviour
{
    public Transform spawnPoint;
    public GameObject soru1;
    public GameObject storyUI;
    public GameObject sonradanWall;
    public GameObject soruDuvar;

    public float catchDistance = 1.2f;

    Transform player;
    bool triggered;
    Vector3 mummySpawnPos;
    Quaternion mummySpawnRot;

    void Awake()
    {
        mummySpawnPos = transform.position;
        mummySpawnRot = transform.rotation;
    }

    void OnEnable()
    {
        var p = GameObject.FindWithTag("Player");
        if (p != null) player = p.transform;
        triggered = false;

        // Warp to spawn position every time the mummy is re-activated
        var agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        if (agent != null)
        {
            agent.enabled = true;
            agent.Warp(mummySpawnPos);
        }
        else
        {
            transform.position = mummySpawnPos;
        }
        transform.rotation = mummySpawnRot;
    }

    void Update()
    {
        if (triggered || player == null) return;
        if (Vector3.Distance(transform.position, player.position) <= catchDistance)
        {
            triggered = true;
            ResetScenario(player);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;
        triggered = true;
        ResetScenario(other.transform.root);
    }

    void ResetScenario(Transform player)
    {
        // Playeri spawn noktasına taşı
        var root = player.root;
        var cc = root.GetComponent<CharacterController>();
        if (cc != null)
        {
            cc.enabled = false;
            root.position = spawnPoint.position;
            cc.enabled = true;
        }
        else
        {
            root.position = spawnPoint.position;
        }

        // Sahneyi sıfırla
        if (soru1 != null)        soru1.SetActive(true);
        if (storyUI != null)      storyUI.SetActive(false);
        if (sonradanWall != null) sonradanWall.SetActive(false);
        if (soruDuvar != null)    soruDuvar.SetActive(true);

        // Q tuşunu tekrar aktif et
        TimeManager.qDisabled = false;

        // Sadece devre dışı bırak — pozisyon OnEnable'da Warp ile sıfırlanacak
        gameObject.SetActive(false);
    }
}
