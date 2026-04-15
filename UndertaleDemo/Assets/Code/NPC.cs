using UnityEngine;
using UnityEngine.UI;
using System;

public class NPC : MonoBehaviour
{
    public string npcName;
    public int maxHP = 100;
    public int currentHP;
    public int Gold;
    public int EXP;
    public bool canFight;


    public Sprite battleSprite; // sprite do pokazania w UI
    public static event Action<NPC> OnPlayerMeetNPC;// Event wywo³ywany przy wejœciu w trigger
    public static event Action<NPC> OnNPCDeath;// Œmieræ NPC

    void Start()
    {
        currentHP = maxHP;
    }

    /// <summary>
    /// Zadaje obra¿enia NPC z którym obecnie walczymy
    /// </summary>
    /// <returns>Czy NPC dalej ¿yje czy ded³</returns>
    public bool TakeDamage(int dmg)
    {
        currentHP -= dmg;
        Debug.Log("chlop");
        if (currentHP <= 0)
        {
            currentHP = 0;
            Debug.Log(npcName + " ded³");
            OnNPCDeath?.Invoke(this);
            gameObject.SetActive(false);//Jak zginie to znika z œwiata
            return false;
        }
        return true;
    }

    /// <summary>
    /// Jak spotkamy NPC to odpala siê ten trigger
    /// </summary>
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