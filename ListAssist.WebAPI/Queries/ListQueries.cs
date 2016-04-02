using ListAssist.Data;
using ListAssist.Data.Models;
using ListAssist.WebAPI.Models;

using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

using AutoMapper.QueryableExtensions;
using AutoMapper;

using System.Diagnostics;

namespace ListAssist.WebAPI.Queries
{
    public class ListQueries
    {
        private ListAssistContext db = new ListAssistContext();
                
        public List<ShoppingList> GetLists()
        {            
            return db.LALists.ProjectTo<ShoppingList>().ToList();
        }

        public ShoppingList GetList(int listId)
        {
            var entityList = db.LALists.Where(s => s.ID == listId);
            var shoppingList = entityList.ProjectTo<ShoppingList>().FirstOrDefault();

            return shoppingList;
        }
        
        public int AddList(string listName)
        {
            int id = 0;

            if (listName != null)
            {
                var duplicate = db.LALists.Where(e => e.Name.Equals(listName)).FirstOrDefault();

                if (duplicate == null)
                {
                    var ShoppingList = new LAList();
                    ShoppingList.Name = listName;

                    db.LALists.Add(ShoppingList);
                    db.SaveChanges();

                    id = ShoppingList.ID;
                }
            }

            return id;
        }

        public bool RemoveList(int ListId)
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

        public bool UpdateList(int listId, string newName)
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

        public bool AddItemToList(ShoppingListItem item)
        {
            Mapper.CreateMap<ShoppingListItem, LAListItem>()
                .ForMember(e => e.Done, opt => opt.MapFrom(s => s.Checked));

            var success = false;
            
            if (item != null)
            {
                var shoppingList = db.LALists.Find(item.ListId);

                if (shoppingList != null)
                {
                    var duplicate = db.LAListItems.Where(e => (e.ListID == item.ListId) && (e.Description.Equals(item.Description))).FirstOrDefault();

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

        public bool UpdateItemFromList(ShoppingListItem item)
        {
            Mapper.CreateMap<ShoppingListItem, LAListItem>()
                .ForMember(e => e.Done, opt => opt.MapFrom(s => s.Checked));

            var success = false;

            if (item != null)
            {
                var shoppingListItem = db.LAListItems.Find(item.Id);

                if (shoppingListItem != null)
                {
                    shoppingListItem.Description = item.Description;
                    shoppingListItem.Done = item.Checked;
                    db.Entry(shoppingListItem).State = EntityState.Modified;
                    db.SaveChanges();
                    success = true;
                }
            }

            return success;
        }

        public ShoppingListItem GetItemFromList(int listId, int itemId)
        {
            var entityList = db.LAListItems.Where(s => (s.ListID == listId & s.ID == itemId));
            var shoppingListItem = entityList.ProjectTo<ShoppingListItem>().FirstOrDefault();
                    
            return shoppingListItem;
        }

        public bool DeleteItemFromList(int itemId, int listId)
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

        public bool CheckOffItemFromList(int listId, int itemId)
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