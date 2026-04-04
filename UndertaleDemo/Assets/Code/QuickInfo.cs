using TMPro;
using UnityEngine;

public class QuickInfo : MonoBehaviour
{
    [SerializeField] private TMP_Text _LVL;
    [SerializeField] private TMP_Text _HP;
    [SerializeField] private TMP_Text _GOLD;

    public void SetLvl(int lvl)
    {
        _LVL.text = lvl.ToString();
    }

    public void SetHp(int hp, int baseHp)
    {
        _HP.text = hp.ToString() + "/" + baseHp.ToString();
    }

    public void SetGold(int gold)
    {
        _GOLD.text = gold.ToString();
    }
}
