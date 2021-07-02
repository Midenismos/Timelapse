using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InvestigationPanel : MonoBehaviour
{
    [SerializeField] private Transform investigationWidgetsContainer = null;
    [SerializeField] private InvestigationWidget investigationWidgetPrefab = null;
    public void CreateNewInvestigationWidget(PointerEventData eventData, GameObject board)
    {
        InvestigationWidget widget = Instantiate(investigationWidgetPrefab, investigationWidgetsContainer, false);

        widget.transform.localPosition = (Vector3)eventData.position - board.transform.position;
    }
}
