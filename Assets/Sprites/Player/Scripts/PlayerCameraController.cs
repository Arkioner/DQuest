using UnityEngine;

namespace Sprites.Player.Scripts
{
    public class PlayerCameraController : MonoBehaviour
    {
        [SerializeField] private GameObject _player;

        private Vector3 _offset;

        // Start is called before the first frame update
        private void Start()
        {
            _offset = transform.position - _player.transform.position;
        }

        // Update is called once per frame
        private void LateUpdate()
        {
            transform.position = _player.transform.position + _offset;
        }
    }
}