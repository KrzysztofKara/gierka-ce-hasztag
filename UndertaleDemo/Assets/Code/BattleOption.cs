using UnityEngine.UI;
using UnityEngine;
using System;

public class BattleOption : MonoBehaviour
{
    [SerializeField] private Sprite ActiveSprite;
    [SerializeField] private Sprite UnactiveSprite;
    [SerializeField] private GameObject Heart;

    [SerializeField] private Image Sprite;

    public void Select(bool action)
    {
        if (action) Sprite.sprite = ActiveSprite;
        else        Sprite.sprite = UnactiveSprite;
        
        Heart.SetActive(action);
    }
}
