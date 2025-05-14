using System;
using UnityEngine;

public enum eMoveAreaPos
{
    None,
    Left,
    Right
}

public class AirHockeyAreaManager : MonoBehaviour
{
    [SerializeField] private GameObject moveAreaLeft;
    [SerializeField] private GameObject moveAreaRight;

    [SerializeField] private GoalArea goalAreaLeft;
    [SerializeField] private GoalArea goalAreaRight;


    
    // Actionにステートを変更する処理を持ってくる
    public void Initialize(eMoveAreaPos moveArea, Action<ePointType, int> goalAction, Action<eGameStatus> sequenceChange)
    {
        switch (moveArea)
        {
            case eMoveAreaPos.Left:
                moveAreaLeft.SetActive(true);
                moveAreaRight.SetActive(false);
                goalAreaLeft.Initialize(ePointType.Player, goalAction, sequenceChange);
                goalAreaRight.Initialize(ePointType.Enemy, goalAction, sequenceChange);
                break;
            case eMoveAreaPos.Right:
                moveAreaLeft.SetActive(false);
                moveAreaRight.SetActive(true);
                goalAreaLeft.Initialize(ePointType.Enemy, goalAction, sequenceChange);
                goalAreaRight.Initialize(ePointType.Player, goalAction, sequenceChange);
                break;
        }
    }
}
