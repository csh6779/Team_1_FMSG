using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class exithouse : Interactable
{
    [SerializeField] Vector2 OutHouse;

    public override void Interact(Character character)
    {
        Rigidbody2D playerposition = character.GetComponent<Rigidbody2D>();
        if (playerposition != null)
        {
            playerposition.position = OutHouse;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                collision.GetComponent<Rigidbody2D>().position = OutHouse;
            }
        }
    }

}