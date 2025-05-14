using System;
using UnityEngine;
using UnityEngine.InputSystem;

public enum eMinigameType
{
    None,
    Lobby,
    AirHockey,
}


public class InputManager : MonoBehaviour
{
    [SerializeField] private PlayerInput playerInput;
    private IInputContext context;
        
    private void Awake()
    {
        //DisableCurrentAction();
        //context.BindInput(inputAction);
        
    }
    public void SetActionMap(eMinigameType type)
    {
        switch (type)
        {
            case eMinigameType.Lobby:
                context = new InputLobby();
                break;
            case eMinigameType.AirHockey:
                context = new InputAirHockey();
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(type), type, null);
        }
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    }
    // Update is called once per frame
    void Update()
    {
    }

}
