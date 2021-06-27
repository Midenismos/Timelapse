using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInteractable
{
    void PlayerHoverStart();
    void PlayerHoverEnd();

    void Interact(GameObject pickup, PlayerController player);

}
