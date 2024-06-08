using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class VideoIntro : MonoBehaviour
{
    [System.Serializable]
    public class IntroLogoClip
    {
        public string name;
        public VideoClip clip;
    }
    public VideoPlayer videoPlayer;
    public List<IntroLogoClip> introLogoClips;
    [SerializeField] int currentClipIndex = 0;

    void Start()
    {
        videoPlayer.clip = introLogoClips[currentClipIndex].clip;
        videoPlayer.Play();

        // Menambahkan event listener ketika video selesai
        videoPlayer.loopPointReached += AutoNextOrSkipVideo;

        //jadikan maksima framerate menjadi 60fps
        Application.targetFrameRate = 60;
    }

    void Update()
    {
        // Cek input dari tombol spasi atau klik kiri mouse
        if (currentClipIndex < introLogoClips.Count - 1)
            if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
                AutoNextOrSkipVideo(videoPlayer);
    }

    void AutoNextOrSkipVideo(VideoPlayer vp)
    {
        if (currentClipIndex < introLogoClips.Count - 1)
        {
            currentClipIndex++;
            videoPlayer.clip = introLogoClips[currentClipIndex].clip;
            videoPlayer.Play();
        }
        else
        {
            videoPlayer.Stop();
            SceneManager.LoadScene("MainMenu");
        }
    }
}
