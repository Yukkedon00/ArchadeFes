using Cysharp.Threading.Tasks;
using Fusion;
using R3;
using UnityEngine;
using UnityEngine.InputSystem;

public class AirHockeyPhoton : FusionMonoBehaviour
{
    [SerializeField] private InputManager inputManager;
    
    // NOTE:もしPhotonクラスが膨大になった時はViweにわけようか
    [SerializeField] private GameObject waitPlayerText;

    [SerializeField] private Transform packMarker;
    [SerializeField] private Transform smasherMarkerLeft;
    [SerializeField] private Transform smasherMarkerRight;
    
    public Observable<bool> IsPreparationObservable => isPreparationSubject;
    private readonly Subject<bool> isPreparationSubject = new Subject<bool>();

    public Observable<AirHockeyPhotonStatus> PlayerStatusObservable => playerStatusSubject;
    private readonly Subject<AirHockeyPhotonStatus> playerStatusSubject = new Subject<AirHockeyPhotonStatus>();

    public Pack PackObject { get; private set; }
    
    private const float defaultPosY = 2.67f;

    
    private int connectPlayers = 0;
    
    private void Start() {
        // PhotonServerSettingsの設定内容を使ってマスターサーバーへ接続する
        //PhotonNetwork.ConnectUsingSettings();
        
    }

    // マスターサーバーへの接続が成功した時に呼ばれるコールバック
    /*public override void OnConnectedToMaster() {
        // "Room"という名前のルームに参加する（ルームが存在しなければ作成して参加する）
        //PhotonNetwork.JoinOrCreateRoom("Room", new RoomOptions(), TypedLobby.Default);
        
        Debug.Log("参加・クリエイト");
    }*/

    // ゲームサーバーへの接続が成功した時に呼ばれるコールバック
    /*public override void OnJoinedRoom()
    {
        // ランダムな座標に自身のアバター（ネットワークオブジェクト）を生成する
        var smasher = PhotonNetwork.Instantiate("EnemySmasher", PhotonNetwork.IsMasterClient ? smasherMarkerLeft.position : smasherMarkerRight.position, Quaternion.identity);
        var controller = smasher.GetComponent<SmasherManager>();
        controller.Initialise(inputManager);
        
        if (PhotonNetwork.IsMasterClient)
        {
            var status = new AirHockeyPhotonStatus(eMoveAreaPos.Left);
            playerStatusSubject.OnNext(status);
        }
        else
        {
            var status = new AirHockeyPhotonStatus(eMoveAreaPos.Right);
            playerStatusSubject.OnNext(status);
        }
        
        if (PhotonNetwork.CurrentRoom.PlayerCount == 2)
        {
            //var packObj = PhotonNetwork.Instantiate("Pack", packMarker.position, Quaternion.identity);
            waitPlayerText.SetActive(false);
            isPreparationSubject.OnNext(true);
        }
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    { 
        Debug.Log("プレイヤー参加");
        //CheckPlayer();
        
        Debug.Log("現在の人数: " + PhotonNetwork.CurrentRoom.PlayerCount);

        if (PhotonNetwork.CurrentRoom.PlayerCount == 2)
        {
            if (PhotonNetwork.IsMasterClient)
            {
                PackObject = PhotonNetwork.Instantiate("Pack", packMarker.position, Quaternion.identity).GetComponent<Pack>();
            }
            waitPlayerText.SetActive(false);
            isPreparationSubject.OnNext(true);
        }
    }*/
}
