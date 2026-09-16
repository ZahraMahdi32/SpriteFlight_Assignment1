using UnityEngine;

public class CameraAspect : MonoBehaviour
{
    private float targetAspect = 16f / 9f;

    void Start()
    {
        Camera cam = GetComponent<Camera>();

        float windowAspect =
            (float)Screen.width / Screen.height;

        float scaleHeight =
            windowAspect / targetAspect;

        if (scaleHeight < 1.0f)
        {
            cam.rect = new Rect(
                0,
                (1.0f - scaleHeight) / 2.0f,
                1.0f,
                scaleHeight
            );
        }
        else
        {
            float scaleWidth =
                1.0f / scaleHeight;

            cam.rect = new Rect(
                (1.0f - scaleWidth) / 2.0f,
                0,
                scaleWidth,
                1.0f
            );
        }
    }
}