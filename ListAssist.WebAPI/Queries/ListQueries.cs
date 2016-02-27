using ListAssist.Data;
using ListAssist.Data.Models;
using ListAssist.WebAPI.Models;

using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System;

using AutoMapper.QueryableExtensions;
using AutoMapper;

namespace ListAssist.WebAPI.Queries
{
    public class ListQueries
    {
        private ListAssistContext db;

        public ListQueries(ListAssistContext db)
        {
            if(db == null)
            {
                throw new ArgumentNullException("Missing db");
            }

            this.db = db;
        }
                
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
        
        public bool AddList(string listName)
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

        public bool AddItemToList(int listId, ShoppingListItem item)
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
                        var newItem = Mapper.Map<ShoppingListItem, LAListItem>(item);

                        newItem.DateAdded = DateTime.Now;
                        newItem.TimesBought = 0;

                        shoppingList.LAListItems.Add(newItem);
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
                updatedItem.TimesBought++;
                updatedItem.Done = true;
                db.Entry(updatedItem).State = EntityState.Modified;
                db.SaveChanges();

                success = true;
            }

            return success;
        }
    }
}