using System;
using UnityEngine;

public class InputService : MonoBehaviour
{
    private const string Jump = "Jump";
    private const string Attack = "Fire1";

    public event Action JumpBtnDown;
    public event Action AttackBtnPressed;
    
    public void GetInput()
    {
        if (Input.GetButtonDown(Jump))
            JumpBtnDown?.Invoke();

        if (Input.GetButton(Attack))
        {
            AttackBtnPressed?.Invoke();
        }
    }
}
