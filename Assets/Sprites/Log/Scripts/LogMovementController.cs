using System;
using System.Threading;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Sprites.Log.Scripts
{
    public class LogMovementController : MonoBehaviour
    {
        [SerializeField] private float speed = 6f;
        [SerializeField] private float secondsBetweenErraticMovementUpdates = 2;

        [SerializeField] private bool _awake;
        private GameObject _target;
        private float _lastErraticMovementUpdate;

        private float _deltaX;
        private float _deltaY;

        private Rigidbody2D _rb2D;

        private LogSpriteAnimatorController _spriteAnimatorController;

        // Start is called before the first frame update
        private void Start()
        {
            _rb2D = GetComponent<Rigidbody2D>();
            _spriteAnimatorController = GetComponent<LogSpriteAnimatorController>();
            _lastErraticMovementUpdate = Time.fixedTime;
        }

        private void FixedUpdate()
        {
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

            _rb2D.velocity = direction * speed;
            _spriteAnimatorController.UpdateAnimator(_deltaX, _deltaY, speed, _awake);
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (!_awake)
            {
                _spriteAnimatorController.UpdateAnimator(0f, 0f, 1f, true);
                _awake = true;
            }
        }
    }
}