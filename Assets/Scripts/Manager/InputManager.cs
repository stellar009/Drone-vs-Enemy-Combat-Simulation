using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    public static InputManager Instance;

    private PlayerInputActions m_PlayerInputActions;
    private GameManager m_GM;

    public Vector2 flightMovement {  get; private set; }
    public bool ascend { get; private set; }
    public bool descend { get; private set; }
    public bool shoot {  get; private set; }

    private void Awake()
    {
        if(!Instance)
        {
            Instance = this;
            DontDestroyOnLoad(Instance);
        }  

        EnableCursor(false);

        m_PlayerInputActions = new PlayerInputActions();
        m_GM = FindObjectOfType<GameManager>();
    }

    private void OnEnable()
    {
        m_PlayerInputActions.Enable();

        m_PlayerInputActions.Drone.FlightMovement.performed += OnMovementPerformed;
        m_PlayerInputActions.Drone.FlightMovement.canceled += OnMovementCancelled;

        m_PlayerInputActions.Drone.Ascend.performed += AscendDrone;
        m_PlayerInputActions.Drone.Descend.performed += DescendDrone;
        m_PlayerInputActions.Drone.Fire.performed += ShootMissile;
        m_PlayerInputActions.Drone.Restart.performed += RestartSimulation;
    }

    private void OnDisable()
    {
        m_PlayerInputActions.Drone.Restart.performed -= RestartSimulation;
        m_PlayerInputActions.Drone.Fire.performed -= ShootMissile;
        m_PlayerInputActions.Drone.Descend.performed -= DescendDrone;
        m_PlayerInputActions.Drone.Ascend.performed -= AscendDrone;

        m_PlayerInputActions.Drone.FlightMovement.canceled -= OnMovementCancelled;
        m_PlayerInputActions.Drone.FlightMovement.performed -= OnMovementPerformed;

        m_PlayerInputActions.Disable();
    }

    void OnMovementPerformed(InputAction.CallbackContext context)
    {
        flightMovement = context.ReadValue<Vector2>();
    }

    void OnMovementCancelled(InputAction.CallbackContext context)
    {
        flightMovement = Vector2.zero;
    }

    void AscendDrone(InputAction.CallbackContext context)
    {
        ascend = !ascend;
    }

    void DescendDrone(InputAction.CallbackContext context)
    {
        descend = !descend;
    }

    void ShootMissile(InputAction.CallbackContext context)
    {
        shoot = !shoot;
    }

    void RestartSimulation(InputAction.CallbackContext context)
    {
        m_GM.RestartSim();
    }

    public void EnableCursor(bool enable)
    {
        Cursor.lockState = enable ? CursorLockMode.None : CursorLockMode.Locked;
        Cursor.visible = enable;
    }
}
