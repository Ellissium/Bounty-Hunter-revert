using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{
    [SerializeField] private Button ua;
    [SerializeField] private Button en;
    [SerializeField] private GameObject unmute;
    [SerializeField] private GameObject mute;
    [SerializeField] private Slider sliderVolume;
    [SerializeField] private Dropdown dropdown;
    [SerializeField] private Localization localization;

    public AudioMixer audioMixer;
    private string localizationType;
    private float volume;
    private int qualityIndex;
    private bool soundPause;
    private PlayerInfo loadedData;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        loadedData = DataSaver.loadData<PlayerInfo>("players");
        volume = loadedData.volumeSave;
        qualityIndex = loadedData.qualityIndexSave;
        soundPause = loadedData.soundPauseSave;
        localizationType = loadedData.localizationTypeSave;

        sliderVolume.value = volume;
        dropdown.value = qualityIndex;

        SetVolume(volume);
        SetQuality(qualityIndex);
        Sound(soundPause);

        if (soundPause)
        {
            mute.SetActive(true);
            unmute.SetActive(false);
        }
        else
        {
            mute.SetActive(false);
            unmute.SetActive(true);
        }

        if (localizationType == "ua")
        {
            ua.interactable = false;
            en.interactable = true;
        }
        else
        {
            en.interactable = false;
            ua.interactable = true;
        }
    }

    public void SetVolume(float volume) 
    {
        this.volume = volume;
        audioMixer.SetFloat("volume", Mathf.Log10(volume) * 20);
    }

    public void SetQuality(int qualityIndex)
    {
        this.qualityIndex = qualityIndex;
        QualitySettings.SetQualityLevel(qualityIndex);
    }

    public void Sound(bool soundPause)
    {
        if(soundPause == true)
        {
            AudioListener.pause = true;
            this.soundPause = true;
        }
        else
        {
            AudioListener.pause = false;
            this.soundPause = false;
        }
    }

    public void OnDestroy()
    {
        foreach(Sound s in AudioManager.instance.sounds)
        {
            if (s.source != null)
            {
                s.source.Stop();
            }
        }
            PlayerInfo saveData = new PlayerInfo
        {
            volumeSave = volume,
            qualityIndexSave = qualityIndex,
            localizationTypeSave = localization.Localizationtype,
            indexSave = localization.ItemIndex,
            soundPauseSave = soundPause,
            lastRecordSave = loadedData.lastRecordSave,
            bestRecordSave = loadedData.bestRecordSave
        };
            DataSaver.saveData(saveData, "players");
    }
}
