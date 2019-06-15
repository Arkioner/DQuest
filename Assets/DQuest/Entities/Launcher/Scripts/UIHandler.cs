using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.UI;

namespace DQuest.Entities.Launcher.Scripts
{
    public class UIHandler : MonoBehaviourPunCallbacks
    {
        public InputField createRoomTf;
        public InputField joinRoomTf;

        public void OnClick_JoinRoom()
        {
            PhotonNetwork.JoinRoom(joinRoomTf.text, null);
        }

        public void OnClick_CreateRoom()
        {
            PhotonNetwork.CreateRoom(createRoomTf.text, new RoomOptions {MaxPlayers = 4}, null);
        }

        public override void OnJoinedRoom()
        {
            Debug.Log("Room Joined Successfully");
            PhotonNetwork.LoadLevel("SampleScene");
        }

        public override void OnJoinRoomFailed(short returnCode, string message)
        {
            Debug.LogError($"Join Room Failed: {returnCode}, message: {message}");
        }
    }
}
