using System.Collections.Generic;
using UnityEngine;

public class SpriteAnimatorController : MonoBehaviour
{
        
    [SerializeField] private float animatorSpeedFactor = 1f;

    protected Animator Animator;

    private readonly Dictionary<char, int> _directionFaces =
        new Dictionary<char, int> {{'s', 0}, {'e', 1}, {'n', 2}, {'w', 3}};
        
    private static readonly int Moving = Animator.StringToHash("moving");
    private static readonly int Direction = Animator.StringToHash("direction");
    // Start is called before the first frame update
    void Start()
    {
        Animator = GetComponent<Animator>();
    }

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
        Animator.SetInteger(Direction, faceDirection);
    }

    private void UpdateAnimatorSetIsMoving(bool moving)
    {
        Animator.SetBool(Moving, moving);
    }

    private void UpdateAnimatorSetAnimationSpeed(float animationSpeed)
    {
        Animator.speed = animationSpeed;
    }
}