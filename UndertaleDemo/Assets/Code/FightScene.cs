using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FightScene : MonoBehaviour
{
    [SerializeField] private RectTransform Heart;
    [SerializeField] private Transform BulletsContainer;
    private void OnEnable()
    {
        Heart.SetLocalPositionAndRotation(new Vector3(0, 0, 0), Quaternion.identity);

        foreach (Transform child in BulletsContainer)
        {
            GameObject.Destroy(child.gameObject);
        }
    }
}
