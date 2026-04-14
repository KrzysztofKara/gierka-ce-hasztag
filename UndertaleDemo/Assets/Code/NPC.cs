using UnityEngine;
using UnityEngine.UI;
using System;

public class NPC : MonoBehaviour
{
    public string npcName;
    public int maxHP = 100;
    public int currentHP;
    public bool canFight;

    public Sprite battleSprite; // sprite do pokazania w UI
    public static event Action<NPC> OnPlayerMeetNPC;// Event wywo³ywany przy wejœciu w trigger
    public static event Action<NPC> OnNPCDeath;// Œmieræ NPC

    void Start()
    {
        currentHP = maxHP;
    }

    public void TakeDamage(int dmg)
    {
        currentHP -= dmg;
        if (currentHP <= 0)
        {
            currentHP = 0;
            Debug.Log(npcName + " ded³");
            gameObject.SetActive(false);//Jak zginie to znika z œwiata
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && canFight)
        {
            Debug.Log("Spotkano NPC: " + npcName);

            // Wywo³anie eventu
            OnPlayerMeetNPC?.Invoke(this);
        }
    }
}