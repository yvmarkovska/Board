using Boarder.Loggers;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Boarder
{
    public static class Board
    {
        private static readonly List<BoardItem> items = new List<BoardItem>();

        public static void AddItem(BoardItem item)
        {
            if (items.Contains(item))
            {
                throw new InvalidOperationException("item already exists");
            }

            items.Add(item);
        }

        public static int TotalItems
        {
            get
            {
                if (!items.Any())
                {
                    throw new ArgumentNullException("No items");
                }
                return items.Count;
            }
        }

        public static void LogHistory(ILogger logger)
        {
            items.OrderBy(d => d.DueDate);

            foreach (var item in items)
            {
                logger.Log(item.ViewHistory());
            }
        }
    }
}
