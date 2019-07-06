using Photon.Pun;
using UnityEngine;
using Random = UnityEngine.Random;

namespace DQuest.Entities.Log.Scripts
{
    public class PhotonLogMovementController : MonoBehaviourPun, IPunObservable
    {
        [SerializeField] private float speed = 6f;
        [SerializeField] private float secondsBetweenErraticMovementUpdates = 2;

        [SerializeField] private bool _awake;
        private GameObject _target;
        private float _lastErraticMovementUpdate;

        private float _deltaX;
        private float _deltaY;

        [SerializeField] private Rigidbody2D rb2D;
        [SerializeField] private LogSpriteAnimatorController spriteAnimatorController;

        private Vector3 _nextPosition;

        // Start is called before the first frame update
        private void Start()
        {
            PhotonNetwork.SendRate = 20;
            PhotonNetwork.SerializationRate = 15;
            _lastErraticMovementUpdate = Time.fixedTime;
            _nextPosition = transform.position;
        }

        public void Update()
        {
            if (!PhotonNetwork.IsMasterClient)
            {
                transform.position = _nextPosition;
            }
        }

        private void FixedUpdate()
        {
            if (!PhotonNetwork.IsMasterClient) return;
            if (_awake)
            {
                if (_target is null &&
                    Time.fixedTime - _lastErraticMovementUpdate > secondsBetweenErraticMovementUpdates)
                {
                    _deltaX = Random.Range(-100, 100) / 100f;
                    _deltaY = Random.Range(-100, 100) / 100f;
                    _lastErraticMovementUpdate = Time.fixedTime;
                }
            }
            else
            {
                _deltaX = 0;
                _deltaY = 0;
            }

            Vector2 direction = new Vector2(_deltaX, _deltaY);

            rb2D.velocity = direction * speed;
            spriteAnimatorController.UpdateAnimator(_deltaX, _deltaY, speed, _awake);
            photonView.RPC("UpdateAnimator", RpcTarget.Others, _deltaX, _deltaY, _awake);
        }

        [PunRPC]
        private void UpdateAnimator(float deltaX, float deltaY, bool awake)
        {
            spriteAnimatorController.UpdateAnimator(deltaX, deltaY, speed, awake);
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (!PhotonNetwork.IsMasterClient) return;
            if (!_awake)
            {
                spriteAnimatorController.UpdateAnimator(0f, 0f, 1f, true);
                _awake = true;
            }
        }

        public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
        {
            if (stream.IsWriting && PhotonNetwork.IsMasterClient)
            {
                stream.SendNext(transform.position);
            }
            else if (stream.IsReading && !PhotonNetwork.IsMasterClient)
            {
                _nextPosition = (Vector3) stream.ReceiveNext();
            }
        }
    }
}