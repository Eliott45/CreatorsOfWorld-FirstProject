using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class LobbyView : MonoBehaviourPunCallbacks
    {
        [SerializeField] private Button _createButton, _joinButton;
        [SerializeField] private InputField _createInput, _joinInput;
        
        private readonly RoomOptions _roomOptions = new () { MaxPlayers = 4 }; // TODO Refactor this

        public override void OnJoinedRoom() => PhotonNetwork.LoadLevel(2);
        
        public override void OnDisable()
        {
            base.OnDisable();
            Unsubscribe();
        }

        private void Awake() => Subscribes();
        
        private void CreateRoom(string roomName) => PhotonNetwork.CreateRoom(roomName, _roomOptions);
        
        private static void JoinRoom(string roomName) => PhotonNetwork.JoinRoom(roomName);
        
        private void Subscribes()
        {
            _createButton.onClick.AddListener(() => CreateRoom(_createInput.text));
            _joinButton.onClick.AddListener(() => JoinRoom(_joinInput.text));
        }

        /// <summary>
        /// Remove all listeners. 
        /// </summary>
        private void Unsubscribe()
        {
            _createButton.onClick.RemoveAllListeners();
            _joinButton.onClick.RemoveAllListeners();
        }
    }
}
