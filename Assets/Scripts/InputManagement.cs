using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

public class InputManagement : MonoBehaviour
{
#pragma warning disable 0649
    [SerializeField]
    [InputAxis]
    private string Horizontal;

    [SerializeField]
    [InputAxis]
    private string Vertical;

    [SerializeField]
    [InputAxis]
    private string Jump;

#pragma warning restore 0649

    /// <summary>
    /// First float - horizontal axis, second - vertical axis
    /// </summary>
    public event Action<float,float,float,bool,bool,bool> OnAxisInput;


    void Update()
    {
        OnAxisInput?.Invoke(Input.GetAxisRaw(Horizontal), Input.GetAxisRaw(Vertical), Input.GetAxisRaw(Jump), Input.GetKeyDown("down"), Input.GetKeyDown("space"), Input.GetKeyDown("right shift"));
    }
}
