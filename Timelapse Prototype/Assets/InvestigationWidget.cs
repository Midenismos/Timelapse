using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InvestigationWidget : MonoBehaviour
{
    [SerializeField] private GameObject investigationCharacterSheet = null;

    [Header("Input Fields")]
    [SerializeField] private InputField nickname = null;
    [SerializeField] private InputField lastname = null;
    [SerializeField] private InputField firstname = null;
    [SerializeField] private InputField age = null;
    [SerializeField] private InputField job = null;
    [SerializeField] private InputField height = null;
    [SerializeField] private InputField weight = null;
    [SerializeField] private InputField iD = null;
    [SerializeField] private InputField bloodGroup = null;
    [SerializeField] private InputField nationality = null;

    public InvestigationCharacterData data = null;

    public void AssignData(InvestigationCharacterData data)
    {
        this.data = data;
        nickname.text = data.nickname;
        firstname.text = data.firstname;
        lastname.text = data.name;
        age.text = data.age.ToString();
        job.text = data.job;
        height.text = data.height.ToString();
        weight.text = data.weight.ToString();
        iD.text = data.iD.ToString();
        bloodGroup.text = data.bloodGroup;
        nationality.text = data.nationality;
    }
    
    public void SwitchCharacterSheetVisibility()
    {
        investigationCharacterSheet.SetActive(!investigationCharacterSheet.activeInHierarchy);
    }

    public void EventDoubleClicked()
    {
        SwitchCharacterSheetVisibility();
    }

    public void NickNameChanged(string nickname)
    {
        data.nickname = nickname;
    }

    public void FirstNameChanged(string firstname)
    {
        data.firstname = firstname;
    }

    public void LastNameChanged(string name)
    {
        data.name = name;
    }

    public void JobChanged(string job)
    {
        data.job = job;
    }

    public void WeightChanged(string weight)
    {
        data.weight = int.Parse(weight);
    }

    public void HeightChanged(string height)
    {
        data.height = int.Parse(height);
    }

    public void IDChanged(string iD)
    {
        data.iD = float.Parse(iD);
    }

    public void AgeChanged(string age)
    {
        data.age = float.Parse(age);
    }

    public void BloodGroupChanged(string bloodGroup)
    {
        data.bloodGroup = bloodGroup;
    }

    public void NationalityChanged(string nationality)
    {
        data.nationality = nationality;
    }
}
