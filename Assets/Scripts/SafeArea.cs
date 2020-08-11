using UnityEngine;

public class SafeArea : MonoBehaviour
{
    private RectTransform panel;
    private Rect lastSafeArea = new Rect (0, 0, 0, 0);

    private void Awake ()
    {
        panel = GetComponent<RectTransform> ();
        Refresh ();
    }

    private void Update ()
    {
        Refresh ();
    }

    private void Refresh ()
    {
        var safeArea = GetSafeArea ();

        if (safeArea != lastSafeArea)
            ApplySafeArea (safeArea);
    }

    private static Rect GetSafeArea ()
    {
        return Screen.safeArea;
    }

    private void ApplySafeArea (Rect r)
    {
        lastSafeArea = r;

        // Convert safe area rectangle from absolute pixels to normalised anchor coordinates
        var anchorMin = r.position;
        var anchorMax = r.position + r.size;
        anchorMin.x /= Screen.width;
        anchorMin.y /= Screen.height;
        anchorMax.x /= Screen.width;
        anchorMax.y /= Screen.height;
        panel.anchorMin = anchorMin;
        panel.anchorMax = anchorMax;
    }
}