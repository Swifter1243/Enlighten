using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[ExecuteInEditMode]
public class RectTransformTesting : MonoBehaviour
{
    private RectTransform rectTransform;

    // Start is called before the first frame update
    void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    private void OnDrawGizmos()
    {
#if UNITY_EDITOR
        Handles.color = Color.black;
        Handles.Label(transform.position, "Center");

        float offset = 0;
        Vector3 GetNextOffset()
        {
            offset += 100;
            return transform.position + new Vector3(0, -offset, 0);
        }

        Handles.Label(GetNextOffset(), "Offset Min: " + rectTransform.offsetMin);
        Handles.Label(GetNextOffset(), "Offset Max: " + rectTransform.offsetMax);
        Handles.Label(GetNextOffset(), "Anchor Min: " + rectTransform.anchorMin);
        Handles.Label(GetNextOffset(), "Anchor Max: " + rectTransform.anchorMax);
        Handles.Label(GetNextOffset(), "Rect: " + rectTransform.rect);

        RectTransformUtility.ScreenPointToLocalPointInRectangle(rectTransform, Input.mousePosition, null, out Vector2 mousePos);
        mousePos += new Vector2(rectTransform.rect.width, rectTransform.rect.height) * 0.5f;
        Handles.Label(GetNextOffset(), "Local Mouse Position: " + mousePos);

        Handles.Label(Input.mousePosition, "Mouse Position");
#endif

        //Vector2 left = new Vector2(rectTransform. + transform.position.x, transform.position.y);
        //Handles.Label(left, "Left");
    }

    // Update is called once per frame
    void Update()
    {

    }
}
