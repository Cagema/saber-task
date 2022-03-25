using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace saber_task
{
    internal class ListRand
    {
        public ListNode Head;
        public ListNode Tail;
        public int Count;

        public void Serialize(FileStream s)
        {
            Serializer.Serialize(s, this);
        }

        public void Deserialize(FileStream s)
        {
            Deserializer.Deserialize(s, this);
        }
    }
}
