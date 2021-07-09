using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InvestigationLink : MonoBehaviour
{
    [SerializeField] private Image image = null;

    private Transform widgetA = null;
    private Transform widgetB = null;

    public Transform WidgetA { get => widgetA; set => widgetA = value; }
    public Transform WidgetB { get => widgetB; set => widgetB = value; }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (widgetA && widgetB)
        {
            transform.position = Vector3.Lerp(widgetA.position, widgetB.position, 0.5f);

            transform.right = widgetA.position - WidgetB.position;

            RectTransform rt = (RectTransform)transform;

            rt.sizeDelta = new Vector2(Vector3.Distance(widgetA.position, widgetB.position), rt.sizeDelta.y);
        }
    }

    public void Initialize(Transform widgetA, Transform widgetB, Color color)
    {
        this.widgetA = widgetA;
        this.widgetB = widgetB;
        image.color = color;
    }

    public void ChangeColor(Color color)
    {
        image.color = color;
    }
}
