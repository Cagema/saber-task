using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace saber_task
{
    internal class ListNode
    {
        public ListNode Prev;
        public ListNode Next;
        public ListNode Rand; // произвольный элемент внутри списка
        public string Data;
    }
}
