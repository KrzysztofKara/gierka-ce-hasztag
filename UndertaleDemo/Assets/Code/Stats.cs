using TMPro;
using UnityEngine;

public class Stats : MonoBehaviour
{
    [SerializeField] private TMP_Text _LVL;
    [SerializeField] private TMP_Text _HP;
    [SerializeField] private TMP_Text _GOLD;
    [SerializeField] private TMP_Text _ATACK;
    [SerializeField] private TMP_Text _DEFENCE;
    [SerializeField] private TMP_Text _WEAPON;
    [SerializeField] private TMP_Text _ARMOR;



    public void SetLvl(int lvl)
    {
        _LVL.text = lvl.ToString();
    }

    public void SetHp(int hp, int baseHp)
    {
        _HP.text = hp.ToString() + " / " + baseHp.ToString();
    }

    public void SetGold(int gold)
    {
        _GOLD.text = gold.ToString();
    }

    public void SetAtack(int atack)
    {
        _ATACK.text = atack.ToString();
    }

    public void SetDefence(int defence)
    {
        _DEFENCE.text = defence.ToString();
    }

    public void SetWeapon(string weapon)
    {
        _WEAPON.text = weapon;
    }

    public void SetArmor(string armor)
    {
        _ARMOR.text = armor;
    }
}
