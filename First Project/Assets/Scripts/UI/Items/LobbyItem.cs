using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Items
{
    public class LobbyItem : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _nameTxt, _countsTxt;
        [SerializeField] private Button _joinButton;

        public void Init(string lobbyName, int currentPlayers, int maxPlayers)
        {
            _nameTxt.SetText(lobbyName);
            _countsTxt.SetText($"{currentPlayers}/{maxPlayers}"); 
        }
    }
}
