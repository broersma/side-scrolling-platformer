using UnityEngine;

public class Saw : MonoBehaviour
{
    public Transform Fire;

    void Update()
    {
        Fire.Rotate(new Vector3(0, 0, Time.deltaTime * 180f));
        transform.Rotate(new Vector3(0, 0, Time.deltaTime * 720f));
    }

    public void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.tag == "Player")
        {
            collider.gameObject.GetComponent<PlayerInput>().OnHit();
        }
    }
}
