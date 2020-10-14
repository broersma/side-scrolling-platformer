using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public CharacterController2D CharacterController;
    public float MoveSpeed = 1f;
    public Animator Animator;
    public Gameplay GamePlay;

    private float move;
    private bool jump;
    private bool crouch;
    private Vector2 startPosition;

    private void Start()
    {
        startPosition = transform.position;
    }

    void Update()
    {
        if (GamePlay.Playing)
        {
            move = Input.GetAxisRaw("Horizontal") * MoveSpeed;
            if (Input.GetButtonDown("Jump"))
            {
                jump = true;
            }

            Animator.SetBool("Running", Mathf.Abs(move) > 0.1f);
        }
        else
        {
            move = 0;
            jump = false;
            if (Input.anyKeyDown)
            {
                GamePlay.RequestRestart();
            }
        }
    }

    public void OnRestart()
    {
        GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;
        transform.position = startPosition;
        GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
        Animator.SetBool("VictoryDance", false);
    }

    public void OnVictory()
    {
        Animator.SetBool("VictoryDance", true);
        Animator.SetBool("Jumping", false);
        Animator.SetBool("Running", false);
    }

    public void OnJumping()
    {
        if (GamePlay.Playing)
        {
            Animator.SetBool("Jumping", true);
        }
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
