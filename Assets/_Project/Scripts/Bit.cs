using UnityEngine;

public class Bit : MonoBehaviour
{
    private Gameplay GamePlay;

    public bool PickedUp = false;

    void Start()
    {
        GamePlay = FindObjectOfType<Gameplay>();
    }

    public void Reset()
    {
        PickedUp = false;
        GetComponent<Renderer>().enabled = !PickedUp;
    }

    public void OnTriggerEnter2D(Collider2D collider)
    {

        if (!PickedUp && collider.tag == "Player")
        {
            PickedUp = true;
            GetComponent<Renderer>().enabled = !PickedUp;
            GamePlay.IncreaseScore();
        }
    }
}
