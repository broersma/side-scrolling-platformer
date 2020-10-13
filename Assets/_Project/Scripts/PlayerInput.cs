﻿using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public CharacterController2D CharacterController;
    public float MoveSpeed = 1f;
    public Animator Animator;

    private float move;
    private bool jump;
    private bool crouch;

    void Update()
    {
        // TODO crouch?
        move = Input.GetAxisRaw("Horizontal") * MoveSpeed;
        if ( Input.GetButtonDown("Jump") )
        {
            jump = true;
        }

        Animator.SetBool("Running", Mathf.Abs(move) > 0.1f);
    }

    public void OnJumping()
    {
        Animator.SetBool("Jumping", true);
    }

    public void OnLanding()
    {
        Animator.SetBool("Jumping", false);
    }

    private void FixedUpdate()
    {
        CharacterController.Move(move * Time.fixedDeltaTime, crouch, jump);
        jump = false;
    }
}
