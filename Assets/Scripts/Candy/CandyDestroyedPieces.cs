using UnityEngine;
using Zenject;

public class CandyDestroyedPieces : MonoBehaviour
{
    [SerializeField] private GameObject[] pieces;
    [SerializeField] private float forceStrength;
    
    private void Start()
    {
        AddForcePieces();
    }
    private void AddForcePieces()
    {
        foreach (GameObject piece in pieces)
        {
            piece.GetComponent<Rigidbody2D>().AddForce(piece.transform.up * forceStrength);
        }
    }
}
