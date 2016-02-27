using ListAssist.Data.Models;

using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System;

namespace ListAssist.Data.Queries
{
    public class ListQueries
    {
        private ListAssistContext db;

        public ListQueries(ListAssistContext db)
        {
            if(db == null)
            {
                throw new ArgumentNullException("Missing db.");
            }

            this.db = db;
        }

        public List<LAList> GetLists()
        {
            return db.LALists.OrderBy(e => e.Name).ToList();
        }

        public LAList GetList(int id)
        {
            return db.LALists.Find(id);
        }

        public bool AddList(LAList list)
        {
            bool success = false;

            if (list.Name != null)
            {
                var duplicate = db.LALists.Where(e => e.Name.Equals(list.Name)).FirstOrDefault();

                if (duplicate == null)
                {
                    db.LALists.Add(list);
                    db.SaveChanges();

                    success = true;
                }
            }

            return success;
        }

        public bool RemoveList(int id)
        {
            bool success = false;
            var list = db.LALists.Find(id);

            if(list != null)
            {
                db.LALists.Remove(list);
                db.SaveChanges();

                success = true;
            }

            return success;
        }

        public bool UpdateList(LAList list)
        {
            bool success = false;

            if (list != null)
            {
                if (list.Name != null && list.LAListItems != null)
                {
                    db.Entry(list).State = EntityState.Modified;
                    db.LALists.Attach(list);

                    foreach (var listItem in list.LAListItems)
                    {
                        db.Entry(listItem).State = EntityState.Modified;
                    }

                    db.SaveChanges();

                    success = true;
                }
            }

            return success;
        }

        public bool AddItemToList(LAListItem item)
        {
            var success = false;

            if (item != null)
            {
                var list = db.LALists.Find(item.ListID);

                if (list != null)
                {
                    var duplicate = db.LAListItems.Where(e => (e.ListID == item.ListID) && (e.Description.Equals(item.Description))).FirstOrDefault();

                    if (duplicate == null)
                    {
                        item.TimesBought = 0;
                        item.DateAdded = DateTime.Now;
                        db.LAListItems.Add(item);
                    }
                    else if (duplicate.Done)
                    {
                        duplicate.Done = false;
                        db.Entry(duplicate).State = EntityState.Modified;
                    }

                    db.SaveChanges();

                    success = true;
                }
            }

            return success;
        }

        public int DeleteItemFromList(int id)
        {
            int listId = -1;
            var item = db.LAListItems.Find(id);

            if (item != null)
            {
                listId = item.ListID;

                db.LAListItems.Remove(item);
                db.SaveChanges();
            }

            return listId;
        }

        public LAListItem GetItem(int id)
        {
            var item = db.LAListItems.Find(id);

            return item;
        }
    }
}
