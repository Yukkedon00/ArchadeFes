using Fusion;
using Fusion.Sockets;
using System;
using System.Collections.Generic;
using UnityEngine;


public enum GameState
{
    None,
    MainMenu,
    InGame,
    GameOver
}

// NOTE:Fusion2対応を先にやるためゲームができるようになったタイミングでステートでの処理を実装する
// 
public class BaseGameState : NetworkBehaviour
{
    
    
    public override void Spawned()
    {
        
    }
}
