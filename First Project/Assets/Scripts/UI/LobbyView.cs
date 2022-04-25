using Photon.Pun;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class LobbyView : MonoBehaviourPunCallbacks
    {
        [SerializeField] private TextMeshProUGUI _nameRoomTxt;
        [SerializeField] private Button _leaveButton;

        public override void OnEnable()
        {
            base.OnEnable();
            Subscribes();
        }
        
        public override void OnDisable()
        {
            base.OnDisable();
            Unsubscribe();
        }

        public void Show()
        {
            gameObject.SetActive(true);
            _nameRoomTxt.SetText($"Room: {PhotonNetwork.CurrentRoom.Name}");
        }

        private void LeaveLobby()
        {
            
        }

        private void Subscribes()
        {
            _leaveButton.onClick.AddListener(LeaveLobby);
        }

        private void Unsubscribe()
        {
            _leaveButton.onClick.RemoveAllListeners();
        }
    }
}
