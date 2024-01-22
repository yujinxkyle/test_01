using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class AudioManager : MonoBehaviour{
    public static AudioManager Instance;
    public GameObject audioMan;
    public Sound[] musicSounds, sfxSounds, menuBGMSounds;
    public AudioSource musicSource, sfxSource, menuBGMSource;
    private int stage = 0;
    public bool menubgmplaying = false;
    public bool stage1bgmplaying = false;
    public bool stage2bgmplaying = false;
    public bool playerDead;
    

    private void Awake() {
        if(Instance==null){
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else{
            Destroy(gameObject);
        }
    }
    private void Update() {
        stage = SceneManager.GetActiveScene().buildIndex;
        if (stage == 0 && !menubgmplaying){
            Debug.Log("at main menu");
            PlayMenuMusic("Main Menu");         
            menubgmplaying = true;
            musicSource.Stop();
        }
        if (stage == 1 && !stage1bgmplaying){
            Debug.Log("at stage 1 ");
            PlayMusic("Stage 1 Map");
            stage1bgmplaying = true;
            menuBGMSource.Stop();
        }
        if (stage == 2 && !stage2bgmplaying){
            Debug.Log("at stage 2");
            PlayMusic("Stage 2 Map");
            stage2bgmplaying = true;
        }
    }
    public void IsDead(){
        stage1bgmplaying = false;
    }
    public void PlayMenuMusic (string name){
        Sound s = Array.Find(menuBGMSounds, x => x.name == name);

        if (s == null){
            Debug.Log("Music not found");
        }
        
        else{
            menuBGMSource.clip = s.clip;
            menuBGMSource.Play();
        }
    }
    public void PlayMusic (string name){
        Sound s = Array.Find(musicSounds, x => x.name == name);

        if (s == null){
            Debug.Log("Music not found");
        }
        
        else{
            musicSource.clip = s.clip;
            musicSource.Play();
        }
    }
    public void PlaySFX(string name){
        Sound s = Array.Find(sfxSounds, x => x.name == name);

        if (s == null){
            Debug.Log("Sound not found");
        }
        
        else{
            sfxSource.PlayOneShot(s.clip);
        }
    }
}
