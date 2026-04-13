using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackSelector : MonoBehaviour
{
    [SerializeField] Rigidbody2D SelectorRigbody;
    [SerializeField] RectTransform SelectorRect;
    [SerializeField] float Speed;
    [SerializeField] float StartPosition;

    public static event Action<float> onSliderSelected;

    private void OnEnable()
    {
        if (SelectorRigbody != null)
        {
            SelectorRigbody.velocity = Vector2.left * Speed;
        }
    }

    private void Update()
    {
        if (SelectorRigbody != null)
        {
            if (SelectorRect.localPosition.x < -StartPosition) //Jak gracz nic nie zaznaczy
            {
                Select();
            }
        }
        else if (Input.GetKeyDown(KeyCode.Return)) //Na Enter aktywujemy Slider
        {
            Select();
        }
    }

    private void OnDisable()
    {
        SetOff();
    }

    /// <summary>
    /// Resetuje pozycjê Slidera
    /// </summary>
    public void SetOff()
    {
        if (SelectorRigbody != null)
        {
            SelectorRect.SetLocalPositionAndRotation(new Vector3(StartPosition, -150, 0), Quaternion.identity);
            SelectorRigbody.velocity = Vector2.zero;            
        }
    }

    /// <summary>
    /// odpala event z jego pozycj¹ wzglêdem œrodka (procentow¹) i Resetuje pozycjê Slidera
    /// </summary>
    public void Select()
    {
        float precent = Math.Abs(SelectorRect.localPosition.x) / StartPosition; //procentowa odleg³oœæ wzglêdem œrodka
        onSliderSelected?.Invoke(precent);
        SetOff();
    }
}
