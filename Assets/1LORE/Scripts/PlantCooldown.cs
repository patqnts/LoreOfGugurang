using MoreMountains.InventoryEngine;
using System.Collections;
using UnityEngine;

public class PlantCooldown : MonoBehaviour
{
    public float cooldownDuration = 5f; // Duration of the cooldown in seconds
    private bool isCoolingDown = false; // Flag to track if the plant is currently cooling down
    public GameObject plantObj;
    public InventoryItem item;

    // Start is called before the first frame update
    void Start()
    {
        // At the start, the plant is not cooling down, so it should be active
        gameObject.SetActive(true);
    }

    public void PickItem()
    {
        MMInventoryEvent.Trigger(MMInventoryEventType.Pick, null, "RogueMainInventory", item, 1, 0, "Player1");
    }
    // Method to start the cooldown
    public void StartCooldown()
    {
        if (!isCoolingDown)
        {
            StartCoroutine(CooldownCoroutine());
        }
    }

    // Coroutine to handle the cooldown
    private IEnumerator CooldownCoroutine()
    {
        // Set the flag to indicate that the cooldown is in progress
        isCoolingDown = true;

        // Disable the plant game object
        plantObj.SetActive(false);

        // Wait for the specified cooldown duration
        yield return new WaitForSeconds(cooldownDuration);

        // Enable the plant game object after cooldown is finished
        plantObj.SetActive(true);

        // Reset the cooldown flag
        isCoolingDown = false;
    }
}
