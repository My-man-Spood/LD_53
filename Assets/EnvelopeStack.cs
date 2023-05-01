using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnvelopeStack : MonoBehaviour
{
    public GameObject smallPrefab;
    public GameObject bigPrefab;
    public Transform target;

    List<int> envelopes = new List<int>();

    [ContextMenu("Add Envelope")]
    public void AddEnvelope(int envelope)
    {
        envelopes.Add(envelope);

        var place = envelopes.Sum();

        var envelopePrefab = envelope == 2 ? smallPrefab : bigPrefab;
        var xOffset = Random.Range(-5, 6);
        var position = target.position + new Vector3(xOffset, place, 0);
        var envelopeObject = Instantiate(envelopePrefab, position, Quaternion.identity, transform);
    }
}
