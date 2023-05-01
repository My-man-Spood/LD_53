using UnityEngine;
using UnityEngine.UI;

public class ShapeIndicator : MonoBehaviour
{
    public Image image;
    public int shapeIndex;

    void Start()
    {
        image.sprite = Asseter.Instance.GetShape(shapeIndex);
    }
}
