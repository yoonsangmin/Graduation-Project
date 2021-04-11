using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class GameData
{
    public float soundVol;
    public bool Stage1Clear;
    public bool Stage2Clear;

    public GameData()
    {
        soundVol = 1.0f;
        Stage1Clear = false;
        Stage2Clear = false;
    }

    public void LoadData()
    {
        GameController.instance.Stage1Clear = Stage1Clear;
        GameController.instance.Stage2Clear = Stage2Clear;
    }

    public void SaveData()
    {
        soundVol = AudioController.instance._volume;
        Stage1Clear = GameController.instance.Stage1Clear;
        Stage2Clear = GameController.instance.Stage2Clear;
    }
}
