using Photon.Pun;
using UnityEngine;

namespace DQuest.Entities.Player.Scripts
{
    [RequireComponent(typeof(PhotonView))]
    public class PhotonPlayerMovingController : MonoBehaviourPun, IPunObservable
    {
        [SerializeField] private float speed = 10f;
        [SerializeField] private Rigidbody2D rb2D;
        [SerializeField] private SpriteAnimatorController spriteAnimatorController;
        [SerializeField] private Camera playerCamera;
        private Vector3 _nextPosition;

        private void Start()
        {
            if (photonView.IsMine)
            {
                PhotonNetwork.SendRate = 20;
                PhotonNetwork.SerializationRate = 15;
                SwitchCameras();
            }
            else
            {
                rb2D.isKinematic = true;
                _nextPosition = transform.position;
            }
        }

        private void SwitchCameras()
        {
            GameObject.FindWithTag("MainCamera").SetActive(false);
            playerCamera.gameObject.SetActive(true);
        }

        // Update is called once per frame
        void FixedUpdate()
        {
            if (photonView.IsMine)
            {
                ProcessInputs();
            }
        }

        private void Update()
        {
            if (!photonView.IsMine)
            {
                transform.position = _nextPosition;
            }
        }

        private void ProcessInputs()
        {
            float deltaX = Input.GetAxis("Horizontal");
            float deltaY = Input.GetAxis("Vertical");

            Vector2 direction = new Vector2(deltaX, deltaY);

            rb2D.velocity = direction * speed;
            spriteAnimatorController.UpdateAnimator(deltaX, deltaY, speed);
            photonView.RPC("UpdateAnimator", RpcTarget.Others, deltaX, deltaY, speed);
        }

        [PunRPC]
        private void UpdateAnimator(float deltaX, float deltaY, float speed)
        {
            spriteAnimatorController.UpdateAnimator(deltaX, deltaY, speed);
        }

        public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
        {
            if (stream.IsWriting)
            {
                stream.SendNext(transform.position);
            }
            else if (stream.IsReading)
            {
                _nextPosition = (Vector3) stream.ReceiveNext();
            }
        }
    }
}