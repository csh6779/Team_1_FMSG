using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Field"))
        {
            Debug.Log("경작지 위에 올라옴! 상호작용 가능");
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Field"))
        {
            Debug.Log("경작지에서 벗어남!");
        }
    }
}