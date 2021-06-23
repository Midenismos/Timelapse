using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrokeCard : MonoBehaviour
{
    [SerializeField]
    private Card card = null;


    //Casse la carte si le joueur marche dessus (entre en contact avec le boxCollider du GameObject Whole
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "FeetCollider")
        {
            card.isBroken = true;
            card.timerSinceBroken = 0;
        }
    }
}
