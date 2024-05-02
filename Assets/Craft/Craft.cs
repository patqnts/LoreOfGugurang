﻿using System;
using System.Linq;
using MoreMountains.InventoryEngine;
using UnityEngine;

namespace Craft
{
    [Serializable]
    public class Ingredient 
    {
        [HideInInspector]
        public string Name;
        public InventoryItem Item;
        public int Quantity;

      
        public override string ToString() { return (Quantity == 1 ? "" : Quantity + " ") + (Item == null ? "null" : Item.ItemName); }
    }

    [Serializable]
    public class Recipe : Ingredient
    {
        public Ingredient[] Ingredients;
        public string IngredientsText => string.Join(", ", Ingredients.Select(ingredient => ingredient.Name));
    }

    public static class Crafting
    {
        public static bool ContainsIngredientsForRecipe(this Inventory inventory, Recipe recipe)
        {
            return !recipe.Ingredients.Any(ingredient => inventory.InventoryContains(ingredient.Item.ItemID).Sum(index => inventory.Content[index].Quantity) < ingredient.Quantity);
        }
        
        public static void Craft(this Inventory inventory, Recipe recipe)
        {
            if (!inventory.ContainsIngredientsForRecipe(recipe)) return;
            foreach (var ingredient in recipe.Ingredients)
                inventory.RemoveItemByID(ingredient.Item.ItemID, ingredient.Quantity);
            if (inventory.AddItem(recipe.Item, recipe.Quantity))
            {
                MMInventoryEvent.Trigger(MMInventoryEventType.Pick, null, string.Empty, recipe.Item, recipe.Quantity, 0, "Player1");
                SinagScript.instance.PlaySound(1);
                return;
            }
            foreach (var ingredient in recipe.Ingredients)
                inventory.AddItem(ingredient.Item, ingredient.Quantity);
        }
    }
    
    [CreateAssetMenu]
    public class Craft : ScriptableObject
    {
        public Recipe[] Recipes;
    }
}
