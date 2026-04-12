using UnityEngine.UI;
using UnityEngine;
using System;
using Ink.Runtime;

public class BattleOption : MonoBehaviour
{
    [SerializeField] private Sprite ActiveSprite;
    [SerializeField] private Sprite UnactiveSprite;
    [SerializeField] private GameObject Heart;

    [SerializeField] private Image Sprite;

    void Start()
    {
        Debug.Log("MENU START");
    }

    public void Active(bool action)
    {
        if (action) Sprite.sprite = ActiveSprite;
        else Sprite.sprite = UnactiveSprite;

        Heart.SetActive(action);
    }
}
