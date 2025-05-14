using Cysharp.Threading.Tasks;
using R3;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputAirHockey : IInputContext
{
    public void BindInput(InputActionAsset inputAction)
    {
        inputAction.FindActionMap("AirHockey");
    }
}
