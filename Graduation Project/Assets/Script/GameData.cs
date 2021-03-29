using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class GameData
{
    public float soundVol;
    public bool Stage1Clear;

    public GameData()
    {
        soundVol = 1.0f;
        Stage1Clear = false;
    }

    public void LoadData()
    {
        GameController.instance.Stage1Clear = Stage1Clear;
    }

    public void SaveData()
    {
        soundVol = AudioController.instance._volume;
        Stage1Clear = GameController.instance.Stage1Clear;
    }
}
