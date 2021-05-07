using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SettingsMenu : MonoBehaviour
{
    [SerializeField] private Localization localization;
    public AudioMixer audioMixer;
    private float volume;
    private int qualityIndex;

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
    public void Sound()
    {
        AudioListener.pause =! AudioListener.pause;
    }

    public void OnDestroy()
    {
        PlayerInfo saveData = new PlayerInfo();
        saveData.volumeSave = volume;
        saveData.qualityIndexSave = qualityIndex;
        saveData.localizationTypeSave = localization.Localizationtype;
        Debug.Log(saveData.volumeSave);
        Debug.Log(saveData.qualityIndexSave);
        DataSaver.saveData(saveData, "players");
    }
}
