using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{
    private NavMeshAgent m_NavMeshAgent;

    [Header("Waypoint Settings")]
    public Transform waypointA;
    public Transform waypointB;

    [Header("Detection Settings")]
    public Transform player;                 
    public float detectionRange = 10f;
    private bool m_PlayerDetected = false;

    [Header("Shooting Settings")]
    public GameObject bulletPrefab;
    public Transform firePoint;
    public float fireRate = 1f;             
    private float m_FireCooldown = 0f;

    private WaitForSeconds m_WaitForSeconds;

    void Start()
    {
        m_WaitForSeconds = new WaitForSeconds(15f);

        m_NavMeshAgent = GetComponent<NavMeshAgent>();
        if (!m_NavMeshAgent) Debug.Log("No Agent Found");

        StartCoroutine(PatrolArea());
    }

    void Update()
    {
        CheckForPlayer();

        if (m_PlayerDetected)
        {
            HandleShooting();
        }
    }

    void CheckForPlayer()
    {
        if (player == null) return;

        float distance = Vector3.Distance(transform.position, player.position);
        bool inRange = distance <= detectionRange;

        if (inRange && !m_PlayerDetected)
        {
            m_PlayerDetected = true;
            m_NavMeshAgent.isStopped = true;
            m_NavMeshAgent.ResetPath();
        }
        else if (!inRange && m_PlayerDetected)
        {
            m_PlayerDetected = false;
            m_NavMeshAgent.isStopped = false;
        }

        if (m_PlayerDetected)
        {
            Vector3 lookDir = player.position - transform.position;
            lookDir.y = 0f;
            if (lookDir.sqrMagnitude > 0.01f)
                transform.rotation = Quaternion.LookRotation(lookDir);
        }
    }

    void HandleShooting()
    {
        m_FireCooldown -= Time.deltaTime;

        if (m_FireCooldown <= 0f)
        {
            Shoot();
            m_FireCooldown = 1f / fireRate;
        }
    }

    void Shoot()
    {
        if (bulletPrefab == null || firePoint == null) return;

        Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
    }

    IEnumerator PatrolArea()
    {
        while (true)
        {
            if (!m_PlayerDetected)
            {
                m_NavMeshAgent.SetDestination(waypointA.position);
            }
            yield return m_WaitForSeconds;

            if (!m_PlayerDetected)
            {
                m_NavMeshAgent.SetDestination(waypointB.position);
            }
            yield return m_WaitForSeconds;
        }
    }
}