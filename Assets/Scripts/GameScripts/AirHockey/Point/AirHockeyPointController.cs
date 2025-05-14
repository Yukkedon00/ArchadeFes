using UnityEngine;

public class AirHockeyPointController : MonoBehaviour
{
    [SerializeField] private AirHockeyPointView playerView;
    [SerializeField] private AirHockeyPointView enemyView;

    public void Initialize(AirHockeyModel playerModel, AirHockeyModel enemyModel)
    {
        playerView.Initialize(playerModel);
        enemyView.Initialize(enemyModel);
    }

    public void UpdateEnemyPoint(int point)
    {
        enemyView.UpdatePoint(point);
    }

    public void UpdatePlayerPoint(int point)
    {
        playerView.UpdatePoint(point);
    }
}
