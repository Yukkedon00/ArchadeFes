using System;
using UnityEngine;

public class Wall : MonoBehaviour
{
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("ball"))
        {
            var ball = other.rigidbody;

            var contactPoint = other.GetContact(0);
            ball.linearVelocity = Vector3.Reflect(other.relativeVelocity, contactPoint.normal);
        }
    }
}
