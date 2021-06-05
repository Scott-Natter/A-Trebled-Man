using UnityEngine;
using UnityEngine.SceneManagement;

public class TeleportPlayerInside : MonoBehaviour
{
    public GameObject destination;

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            SceneManager.LoadScene("Elevator");
        }
    }
}
