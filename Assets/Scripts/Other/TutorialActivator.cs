using System.Collections;
using UnityEngine;

public class TutorialActivator : MonoBehaviour, IInteractable
{
    [SerializeField] private GameObject[] tutorialPanels;
    [SerializeField] private float activateDelay;
    public void OnInteract()
    {
        StartCoroutine(ActivatePanels());
    }
    private IEnumerator ActivatePanels()
    {
        yield return new WaitForSeconds(activateDelay);

        foreach (GameObject go in tutorialPanels)
        {
            go.SetActive(true);
        }
    }
}
