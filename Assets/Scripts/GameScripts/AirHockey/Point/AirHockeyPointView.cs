using TMPro;
using UnityEngine;

public class AirHockeyPointView : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI pointUi;
    [SerializeField] private TextMeshProUGUI nameUi;

    public void Initialize(AirHockeyModel model)
    {
        // Memo:
        //nameUi.text = model.PlayerName;
        pointUi.text = model.ScorePoint.CurrentValue.ToString();
    }

    public void UpdatePoint(int point)
    {
        pointUi.text = point.ToString();
    }
}
