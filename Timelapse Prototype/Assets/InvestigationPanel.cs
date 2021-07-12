using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InvestigationPanel : MonoBehaviour
{
    [SerializeField] private Transform investigationWidgetsContainer = null;
    [SerializeField] private Transform investigationLinkContainer = null;
    [SerializeField] private InvestigationWidget investigationWidgetPrefab = null;
    [SerializeField] private InvestigationLink investigationLinkPrefab = null;

    [Header("Line Colors")]
    [SerializeField] private Color friendColor = Color.green;
    [SerializeField] private Color enemyColor = Color.red;
    [SerializeField] private Color neutralColor = Color.black;

    private InvestigationDataBase dataBase = null;

    private InvestigationLink heldLink = null;
    private InvestigationLinkType heldLinkType;

    private Dictionary<InvestigationWidgetData, InvestigationWidget> investigationWidgets = new Dictionary<InvestigationWidgetData, InvestigationWidget>();

    private void Start()
    {
        dataBase = FindObjectOfType<InvestigationDataBase>();

        if (dataBase)
        {
            InitializePanel();
        }
    }

    private void InitializePanel()
    {
        List<InvestigationCharacterData> characterData = dataBase.InvestigationItems;

        for (int i = 0; i < characterData.Count; i++)
        {
            CreateNewInvestigationWidget(characterData[i]);
        }

        List<InvestigationLinkData> linkData = dataBase.InvestigationLinks;

        for (int i = 0; i < linkData.Count; i++)
        {
            AddLink(linkData[i]);
        }
    }

    public void InvestigationWidgetLeftClicked(PointerEventData data, GameObject widget)
    {
        if(heldLink)
        {
            if(heldLink.WidgetA)
            {
                heldLink.WidgetB = widget.transform;

                dataBase.InvestigationLinkCreated(
                    heldLink.WidgetA.GetComponent<InvestigationWidget>().data.widgetData,
                    heldLink.WidgetB.GetComponent<InvestigationWidget>().data.widgetData,
                    heldLinkType
                    );

                heldLink = null;
            }
            else
            {
                heldLink.WidgetA = widget.transform;
            }
        }
    }

    public void CreateNewInvestigationWidget(PointerEventData eventData, GameObject board)
    {
        InvestigationWidget widget = Instantiate(investigationWidgetPrefab, investigationWidgetsContainer, false);
        widget.GetComponent<UIPointerEvents>().OnClick.AddListener(InvestigationWidgetLeftClicked);

        widget.transform.localPosition = (Vector3)eventData.position - board.transform.position;

        widget.data = dataBase.CharacterWidgetCreated(widget.transform.localPosition);

        investigationWidgets.Add(widget.data.widgetData, widget);

    }

    public void CreateNewInvestigationWidget(InvestigationCharacterData data)
    {
        InvestigationWidget widget = Instantiate(investigationWidgetPrefab, investigationWidgetsContainer, false);
        widget.GetComponent<UIPointerEvents>().OnClick.AddListener(InvestigationWidgetLeftClicked);

        widget.transform.localPosition = data.widgetData.position;
        widget.AssignData(data);

        investigationWidgets.Add(widget.data.widgetData, widget);
    }

    

    public void CreateNewLink(int linkType)
    {
        if (heldLink)
        {
            heldLink.ChangeColor(GetCorrespondingColor(linkType));
            heldLinkType = (InvestigationLinkType)linkType;
        }
        else
        {
            heldLink = Instantiate(investigationLinkPrefab, investigationLinkContainer);
            heldLink.ChangeColor(GetCorrespondingColor(linkType));
            heldLinkType = (InvestigationLinkType)linkType;
        }
    }

    public void AddLink(InvestigationLinkData data)
    {
        InvestigationLink link = Instantiate(investigationLinkPrefab, investigationLinkContainer);
        link.ChangeColor(GetCorrespondingColor(data.linkType));

        link.WidgetA = investigationWidgets[data.widgetA].transform;
        link.WidgetB = investigationWidgets[data.widgetB].transform;
    }

    private Color GetCorrespondingColor(int linkType)
    {
        InvestigationLinkType type = (InvestigationLinkType)linkType;

        switch (type)
        {
            case InvestigationLinkType.FRIEND:
                return friendColor;            
            
            case InvestigationLinkType.ENEMY:
                return enemyColor;            
            
            case InvestigationLinkType.NEUTRAL:
                return neutralColor;
        }

        return friendColor;
    }

    private Color GetCorrespondingColor(InvestigationLinkType type)
    {
        switch (type)
        {
            case InvestigationLinkType.FRIEND:
                return friendColor;

            case InvestigationLinkType.ENEMY:
                return enemyColor;

            case InvestigationLinkType.NEUTRAL:
                return neutralColor;
        }

        return friendColor;
    }
}
