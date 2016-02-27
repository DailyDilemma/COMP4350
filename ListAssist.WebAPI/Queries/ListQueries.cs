using ListAssist.Data;
using ListAssist.Data.Models;
using ListAssist.WebAPI.Models;

using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

using AutoMapper.QueryableExtensions;
using AutoMapper;

namespace ListAssist.WebAPI.Queries
{
    public static class ListQueries
    {
        private static ListAssistContext db = new ListAssistContext();
                
        public static List<ShoppingList> GetLists()
        {
            return db.LALists.ProjectTo<ShoppingList>().ToList();
        }

        public static ShoppingList GetList(int listId)
        {
            var entityList = db.LALists.Where(s => s.ID == listId);
            var shoppingList = entityList.ProjectTo<ShoppingList>().FirstOrDefault();

            return shoppingList;
        }
        
        public static bool AddList(string listName)
        {
            bool success = false;

            if (listName != null)
            {
                var duplicate = db.LALists.Where(e => e.Name.Equals(listName));

                if (duplicate == null)
                {
                    var ShoppingList = new LAList();
                    ShoppingList.Name = listName;

                    db.LALists.Add(ShoppingList);
                    db.SaveChanges();

                    success = true;
                }
            }

            return success;
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

        public static bool UpdateList(int listId, string newName)
        {
            var success = false;

            if (newName != null)
            {
                var updatedList = db.LALists.Where(e => e.ID == listId).FirstOrDefault();

                if (updatedList != null)
                {
                    updatedList.Name = newName;
                    db.Entry(updatedList).State = EntityState.Modified;
                    db.SaveChanges();

                    success = true;
                }
            }

            return success;
        }

        public static bool AddItemToList(int listId, ShoppingListItem item)
        {
            Mapper.CreateMap<ShoppingListItem, LAListItem>()
                .ForMember(e => e.Done, opt => opt.MapFrom(s => s.Checked));

            var success = false;
            
            if (item != null)
            {
                var shoppingList = db.LALists.Find(listId);

                if (shoppingList != null)
                {
                    var duplicate = db.LAListItems.Where(e => (e.ListID == listId) && (e.Description.Equals(item.Description))).FirstOrDefault();

                    if (duplicate == null)
                    {
                        shoppingList.LAListItems.Add(Mapper.Map<ShoppingListItem, LAListItem>(item));
                        db.Entry(shoppingList).State = EntityState.Modified;
                        db.SaveChanges();
                    }
                    else
                    {
                        duplicate.Done = false;
                        db.Entry(duplicate).State = EntityState.Modified;
                        db.SaveChanges();
                    }

                    success = true;
                }
            }

            return success;
        }

        public static bool DeleteItemFromList(int itemId, int listId)
        {
            var success = false;
            var list = db.LALists.Find(listId);
            
            if(list != null)
            {
                var item = db.LAListItems.Find(itemId);
                
                if(item != null)
                {
                    db.LAListItems.Remove(item);
                    db.SaveChanges();
                    success = true;
                }
            }

            
            return success;
        }

        public static bool CheckOffItemFromList(int listId, int itemId)
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