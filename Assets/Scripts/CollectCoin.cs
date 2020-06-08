using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CollectCoin : MonoBehaviour
{
    private float coins = 0.0f;
    private bool isDeath = false;
#pragma warning disable 0649
    [SerializeField]
    private Text Coins;

#pragma warning restore 0649

    void Start()
    {
        coins = (int)PlayerPrefs.GetFloat("Coins");
        Coins.text = ((int)PlayerPrefs.GetFloat("Coins")).ToString();
    }

    void Update()
    {
        Coins.text = ((int)coins).ToString();
    }

    public void IncreaseCoins()
    {
        if (isDeath)
            return;
        coins += 1f;
    }
    public void IncreaseSpecialCoin()
    {
        if (isDeath)
            return;
        coins += 100f;
    }
    public void OnDeath()
    {
        isDeath = true;
            PlayerPrefs.SetFloat("Coins", coins);
    }
}
