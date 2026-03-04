using TMPro;
using UnityEngine;

public class InventorySlot : MonoBehaviour
{
    [SerializeField] private TMP_Text _itemNameText;

    public void SetItemName(string itemName)
    {
        _itemNameText.text = itemName;
    }
}
