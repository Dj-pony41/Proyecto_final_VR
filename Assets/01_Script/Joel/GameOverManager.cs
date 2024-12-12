using System.Collections;
using UnityEngine;
using UnityEngine.Video;

public class GameOverManager : MonoBehaviour
{
    public GameObject wastedCanvas; // Canvas que contiene el video
    public VideoPlayer videoPlayer; // Video Player para el video
    public RenderTexture wastedRenderTexture; // La Render Texture usada para el video
    private bool isGameOver = false;

    public void TriggerGameOver()
    {
        if (isGameOver) return;

        isGameOver = true;

        // Activa el Canvas
        wastedCanvas.SetActive(true);

        // Reproduce el video
        if (videoPlayer != null)
        {
            videoPlayer.Play();
        }

        // Detén el tiempo del juego después del video
        StartCoroutine(StopGameAfterVideo(videoPlayer.clip.length));
    }

    private IEnumerator StopGameAfterVideo(double videoDuration)
    {
        yield return new WaitForSecondsRealtime((float)videoDuration);

        // Detener el tiempo del juego
        Time.timeScale = 0f;

        // Limpia la Render Texture desactivando el Canvas
        wastedCanvas.SetActive(false);

        // Detén el Video Player
        if (videoPlayer != null)
        {
            videoPlayer.Stop();
        }

        // Limpia la Render Texture
        if (wastedRenderTexture != null)
        {
            RenderTexture.active = null; // Limpia la Render Texture activa
            wastedRenderTexture.Release(); // Libera cualquier contenido que tenga
        }
    }
}
