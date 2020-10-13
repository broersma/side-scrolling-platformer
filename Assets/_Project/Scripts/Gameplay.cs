using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Gameplay : MonoBehaviour
{
    public GameObject Player;

    public Text TitleText;
    public Text CreditsText;
    public Text PressAnyKeyToContinueText;
    public Text GameOverText;
    private Vector2 playerStartPosition;
    private bool restartAllowed;

    public bool Playing { get; private set; }

    void Start()
    {
        Playing = false;
        TitleText.gameObject.SetActive(true);
        CreditsText.gameObject.SetActive(true);
        StartCoroutine(EnablePressAnyKeyToStart());

        playerStartPosition = Player.transform.position;
    }

    void Update()
    {
        // Don't fall down a pit.
        if (Player.transform.position.y < -5)
        {
            TriggerGameOver();
        }
    }

    private void TriggerGameOver()
    {
        if ( Playing )
        {
            Playing = false;
            GameOverText.gameObject.SetActive(true);

            StartCoroutine(EnablePressAnyKeyToStart());
       }
    }

    private IEnumerator EnablePressAnyKeyToStart()
    {
        yield return new WaitForSeconds(1f);

        restartAllowed = true;

        while (!Playing)
        {
            PressAnyKeyToContinueText.gameObject.SetActive(true);
            yield return new WaitForSeconds(0.8f);
            PressAnyKeyToContinueText.gameObject.SetActive(false);
            yield return new WaitForSeconds(0.3f);
        }
    }

    public void RequestRestart()
    {
        if (restartAllowed)
        {
            TitleText.gameObject.SetActive(false);
            CreditsText.gameObject.SetActive(false);
            GameOverText.gameObject.SetActive(false);
            PressAnyKeyToContinueText.gameObject.SetActive(false);
            Player.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;
            Player.transform.position = playerStartPosition;
            Player.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;

            Playing = true;
            restartAllowed = false;
        }
    }
}
