using ListAssist.Data;
using ListAssist.Data.Models;
using ListAssist.WebAPI.Models;

using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

using AutoMapper.QueryableExtensions;
using AutoMapper;

namespace ListAssist.WebAPI.Helpers
{
    public static class ListQueries
    {
        private static ListAssistContext db = new ListAssistContext();
                
        public static List<ShoppingList> GetLists()
        {
            return db.LALists.ProjectTo<ShoppingList>().ToList();
        }

        public static bool AddList(string listName)
        {
            var ShoppingList = new LAList();
            ShoppingList.Name = listName;

            db.LALists.Add(ShoppingList);
            db.SaveChanges();

            return true;
        }

        public static bool RemoveList(int ListId)
        {
            var ListToDelete = db.LALists.Find(ListId);
            var success = false;

            if (ListToDelete != null)
            {
                db.LALists.Remove(ListToDelete);
                db.SaveChanges();

                success = true;
            }

            return success;
        }

        public static ShoppingList GetList(int listId)
        {
            var entityList = db.LALists.Where(s => s.ID == listId);
            var shoppingList = entityList.ProjectTo<ShoppingList>().FirstOrDefault();

            return shoppingList;
        }

        public static bool AddItemToList(int listId, ShoppingListItem item)
        {
            var success = false;

            Mapper.CreateMap<ShoppingListItem, LAListItem>()
                .ForMember(e => e.Done, opt => opt.MapFrom(s => s.Checked));
            Mapper.CreateMap<ShoppingList, LAList>()
                .ForMember(e => e.LAListItems, opt => opt.MapFrom(s => s.ShoppingListItems));

            var shoppingList = GetList(listId);

            if (shoppingList != null)
            {
                shoppingList.ShoppingListItems.Add(item);
                
                db.LALists.Add(Mapper.Map<ShoppingList, LAList>(shoppingList));
                db.SaveChanges();

                success = true;
            }

            return success;
        }

        public static bool RemoveItemFromList(int listId, int itemId)
        {
            var updatedItem = db.LAListItems.Where(n => (n.ID == itemId) && (n.ListID == listId)).FirstOrDefault();
            var success = false;

            if(updatedItem != null)
            {
                updatedItem.Done = true;
                db.Entry(updatedItem).State = EntityState.Modified;
                db.SaveChanges();
                success = true;
            }

            return success;
        }
    }
}