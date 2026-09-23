using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public TextMeshProUGUI droneStatus;
    public GameObject restartButton;

    private GameManager gameManager;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if(gameManager.isDroneOnline)
        {
            restartButton.SetActive(false);
            droneStatus.color = Color.green;
            droneStatus.text = "Drone Status: ONLINE".ToString();
        }
        else
        {
            restartButton.SetActive(true);
            droneStatus.color = Color.red;
            droneStatus.text = "Drone Status: OFFLINE".ToString();
        }
    }
}
