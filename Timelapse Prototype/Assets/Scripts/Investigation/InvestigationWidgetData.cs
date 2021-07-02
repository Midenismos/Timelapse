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
}

public class InvestigationLinkData
{
    public InvestigationWidgetData widgetA = null;
    public InvestigationWidgetData widgetB = null;
    public InvestigationLinkType linkType;
}
