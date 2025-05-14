using System;
using R3;
using UnityEngine;

public class GoalArea : MonoBehaviour
{
    private ePointType goalType;

    private Action<ePointType, int> goalAction;
    private Action<eGameStatus> sequenceChange;

    public void Initialize(ePointType type, Action<ePointType, int> goal, Action<eGameStatus> sequence)
    {
        goalType = type;
        goalAction = goal;
        sequenceChange = sequence;
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log($"haitta!!!!!!!!!!!!{other.name}");
        if (other.gameObject.tag == "ball")
        {
            Debug.Log($"haitta!!!!!!!!!!!!");
            goalAction?.Invoke(goalType, 1);
            sequenceChange?.Invoke(eGameStatus.Goal);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log($"OnCollisionEnter: {collision.gameObject.name}");
    }
}
