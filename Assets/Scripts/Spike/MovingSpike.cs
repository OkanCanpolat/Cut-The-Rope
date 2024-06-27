using System.Collections;
using UnityEngine;
public class MovingSpike : SpikeBase
{
    [SerializeField] private Transform target;
    [SerializeField] private float travelTime;
    private Vector2 currentPos;
    private Vector2 targetPos;

    private void Start()
    {
        currentPos = transform.position;
        targetPos = target.position;
        StartCoroutine(StartMovement());
    }
    private IEnumerator StartMovement()
    {
        while (true)
        {
            float t = 0;

            while (t < 1)
            {
                yield return null;
                transform.position = Vector2.Lerp(currentPos, targetPos, t);
                t += Time.deltaTime / travelTime;
            }

            transform.position = targetPos;

            Vector3 temp = targetPos;
            targetPos = currentPos;
            currentPos = temp;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, target.position);
    }
}
