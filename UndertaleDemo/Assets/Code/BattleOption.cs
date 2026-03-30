using UnityEngine.UI;
using UnityEngine;
using System;

public class BattleOption : MonoBehaviour
{
    [SerializeField] private Sprite ActiveSprite;
    [SerializeField] private Sprite UnActiveSprite;
    [SerializeField] private GameObject Heart;

    [SerializeField] private Image Sprite;

    public void Activate()
    {
        Sprite.sprite = ActiveSprite;
        Heart.SetActive(true);
    }

    public void DeActivate()
    {
        Sprite.sprite = UnActiveSprite;
        Heart.SetActive(false);
    }
}
