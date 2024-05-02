using UnityEngine;
using System.Collections;
using MoreMountains.Tools;
using System;

namespace MoreMountains.InventoryEngine
{	
	[CreateAssetMenu(fileName = "BaseItem", menuName = "MoreMountains/InventoryEngine/BaseItem", order = 0)]
	[Serializable]
	/// <summary>
	/// Base item class, to use when your object doesn't do anything special
	/// </summary>
	public class BaseItem : InventoryItem 
	{
        [Header("Health Bonus")]
        /// the amount of health to add to the player when the item is used
        public int HealthBonus;

        /// <summary>
        /// What happens when the object is used 
        /// </summary>
        public override bool Use(string playerID)
        {
            base.Use("Player1");
            // This is where you would increase your character's health,
            // with something like : 
            // Player.Life += HealthValue;
            // of course this all depends on your game codebase.
            if(SinagScript.instance.Health < SinagScript.instance.MaxHealth)
            {
                SinagScript.instance.Health += HealthBonus;
                SinagScript.instance.SetHealth();
            }            
            Debug.LogFormat("increase character " + playerID + "'s health by " + HealthBonus);
            return true;
        }
    }
}