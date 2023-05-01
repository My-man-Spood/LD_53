using EasyDragAndDrop.Core;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Scale : MonoBehaviour
{
    public static string Tag = "Scale";

    public Sprite scaleup;
    public Sprite scaledown;
    public Image scaleImage;
    public TextMeshProUGUI weightText;

    public void OnDrop(DragObj2D eventData)
    {
        scaleImage.sprite = scaledown;
        var envelope = eventData.GetComponent<Envelope>();
        weightText.text = $"{envelope.weight:0.00}";
    }

    public void Reset()
    {
        scaleImage.sprite = scaleup;
        weightText.text = $"{0:0.00}";
    }
}
