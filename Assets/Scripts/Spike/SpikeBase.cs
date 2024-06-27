using UnityEngine;

public class SpikeBase : MonoBehaviour, IInteractable
{
    private Candy candy;

    protected virtual void Awake()
    {
        candy = FindObjectOfType<Candy>();
    }
    public void OnInteract()
    {
        candy.Destroy();
    }
}
