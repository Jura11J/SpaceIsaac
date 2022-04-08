using UnityEngine;
using System;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour {
    public Sound[] sounds;      
    public Sound[] playlist;    

    private int currentPlayingIndex = 999; 

    
    private bool shouldPlayMusic = false; 

    public static AudioManager instance; 

    private float mvol; 
    private float evol; 

    private void Start() {
        PlayMusic();
    }


    private void Awake() {

        if (instance == null) {     
            instance = this;        
        } else {
            Destroy(gameObject);    
            return;                 
        }

        DontDestroyOnLoad(gameObject); 

       
        mvol = PlayerPrefs.GetFloat("MusicVolume", 0.75f);
        evol = PlayerPrefs.GetFloat("EffectsVolume", 0.75f);

        createAudioSources(sounds, evol);     
        createAudioSources(playlist, mvol);   

    }

    // create sources
    private void createAudioSources(Sound[] sounds, float volume) {
        foreach (Sound s in sounds) {   
            s.source = gameObject.AddComponent<AudioSource>(); 
            s.source.clip = s.clip;     
            s.source.volume = s.volume * volume; 
            s.source.pitch = s.pitch;   
            s.source.loop = s.loop;     
        }
    }

    public void PlaySound(string name) {
        
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null) {
            Debug.LogError("Unable to play sound " + name);
            return;
        }
        s.source.Play(); 
    }

    public void PlayMusic() {
        if (shouldPlayMusic == false) {
            shouldPlayMusic = true;
            
            currentPlayingIndex = UnityEngine.Random.Range(0, playlist.Length - 1);
            playlist[currentPlayingIndex].source.volume = playlist[0].volume * mvol; 
            playlist[currentPlayingIndex].source.Play(); 
        }

    }

    
    public void StopMusic() {
        if (shouldPlayMusic == true) {
            shouldPlayMusic = false;
            currentPlayingIndex = 999; 
        }
    }

    void Update() {
        
        if (currentPlayingIndex != 999 && !playlist[currentPlayingIndex].source.isPlaying) {
            currentPlayingIndex++; 
            if (currentPlayingIndex >= playlist.Length) { 
                currentPlayingIndex = 0; 
            }
            playlist[currentPlayingIndex].source.Play(); 
        }
    }

    
    public String getSongName() {
        return playlist[currentPlayingIndex].name;
    }

    
    public void musicVolumeChanged() {
        foreach (Sound m in playlist) {
            mvol = PlayerPrefs.GetFloat("MusicVolume", 0.45f);
            m.source.volume = playlist[0].volume * mvol;
        }
    }

    
    public void effectVolumeChanged() {
        evol = PlayerPrefs.GetFloat("EffectsVolume", 0.45f);
        foreach (Sound s in sounds) {
            s.source.volume = s.volume * evol;
        }
        sounds[0].source.Play(); 
    }
}