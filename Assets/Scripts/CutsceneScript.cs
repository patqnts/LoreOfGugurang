using UnityEngine;
using UnityEngine.Video;

public class CutsceneScript : MonoBehaviour
{
    public VideoPlayer videoPlayer;
    public VideoClip[] module1;
    public VideoClip[] module2;
    public VideoClip[] module3;
    public VideoClip[] module4;

    public GameObject VideoParent;
    public static CutsceneScript instance;

    public void Start()
    {
        instance = this;

        SaveTypeScript sts = FindObjectOfType<SaveTypeScript>();
        if(sts.saveType == 0)
        {
            PlayModule1Cutscene(0);
        }
    }
    public void PlayModule1Cutscene(int videoIndex)
    {
        PlayCutscene(module1, videoIndex);
    }

    public void PlayModule2Cutscene(int videoIndex)
    {
        PlayCutscene(module2, videoIndex);
    }

    public void PlayModule3Cutscene(int videoIndex)
    {
        PlayCutscene(module3, videoIndex);
    }

    public void PlayModule4Cutscene(int videoIndex)
    {
        PlayCutscene(module4, videoIndex);
    }

    void PlayCutscene(VideoClip[] clips, int videoIndex)
    {
        if (videoIndex >= 0 && videoIndex < clips.Length && clips[videoIndex] != null)
        {
            AudioHandler.instance.ChangeMusic(0);
            videoPlayer.source = VideoSource.VideoClip; // Set video player source
            videoPlayer.clip = clips[videoIndex]; // Assign the appropriate video clip
            VideoParent.SetActive(true);
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
        AudioHandler.instance.ChangeMusic(1);
        PixelCrushers.DialogueSystem.Sequencer.Message("SceneEnd");
        VideoParent.SetActive(false);
        videoPlayer.loopPointReached -= EndReached; // Unsubscribe to prevent memory leaks
    }
}
