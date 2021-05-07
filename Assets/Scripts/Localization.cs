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
    [SerializeField] private Text exitText;
    [SerializeField] private Text itemText;
    [SerializeField] private Text labelText;


    /* [SerializeField] private Text highText;
 */
    [SerializeField] private GameObject settingsMenu;
    [SerializeField] private GameObject dropdawn;
    
    [SerializeField] private GameObject template;
    private GameObject[] buttonsText;
    private GameObject[] defaultsText;

    private string localizationType;

    private int itemIndex;
    public string Localizationtype { get { return localizationType; } set { localizationType = value; } }

    private void Start()
    {
        buttonsText = GameObject.FindGameObjectsWithTag("ButtonText");
        defaultsText = GameObject.FindGameObjectsWithTag("DefaultText");
        settingsMenu.SetActive(false);
        template.SetActive(false);

        itemIndex = 0;
        localizationType = "en";
        SetLocalization();
    }

    public void SetIndex(int index) 
    {
        itemIndex = index;
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
                defaultText.GetComponent<Text>().fontSize = 15;
                defaultText.GetComponent<Text>().fontStyle = FontStyle.Bold;
            }

         
           
            dropdawn.GetComponent<Dropdown>().options[0].text = "ƒ”∆≈   Õ»«‹ ¿";
            dropdawn.GetComponent<Dropdown>().options[1].text = "Õ»«‹ ¿";
            dropdawn.GetComponent<Dropdown>().options[2].text = "—≈–≈ƒÕﬂ";
            dropdawn.GetComponent<Dropdown>().options[3].text = "¬»—Œ ¿";
            dropdawn.GetComponent<Dropdown>().options[4].text = "ƒ”∆≈   ¬»—Œ ¿";
            dropdawn.GetComponent<Dropdown>().options[5].text = "”À‹“–¿";
            itemText.fontSize = 14;
            itemText.fontStyle = FontStyle.Bold;

            labelText.text = dropdawn.GetComponent<Dropdown>().options[itemIndex].text;
            labelText.fontSize =14;
            labelText.fontStyle = FontStyle.Bold;
            muteSoundText.text = "¬ À/¬» À   «¬” ";
            soundVolumeText.text = "√”◊Õ≤—“‹   «¬” ”";
            graphicsQualityText.text = "ﬂ ≤—“‹   √–¿‘≤ »";
            languageText.text = "ÃŒ¬¿";
            generalText.text = "Œ—ÕŒ¬Õ≤";
            controlText.text = "”œ–¿¬À≤ÕÕﬂ";
            playText.text = "√–¿“»";
            settingsText.text = "Õ¿À¿ÿ“”¬¿ÕÕﬂ";
            aboutUsText.text = "œ–Œ Õ¿—";
            exitText.text = "¬»’≤ƒ";
        
        }

        else if (localizationType == "en")
        {
            foreach (GameObject buttonText in buttonsText)
            {
                buttonText.GetComponent<Text>().fontSize = 11;
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
            exitText.text = "Exit";
            dropdawn.GetComponent<Dropdown>().options[0].text = "Very   Low";
            dropdawn.GetComponent<Dropdown>().options[1].text = "Low";
            dropdawn.GetComponent<Dropdown>().options[2].text = "Medium";
            dropdawn.GetComponent<Dropdown>().options[3].text = "High";
            dropdawn.GetComponent<Dropdown>().options[4].text = "Very   High";
            dropdawn.GetComponent<Dropdown>().options[5].text = "Ultra";
            itemText.fontSize = 18;
            itemText.fontStyle = FontStyle.Normal;
            labelText.text = dropdawn.GetComponent<Dropdown>().options[itemIndex].text;
            labelText.fontSize = 18;
            labelText.fontStyle = FontStyle.Normal;
        }
    }
}
