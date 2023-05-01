using UnityEngine;
using UnityEngine.UI;

public class ColorIndicator : MonoBehaviour
{
    public Image image;
    public int colorIndex;

    void Start()
    {
        image.color = Asseter.Instance.GetColor(colorIndex);
    }

}
