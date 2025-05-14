using UnityEngine;

// Memo:敵か味方かどちらかを現したいけど
//      Typeって適切？
public enum ePointType
{
    None,
    Player,
    Enemy,
}

// モデルおおもとのクラスとしてやっていこう
public class AirHockeyModel : PointModel
{
    public ePointType Type { get; private set; }
    public string PlayerName{ get; private set; }

    public AirHockeyModel(int scorePoint, int setPoint, ePointType type, string playerName) : base(scorePoint, setPoint)
    {
        Type = type;
        PlayerName = playerName;
    }
}
