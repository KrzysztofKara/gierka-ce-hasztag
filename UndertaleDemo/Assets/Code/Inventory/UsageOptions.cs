using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UsageOptions : MonoBehaviour
{
    [SerializeField] int CurrIndex;

    [SerializeField] private List<GameObject> Hearts;

    /// <summary>
    /// Wy³¹cza wszystkie opcje
    /// </summary>
    public void Off()
    {
        for (int i = 0; i < Hearts.Count; i++)
        {
            Hearts[i].SetActive(false);
        }
        CurrIndex = 0;
    }

    /// <summary>
    /// Przesuwa aktywn¹ opcjê i ustawia obecny index 
    /// </summary>
    public void Move(Direction dir)
    {
        if (dir == Direction.Left)
        {
            Activate(CurrIndex - 1);
        }
        else if (dir == Direction.Right)
        {
            Activate(CurrIndex + 1);
        }
    }

    /// <summary>
    /// Wybiera obecnie zaznaczon¹ opcjê 
    /// </summary>
    /// <returns>Index Opcji któr¹ zaznaczono (-1 jeœli nie by³a ¿adna zaznaczona)</returns>
    public int Select()
    {
        if (CurrIndex > -1 && CurrIndex < Hearts.Count)
        {
            Hearts[CurrIndex].SetActive(false);
            return CurrIndex;
        }
        return -1;
    }

    /// <summary>
    /// W³¹cza opcjê o podanym indexie, ustawia obecny index i wy³¹cza poprzednio aktywn¹ opcjê
    /// </summary>
    /// <returns>Informacjê czy uda³o siê wykonaæ akcjê</returns>
    public bool Activate(int index)
    {
        if (index > -1 && index < Hearts.Count)
        {
            Hearts[CurrIndex].SetActive(false);
            Hearts[index].SetActive(true);
            CurrIndex = index;
            return true;
        }
        else
        {
            return false;
        }
    }


    /// <summary>
    /// Aktywuje/Deaktywuje Serce elementu o podamnym id (Starszy system u¿ywany w Menu g³ównym)
    /// </summary>
    public void Active(int index, bool action)
    {
        if (index > -1 && index < Hearts.Count)
        {
            Hearts[index].SetActive(action);
        }
    }
}
