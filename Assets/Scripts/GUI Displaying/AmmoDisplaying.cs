using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AmmoDisplaying : MonoBehaviour
{
    [SerializeField] private LocalizationMainScene localizationMainScene;
    [SerializeField] private Image ammoImage;
    [SerializeField] private Text ammoAmount;
    [SerializeField] private GameObject background;
    [SerializeField] private Sprite shortBackground;
    [SerializeField] private Sprite longBackground;
    [SerializeField] private Text currentAmmoText;
    [SerializeField] private Image currentAmmoImage;
    private int ammoInCylinder;
    private int ammoReload;
    private Vector3 offset = new Vector3(0f,0f,0f);

    private void Start()
    {
        ammoInCylinder = CharacterStats.instance.AmmoInCylinder;
        ammoReload = CharacterStats.instance.AmmoReload;
        currentAmmoImage.rectTransform.position += offset;
        currentAmmoText.rectTransform.position += new Vector3(offset.x, offset.y, offset.z);
        CharacterStats.instance.onAmmoChanged += DrawAmmoStats;
        DrawAmmoStats();
    }
    private void DrawAmmoStats()
    {
        ammoInCylinder = CharacterStats.instance.AmmoInCylinder;
        ammoReload = CharacterStats.instance.AmmoReload;
        if (ammoReload != 0)
        {
            background.GetComponent<Image>().sprite = shortBackground;
            background.GetComponent<RectTransform>().sizeDelta = new Vector2(53f, 17f);
            currentAmmoText.text = $"{ammoInCylinder.ToString()} / {ammoReload.ToString()}";
        } 
        else
        {
            if (ammoInCylinder != 0)
            {
                background.GetComponent<Image>().sprite = shortBackground;
                background.GetComponent<RectTransform>().sizeDelta = new Vector2(53f, 17f);
                currentAmmoText.text = $"{ammoInCylinder.ToString()}";
            } 
            else
            {
                StartCoroutine(DrawNoAmmo());
            }
        }
    }
    private IEnumerator DrawNoAmmo()
    {
        while(ammoInCylinder == 0)
        {
            if (CharacterStats.instance.AmmoInCylinder == 0)
            {
                background.GetComponent<Image>().sprite = longBackground;
                background.GetComponent<RectTransform>().sizeDelta = new Vector2(76f, 17f);
                if (localizationMainScene.Localizationtype == "en") 
                {
                    currentAmmoText.fontSize = 14;
                    currentAmmoText.fontStyle = FontStyle.Normal;
                    currentAmmoText.text = "No Ammo!"; 
                }
                else
                {
                    currentAmmoText.fontSize = 7;
                    currentAmmoText.fontStyle = FontStyle.Bold;
                    currentAmmoText.text = "Õ≈Ã¿™ Õ¿¡ŒØ¬";
                }
            }
            yield return new WaitForSeconds(1f);
            if (CharacterStats.instance.AmmoInCylinder == 0)
                currentAmmoText.text = string.Empty;
            yield return new WaitForSeconds(0.5f);
        }
    }
}
