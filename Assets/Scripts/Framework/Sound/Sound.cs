using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sound : MonoSingleton<Sound> {
    private AudioSource m_Bg;
    private AudioSource m_Effect;
    public string ResourcesDir = "";


    protected override void Awake()
    {
        base.Awake();
        m_Bg = gameObject.AddComponent<AudioSource>();
        m_Bg.playOnAwake = false;
        m_Bg.loop = true;

        m_Effect = gameObject.AddComponent<AudioSource>();
    }

    //播放背景音乐
    public void PlayBg(string audioName)
    {
        string oldName;
        if(m_Bg.clip == null)
        {
            oldName = "";
        }
        else
        {
            oldName = m_Bg.clip.name;
        }

        if(audioName != oldName)
        {
            //加载资源clip
            string path = ResourcesDir + "/" + audioName;
            AudioClip clip = Resources.Load<AudioClip>(path);

            //资源不为空播放
            if(clip != null)
            {
                m_Bg.clip = clip;
                m_Bg.Play();
            }
        }
    }

    //播放音效
    public void PlayEffect(string audioName)
    {
        //加载资源clip
        string path = ResourcesDir + "/" + audioName;
        AudioClip clip = Resources.Load<AudioClip>(path);

        if(clip != null)
            m_Effect.PlayOneShot(clip);
    }
}
