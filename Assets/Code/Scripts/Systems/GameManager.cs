using DG.Tweening.Core.Easing;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    static GameManager gameManager;
    public static GameManager Instance { get { return RequestGameManager(); } }

    static GameManager RequestGameManager()
    {
        if (gameManager == null)
        {
            GameObject gameManagerObj = new GameObject("GameManager");
            gameManager = gameManagerObj.AddComponent<GameManager>();

        }
        return gameManager;

    }
    public void Exit()
    {
        Application.Quit();

    }
}
