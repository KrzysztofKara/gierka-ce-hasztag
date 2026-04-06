using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Options : MonoBehaviour
{
    [SerializeField] private List<GameObject> Hearts;

    public void Active(int index, bool action)
    {
        if (index > -1 && index < Hearts.Count)
        {
            Hearts[index].SetActive(action);
        }
    }
}
