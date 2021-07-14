using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class Digicode : MonoBehaviour
{
    public GameObject[] buttons ;

    //mettre les boutons dans l'ordre de clignotement
    public GameObject[] buttonsBlinkingOrder;

    [SerializeField]
    private Material activatedMaterial = null;
    [SerializeField]
    private Material deactivatedMaterial = null;

    private int maxNumber = 0;

    [SerializeField]
    private int currentNumber = 0;
    [SerializeField]
    private int expectedNumber = 0;

    // à fixer pour définir la durée de clignotement
    [SerializeField]
    public float timerBlinkMax;

    public float timerBlink;

    public float multiplier = 1f;

    private GameObject TimeManager;

    private int count = 0;
    private int previousCount = 0;

    public UnityEvent executedFunction;
    public UnityEvent executedRewindedFunction;

    private bool isActivated = false;


    // Start is called before the first frame update
    void Start()
    {
        maxNumber = buttons.Length;

        //Connecte l'objet au TimeManager
        TimeManager = GameObject.Find("TimeManager");

        timerBlink = timerBlinkMax;
        foreach (GameObject button in buttons)
        {
            button.GetComponent<Button>().buttonActivatedMaterial = activatedMaterial;
            button.GetComponent<Button>().buttonDeactivatedMaterial = deactivatedMaterial;
        }

    }

    // Update is called once per frame
    void Update()
    {
        multiplier = TimeManager.GetComponent<TimeManager>().multiplier;

        //Règle le nombre attendu par la machine
        expectedNumber = currentNumber+1 ;

        
        foreach (GameObject button in buttons)
        {
            if (button.GetComponent<Button>().clicked == true)
            {
                if (button.GetComponent<DigicodeButton>().CheckedButton == false)
                {
                    //Valide le bouton et change son apparence s'il est appuyé dans le bon ordre
                    if (button.GetComponent<DigicodeButton>().ButtonOrderNumber == expectedNumber)
                    {
                        button.GetComponent<DigicodeButton>().CheckedButton = true;
                        currentNumber += 1;
                    }
                    //Reset le digicode si le joueur n'appuye pas de le bon ordre.
                    else
                    {
                        Fail();
                    }
                }
            }
        }

        //Gère le clignotement des boutons sur le digicode
        timerBlink -= Time.deltaTime * multiplier;

        if (count < 0)
        {
            count = maxNumber - 1;
        }

        else if (count > maxNumber - 1)
        {
            count = 0;
        }

        if (timerBlink <= 0)
        {

            buttonsBlinkingOrder[count].gameObject.transform.GetChild(0).gameObject.SetActive(true);
            buttonsBlinkingOrder[previousCount].gameObject.transform.GetChild(0).gameObject.SetActive(false);

            previousCount = count;
            count += 1;

            timerBlink = timerBlinkMax;

        } 
        else if (timerBlink > timerBlinkMax)
        {

            buttonsBlinkingOrder[count].gameObject.transform.GetChild(0).gameObject.SetActive(true);
            buttonsBlinkingOrder[previousCount].gameObject.transform.GetChild(0).gameObject.SetActive(false);

            previousCount = count;
            count -= 1;

            timerBlink = 0;

        }


        if (currentNumber == maxNumber && isActivated == false)
        {
            FindObjectOfType<SoundManager>().Play("AccessGranted");
            executedFunction?.Invoke();
            isActivated = true;
        }

    }

    private void Fail()
    {
        //Reset le digicode en cas d'échec
        foreach (GameObject button in buttons)
        {
            button.GetComponent<DigicodeButton>().CheckedButton = false;
            button.GetComponent<Button>().clicked = false;
            FindObjectOfType<SoundManager>().Play("Fail");
        }
        currentNumber = 0;
    }

    public void rewindNumber()
    {
        currentNumber -= 1;
        executedRewindedFunction?.Invoke();
        buttons[currentNumber].GetComponent<DigicodeButton>().CheckedButton = false;
        if (currentNumber == maxNumber - 1)
        {

            isActivated = false;
        }
    }
}
