using UnityEngine;

namespace DQuest.Entities.FrostBall.Scripts
{
    public class FrostBallSpriteAnimatorController : SpriteAnimatorController
    {
        private static readonly int Hit = Animator.StringToHash("hit");
        // Start is called before the first frame update


        public void UpdateAnimatorSetHadHit()
        {
            animator.SetBool(Hit, true);
        }
    }
}
