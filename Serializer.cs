using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace saber_task
{
    internal class Serializer
    {
        public static void Serialize(FileStream s, ListRand list)
        {
            if (list == null)
            {
                throw new ArgumentNullException(nameof(list));
            }

            Dictionary<ListNode, int> nodesWithIndexes = GenerateNodesWithIndexes(list);
            string jsonString = JsonFormatter(list, nodesWithIndexes);
            byte[] bytes = Encoding.UTF8.GetBytes(jsonString);
            s.Write(bytes, 0, bytes.Length);
        }

        private static string JsonFormatter(ListRand list, Dictionary<ListNode, int> nodesWithIndexes)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append('[');
            ListNode node = list.Head;
            while (node != null)
            {
                WriteNode(sb, node, nodesWithIndexes);

                node = node.Next;
                if(node == null)
                {
                    break;
                }
                else
                {
                    sb.Append(',');
                }
            }

            sb.Append(']');
            return sb.ToString();
        }

        private static void WriteNode(StringBuilder sb, ListNode node, Dictionary<ListNode, int> nodesWithIndexes)
        {
            string next = "null";
            string prev = "null";
            string rand = "null";

            if (node.Next != null)
            {
                next = nodesWithIndexes[node.Next].ToString();
            }

            if (node.Prev != null)
            {
                prev = nodesWithIndexes[node.Prev].ToString();
            }

            if (node.Rand != null)
            {
                rand = nodesWithIndexes[node.Rand].ToString();
            }

            sb.Append('{');
            sb.Append($"Data={node.Data},");
            sb.Append($"Next={next},");
            sb.Append($"Prev={prev},");
            sb.Append($"Rand={rand}");
            sb.Append('}');
        }

        private static Dictionary<ListNode, int> GenerateNodesWithIndexes(ListRand list)
        {
            Dictionary<ListNode, int> nodesWithIndexes = new Dictionary<ListNode, int>();
            ListNode node = list.Head;
            int index = 0;
            while (node != null)
            {
                nodesWithIndexes.Add(node, index++);
                node = node.Next;
            }

            return nodesWithIndexes;
        }
    }
}
