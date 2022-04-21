using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class LobbyListView : MonoBehaviourPunCallbacks
    {
        [SerializeField] private LobbyView _lobbyView;
        [SerializeField] private Button _createButton, _joinButton;
        [SerializeField] private InputField _createInput, _joinInput;
        
        private readonly RoomOptions _roomOptions = new () { MaxPlayers = 4 }; // TODO Refactor this
        
        private void Awake()
        {
            Subscribes();
            PhotonNetwork.JoinLobby();
        }
        
        public override void OnJoinedRoom()
        {
            // PhotonNetwork.LoadLevel(2);
            _lobbyView.Show();
        }

        public override void OnDisable()
        {
            base.OnDisable();
            Unsubscribe();
        }
        
        private void Subscribes()
        {
            _createButton.onClick.AddListener(() => CreateRoom(_createInput.text));
            _joinButton.onClick.AddListener(() => JoinRoom(_joinInput.text));
            _createInput.onValueChanged.AddListener(roomName => CheckCorrectRoomName(roomName, _createButton));
            _joinInput.onValueChanged.AddListener(roomName => CheckCorrectRoomName(roomName, _joinButton));
        }

        /// <summary>
        /// Remove all listeners. 
        /// </summary>
        private void Unsubscribe()
        {
            _createButton.onClick.RemoveAllListeners();
            _joinButton.onClick.RemoveAllListeners();
            _createInput.onValueChanged.RemoveAllListeners();
            _joinInput.onValueChanged.RemoveAllListeners();
        }
        
        private void CreateRoom(string roomName)
        {
            PhotonNetwork.CreateRoom(roomName, _roomOptions);
        }

        /// <summary>
        /// Join room.
        /// </summary>
        /// <param name="roomName">Name of room.</param>
        private static void JoinRoom(string roomName) => PhotonNetwork.JoinRoom(roomName);
        
        private static void CheckCorrectRoomName(string roomName, Selectable button)
        {
            var roomNameWithoutSpaces = roomName.Replace(" ", "");
            button.interactable = roomNameWithoutSpaces.Length > 2;
        }
    }
}
