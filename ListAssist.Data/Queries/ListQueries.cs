using ListAssist.Data.Models;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace ListAssist.Data.Queries
{
    public static class ListQueries
    {
        private static ListAssistContext db = new ListAssistContext();

        public static List<LAList> GetLists()
        {
            return db.LALists.OrderBy(e => e.Name).ToList();
        }

        public static LAList GetList(int id)
        {
            return db.LALists.Find(id);
        }

        public static bool AddList(LAList list)
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

        public static bool RemoveList(int id)
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

        public static bool UpdateList(LAList list)
        {
            bool success = false;
            var dbList = db.LALists.Find(list.ID);

            if (list != null)
            {
                if (list.Name != null && list.LAListItems != null)
                {
                    db.Entry(dbList).CurrentValues.SetValues(list);
                    db.Entry(dbList).State = EntityState.Modified;                    

                    foreach (var listItem in list.LAListItems)
                    {
                        var dbListItem = db.LAListItems.Find(listItem.ID);
                        db.Entry(dbListItem).CurrentValues.SetValues(listItem);
                        db.Entry(dbListItem).State = EntityState.Modified;
                    }

                    db.SaveChanges();

                    success = true;
                }
            }

            return success;
        }

        public static bool AddItemToList(LAListItem item)
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
                        db.LAListItems.Add(item);
                    }
                    //else if (duplicate.Done)
                    //{
                    //    duplicate.Done = false;
                    //    db.Entry(duplicate).State = EntityState.Modified;
                    //}

                    db.SaveChanges();

                    success = true;
                }
            }

            return success;
        }

        public static int DeleteItemFromList(int id)
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
    }
}
