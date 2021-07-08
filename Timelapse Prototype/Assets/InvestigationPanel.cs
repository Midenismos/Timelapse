using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InvestigationPanel : MonoBehaviour
{
    [SerializeField] private Transform investigationWidgetsContainer = null;
    [SerializeField] private InvestigationWidget investigationWidgetPrefab = null;

    private InvestigationDataBase dataBase = null;

    private void Start()
    {
        dataBase = FindObjectOfType<InvestigationDataBase>();

        if (dataBase)
        {
            InitializePanel();
        }
    }

    public void CreateNewInvestigationWidget(PointerEventData eventData, GameObject board)
    {
        InvestigationWidget widget = Instantiate(investigationWidgetPrefab, investigationWidgetsContainer, false);

        widget.transform.localPosition = (Vector3)eventData.position - board.transform.position;

        widget.data = dataBase.CharacterWidgetCreated(widget.transform.localPosition);
    }

    public void CreateNewInvestigationWidget(InvestigationCharacterData data)
    {
        InvestigationWidget widget = Instantiate(investigationWidgetPrefab, investigationWidgetsContainer, false);

        widget.transform.localPosition = data.widgetData.position;
        widget.AssignData(data);
    }

    private void InitializePanel()
    {
        List<InvestigationCharacterData> data = dataBase.InvestigationItems;

        for (int i = 0; i < data.Count; i++)
        {
            CreateNewInvestigationWidget(data[i]);
        }
    }
}
