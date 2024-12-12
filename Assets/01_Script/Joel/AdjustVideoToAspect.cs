using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;


public class AdjustVideoToAspect : MonoBehaviour
{
    public RawImage rawImage; // Asigna el Raw Image
    public VideoPlayer videoPlayer; // Asigna el Video Player

    void Start()
    {
        if (videoPlayer != null && rawImage != null)
        {
            StartCoroutine(AdjustAspect());
        }
    }

    private IEnumerator AdjustAspect()
    {
        // Espera a que el video cargue
        while (!videoPlayer.isPrepared)
        {
            yield return null;
        }

        // Ajusta el tamaño del Raw Image según la relación de aspecto del video
        float videoWidth = videoPlayer.texture.width;
        float videoHeight = videoPlayer.texture.height;
        float aspectRatio = videoWidth / videoHeight;

        RectTransform rt = rawImage.GetComponent<RectTransform>();
        rt.sizeDelta = new Vector2(rt.sizeDelta.y * aspectRatio, rt.sizeDelta.y); // Ajusta ancho basado en alto
    }
}
