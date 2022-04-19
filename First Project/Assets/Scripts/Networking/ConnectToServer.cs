using Photon.Pun;
using UnityEngine.SceneManagement;

namespace Networking
{
    /// <summary>
    /// The class responsible for connecting the user to the server, and then going to the menu.
    /// </summary>
    public class ConnectToServer : MonoBehaviourPunCallbacks
    {
        private void Start() => PhotonNetwork.ConnectUsingSettings(); 

        public override void OnConnectedToMaster()
        {
            base.OnConnectedToMaster(); 
            SceneManager.LoadScene(1); 
        }
    }
}
