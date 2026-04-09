using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.ShaderGraph.Drawing;
using UnityEngine;

public class BattleOptions : MonoBehaviour
{
    [SerializeField] int CurrIndex;

    [SerializeField] List<BattleOption> Options;
    
    /// <summary>
    /// Wy³¹cza wszystkie opcje
    /// </summary>
    public void Off()
    {
        for (int i = 0; i < Options.Count; i++)
        {
            Options[i].Active(false);
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
        if (CurrIndex > -1 && CurrIndex < Options.Count)
        {
            Options [CurrIndex].Active(false);
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
        if (index > -1 && index < Options.Count)
        {
            Options[CurrIndex].Active(false);
            Options[index].Active(true);
            CurrIndex = index;
            return true;
        }
        else
        {
            return false;
        }
    }
}
