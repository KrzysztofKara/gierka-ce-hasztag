using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleNPC : MonoBehaviour
{
    [SerializeField] GameObject _NPCDialogue;
    [SerializeField] Image _NPCBackground;
    [SerializeField] Image _NPCSprite;
    public string npcID; 
    public void SetNPCsprite(Sprite sprite)
    {
        if (sprite != null) _NPCSprite.sprite = sprite;
    }

    public void SetNPCBackground(Sprite sprite)
    {
        if (sprite != null) _NPCBackground.sprite = sprite;
    }
}
