using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class OptionsMenu : MonoBehaviour {
    public TMP_InputField username;
    public Slider musicVolumeSlider;
    public Slider effectsVolumeSlider;
    string uname;

    // Start is called before the first frame update
    void Start() {
        musicVolumeSlider.value = PlayerPrefs.GetFloat("MusicVolume", 0.5f);
        effectsVolumeSlider.value = PlayerPrefs.GetFloat("EffectsVolume", 0.5f);
        uname = PlayerPrefs.GetString("username"); 
        Debug.Log("uname is :" + uname);
        if (uname == null || uname == "") { 
            PlayerPrefs.SetString("username", createUname()); 
            uname = PlayerPrefs.GetString("username"); 
        }
        username.text = uname; 
    }

    public void updateUname() {
        PlayerPrefs.SetString("username", username.text);
    }

    public void updateMusicVolume() {
        PlayerPrefs.SetFloat("MusicVolume", musicVolumeSlider.value);
        AudioManager.instance.musicVolumeChanged();
    }

    public void updateEffectsVolume() {
        PlayerPrefs.SetFloat("EffectsVolume", effectsVolumeSlider.value);
        AudioManager.instance.effectVolumeChanged();
    }

    private string createUname() {
        
        TextAsset fnamesAsset = Resources.Load("fname") as TextAsset;
        string[] fnames = fnamesAsset.text.Split('\n'); 
        Debug.Log(fnames[0]); 

        TextAsset lnamesAsset = Resources.Load("lname") as TextAsset;
        string[] lnames = lnamesAsset.text.Split('\n');
        Debug.Log(lnames[0]);
        
        string uname = fnames[Random.Range(0, fnames.Length - 1)].Trim();
        uname += "-" + lnames[Random.Range(0, lnames.Length - 1)].Trim();

        Debug.Log(uname); 
        return uname;  
    }
}
