using R3;

// Note:今後セット数等出てくると思うのでここに追加していく
public abstract class PointModel
{
    // 外部公開用
    public ReadOnlyReactiveProperty<int> ScorePoint;
    public ReadOnlyReactiveProperty<int> SetPoint;
    
    // 実態
    private ReactiveProperty<int> pScorePoint = new ReactiveProperty<int>();
    private ReactiveProperty<int> pSetPoint = new ReactiveProperty<int>();
    public PointModel(int scorePoint, int setPoint)
    {
        pScorePoint.Value = scorePoint;
        pSetPoint.Value = setPoint;

        ScorePoint = pScorePoint.ToReadOnlyReactiveProperty();
        SetPoint = pSetPoint.ToReadOnlyReactiveProperty();
    }

    public void AddScorePoint(int score)
    {
        pScorePoint.Value += score;
    }

    public void AddSetPoint()
    {
        pSetPoint.Value += 1;
        pScorePoint.Value = 0;
    }

    public void ResetPoint()
    {
        pSetPoint.Value = 0;
        pScorePoint.Value = 0;
    }
}
