using System;
using UnityEngine;

[RequireComponent(typeof(RectTransform))]
[RequireComponent(typeof(Material))]
public class UIBorder : MonoBehaviour
{
    private static readonly int TopRightLocal = Shader.PropertyToID("_TopRightCorner");
    public RectTransform rectTransform;
    public Material material;

    private void OnValidate()
    {
        material.SetVector(TopRightLocal, GetTopRightLocalPosition());
    }

    Vector2 GetTopRightLocalPosition()
    {
        Rect rect = rectTransform.rect;
        return new Vector3(rect.width * 0.5f, rect.height * 0.5f);
    }
}
