using TMPro;
using UnityEngine;

public class BattleHP : MonoBehaviour
{
    [SerializeField] private TMP_Text _HP;
    [SerializeField] private RectTransform _HPlvl; //poziom obecnego hp
    [SerializeField] private RectTransform _HPsize;//szerokoœæ ca³ego elementu


    public void UpdateHP(int newHP, int baseHP)
    {
        _HP.text = $"{newHP} / {baseHP}";

        float Width = _HPsize.sizeDelta.x; //szerokoœæ panelu hp
        float NewWidth = Width * ((float)newHP / (float)baseHP ); //nowa szerokoœæ obliczona na podstawie procentu pozosta³ego hp

        _HPlvl.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, NewWidth);

    }
}
