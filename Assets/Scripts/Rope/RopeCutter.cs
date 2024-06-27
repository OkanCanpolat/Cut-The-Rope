using UnityEngine;

public class RopeCutter : MonoBehaviour
{
    [SerializeField] private LayerMask cuttableLayer;
    [SerializeField] private GameObject trailRenderer;
    private Camera cam;
    private Vector3 lastMousePos;

    private void Awake()
    {
        cam = Camera.main;
    }
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            lastMousePos = cam.ScreenToWorldPoint(Input.mousePosition);
            trailRenderer.SetActive(true);
        }

        if (Input.GetMouseButton(0))
        {
            Vector3 mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
            transform.position = mousePos;

            if(lastMousePos != mousePos)
            {
                RaycastHit2D hit = Physics2D.Linecast(lastMousePos, mousePos, cuttableLayer.value);
                lastMousePos = mousePos;

                if (hit)
                {
                    ICuttable cuttable = hit.collider.GetComponent<ICuttable>();
                    cuttable.OnCut();
                }
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            trailRenderer.SetActive(false);
        }
    }
}
