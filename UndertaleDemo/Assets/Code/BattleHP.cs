using TMPro;
using UnityEngine;

public class BattleHP : MonoBehaviour
{
    [SerializeField] private TMP_Text _HP;
    [SerializeField] private RectTransform _HPLvl; //poziom obecnego hp
    [SerializeField] private RectTransform _HPSize;//szerokoœæ ca³ego elementu

    [SerializeField] private int DefaultHPLvl;

    public void UpdateHP(int newHP, int baseHP)
    {
        _HP.text = $"{newHP}/{baseHP}";

        float NewWidh = 100 * (float)baseHP / DefaultHPLvl; //ustawiamy szerokoœæ pola hp
        float NewLVL = NewWidh * ((float)newHP / (float)baseHP ); //nowa szerokoœæ obliczona na podstawie procentu pozosta³ego hp

        _HPLvl.offsetMax = new Vector2(- NewWidh + NewLVL, 0);
        _HPSize.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, NewWidh);

    }
}
