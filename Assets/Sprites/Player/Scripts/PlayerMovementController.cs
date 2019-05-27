using UnityEngine;

namespace Sprites.Player.Scripts
{
    public class PlayerMovementController : MonoBehaviour
    {
        [SerializeField] private float speed = 10f;

        private Rigidbody2D _rb2D;

        private SpriteAnimatorController _spriteAnimatorController;


        // Start is called before the first frame update
        private void Start()
        {
            _rb2D = GetComponent<Rigidbody2D>();
            _spriteAnimatorController = GetComponent<SpriteAnimatorController>();
        }

        private void FixedUpdate()
        {
            float deltaX = Input.GetAxis("Horizontal");
            float deltaY = Input.GetAxis("Vertical");

            Vector2 direction = new Vector2(deltaX, deltaY);

            _rb2D.velocity = direction * speed;
            _spriteAnimatorController.UpdateAnimator(deltaX, deltaY, speed);
        }
    }
}