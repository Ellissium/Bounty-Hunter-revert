using System;
using System.Collections.Generic;

[Serializable]
public class PlayerInfo
{
    public List<long> recordsSave = new List<long>();
    public int qualityIndexSave = 0;
    public float volumeSave = 1;
    public string localizationTypeSave = "en";
    public int indexSave = 0;
    public bool soundPauseSave = false;
}
