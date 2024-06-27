using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RopeBasic : RopeBase
{
    protected void Awake()
    {
        candy = FindObjectOfType<Candy>();
        source = GetComponent<AudioSource>();
    }
    public override void GenerateRope()
    {
        Rigidbody2D previousRB = firstJointPoint;
        GameObject lastLink = null;

        for (int i = 0; i < linkCount; i++)
        {
            GameObject link = Instantiate(linkPrefab, transform);
            ropeLinks.Add(link);
            lastLink = link;
            HingeJoint2D joint = link.GetComponent<HingeJoint2D>();
            joint.connectedBody = previousRB;
            previousRB = link.GetComponent<Rigidbody2D>();
        }

        if (lastLink != null)
        {
            candy.ConnectToRope(lastLink.GetComponent<Rigidbody2D>());
            GenerateRopeVisual();
        }
    }

    public override void DestroyRope()
    {
        string defaultLayer = "Default";

        foreach (GameObject link in ropeLinks)
        {
            link.layer = LayerMask.NameToLayer(defaultLayer);
        }

        StartCoroutine(FadeOutLinks());
    }
    public override void RemoveLink(GameObject link)
    {
        if (ropeLinks.Contains(link))
        {
            ropeLinks.Remove(link);
            source.PlayOneShot(linkDestroyClip);
        }
    }
    protected override IEnumerator FadeOutLinks()
    {
        float t = 0;
        List<LineRenderer> renderers = new List<LineRenderer>();

        foreach (GameObject link in ropeLinks)
        {
            renderers.Add(link.GetComponent<LineRenderer>());
        }

        while (t < 1)
        {
            t += Time.deltaTime / linkFadeTime;
            yield return null;

            foreach (LineRenderer lineRenderer in renderers)
            {
                Color color = lineRenderer.startColor;
                color.a = 1f - t;
                lineRenderer.startColor = color;
                lineRenderer.endColor = color;
            }
        }

        foreach (GameObject link in ropeLinks)
        {
            Destroy(link);
        }
    }
    protected override void GenerateRopeVisual()
    {
        LineRenderer lineRenderer = ropeLinks[0].GetComponent<LineRenderer>();
        lineRenderer.positionCount = 2;
        lineRenderer.SetPosition(0, new Vector3(0, 1, 0));
        lineRenderer.SetPosition(1, Vector3.zero);

        for (int i = 1; i < ropeLinks.Count; i++)
        {
            lineRenderer = ropeLinks[i].GetComponent<LineRenderer>();
            lineRenderer.positionCount = 2;
            lineRenderer.SetPosition(0, new Vector3(0, 1, 0));
            lineRenderer.SetPosition(1, Vector3.zero);
        }
    }
}
