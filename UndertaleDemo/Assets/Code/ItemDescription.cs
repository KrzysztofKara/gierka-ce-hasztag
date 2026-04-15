using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ItemDescription : MonoBehaviour
{
    [SerializeField] private TMP_Text _ItemDescription;

    public void SetDescription(string text)
    {
        _ItemDescription.text = text;
    }
}
