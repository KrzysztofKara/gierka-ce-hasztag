using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleOptions : MonoBehaviour
{
    [SerializeField] BattleOption[] Options;

    public void SelectOption(int index, bool action)
    {
        if (index > -1 && index < Options.Length)
        {
            Options[index].Select(action);
        }
        else
        {
            Debug.Log("Podano z³y idex przy aktywacji Battle Options");
        }
        
    }
}
