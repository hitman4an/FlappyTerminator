using UnityEngine;

[RequireComponent(typeof(Animator))]
public class CharacterAnimator : MonoBehaviour
{
    protected Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    public void SetAttack()
    {
        _animator.SetTrigger(CharacterAnimatorData.Params.Attack);        
    }

    public class CharacterAnimatorData
    {
        public class Params
        {
            public static readonly int Attack = Animator.StringToHash(nameof(Attack));
        }
    }
}
