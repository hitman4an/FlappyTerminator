using UnityEngine;

public class PlayerAnimator : CharacterAnimator
{
    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    public void SetJump()
    {
        _animator.SetTrigger(PlayerAnimatorData.Params.Jump);
    }

    public class PlayerAnimatorData: CharacterAnimatorData
    {
        new public class Params: CharacterAnimatorData.Params
        {
            public static readonly int Jump = Animator.StringToHash(nameof(Jump));
        }
    }
}
