using Photon.Pun;
using UnityEngine;

namespace DQuest.Entities.Player.Scripts
{
    [RequireComponent(typeof(PhotonView))]
    public class PhotonPlayerMovingController : MonoBehaviourPun, IPunObservable
    {
        [SerializeField] private float speed = 10f;

        private Rigidbody2D _rb2D;
        private SpriteAnimatorController _spriteAnimatorController;
        private PhotonView _photonView;
        public Camera playerCamera;

        private Vector3 _smoothMove;

        private void Start()
        {
            _rb2D = GetComponent<Rigidbody2D>();
            _spriteAnimatorController = GetComponent<SpriteAnimatorController>();
            _photonView = GetComponent<PhotonView>();
            if (_photonView.IsMine)
            {
                SwitchCameras();
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
            if (_photonView.IsMine)
            {
                ProcessInputs();
            }
            else
            {
                SmoothMovement();
            }
        }

        private void ProcessInputs()
        {
            float deltaX = Input.GetAxis("Horizontal");
            float deltaY = Input.GetAxis("Vertical");

            Vector2 direction = new Vector2(deltaX, deltaY);

            _rb2D.velocity = direction * speed;
            _spriteAnimatorController.UpdateAnimator(deltaX, deltaY, speed);
        }

        private void SmoothMovement()
        {
            transform.position = Vector3.Lerp(transform.position, _smoothMove, Time.deltaTime * 10);
        }

        public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
        {
            if (stream.IsWriting)
            {
                stream.SendNext(transform.position);
            }
            else if (stream.IsReading)
            {
                _smoothMove = (Vector3) stream.ReceiveNext();
            }
        }
    }
}