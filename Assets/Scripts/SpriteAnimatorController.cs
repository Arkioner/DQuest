using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class SpriteAnimatorController : MonoBehaviour
{
    [SerializeField] private float animatorSpeedFactor = 1f;

    [SerializeField] protected Animator animator;

    private readonly Dictionary<char, int> _directionFaces =
        new Dictionary<char, int> {{'s', 0}, {'e', 1}, {'n', 2}, {'w', 3}};
        
    private static readonly int Moving = Animator.StringToHash("moving");
    private static readonly int Direction = Animator.StringToHash("direction");

    public virtual void UpdateAnimator(float deltaX, float deltaY, float speed)
    {
        float magX = Mathf.Abs(deltaX);
        float magY = Mathf.Abs(deltaY);

        if (magX > 0 || magY > 0)
        {
            //Has movement
            UpdateAnimatorSetIsMoving(true);
            UpdateAnimatorSetAnimationSpeed(Mathf.Max(magX, magY) * speed * animatorSpeedFactor);
            if (magX > magY)
            {
                UpdateAnimatorSetDirection(deltaX > 0 ? _directionFaces['e'] : _directionFaces['w']);
            }
            else
            {
                UpdateAnimatorSetDirection(deltaY > 0 ? _directionFaces['n'] : _directionFaces['s']);
            }
        }
        else
        {
            UpdateAnimatorSetIsMoving(false);
            UpdateAnimatorSetAnimationSpeed(1);
        }
    }

    private void UpdateAnimatorSetDirection(int faceDirection)
    {
        animator.SetInteger(Direction, faceDirection);
    }

    private void UpdateAnimatorSetIsMoving(bool moving)
    {
        animator.SetBool(Moving, moving);
    }

    private void UpdateAnimatorSetAnimationSpeed(float animationSpeed)
    {
        animator.speed = animationSpeed;
    }
}