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

// NOTE:Fusion2�Ή����ɂ�邽�߃Q�[�����ł���悤�ɂȂ����^�C�~���O�ŃX�e�[�g�ł̏�������������
// 
public class BaseGameState : NetworkBehaviour
{
    
    
    public override void Spawned()
    {
        
    }
}
