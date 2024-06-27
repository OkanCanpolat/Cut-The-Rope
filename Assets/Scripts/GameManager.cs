using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

public class GameManager : MonoBehaviour
{
    private SignalBus signalBus;

    [Inject]
    public void Construct(SignalBus signalBus)
    {
        this.signalBus = signalBus;
    }
    private void Awake()
    {
        signalBus.Subscribe<LevelFailedSignal>(RestartDelayed);
    }
    public void RestartLevel()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.buildIndex);
    }
    public void NextLevel()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        int maxLevel = SceneManager.sceneCountInBuildSettings;
        int nextLevel = currentScene.buildIndex + 1;

        if (nextLevel >= maxLevel) SceneManager.LoadScene(0);
        else SceneManager.LoadScene(nextLevel);
    }
    private void RestartDelayed()
    {
        StartCoroutine(RestartDelayedCoroutine());
    }
    public IEnumerator RestartDelayedCoroutine()
    {
        yield return new WaitForSeconds(2f);
        RestartLevel();
    }
}
