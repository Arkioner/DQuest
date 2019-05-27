using UnityEngine;

namespace Sprites.Log.Scripts
{
    public class LogSpriteAnimatorController : SpriteAnimatorController
    {
        public void UpdateAnimator(float deltaX, float deltaY, float speed, bool awake)
        {
            UpdateAnimatorSetIsAwake(awake);
            UpdateAnimator(deltaX, deltaY, speed);
        }

        private static readonly int Awake = Animator.StringToHash("awake");
        // Start is called before the first frame update


        private void UpdateAnimatorSetIsAwake(bool awake)
        {
            Animator.SetBool(Awake, awake);
        }
    }
}