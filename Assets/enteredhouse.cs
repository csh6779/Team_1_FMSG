using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enteredhouse : Interactable
{
    [SerializeField] Vector2 IntoHouse;

    public override void Interact(Character character)
    {
        Rigidbody2D playerposition = character.GetComponent<Rigidbody2D>();
        if (playerposition != null)
        {
            playerposition.position = IntoHouse;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                collision.GetComponent<Rigidbody2D>().position = IntoHouse;
            }
            
        }
    }

}