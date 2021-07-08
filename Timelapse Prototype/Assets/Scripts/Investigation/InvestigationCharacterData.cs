using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvestigationCharacterData : InvestigationItemData
{
    public string firstname = "";
    public string nickname = "";
    public string job = "";
    public float height = 0;
    public float weight = 0;
    public float iD = 0;
    public float age = 0;
    public string bloodGroup = "";
    public string nationality = "";
    public Texture2D portrait = null;

    public InvestigationCharacterData()
    {

    }

    public InvestigationCharacterData(Vector2 position)
    {
        this.widgetData = new InvestigationWidgetData(position);
    }

    public InvestigationCharacterData(string name, string firstname, string nickname, string job, float height, float weight, float iD, float age, string bloodGroup, string nationality, Texture2D portrait)
    {
        this.name = name;
        this.firstname = firstname;
        this.nickname = nickname;
        this.job = job;
        this.height = height;
        this.weight = weight;
        this.iD = iD;
        this.age = age;
        this.bloodGroup = bloodGroup;
        this.nationality = nationality;
        this.portrait = portrait;
    }
}
