using System;
using UnityEngine;

public class InputService : MonoBehaviour
{
    private const string Jump = "Jump";
    private const string Attack = "Fire1";

    public event Action JumpButtonDown;
    public event Action AttackButtonPressed;
    
    public void GetInput()
    {
        if (Input.GetButtonDown(Jump))
            JumpButtonDown?.Invoke();

        if (Input.GetButton(Attack))
            AttackButtonPressed?.Invoke();        
    }
}
