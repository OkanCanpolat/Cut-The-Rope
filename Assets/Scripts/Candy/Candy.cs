using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class Candy : MonoBehaviour
{
    [SerializeField] private GameObject destroyObject;
    private SignalBus signalBus;
    private List<ICandyAttachment> attachments = new List<ICandyAttachment>();

    [Inject]
    public void Construct(SignalBus signalBus)
    {
        this.signalBus = signalBus;
    }
    public void ConnectToRope(Rigidbody2D connectBody)
    {
        HingeJoint2D joint = gameObject.AddComponent<HingeJoint2D>();
        joint.autoConfigureConnectedAnchor = false;
        joint.connectedBody = connectBody;
        joint.connectedAnchor = Vector2.zero;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        IInteractable interactable = collision.GetComponent<IInteractable>();

        if(interactable != null)
        {
            interactable.OnInteract();
        }
    }
    public ICandyAttachment TryGetAttachment(Type type)
    {
        foreach(ICandyAttachment attachment in attachments)
        {
            if(attachment.GetType() == type)
            {
                return attachment;
            }
        }

        return null;
    }
    public void AddAttachment(ICandyAttachment attachment)
    {
        attachments.Add(attachment);
        attachment.OnAttach();
    }
    public void RemoveAttachment(ICandyAttachment attachment)
    {
        attachments.Remove(attachment);
    }
    public void Destroy()
    {
        Instantiate(destroyObject, transform.position, Quaternion.identity);
        gameObject.SetActive(false);
        signalBus.TryFire<LevelFailedSignal>();
        signalBus.TryFire<CandyDestroyedSignal>();
    }
}
