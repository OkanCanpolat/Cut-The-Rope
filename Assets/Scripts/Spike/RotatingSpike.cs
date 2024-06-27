using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatingSpike : SpikeBase
{
    [SerializeField] private float rotationSpeed;
    [SerializeField] private bool negativeDirection;

    private void Start()
    {
        StartCoroutine(StartRotation());
    }

    private IEnumerator StartRotation()
    {
        float targetRotation = 360f;

        if (negativeDirection) targetRotation = -360f;

        while (true)
        {
            float t = 0;
            float startRot = transform.eulerAngles.z;
            float endRot = transform.eulerAngles.z + targetRotation;

            while (t < 1)
            {
                t += Time.deltaTime / rotationSpeed;
                float zRotation = Mathf.Lerp(startRot, endRot, t) % 360.0f;
                transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, zRotation);
                yield return null;
            }

            transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, endRot % 360.0f);

        }
    }
}
