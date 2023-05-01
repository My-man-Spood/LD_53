using UnityEngine;
using UnityEngine.UI;

public class LabelUi : MonoBehaviour
{
    public Image shapeImage;
    public Image dotsImage;

    public void Init(int shapeIndex, int ColorIndex, int dots)
    {
        if (dots > 0)
        {
            dotsImage.sprite = Asseter.Instance.GetDots(dots);
            dotsImage.color = new Color(0.09f, 0.12f, 0.15f);
        }
        else
        {
            dotsImage.color = new Color(0, 0, 0, 0);
        }

        shapeImage.sprite = Asseter.Instance.GetShape(shapeIndex);
        shapeImage.color = Asseter.Instance.GetColor(ColorIndex);
    }
}
