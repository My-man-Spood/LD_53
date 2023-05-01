using EasyDragAndDrop.Core;
using UnityEngine;

public class Garbage : MonoBehaviour
{
    public void OnDrop(DragObj2D eventData)
    {
        var envelope = eventData.GetComponent<Envelope>();
        if (envelope.isValid)
        {
            print("IT WAS VALID T'ES BEN CAVE CRISS");
            GameObject.FindGameObjectWithTag(EnvelopeGenerator.Tag).GetComponent<EnvelopeGenerator>().Failure();
        }
        else
        {
            print("GOOD SHIT!");
            GameObject.FindGameObjectWithTag(EnvelopeGenerator.Tag).GetComponent<EnvelopeGenerator>().Success();
        }

        Destroy(eventData.gameObject);
    }
}
