using UnityEngine;
using UnityEngine.UIElements;

public class Drone : MonoBehaviour
{
    private Vector3 m_DroneMovement;

    [Header("Drone Flight Settings")]
    public float flightSpeed = 5f;
    public float verticleSpeed = 0.4f;

    [Header("Missile Or Fire Settings")]
    public GameObject missilePrefab;
    public Transform firePoint;

    float vertical = 0;
    Vector3 droneVelocity;

    bool m_ShootedInLastFrame = false;

    private void Update()
    {
        DroneMovement();

        if(InputManager.Instance.shoot && !m_ShootedInLastFrame)
        {
            ShootMissile();
        }
        m_ShootedInLastFrame = InputManager.Instance.shoot;
    }

    void DroneMovement()
    {
        vertical = 0f;

        m_DroneMovement.x = InputManager.Instance.flightMovement.x;
        m_DroneMovement.z = InputManager.Instance.flightMovement.y;

        if (InputManager.Instance.ascend) vertical += 1;

        if (InputManager.Instance.descend) vertical -= 1;

        m_DroneMovement.Set(m_DroneMovement.x, vertical, m_DroneMovement.z);

        m_DroneMovement = Vector3.ClampMagnitude(m_DroneMovement, 1f);

        droneVelocity.Set(m_DroneMovement.x * flightSpeed,
            m_DroneMovement.y * verticleSpeed, 
            m_DroneMovement.z * flightSpeed);

        transform.Translate(droneVelocity * Time.deltaTime);
    }

    void ShootMissile()
    {
        Instantiate(missilePrefab, firePoint.position, missilePrefab.transform.rotation);
    }
}