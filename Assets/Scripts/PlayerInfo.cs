using System;
using System.Collections.Generic;

[Serializable]
public class PlayerInfo
{
    public long lastRecordSave = 0;
    public long bestRecordSave = 0;
    public int qualityIndexSave = 0;
    public float volumeSave = 1;
    public string localizationTypeSave = "en";
    public int indexSave = 0;
    public bool soundPauseSave = false;
}
