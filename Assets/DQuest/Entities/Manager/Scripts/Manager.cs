using Photon.Pun;
using UnityEngine;

namespace DQuest.Entities.Manager.Scripts
{
    public class Manager : MonoBehaviour
    {
        public GameObject playerPrefab;
        public GameObject logPrefab;

        // Start is called before the first frame update
        void Start()
        {
            SpawnPlayer();
            if (PhotonNetwork.IsMasterClient)
            {
                SpawnMinion();
            }
        }

        void SpawnPlayer()
        {
            PhotonNetwork.Instantiate(
                playerPrefab.name,
                playerPrefab.transform.position,
                playerPrefab.transform.rotation
            );
        }

        void SpawnMinion()
        {
            PhotonNetwork.Instantiate(
                logPrefab.name,
                logPrefab.transform.position,
                logPrefab.transform.rotation
            );
        }
    }
}