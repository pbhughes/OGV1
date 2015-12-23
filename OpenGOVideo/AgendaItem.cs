using System;
using System.Collections.Generic;
using System.Text;

namespace OpenGOVideo
{
    public class AgendaItem
    {
        public int id { get; set; }
        public int parent_id { get; set; }
        public string title { get; set; }
        public string desc { get; set; }
        public Timestamp timestamp { get; set; }
        //public List<AgendaItem> subitems { get; set; }
    }
}
