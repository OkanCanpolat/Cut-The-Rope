using UnityEngine;
using Zenject;

public class Buttons : MonoBehaviour
{
    private GameManager gameManager;


    [Inject]
    public void Construct(GameManager gameManager)
    {
        this.gameManager = gameManager;
    }

    public void RestartButton()
    {
        gameManager.RestartLevel();
    }
    public void NextLevelButton()
    {
        gameManager.NextLevel();
    }
}
