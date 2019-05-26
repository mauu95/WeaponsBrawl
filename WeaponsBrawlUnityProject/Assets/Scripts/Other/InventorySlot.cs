using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class InventorySlot : MonoBehaviour
{
    public Image icon;

    public AbstractWeaponGeneric item;

    public void AddItem(AbstractWeaponGeneric newItem)
    {
        item = newItem;
        icon.sprite = item.info.icon;
        icon.enabled = true;
    }

    public void SwitchWeapon()
    {
        PlayerWeaponManager_Inventory inventory = GetGameObjectInRoot("Canvas").GetComponent<InventoryUI>().Player.GetComponent<PlayerWeaponManager_Inventory>();
        int wid = inventory.Weapons.FindIndex(a => a == item);
        inventory.CmdSwitchWeapon(wid);
    }


















    private GameObject GetGameObjectInRoot(string objname)
    {
        GameObject[] root = SceneManager.GetActiveScene().GetRootGameObjects();
        foreach (GameObject obj in root)
            if (obj.name == objname)
                return obj;
        return null;
    }
}