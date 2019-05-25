using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    public Image icon;

    AbstractWeaponGeneric item;

    public void AddItem(AbstractWeaponGeneric newItem)
    {
        item = newItem;
        icon.sprite = item.info.icon;
        icon.enabled = true;
    }



}