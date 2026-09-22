using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private GameObject m_PlayerDrone;

    [HideInInspector] public bool isDroneOnline;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        m_PlayerDrone = FindObjectOfType<Drone>().gameObject;

        StartCoroutine(DroneStatusCheck());
    }

    IEnumerator DroneStatusCheck()
    {
        while (true)
        {
            if (m_PlayerDrone)
            {
                isDroneOnline = true;
            }
            else
            {
                InputManager.Instance.EnableCursor(true);
                isDroneOnline = false;
            }
            yield return new WaitForSeconds(0.1f);
        }
    }

    public void RestartSim()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
