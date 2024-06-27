using UnityEngine;

public class Raycaster : MonoBehaviour
{
    [SerializeField] private LayerMask castLayer;
    private Camera cam;

    private void Awake()
    {
        cam = Camera.main;
    }
    private void Update()
    {
        CheckCast();
    }
    private void CheckCast()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector3.zero, 0f, castLayer);

            if (hit)
            {
                IRaycastTarget raycastTarget = hit.collider.transform.GetComponent<IRaycastTarget>();
                raycastTarget.OnHit();
            }
        }
    }
}
