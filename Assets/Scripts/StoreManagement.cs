using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StoreManagement : MonoBehaviour
{
#pragma warning disable 0649
    [SerializeField]
    private Text Speed;

    [SerializeField]
    private Text Jetpack;

    [SerializeField]
    private Text Skateboard;

    [SerializeField]
    private Text Coins;

    [SerializeField]
    private GameObject CanvasAlert;
#pragma warning restore 0649
    private float numOfSpeed = 0.0f;
    private float numOfJetpack = 0.0f;
    private float numOfSkateboard = 0.0f;
    private float coins;

    void Start()
    {
         coins = (int)PlayerPrefs.GetFloat("Coins");
        SetText();
        numOfSpeed = (int)PlayerPrefs.GetFloat("Speed");
        numOfJetpack = (int)PlayerPrefs.GetFloat("Jetpack");
        numOfSkateboard = (int)PlayerPrefs.GetFloat("Skateboard");
        CanvasAlert.SetActive(false);

    }

    void Update()
    {
        SetText();
        numOfSpeed = (int)PlayerPrefs.GetFloat("Speed");
        numOfJetpack = (int)PlayerPrefs.GetFloat("Jetpack");
        numOfSkateboard = (int)PlayerPrefs.GetFloat("Skateboard");
    }
    private void SetText()
    {
        Coins.text = ((int)coins).ToString();
        Speed.text = ((int)PlayerPrefs.GetFloat("Speed")).ToString();
        Jetpack.text = ((int)PlayerPrefs.GetFloat("Jetpack")).ToString();
        Skateboard.text = ((int)PlayerPrefs.GetFloat("Skateboard")).ToString();
    }
    public void IncreaseNumOfSlowSpeed()
    {
        if (coins >= 100)
        {
            numOfSpeed++;
            coins-=100;
            PlayerPrefs.SetFloat("Coins", coins);
            PlayerPrefs.SetFloat("Speed", numOfSpeed);
        }
        else
        {
           CanvasAlert.SetActive(true);
        }
    }
    public void IncreaseNumOfJetpack()
    {
        if (coins >= 500)
        {
            numOfJetpack++;
            coins -= 500;
            PlayerPrefs.SetFloat("Coins", coins);
            PlayerPrefs.SetFloat("Jetpack", numOfJetpack);
        }
        else
        {
            CanvasAlert.SetActive(true);
        }
    }
    public void IncreaseNumOfSkateboard()
    {
        if (coins >= 200)
        {
            numOfSkateboard++;
            coins -= 200;
            PlayerPrefs.SetFloat("Coins", coins);
            PlayerPrefs.SetFloat("Skateboard", numOfSkateboard);
        }
        else
        {
            CanvasAlert.SetActive(true);
        }
    }

    public void ExitAlert()
    {
            CanvasAlert.SetActive(false);
    }
}
