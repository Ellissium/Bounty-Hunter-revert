using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AmmoDisplaying : MonoBehaviour
{
    [SerializeField] private Image ammoImage;
    [SerializeField] private Text ammoAmount;
    [SerializeField] private GameObject background;
    [SerializeField] private Sprite shortBackground;
    [SerializeField] private Sprite longBackground;

    private int ammoInCylinder;
    private int ammoReload;
    private Vector3 offset = new Vector3(140f,-95f,0);
    Text currentAmmoText;

    private void Start()
    {
        ammoInCylinder = CharacterStats.instance.AmmoInCylinder;
        ammoReload = CharacterStats.instance.AmmoReload;
        Image currentAmmoImage = Instantiate(ammoImage, transform);
        currentAmmoImage.rectTransform.position += offset;
        currentAmmoText = Instantiate(ammoAmount, transform);
        currentAmmoText.rectTransform.position += new Vector3(offset.x + currentAmmoImage.rectTransform.rect.width * 5, offset.y, offset.z);
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
                currentAmmoText.text = "No Ammo!";
            }
            yield return new WaitForSeconds(1f);
            if (CharacterStats.instance.AmmoInCylinder == 0)
                currentAmmoText.text = string.Empty;
            yield return new WaitForSeconds(0.5f);
        }
    }
}
