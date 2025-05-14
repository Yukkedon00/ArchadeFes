using System;
using UnityEngine;
using Fusion;

public class SmasherManager : NetworkBehaviour
{
    [SerializeField] private Rigidbody myRb;

    private float defaultPosY = 0f;
    private float cameraDistance = 0f;

    private NetworkObject networkObject;
    private Vector2 mousePos;

    [Networked]
    public PlayerInput CurrentInput { get; set; }

    public override void Spawned()
    {
        var component = GetComponent<NetworkObject>();

        if (component.HasInputAuthority)
        {

        }
        networkObject = component;
        cameraDistance = Camera.main.transform.position.z - this.transform.position.z;
        defaultPosY = this.transform.position.y;
    }

    //private PlayerInput playerInput;
    /*    public void Initialise(InputManager inputManger)
        {
            var component = GetComponent<NetworkObject>();

            if (component.HasInputAuthority)
            {

            }
            networkObject = component;
            cameraDistance = Camera.main.transform.position.z - this.transform.position.z;
            defaultPosY = this.transform.position.y;
            //SetInput(inputManger.GetPlayerInput);
        }*/

    /*    private void SetInput(PlayerInput input)
        {
            if (networkObject.HasInputAuthority)
            {
                //var inputGame = input.actions.FindActionMap("AirHockey");
                input.SwitchCurrentActionMap("AirHockey");
                input.actions["Move"].performed += OnMove;
                input.actions["SmasherDown"].started += OnSmasherDown;
            }
        }

        private void OnMove(InputAction.CallbackContext context)
        {
            mousePos = context.ReadValue<Vector2>();
        }

        private void OnSmasherDown(InputAction.CallbackContext context)
        {
            Debug.Log("Down");
        }*/

    public override void FixedUpdateNetwork()
    {
        if (GetInput(out PlayerInput input))
        {
            CurrentInput = input;
        }

        var ray = Camera.main.ScreenPointToRay(CurrentInput.MousePos);

        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            // NOTE:当たり判定ではない部分で消してしまったら
            //      処理しない部分でつっかえが起こってしまう為要修正
            if (hit.collider.tag != "moveSeigen")
            {
                return;
            }
        }

        var t = (defaultPosY - ray.origin.y) / ray.direction.y;
        var hitPoint = ray.origin + ray.direction * t;

        // myRb.MovePosition(hitPoint);
        //myRb.linearVelocity = hitPoint;
        // 速度ベクトルが移動していたらその方向に向かっていくのは当り前じゃanaika
        myRb.MovePosition(hitPoint);
    }
}
