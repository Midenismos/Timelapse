using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvestigationWidget : MonoBehaviour
{
    [SerializeField] private GameObject investigationCharacterSheet = null;
    
    public void SwitchCharacterSheetVisibility()
    {
        investigationCharacterSheet.SetActive(!investigationCharacterSheet.activeInHierarchy);
    }

    public void EventDoubleClicked()
    {
        SwitchCharacterSheetVisibility();
    }
}
