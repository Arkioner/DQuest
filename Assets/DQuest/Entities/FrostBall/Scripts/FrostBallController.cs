using UnityEngine;

namespace DQuest.Entities.FrostBall.Scripts
{
    public class FrostBallController : MonoBehaviour
    {
        [SerializeField] private float speed;

        [SerializeField] private float ttl;

        [SerializeField] private FrostBallSpriteAnimatorController spriteAnimatorController;

        private bool _isAlive;
        private float _createdAt;

        private void Start()
        {
            _createdAt = Time.time;
            _isAlive = true;
        }


        // Update is called once per frame
        void FixedUpdate()
        {
            if (_isAlive)
            {
                if (IsTimeToDie())
                {
                    Blow();
                }
                else
                {
                    Move();
                }
            }
        }

        private bool IsTimeToDie()
        {
            return Time.time - _createdAt > ttl;
        }

        private void Move()
        {
            transform.Translate(new Vector3(-speed, 0));
        }

        private void Blow()
        {
            _isAlive = false;
            spriteAnimatorController.UpdateAnimatorSetHadHit();
            Destroy(gameObject, 1);
        }
    }
}