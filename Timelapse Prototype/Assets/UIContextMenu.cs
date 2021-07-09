using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct UIContextMenuButtonData
{
    public string title;
    public Action function;
}
public class UIContextMenu : MonoBehaviour
{
    [SerializeField] private Button ContextButtonPrefab = null;

    public void OnServerInitialized()
    {

    }
}
