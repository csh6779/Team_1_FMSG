using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestOpen : MonoBehaviour
{
    [SerializeField] Sprite close;
    [SerializeField] Sprite open;
    private bool isOpen = false;
    private bool isPlayerNearby = false;

    private SpriteRenderer chestRender;

    void Start()
    {
        chestRender = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (isPlayerNearby && Input.GetMouseButtonDown(0))
        {
            ToggleChest();
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) // 플레이어일 때만 반응
        {
            isPlayerNearby = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNearby = false;
        }
    }

    void ToggleChest()
    {
        chestRender.sprite = isOpen ? close : open;
        isOpen = !isOpen;
    }
}
