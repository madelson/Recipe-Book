using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RecipeBook.Domain
{
    public interface IRecipeBook
    {
        string SavePath { get; set; }

        IEnumerable<Recipe> Recipes { get; }
        IEnumerable<Item> Items { get; }
        IEnumerable<ShoppingListItem> ShoppingList { get; }

        T Get<T>(Guid id) where T : DomainObject;
        IEnumerable<T> GetAll<T>() where T : DomainObject;
        void Add<T>(T domainObject) where T : DomainObject;
        void Remove(Guid id);
        
        void Save();
    }

    public static class RecipeBookExtensions
    {
        public static void Remove(this IRecipeBook recipeBook, DomainObject domainObject)
        {
            recipeBook.Remove(domainObject.Id);
        }

        public static bool IsInShoppingList(this IRecipeBook recipeBook, Recipe recipe)
        {
            return recipeBook.ShoppingList.Any(sli => sli.RecipeId == recipe.Id);
        }

        public static bool IsInShoppingList(this IRecipeBook recipeBook, Item item, bool excludeIfInRecipe = false)
        {
            return recipeBook.ShoppingList.Any(sli => sli.ItemId == item.Id && (!excludeIfInRecipe || sli.RecipeId == null));
        }

        public static void RemoveFromShoppingList(this IRecipeBook recipeBook, Recipe recipe)
        {
            recipeBook.ShoppingList
                .Where(sli => sli.RecipeId == recipe.Id)
                .ToArray()
                .ForEach(sli => recipeBook.Remove(sli));
        }

        public static void RemoveFromShoppingList(this IRecipeBook recipeBook, Item item, bool excludeIfInRecipe = false)
        {
            recipeBook.ShoppingList
                .Where(sli => sli.ItemId == item.Id && (!excludeIfInRecipe || sli.RecipeId == null))
                .ToArray()
                .ForEach(sli => recipeBook.Remove(sli));
        }
    }
}
