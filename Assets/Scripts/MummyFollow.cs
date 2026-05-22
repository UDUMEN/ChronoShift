using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class MummyFollow : MonoBehaviour
{
    public float speed = 3f;
    public float stoppingDistance = 1.5f;
    public float jumpHeight = 1.5f;
    public float jumpDuration = 0.6f;
    public float stuckTimeout = 1.5f;
    public float jumpCooldown = 3f;

    private NavMeshAgent agent;
    private Animator animator;
    private Transform target;
    private bool isJumping;

    private float stuckTimer;
    private float cooldownTimer;
    private Vector3 lastPos;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();

        agent.speed = speed;
        agent.stoppingDistance = stoppingDistance;
        agent.autoTraverseOffMeshLink = false;

        var player = GameObject.FindWithTag("Player");
        if (player != null)
            target = player.transform;

        lastPos = transform.position;
    }

    void OnEnable()
    {
        isJumping = false;
        stuckTimer = 0f;
        cooldownTimer = 0f;
        lastPos = transform.position;

        if (agent != null)
        {
            agent.isStopped = false;
            agent.ResetPath();
        }
    }

    void Update()
    {
        if (target == null || isJumping) return;

        cooldownTimer -= Time.deltaTime;
        agent.SetDestination(target.position);

        if (agent.isOnOffMeshLink)
        {
            StartCoroutine(JumpAcrossLink());
            return;
        }

        // Takılı kalma tespiti: hareket etmiyor ama hedefe uzak
        float moved = Vector3.Distance(transform.position, lastPos);
        float distToTarget = Vector3.Distance(transform.position, target.position);
        lastPos = transform.position;

        bool stuck = moved < 0.05f && distToTarget > stoppingDistance + 0.5f;
        stuckTimer = stuck ? stuckTimer + Time.deltaTime : 0f;

        if (stuckTimer >= stuckTimeout && cooldownTimer <= 0f)
        {
            NavMeshHit hit;
            if (NavMesh.SamplePosition(target.position, out hit, 6f, NavMesh.AllAreas))
            {
                stuckTimer = 0f;
                cooldownTimer = jumpCooldown;
                StartCoroutine(JumpToPosition(hit.position));
                return;
            }
        }

        bool isMoving = agent.hasPath &&
                        !agent.isStopped &&
                        agent.remainingDistance > agent.stoppingDistance;

        if (animator != null)
            animator.enabled = isMoving;
    }

    IEnumerator JumpAcrossLink()
    {
        isJumping = true;
        agent.isStopped = true;

        OffMeshLinkData link = agent.currentOffMeshLinkData;
        Vector3 from = transform.position;
        Vector3 to = link.endPos + Vector3.up * agent.baseOffset;

        yield return StartCoroutine(ArcMove(from, to));

        agent.CompleteOffMeshLink();
        agent.isStopped = false;
        isJumping = false;
    }

    IEnumerator JumpToPosition(Vector3 to)
    {
        isJumping = true;
        agent.isStopped = true;
        agent.enabled = false;

        yield return StartCoroutine(ArcMove(transform.position, to));

        agent.Warp(to);
        agent.enabled = true;
        agent.isStopped = false;
        isJumping = false;
    }

    IEnumerator ArcMove(Vector3 from, Vector3 to)
    {
        if (animator != null) animator.enabled = true;

        float elapsed = 0f;
        while (elapsed < jumpDuration)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / jumpDuration;
            float arc = Mathf.Sin(t * Mathf.PI) * jumpHeight;
            transform.position = Vector3.Lerp(from, to, t) + Vector3.up * arc;

            Vector3 dir = to - from;
            dir.y = 0f;
            if (dir.sqrMagnitude > 0.001f)
                transform.rotation = Quaternion.LookRotation(dir);

            yield return null;
        }

        transform.position = to;
    }
}
