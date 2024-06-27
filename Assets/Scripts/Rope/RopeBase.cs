using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public abstract class RopeBase : MonoBehaviour
{
    [SerializeField] protected bool generateOnStart = true;
    [SerializeField] protected Rigidbody2D firstJointPoint;

    [Header("Link")]
    [SerializeField] protected AudioClip linkDestroyClip;
    [SerializeField] protected GameObject linkPrefab;
    [SerializeField] protected int linkCount;
    [SerializeField] protected float linkFadeTime;
    protected List<GameObject> ropeLinks = new List<GameObject>();

    [Header("Gizmos")]
    [SerializeField] protected float ropeLength;

    protected AudioSource source;
    protected Candy candy;
    public int LinkCount { get => linkCount; set => linkCount = value; }
    protected void Start()
    {
        if (generateOnStart)
        {
            GenerateRope();
        }
    }
    public abstract void GenerateRope();
    public abstract void DestroyRope();
    public abstract void RemoveLink(GameObject link);
    protected abstract IEnumerator FadeOutLinks();
    protected abstract void GenerateRopeVisual();

    protected void OnDrawGizmosSelected()
    {
        float distance = ropeLength * linkCount;
        Vector3 origin = transform.position;
        Handles.color = Color.red;
        Handles.DrawWireDisc(origin, Vector3.forward, distance);
    }
}
