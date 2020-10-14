using System.Collections;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public CharacterController2D CharacterController;
    public float MoveSpeed = 1f;
    public Animator Animator;
    public Gameplay GamePlay;


    public AudioSource AudioSource;
    public AudioClip JumpClip;
    public AudioClip LandClip;
    public AudioClip SawHitClip;
    public AudioClip VictoryClip;

    private float move;
    private bool jump;
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

    public void OnHit()
    {
        AudioSource.PlayOneShot(SawHitClip);
        Animator.SetBool("Hurting", true);
        Animator.SetBool("Jumping", false);
        Animator.SetBool("Running", false);
        GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;
        GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        StartCoroutine(Disappear());
        GamePlay.TriggerGameOver();
    }

    private IEnumerator Disappear()
    {
        yield return new WaitForSeconds(1f);

        GetComponent<SpriteRenderer>().enabled = false;
    }

    public void OnRestart()
    {
        GetComponent<SpriteRenderer>().enabled = true;

        GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;
        transform.position = startPosition;
        GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
        Animator.SetBool("VictoryDance", false);
        Animator.SetBool("Hurting", false);
    }

    public void OnVictory()
    {
        Animator.SetBool("VictoryDance", true);
        Animator.SetBool("Jumping", false);
        Animator.SetBool("Running", false);

        StartCoroutine(PlayVictoryClip());
    }

    private IEnumerator PlayVictoryClip()
    {
        // Wait for pickup sound to have played.
        yield return new WaitForSeconds(0.3f);
        AudioSource.PlayOneShot(VictoryClip);
    }

    public void OnJumping()
    {
        if (GamePlay.Playing)
        {
            AudioSource.PlayOneShot(JumpClip);
            Animator.SetBool("Jumping", true);
        }
    }

    public void OnLanding()
    {
        AudioSource.PlayOneShot(LandClip);
        Animator.SetBool("Jumping", false);
    }

    private void FixedUpdate()
    {
        CharacterController.Move(move * Time.fixedDeltaTime, false, jump);
        jump = false;
    }
}
