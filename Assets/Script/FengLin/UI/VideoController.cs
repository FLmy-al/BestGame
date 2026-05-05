using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class VideoController : MonoBehaviour
{
    public VideoPlayer vp;

    void Start()
    {
        // 开场自动播放
        vp.Play();
        Time.timeScale = 0f;
        vp.loopPointReached += OnVideoFinish;
    }

    public void OnVideoFinish(VideoPlayer vp)
    {
        Time.timeScale = 1.0f;
        Destroy(vp.gameObject);
    }

    private void OnDestroy()
    {
        vp.loopPointReached -= OnVideoFinish;
    }
}
