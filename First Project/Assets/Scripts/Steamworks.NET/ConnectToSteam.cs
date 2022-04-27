using UnityEngine;
using UnityEngine.SceneManagement;

namespace Steamworks.NET
{
    public class ConnectToSteam : MonoBehaviour
    {
        private void Start()
        {
            Debug.Log($"Steam connected: {SteamManager.Initialized}");
            if (SteamManager.Initialized)
            {
                SceneManager.LoadScene(1); 
            }
            else
            {
                Debug.LogError("Error connecting to steam servers");
            }
        }
    }
}
