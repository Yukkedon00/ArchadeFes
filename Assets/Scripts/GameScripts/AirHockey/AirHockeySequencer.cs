using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.Playables;

public enum eGameStatus
{
    None,
    Waiting,
    Opening,
    SpawnPack,
    Playing,
    Goal,
    //AddPoint, 演出関連で必要になったら解放？でもGoalだけでよくねということでコメントアウト
    Reset,
    GameOver,
}

public class AirHockeySequencer : MonoBehaviour
{
    [SerializeField] private AirHockeyController controller;
    [SerializeField] private GameLauncher launcher;
    [SerializeField] private PlayableDirector playableDirector;
    [SerializeField] private GameObject WaitingPlayerText;

    private eGameStatus currentStatus = eGameStatus.None;

    private void Awake()
    {
        currentStatus = eGameStatus.Waiting;
    }

    private void Start()
    {
        // ゲームの流れを開始
        GameSequenserAsync(this.destroyCancellationToken).Forget();
    }

    private async UniTask GameSequenserAsync(CancellationToken token)
    {
        // ゲームの流れ
        while (true)
        {
            if (token.IsCancellationRequested) break;

            switch (currentStatus)
            {
                case eGameStatus.Waiting:
                    // プレイヤーが2人揃うまで待機
                    await InitializeAsync(token);
                    currentStatus = eGameStatus.Opening;
                    break;
                case eGameStatus.Opening:
                    Debug.Log("Opening");
                    WaitingPlayerText.SetActive(false);
                    if (playableDirector.state == PlayState.Paused)
                    {
                        Debug.Log($"playableDirector:PlayNow");
                        playableDirector.Play();
                    }

                    // 総再生時間 > 再生してからの時間
                    await UniTask.WaitUntil(() => playableDirector.duration > playableDirector.time, cancellationToken: token);
                    currentStatus = eGameStatus.SpawnPack;

                    break;
                case eGameStatus.SpawnPack:
                    // パックをスポーンする
                    if (launcher.IsHost)
                    {
                        await launcher.ResetPackAsync(token);
                        currentStatus = eGameStatus.Playing;
                    }
                    
                    break;
                case eGameStatus.Playing:
                    Debug.Log("Playing");
                    await UniTask.WaitUntil(() => currentStatus == eGameStatus.Playing, cancellationToken: token);
                    break;
                case eGameStatus.Goal:
                    Debug.Log("Goal");
                    currentStatus = eGameStatus.SpawnPack;
                    break;
                case eGameStatus.Reset:
                    break;
                default:                    
                    return;
            }   
        }
    }

    private async UniTask InitializeAsync(CancellationToken token)
    {
        // 初期化軍
        await launcher.InitializeAsync();

        await UniTask.WaitUntil(() => launcher.IsStartGame(), cancellationToken: token);
        controller.Initialize(launcher.MoveAreaPos, launcher.IsHost, (status) => { currentStatus = status; });
    }
}
