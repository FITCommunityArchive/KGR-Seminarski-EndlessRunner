using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AssetsManagement : MonoBehaviour
{
#pragma warning disable 0649

    [SerializeField]
    private Text SlowSpeed;
    [SerializeField]
    private Text Jetpack;
    [SerializeField]
    private Text Skateboard;

#pragma warning restore 0649
    private float numOfSpeed = 0.0f;
    private float numOfJetpack = 0.0f;
    private float numOfSkateboard = 0.0f;
    void Start()
    {
        SetText();
        GetValues();
    }

    void Update()
    {
        SetText();
        GetValues();
    }

    private void SetText()
    {
        SlowSpeed.text = ((int)PlayerPrefs.GetFloat("Speed")).ToString();
        Jetpack.text = ((int)PlayerPrefs.GetFloat("Jetpack")).ToString();
        Skateboard.text = ((int)PlayerPrefs.GetFloat("Skateboard")).ToString();
    }
    private void GetValues()
    {
        numOfSpeed = (int)PlayerPrefs.GetFloat("Speed");
        numOfJetpack = (int)PlayerPrefs.GetFloat("Jetpack");
        numOfSkateboard = (int)PlayerPrefs.GetFloat("Skateboard");
    }
    public void UseSlowSpeed()
    {
        numOfSpeed-=1;
        PlayerPrefs.SetFloat("Speed", numOfSpeed);

    }
    public void UseJetpack()
    {
        if (numOfJetpack > 0)
        {
        numOfJetpack--;
        PlayerPrefs.SetFloat("Jetpack", numOfJetpack);
        }
    }
    public void UseSkateboard()
    {
        if (numOfSkateboard > 0)
        {
        numOfSkateboard--;
        PlayerPrefs.SetFloat("Skateboard", numOfSkateboard);
        }
    }
}
