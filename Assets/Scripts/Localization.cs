using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine;

public class Localization : MonoBehaviour
{
    [SerializeField] private Text muteSoundText;
    [SerializeField] private Text soundVolumeText;
    [SerializeField] private Text graphicsQualityText;
    [SerializeField] private Text languageText;
    [SerializeField] private Text generalText;
    [SerializeField] private Text controlText;
    [SerializeField] private Text playText;
    [SerializeField] private Text settingsText;
    [SerializeField] private Text aboutUsText;
    [SerializeField] private Text ExitText;
    /*[SerializeField] private Text mutSoundText;*/

    [SerializeField] private GameObject settingsMenu;
    private GameObject[] buttonsText;
    private GameObject[] defaultsText;

    private string localizationType;

    public string Localizationtype { get { return localizationType; } set { localizationType = value; } }

    private void Start()
    {
        buttonsText = GameObject.FindGameObjectsWithTag("ButtonText");
        defaultsText = GameObject.FindGameObjectsWithTag("DefaultText");
        settingsMenu.SetActive(false);
    }

    public void SetLocalization()
    {
        if (localizationType == "ua")
        {
            foreach (GameObject buttonText in buttonsText)
            {
                buttonText.GetComponent<Text>().fontSize = 8;
                buttonText.GetComponent<Text>().fontStyle = FontStyle.Bold;
            }

            foreach (GameObject defaultText in defaultsText)
            {
                defaultText.GetComponent<Text>().fontSize = 16;
                defaultText.GetComponent<Text>().fontStyle = FontStyle.Bold;
            }

            muteSoundText.text = "¬ À/¬» À   «¬” ";
            soundVolumeText.text = "√”◊Õ≤—“‹   «¬” ”";
            graphicsQualityText.text = "ﬂ ≤—“‹   √–¿‘≤ »";
            languageText.text = "ÃŒ¬¿";
            generalText.text = "Œ—ÕŒ¬Õ≤";
            controlText.text = "”œ–¿¬À≤ÕÕﬂ";
            playText.text = "√–¿“»";
            settingsText.text = "Õ¿À¿ÿ“”¬¿ÕÕﬂ";
            aboutUsText.text = "œ–Œ Õ¿—";
            ExitText.text = "¬»’≤ƒ";
        }

        else if (localizationType == "en")
        {
            foreach (GameObject buttonText in buttonsText)
            {
                buttonText.GetComponent<Text>().fontSize = 14;
                buttonText.GetComponent<Text>().fontStyle = FontStyle.Normal;
            }

            foreach (GameObject defaultText in defaultsText)
            {
                defaultText.GetComponent<Text>().fontSize = 20;
                defaultText.GetComponent<Text>().fontStyle = FontStyle.Normal;
            }

            muteSoundText.text = "Mute/Unmute   Sound";
            soundVolumeText.text = "Sound   Volume";
            graphicsQualityText.text = "Graphics   Quality";
            languageText.text = "Language";
            generalText.text = "General";
            controlText.text = "Control";
            playText.text = "Play";
            settingsText.text = "Settings";
            aboutUsText.text = "About   Us";
            ExitText.text = "Exit";
        }
    }
}
