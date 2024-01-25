using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;
    public Sound[] musicSounds, sfxSounds, menuBGMSounds;
    public AudioSource musicSource, sfxSource, menuBGMSource;
    private int stage = 0;
    public bool menubgmplaying = false;
    public bool stage1bgmplaying = false;
    public bool stage2bgmplaying = false;
    public bool playerDead;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        SceneManager.sceneLoaded += OnSceneLoaded; // Subscribe to the sceneLoaded event
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Reset audio state when a new scene is loaded
        ResetAudioState();

        // Check the loaded scene name for debugging
        Debug.Log("Loaded Scene: " + scene.name);
    }

    private void ResetAudioState()
    {
        menubgmplaying = false;
        stage1bgmplaying = false;
        stage2bgmplaying = false;
        StopMusic();
        StopMenuBGM();
    }

    private void Update()
    {
        stage = SceneManager.GetActiveScene().buildIndex;

        if (stage == 0 && !menubgmplaying)
        {
            Debug.Log("At main menu");
            PlayMenuMusic("Main Menu");
            menubgmplaying = true;
            StopMusic();
        }
        else if (stage == 1 && !stage1bgmplaying)
        {
            Debug.Log("At stage 1");
            PlayStageMusic("Stage 1 Map");
            stage1bgmplaying = true;
            StopMenuBGM();
        }
        else if (stage == 2 && !stage2bgmplaying)
        {
            Debug.Log("At stage 2");
            PlayStageMusic("Stage 2 Map");
            stage2bgmplaying = true;
        }
    }

    public void IsDead()
    {
        StopStageMusic();
    }

    public void PlayMenuMusic(string name)
    {
        Sound s = Array.Find(menuBGMSounds, x => x.name == name);

        if (s == null)
        {
            Debug.Log("Menu music not found");
        }
        else
        {
            menuBGMSource.clip = s.clip;
            menuBGMSource.Play();
            Debug.Log("Playing menu music");
        }
    }

    public void PlayStageMusic(string name)
    {
        Sound s = Array.Find(musicSounds, x => x.name == name);

        if (s == null)
        {
            Debug.Log("Stage music not found");
        }
        else
        {
            musicSource.clip = s.clip;
            musicSource.Play();
            Debug.Log("Playing stage music");
        }
    }

    public void StopMusic()
    {
        musicSource.Stop();
        Debug.Log("Music stopped");
    }

    public void StopMenuBGM()
    {
        menuBGMSource.Stop();
        Debug.Log("Menu BGM stopped");
    }

    public void StopStageMusic()
    {
        musicSource.Stop();
        stage1bgmplaying = false;
        stage2bgmplaying = false;
        Debug.Log("Stage music stopped");
    }

    public void PlaySFX(string name)
    {
        Sound s = Array.Find(sfxSounds, x => x.name == name);

        if (s == null)
        {
            Debug.Log("Sound not found");
        }
        else
        {
            sfxSource.PlayOneShot(s.clip);
            Debug.Log("Playing SFX");
        }
    }
}
