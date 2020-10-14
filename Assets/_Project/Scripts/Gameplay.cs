using System.Collections;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;

public class Gameplay : MonoBehaviour
{
    public PlayerInput Player;

    public Text TitleText;
    public Text CreditsText;
    public Text PressAnyKeyToContinueText;
    public Text PressAnyKeyToRestartText;
    public Text VictoryText;
    public Text GameOverText;
    public Text ScoreText;

    public AudioSource AudioSource;
    public AudioClip StartClip;
    public AudioClip BitPickupClip;
    public AudioClip FallClip;

    private bool restartAllowed;
    private int score;

    public bool Playing { get; private set; }

    void Start()
    {
        Playing = false;
        TitleText.gameObject.SetActive(true);
        CreditsText.gameObject.SetActive(true);
        StartCoroutine(EnablePressAnyKeyToStart(1f, false));
    }

    public void IncreaseScore()
    {
        score++;

        ScoreText.text = score.ToString();
        AudioSource.PlayOneShot(BitPickupClip);

        if ( score >= 8 )
        {
            TriggerVictory();
        }
    }

    void Update()
    {
        // Don't fall down a pit.
        if (Playing && Player.transform.position.y < -5)
        {

            AudioSource.PlayOneShot(FallClip);
            TriggerGameOver();
        }
    }

    private void TriggerVictory()
    {
        if (Playing)
        {
            Playing = false;
            VictoryText.gameObject.SetActive(true);

            Player.OnVictory();

            StartCoroutine(EnablePressAnyKeyToStart(3f, true));
        }
    }

    public void TriggerGameOver()
    {
        if ( Playing )
        {
            Playing = false;
            GameOverText.gameObject.SetActive(true);

            ScoreText.gameObject.SetActive(false);

            StartCoroutine(EnablePressAnyKeyToStart(1f, true));
       }
    }

    private IEnumerator EnablePressAnyKeyToStart(float waitBeforeRestartAllowed, bool restart)
    {
        yield return new WaitForSeconds(waitBeforeRestartAllowed);

        restartAllowed = true;

        while (!Playing)
        {
            (restart ? PressAnyKeyToRestartText : PressAnyKeyToContinueText).gameObject.SetActive(true);
            yield return new WaitForSeconds(0.8f);
            (restart ? PressAnyKeyToRestartText : PressAnyKeyToContinueText).gameObject.SetActive(false);
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
            VictoryText.gameObject.SetActive(false);
            PressAnyKeyToContinueText.gameObject.SetActive(false);
            PressAnyKeyToRestartText.gameObject.SetActive(false);
            Player.OnRestart();

            score = 0;
            ScoreText.gameObject.SetActive(true);
            ScoreText.text = score.ToString();
            FindObjectsOfType<Bit>().ToList().ForEach(bit => bit.Reset());

            AudioSource.PlayOneShot(StartClip);

            Playing = true;
            restartAllowed = false;
        }
    }
}
