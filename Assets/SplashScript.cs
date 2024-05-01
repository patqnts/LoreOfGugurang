using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class SplashScript : MonoBehaviour
{
    public VideoPlayer videoPlayer;
    public VideoClip clips;
    private void Start()
    {
        PlayCutscene();
    }
    void PlayCutscene()
    {
        if (clips != null)
        {
            videoPlayer.source = VideoSource.VideoClip; // Set video player source
            videoPlayer.clip = clips; // Assign the appropriate video clip
            videoPlayer.loopPointReached += EndReached;
            videoPlayer.Play();
        }
        else
        {
            Debug.LogError("Invalid videoIndex");
        }
    }

    void EndReached(UnityEngine.Video.VideoPlayer vp)
    {
        SceneManager.LoadScene("LoreMenu");
        videoPlayer.loopPointReached -= EndReached; // Unsubscribe to prevent memory leaks
    }
}
