using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class LootControllInteract : Interactable
{
    [SerializeField] GameObject Chestclosed;
    [SerializeField] GameObject Chestopened;
    [SerializeField] bool opend;

    public override void Interact(Character character)
    {
        if(opend == false)
        {
            opend = true;
            Chestclosed.SetActive(false);
            Chestopened.SetActive(true);
        }
    }
}
