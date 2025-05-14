using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Cysharp.Threading.Tasks;
using Fusion;
using Fusion.Addons.Physics;
using Fusion.Sockets;
using UnityEngine;
using UnityEngine.AddressableAssets;

public class GameLauncher : MonoBehaviour, INetworkRunnerCallbacks
{
    [SerializeField] private NetworkRunner networkRunnerPrefab;
    [SerializeField] private InputManager inputManager;
    // NOTE:スポーン関係はまとめて別にしたほうが良いね
    // プロトタイプができたらリファクタリングする
    
    [SerializeField] private Transform spawnPackPos;
    [SerializeField] private Transform spawnSmasherLeft;
    [SerializeField] private Transform spawnSmasherRight;
    
    public bool IsHost => networkRunner.IsServer;

    public eMoveAreaPos MoveAreaPos { get; private set; }

    public Pack PackObject { get; private set; }
    private string packPath = "AirHockey_Pack";
    private string smasherPath = "AirHockey_Smasher";
    
    private const float defaultPosY = 2.67f;

    private NetworkRunner networkRunner;

    private bool isFirstSpawn = true;

    private Dictionary<PlayerRef, NetworkObject> playerSmashers = new Dictionary<PlayerRef, NetworkObject>();

    public async UniTask InitializeAsync()
    {
        var runnerSimulatePhysics3D = gameObject.AddComponent<RunnerSimulatePhysics3D>();
        runnerSimulatePhysics3D.ClientPhysicsSimulation = ClientPhysicsSimulation.SimulateAlways;

        // NetworkRunnerを生成する
        networkRunner = Instantiate(networkRunnerPrefab);
        // GameLauncherを、NetworkRunnerのコールバック対象に追加する
        networkRunner.AddCallbacks(this);
        // 共有モードのセッションに参加する
        var result = await networkRunner.StartGame(new StartGameArgs {
#if DEBUG
            //GameMode = GameMode.Host,
            GameMode = GameMode.AutoHostOrClient,
#else
            GameMode = GameMode.Host,
#endif 
            SessionName = "AirHockey",
        });
        // 結果をコンソールに出力する
        Debug.Log(result);
    }

    public bool IsStartGame()
    {
        return networkRunner.ActivePlayers.Count() == 2;
    }

    public async UniTask ResetPackAsync(CancellationToken token)
    {
        if (PackObject != null)
        {
            PackObject.Despawned(networkRunner, true);
        }
        

        var packObj = Addressables.LoadAssetAsync<GameObject>(packPath);

        await packObj.Task;

        networkRunner.Spawn(packObj.Result, spawnPackPos.position);
        PackObject = packObj.Result.GetComponent<Pack>();
    }

    public void OnPlayerJoined(NetworkRunner runner, PlayerRef player) 
    {
        if (!runner.IsServer)
        {
            return;
        }

        LoadSamsher(networkRunner, player).Forget();
    }

    private async UniTask LoadSamsher(NetworkRunner runner, PlayerRef player = default)
    {
        var smasherObj = await Addressables.LoadAssetAsync<GameObject>(smasherPath).Task;
        
        var pos = runner.IsServer && player == runner.LocalPlayer
            ? eMoveAreaPos.Right
            : eMoveAreaPos.Left;

        var spawnPos = pos == eMoveAreaPos.Left 
            ? spawnSmasherLeft.position 
            : spawnSmasherRight.position;

        var nwObj = runner.Spawn(
            smasherObj,
            spawnPos,
            inputAuthority: player);

        playerSmashers.Add(player, nwObj);
    }

    /*    public void OnPlayerJoined(NetworkRunner runner, PlayerRef player)
        {
            if (!networkRunner.IsServer)
            {
                return;
            }

            if (networkRunner.ActivePlayers.Count() == 2)
            {
                var packObj = Addressables.LoadAssetAsync<GameObject>(packPath);

                //await packObj.Task;

                networkRunner.Spawn(packObj.Result, spawnPackPos.position);

                var packObj = Addressables.LoadAssetAsync<GameObject>(packPath);
                var smasherObj = Addressables.LoadAssetAsync<GameObject>(smasherPath);

                var tasks = new List<Task>();
                tasks.Add(packObj.Task);
                tasks.Add(smasherObj.Task);

                await Task.WhenAll(tasks);

                // TODO:パックの処理はホストに任せたいのでここじゃないところでやろうね
                var nwobj = networkRunner.Spawn(packObj.Result, spawnPackPos.position);

                networkRunner.Spawn(smasherObj.Result, spawnSmasherLeft.position, inputAuthority: networkRunner.LocalPlayer);

                PackObject = packObj.Result.GetComponent<Pack>();
                MoveAreaPos = eMoveAreaPos.Left;

            }
        }*/
    public void OnPlayerLeft(NetworkRunner runner, PlayerRef player) 
    {
        if (playerSmashers.TryGetValue(player, out NetworkObject smasher))
        {
            runner.Despawn(smasher);
            playerSmashers.Remove(player);
        }
    }

    public void OnObjectExitAOI(NetworkRunner runner, NetworkObject obj, PlayerRef player) { }
    public void OnObjectEnterAOI(NetworkRunner runner, NetworkObject obj, PlayerRef player) { }    
    public void OnShutdown(NetworkRunner runner, ShutdownReason shutdownReason) { }
    public void OnDisconnectedFromServer(NetworkRunner runner, NetDisconnectReason reason) { }
    public void OnConnectRequest(NetworkRunner runner, NetworkRunnerCallbackArgs.ConnectRequest request, byte[] token) { }
    public void OnConnectFailed(NetworkRunner runner, NetAddress remoteAddress, NetConnectFailedReason reason) { }
    public void OnUserSimulationMessage(NetworkRunner runner, SimulationMessagePtr message) { }
    public void OnReliableDataReceived(NetworkRunner runner, PlayerRef player, ReliableKey key, ArraySegment<byte> data) { }
    public void OnReliableDataProgress(NetworkRunner runner, PlayerRef player, ReliableKey key, float progress) { }
    public void OnInput(NetworkRunner runner, NetworkInput input) { }
    public void OnInputMissing(NetworkRunner runner, PlayerRef player, NetworkInput input) { }
    public void OnConnectedToServer(NetworkRunner runner) { }
    public void OnSessionListUpdated(NetworkRunner runner, List<SessionInfo> sessionList) { }
    public void OnCustomAuthenticationResponse(NetworkRunner runner, Dictionary<string, object> data) { }
    public void OnHostMigration(NetworkRunner runner, HostMigrationToken hostMigrationToken) { }
    public void OnSceneLoadDone(NetworkRunner runner) { }
    public void OnSceneLoadStart(NetworkRunner runner) { }
}
