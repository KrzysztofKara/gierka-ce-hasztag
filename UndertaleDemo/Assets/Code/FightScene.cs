using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FightScene : MonoBehaviour
{
    [SerializeField] private RectTransform Heart;
    private void OnDisable()
    {
        Heart.SetLocalPositionAndRotation(new Vector3(0, 0, 0), Quaternion.identity);
    }
}
