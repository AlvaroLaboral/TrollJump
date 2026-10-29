using UnityEngine;

public class CtrlZonaMuerte : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<CtrlJugador>().QuitarVida();
        }
    }
}