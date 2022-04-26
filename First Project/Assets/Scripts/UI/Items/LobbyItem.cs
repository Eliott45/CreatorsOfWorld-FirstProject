using Photon.Pun;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Items
{
    /// <summary>
    /// Lobby item in lobby list.
    /// </summary>
    public class LobbyItem : MonoBehaviourPunCallbacks
    {
        [SerializeField] private TextMeshProUGUI _nameTxt, _countsTxt;
        [SerializeField] private Button _joinButton;

        private string _lobbyName;

        public void Init(string lobbyName, int currentPlayers, int maxPlayers)
        {
            _lobbyName = lobbyName;
            _nameTxt.SetText(_lobbyName);
            _countsTxt.SetText($"{currentPlayers}/{maxPlayers}");
            _joinButton.interactable = currentPlayers < maxPlayers;
            Subscribes();
        }
        
        public override void OnDisable()
        {
            base.OnDisable();
            Unsubscribes();
        }

        private void Subscribes()
        {
            _joinButton.onClick.AddListener(() => JoinLobby(_lobbyName));
        }
        
        private void Unsubscribes()
        {
            _joinButton.onClick.RemoveAllListeners();
        }
        
        /// <summary>
        /// Join lobby.
        /// </summary>
        /// <param name="lobbyName">Name of lobby.</param>
        private static void JoinLobby(string lobbyName) => PhotonNetwork.JoinRoom(lobbyName);
    }
}
