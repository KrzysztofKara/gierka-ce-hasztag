using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialougeScene : MonoBehaviour
{
    [SerializeField] private TMP_Text description;

    public void SetContent(string text)
    {
        description.text = text;
    }
}
