using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStats : MonoBehaviour
{
    private const int MAX_AMMO_IN_CYLINDER = 6;
    public static CharacterStats instance = null;

    private int heatlhPoint;
    private int ammoInCylinder;
    private int ammoReload;

    public int HealthPoint { get { return heatlhPoint; } set { heatlhPoint = value; } }
    public int AmmoInCylinder { get { return ammoInCylinder; } set { ammoInCylinder = value; } }
    public int AmmoReload { get { return ammoReload; } set { ammoReload = value; } }

    public delegate void onEvent();
    public onEvent onShoot;
    public onEvent onHeatlhChanged;
    public void CharacterShooted()
    {
        if (ammoInCylinder > 0)
        {
            ammoInCylinder -= 1;
            if (ammoInCylinder == 0 && ammoReload > 0)
            {
                ammoInCylinder = MAX_AMMO_IN_CYLINDER;
                ammoReload -= 1;
            } 
        }
    }

    private void Awake()
    {
        if (instance)
        {
            DestroyImmediate(gameObject);
            return;
        }
        instance = this;
        InitializeInstance();
    }

    private void InitializeInstance()
    {
        heatlhPoint = 5;
        ammoInCylinder = MAX_AMMO_IN_CYLINDER;
        ammoReload = 0;
        onShoot += CharacterShooted;
}

    void Update()
    {
        
    }
}
