using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.UI;

namespace DQuest.Entities.Launcher.Scripts
{
    public class Launcher : MonoBehaviourPunCallbacks
    {
        public GameObject connectedScreen;
        public GameObject disconnectedScreen;

        public void OnClick_ConnectBtn()
        {
            PhotonNetwork.ConnectUsingSettings();
        }
        public void OnClick_ExitBtn()
        {
            PhotonNetwork.Disconnect();
        }

        public override void OnConnectedToMaster()
        {
            base.OnConnectedToMaster();
            connectedScreen.SetActive(true);
            PhotonNetwork.JoinLobby(TypedLobby.Default);
        }

        public override void OnDisconnected(DisconnectCause cause)
        {
            base.OnDisconnected(cause);
            disconnectedScreen.SetActive(true);
            Text disconnectionText = disconnectedScreen.GetComponentInChildren<Text>();
            disconnectionText.text = disconnectionText.text + "\n" + cause;
        }

        public override void OnJoinedLobby()
        {
            base.OnJoinedLobby();
            if (disconnectedScreen.activeSelf)
            {
                disconnectedScreen.SetActive(false);
            }
            connectedScreen.SetActive(true);
        }
    }
}
