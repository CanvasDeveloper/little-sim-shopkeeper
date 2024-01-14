using CanvasDEV.Runtime.Systems.UI.Core;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UISlotBase : ButtonBase, IDropHandler
{
    public event Action<UISlotBase> OnClick;

    [SerializeField] private DragItem dragItem;
    [SerializeField] private Image itemBackground;

    [SerializeField] private TextMeshProUGUI amountGUI;

    private InventoryItemData _currentItem;

    public virtual void UpdateItem(InventoryItemData item)
    {
        amountGUI.text = item.stack.ToString();
        dragItem.UpdateItem(item);
    }

    protected override void ButtonBehaviour()
    {
        OnClick?.Invoke(this);
    }

    public void OnDrop(PointerEventData eventData)
    {
        if(eventData.pointerDrag != null)
        {
            eventData.position = GetComponent<RectTransform>().anchoredPosition;
        }
    }

    public InventoryItemData GetItem()
    {
        return _currentItem;
    }
}
