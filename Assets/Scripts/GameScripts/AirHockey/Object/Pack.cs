using System;
using Fusion;
using R3;
using UnityEngine;

public enum eHitType
{
    None,
    Wall,
    Smasher,
    Goal,
}

public class Pack : NetworkBehaviour
{
    [SerializeField] private Rigidbody myRb;
    
    public void ResetPosition(Vector3 position)
    {
        this.transform.position = position;
        myRb.linearVelocity = Vector3.zero;
        //photonView.RPC("ActivePack", RpcTarget.All, position);
    }
    
    private void OnCollisionEnter(Collision other)
    {
        Debug.Log("haitta!");
        if (other.gameObject.CompareTag("Untagged"))
        {
            Debug.Log("haitta!");
            return;
        }
        
        if (other.gameObject.CompareTag("wall"))
        {
            Debug.Log("haitta!");
            SeManager.Instance.PlayPositionSe(this.transform.position, 0);
        }
        else if (other.gameObject.CompareTag("smasher"))
        {
            Debug.Log("Smasher");
            var puckNWObj = other.gameObject.GetComponent<NetworkObject>();

            puckNWObj.RequestStateAuthority();
            SeManager.Instance.PlayPositionSe(this.transform.position, 1);
            return;
        }
        Debug.Log("Nuketa!");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("goal"))
        {
            SeManager.Instance.PlayPositionSe(this.transform.position, 2);
        }

        if (!HasStateAuthority)
        {

            return;
        }
    }

    //[PunRPC]
    private void ActivePack(Vector3 position)
    {
        gameObject.transform.position = position;
        myRb.linearVelocity = Vector3.zero;
        this.gameObject.SetActive(true);
    }

    [Rpc(RpcSources.InputAuthority, RpcTargets.All)]
    private void RPC_Goal()
    {
        
    }
}
