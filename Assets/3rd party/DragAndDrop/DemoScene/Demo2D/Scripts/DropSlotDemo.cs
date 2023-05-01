using EasyDragAndDrop.Core;
using UnityEngine.EventSystems;

namespace EasyDragAndDrop.DemoScene.Demo2D
{
    public class DropSlotDemo : DropSlot
    {
        public DataInfoDemo dataInfoDemo;
        public bool isOverrideOnDrop = true;
        public override void OnDrop(PointerEventData eventData)
        {
            base.OnDrop(eventData);
            if (!isOverrideOnDrop) return;
            var model = (DataInfoDemo) dropObj2D.m_ObjInfoComponent;
            dataInfoDemo.Initialize(model);
        }
    }
}