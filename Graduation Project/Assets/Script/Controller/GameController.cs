using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    private static GameController gameController;
    public static GameController instance
    {
        get
        {
            if (gameController == null)
                gameController = FindObjectOfType<GameController>();
            return gameController;
        }
    }

    public bool Stage1Clear = false;
}
