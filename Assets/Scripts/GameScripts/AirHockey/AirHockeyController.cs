
using Cysharp.Threading.Tasks;
using R3;
using System;
using UnityEngine;

// NOTE:ホスト・クライアント別のController作ったほうが良いか
// あと、
public class AirHockeyController : MonoBehaviour
{
    [SerializeField] private AirHockeyPointController pointController;
    [SerializeField] private AirHockeyAreaManager areaManager;

    [SerializeField] private Transform resetPackPosition;
    
    private static int defaultSetPoint = 10;
    
    private AirHockeyModel playerPointModel;
    private AirHockeyModel enemyPointModel;

    private bool isEnd = false;


    // プレイヤーネームもここらへんでやる
    public void Initialize(eMoveAreaPos status, bool isHost, Action<eGameStatus> sequenceChange)
    {
        // NOTE:プレイヤー情報などは今後実装
        playerPointModel = new AirHockeyModel(
            0,
            0,
            ePointType.Player,
            "PlayerName");
        enemyPointModel = new AirHockeyModel(
            0,
            0,
            ePointType.Enemy,
            "EnemyName");
        
        pointController.Initialize(playerPointModel, enemyPointModel);
        
        // Actionを2つ以上の引数として渡す方法があるかもしれんので調べてこよう
        areaManager.Initialize(status, (ePointType, addscore) => AddScorePoint(ePointType, addscore), sequenceChange);
    }

    private void AddScorePoint(ePointType type, int addScore)
    {
        switch (type)
        {
            case ePointType.Enemy:
                enemyPointModel.AddScorePoint(addScore);
                pointController.UpdateEnemyPoint(enemyPointModel.ScorePoint.CurrentValue);
                break;
            case ePointType.Player:
                playerPointModel.AddScorePoint(addScore);
                pointController.UpdatePlayerPoint(playerPointModel.ScorePoint.CurrentValue);
                break;
        }
    }

}
