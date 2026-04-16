using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour
{
    [Header("Mo¿liwe itemy (do losowania)")]
    public List<Item> possibleItems;

    [Header("Ile itemów wylosowaæ")]
    public int minItems = 1;
    public int maxItems = 3;

    private List<Item> itemsInChest = new List<Item>();
    private bool isPlayerNearby = false;
    private bool isOpened = false;

    void Update()
    {
        if (isPlayerNearby && Input.GetKeyDown(KeyCode.E))
        {
            OpenChest();
        }

        if (isOpened && Input.GetKeyDown(KeyCode.Return))
        {
            GiveItemsToPlayer();
        }
    }

    void OpenChest()
    {
        if (isOpened) return;

        isOpened = true;
        itemsInChest.Clear();

        int itemCount = Random.Range(minItems, maxItems + 1);

        for (int i = 0; i < itemCount; i++)
        {
            int index = Random.Range(0, possibleItems.Count);
            itemsInChest.Add(possibleItems[index]);
        }

        Debug.Log("Skrzynia zawiera:");
        foreach (var item in itemsInChest)
        {
            Debug.Log(item.Name);
        }

        Debug.Log("Naciœnij ENTER, aby zebraæ przedmioty");
    }

    void GiveItemsToPlayer()
    {
        GameObject _Player = GameObject.FindWithTag("Player");
        Inventory inventory = _Player.GetComponent<Player>().inventory;

        foreach (var item in itemsInChest)
        {
            inventory.AddItem(item);
        }

        itemsInChest.Clear();
        Debug.Log("Przedmioty dodane do ekwipunku!");
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNearby = true;
            Debug.Log("Naciœnij E, aby otworzyæ skrzyniê");
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNearby = false;
        }
    }
}