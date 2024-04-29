using MoreMountains.InventoryEngine;
using PixelCrushers.DialogueSystem;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class InventoryWeaponCheck : MonoBehaviour
{
    public Inventory weaponInventory;
    public Inventory mainInventory;
    public PlayerController playerController;
    void OnEnable()
    {
        Lua.RegisterFunction("IsWeaponEquipped_" + name, this, SymbolExtensions.GetMethodInfo(() => IsWeaponEquipped(name)));
    }

    void OnDisable()
    {
        Lua.UnregisterFunction("IsWeaponEquipped");
    }

    public void InputNameForItem(string itemID)
    {
        IsItemExisting(itemID);
    }
    public bool IsItemExisting(string itemID)
    {
        List<int> myList = mainInventory.InventoryContains(itemID);

        if (myList.Count > 0)
        {
            Debug.Log("EXISTS");
            DialogueLua.SetVariable($"Is{itemID}Exists", true);
            return true;

        }

        Debug.Log("NOT EXISTS");
        DialogueLua.SetVariable($"Is{itemID}Exists", false);
        return false;
    }

    public void IsAntiRamoEnough()
    {
        if(mainInventory.GetQuantity("Anti-ramoPotion") >= 3)
        {
            DialogueLua.SetVariable($"CanPassTo3", true);
        }
        else
        {
            DialogueLua.SetVariable($"CanPassTo3", false);
        }
    }

    public bool IsWeaponEquipped(string weaponID)
    {

        List<int> myList = weaponInventory.InventoryContains(weaponID);

        if (myList.Count > 0)
        {
            Debug.Log("EXISTS");
            DialogueLua.SetVariable("IsWeaponEquipped", true);
            return true;

        }
        Debug.Log("NOT EXISTS");
        DialogueLua.SetVariable("IsWeaponEquipped", false);
        return false;
    }

    public void UseItemForDialogue(string weaponID)
    {
        List<int> myList = weaponInventory.InventoryContains(weaponID);

        if (myList.Count > 0)
        {
            MMInventoryEvent.Trigger(MMInventoryEventType.UseRequest, null, "RogueWeaponInventory", weaponInventory.Content[myList[0]], 1, 0, "Player1");
            Debug.Log(weaponInventory.PlayerID);

            playerController.SaveInventory();
            playerController.LoadInventory();
        }
    }

    public void UseMainItemForDialogue(string itemID)
    {
        List<int> myList = mainInventory.InventoryContains(itemID);

        if (myList.Count > 0)
        {
            MMInventoryEvent.Trigger(MMInventoryEventType.UseRequest, null, "RogueMainInventory", mainInventory.Content[myList[0]], 1, 0, "Player1");

            playerController.SaveInventory();
            playerController.LoadInventory();
        }
    }

  
}
