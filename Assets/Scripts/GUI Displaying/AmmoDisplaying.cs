using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AmmoDisplaying : MonoBehaviour
{
    [SerializeField] private Image ammoImage;
    [SerializeField] private Text ammoAmount;

    private int ammoInCylinder;
    private int ammoReload;
    private Vector3 offset = new Vector3(122f,-76f,0);
    Text currentAmmoText;
    private void Start()
    {
        ammoInCylinder = CharacterStats.instance.AmmoInCylinder;
        ammoReload = CharacterStats.instance.AmmoReload;
        Image currentAmmoImage = Instantiate(ammoImage, transform);
        currentAmmoImage.rectTransform.position += offset;
        currentAmmoText = Instantiate(ammoAmount, transform);
        currentAmmoText.rectTransform.position += new Vector3(offset.x + currentAmmoImage.rectTransform.rect.width * 5, offset.y, offset.z);
        currentAmmoText.text = $"{ammoInCylinder.ToString()} / {ammoReload.ToString()}";
        CharacterStats.instance.onShoot += DrawAmmoStats;
    }
    private void DrawAmmoStats()
    {
        ammoInCylinder = CharacterStats.instance.AmmoInCylinder;
        ammoReload = CharacterStats.instance.AmmoReload;
        if (ammoReload != 0)
        {
            currentAmmoText.text = $"{ammoInCylinder.ToString()} / {ammoReload.ToString()}";
        } else
        {
            if (ammoInCylinder != 0)
            {
                currentAmmoText.text = $"{ammoInCylinder.ToString()}";
            } else
            {
                StartCoroutine(DrawNoAmmo());
            }
        }
    }

    private IEnumerator DrawNoAmmo()
    {
        while(ammoInCylinder == 0)
        {
            currentAmmoText.text = "No Ammo!";
            yield return new WaitForSeconds(1f);
            currentAmmoText.text = string.Empty;
            yield return new WaitForSeconds(0.5f);
        }
    }

    private void OnGUI()
    {
        
    }

}
