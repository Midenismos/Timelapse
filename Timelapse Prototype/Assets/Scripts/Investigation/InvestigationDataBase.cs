using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvestigationDataBase : MonoBehaviour
{
    private static InvestigationDataBase instance = null;

    private List<InvestigationCharacterData> investigationItems = new List<InvestigationCharacterData>();

    public List<InvestigationCharacterData> InvestigationItems { get => investigationItems; }

    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(gameObject);

        if(instance)
        {
            Destroy(gameObject);
        } else
        {
            instance = this;
        }
    }

    public InvestigationCharacterData CharacterWidgetCreated(Vector2 position)
    {
        InvestigationCharacterData character = new InvestigationCharacterData(position);
        investigationItems.Add(character);

        return character;
    }
}
