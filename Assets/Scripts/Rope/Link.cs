using UnityEngine;

public class Link : MonoBehaviour, ICuttable
{
    private RopeBase rope;

    private void Awake()
    {
        rope = GetComponentInParent<RopeBase>();
    }
    public void OnCut()
    {
        rope.RemoveLink(gameObject);
        rope.DestroyRope();
        Destroy(gameObject);
    }
}
