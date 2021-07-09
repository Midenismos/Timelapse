using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum InvestigationLinkType
{
    FRIEND,
    NEUTRAL,
    ENEMY
}

public class InvestigationWidgetData
{
    public Vector2 position;
    
    public InvestigationWidgetData(Vector2 position)
    {
        this.position = position;
    }
}

public class InvestigationLinkData
{
    public InvestigationWidgetData widgetA = null;
    public InvestigationWidgetData widgetB = null;
    public InvestigationLinkType linkType;

    public InvestigationLinkData(InvestigationWidgetData widgetA, InvestigationWidgetData widgetB, InvestigationLinkType type)
    {
        this.widgetA = widgetA;
        this.widgetB = widgetB;
        this.linkType = type;
    }
}
