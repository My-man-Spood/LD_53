using EasyDragAndDrop.Core;
using UnityEngine;

namespace EasyDragAndDrop.DemoScene.Demo2D
{
    public class DragAndDropEventDemo : MonoBehaviour
    {
        public void ShowDragInfo(DragObj2D dragObj2D)
        {
            Debug.Log("ObjName:" + dragObj2D.name + " | Status:" + dragObj2D.dragState);
        }

        public void ShowDropInfo(DragObj2D dragObj2D)
        {
            Debug.Log(dragObj2D.name + " Drop");
        }
    }
}