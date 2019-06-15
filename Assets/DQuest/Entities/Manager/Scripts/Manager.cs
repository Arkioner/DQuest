using Photon.Pun;
using UnityEngine;

namespace DQuest.Entities.Manager.Scripts
{
    public class Manager : MonoBehaviour
    {
        public GameObject playerPrefab;

        // Start is called before the first frame update
        void Start()
        {
            SpawnPlayer();
        }

        void SpawnPlayer()
        {
            PhotonNetwork.Instantiate(
                playerPrefab.name,
                playerPrefab.transform.position,
                playerPrefab.transform.rotation
            );
        }
    }
}