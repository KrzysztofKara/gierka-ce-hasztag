using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using UnityEngine;
using UnityEngine.UI;

public class ActionPanel : MonoBehaviour
{
    [SerializeField] private RectTransform _Size;

    [SerializeField] float MaxWidth;
    [SerializeField] float SquareWidth;
    [SerializeField] float DefaultHeight;

    [SerializeField] float AnimationSpeed;

    /// <summary>
    /// Ustawia (Domyœlnie animacj¹) rozmiar dla Panelu (mo¿na podaæ tylko jeden wymiar).
    /// </summary>
    public void SetSize(float width = 0, float height = 0, ActionPanelSize type = ActionPanelSize.Max, bool animate = true)
    {
        if (animate) 
        {
            if (width > 0 && height > 0)
            {
                StartCoroutine(AnimateSize(AnimationAxis.Horizontal, AnimationSpeed, width , height));
            }
            else if (type == ActionPanelSize.Max)
            {
                StartCoroutine(AnimateSize(AnimationAxis.Horizontal, AnimationSpeed, MaxWidth, DefaultHeight));
            }
            else if (type == ActionPanelSize.Square)
            {
                StartCoroutine(AnimateSize(AnimationAxis.Horizontal, AnimationSpeed, SquareWidth, SquareWidth));
            }
        }
        else
        {
            if (height > 0) _Size.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, height);
            if (width > 0) _Size.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, width);
        }
        
        
    }

    /// <summary>
    /// Animuje Rozmiar Panelu akcji (mo¿na podaæ tylko jeden wymiar)
    /// </summary>
    IEnumerator AnimateSize(AnimationAxis axis, float time, float targetWidth = 0, float targetHeight = 0)
    {
        float startWidth = _Size.sizeDelta.x;
        float startHeight = _Size.sizeDelta.y;

        float t = 0f;

        //Animowanie Wysokoœci
        while (t < time && targetHeight > 0)
        {
            t += Time.deltaTime;
            float height = Mathf.Lerp(startHeight, targetHeight, t / time);

            _Size.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, height);

            yield return null; // czeka do nastêpnej klatki
        }

        t = 0f;

        //Animowanie Szerokoœci
        while (t < time && targetWidth > 0)
        {
            t += Time.deltaTime;
            float width = Mathf.Lerp(startWidth, targetWidth, t / time);

            _Size.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, width);

            yield return null; // czeka do nastêpnej klatki
        }


        // ustaw dok³adnie koñcow¹ wartoœæ
        if (targetHeight > 0) _Size.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, targetHeight);
        if (targetWidth > 0) _Size.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, targetWidth);
        
    }
}

