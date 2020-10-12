using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public CharacterController2D CharacterController;
    public float MoveSpeed = 1f;

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
    }

    private void FixedUpdate()
    {

        CharacterController.Move(move * Time.fixedDeltaTime, crouch, jump);
        jump = false;
    }
}
