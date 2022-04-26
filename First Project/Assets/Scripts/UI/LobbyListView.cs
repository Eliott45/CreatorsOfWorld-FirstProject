using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using UI.Items;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class LobbyListView : MonoBehaviourPunCallbacks
    {
        [SerializeField] private LobbyItem _lobbyItemPrefab;
        [SerializeField] private LobbyView _lobbyView;
        [SerializeField] private Button _createButton, _joinButton;
        [SerializeField] private InputField _createInput, _joinInput;
        [SerializeField] private Transform _scrollContent;

        private readonly RoomOptions _roomOptions = new () { MaxPlayers = 4 }; // TODO Refactor this
        private List<LobbyItem> _lobbyList = new List<LobbyItem>();
        
        private void Awake()
        {
            Subscribes();
            PhotonNetwork.JoinLobby();
        }
        
        public override void OnJoinedRoom()
        {
            _lobbyView.Show();
        }

        public override void OnRoomListUpdate(List<RoomInfo> roomList)
        {
            base.OnRoomListUpdate(roomList);
            UpdateLobbyList(roomList);
        }

        public override void OnConnectedToMaster()
        {
            base.OnConnectedToMaster();
            PhotonNetwork.JoinLobby();
        }

        public override void OnDisable()
        {
            base.OnDisable();
            Unsubscribe();
        }
        
        private void Subscribes()
        {
            _createButton.onClick.AddListener(() => CreateLobby(_createInput.text));
            _joinButton.onClick.AddListener(() => JoinLobby(_joinInput.text));
            _createInput.onValueChanged.AddListener(roomName => CheckCorrectLobbyName(roomName, _createButton));
            _joinInput.onValueChanged.AddListener(roomName => CheckCorrectLobbyName(roomName, _joinButton));
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
        
        private void CreateLobby(string lobbyName)
        {
            PhotonNetwork.CreateRoom(lobbyName, _roomOptions);
        }

        /// <summary>
        /// Join lobby.
        /// </summary>
        /// <param name="lobbyName">Name of lobby.</param>
        private static void JoinLobby(string lobbyName) => PhotonNetwork.JoinRoom(lobbyName);
        
        /// <summary>
        /// Checking if the lobby name is correct.
        /// </summary>
        /// <param name="lobbyName">Name of lobby.</param>
        /// <param name="button">Button to unlock/lock.</param>
        private static void CheckCorrectLobbyName(string lobbyName, Selectable button)
        {
            var roomNameWithoutSpaces = lobbyName.Replace(" ", "");
            button.interactable = roomNameWithoutSpaces.Length > 2;
        }

        private void UpdateLobbyList(List<RoomInfo> rooms)
        {
            foreach (var lobby in _lobbyList)
            {
                Destroy(lobby.gameObject); //TODO Make a pool
            }
            _lobbyList.Clear();

            foreach (var room in rooms)
            {
                var newLobbyItem = Instantiate(_lobbyItemPrefab, _scrollContent);
                newLobbyItem.Init(room.Name, room.PlayerCount, room.MaxPlayers);
                _lobbyList.Add(newLobbyItem);
            }
        }
    }
}
