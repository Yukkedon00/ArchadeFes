using UnityEngine;

public abstract class BaseCollider : MonoBehaviour
{
    public bool CollisionEnter()
    {
        return false;
    }

    public bool CollisionExit()
    {
        return false;
    }
}
