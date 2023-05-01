using UnityEngine;
using UnityEngine.UI;

namespace EasyDragAndDrop.DemoScene.Demo2D
{
    public class DataInfoDemo : MonoBehaviour
    {
        public Text Info;
        public Image Image;

        public void Initialize(DataInfoDemo demo)
        {
            Info.text = demo.Info.text;
            Image.sprite = demo.Image.sprite;
            Image.color = demo.Image.color;
            Image.gameObject.SetActive(false);
            Image.gameObject.SetActive(true);
        }
    }
}