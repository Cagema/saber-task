using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace saber_task
{
    internal struct NodeFromJson
    {
        public readonly string Next;
        public readonly string Prev;
        public readonly string Rand;
        public readonly ListNode Node;

        public NodeFromJson(ListNode node, string data, string next, string prev, string rand)
        {
            Next = next;
            Prev = prev;
            Rand = rand;
            node.Data = data;
            Node = node;
        }
    }

    internal class Deserializer
    {
        public static void Deserialize(FileStream s, ListRand list)
        {
            CheckFirstByte(s);

            List<NodeFromJson> nodes = new List<NodeFromJson>();
            while (true)
            {
                int currentByte = s.ReadByte();

                if (currentByte == '{')
                {
                    nodes.Add(ReadNodeFromStream(s));
                }
                else if (currentByte == ',')
                {
                    continue;
                }
                else if (currentByte == -1)
                {
                    throw new EndOfStreamException("Unexpected end of stream");
                }
                else if (currentByte == ']')
                {
                    break;
                }
                else
                {
                    throw new ArgumentException("Incorrected FileStream", nameof(s));
                }
            }

            PutTogetherList(list, nodes);
        }

        private static void PutTogetherList(ListRand list, List<NodeFromJson> nodes)
        {
            if (nodes == null || nodes.Count == 0)
            {
                return;
            }

            foreach (var item in nodes)
            {
                if (item.Next != "null")
                {
                    item.Node.Next = nodes[Convert.ToInt32(item.Next)].Node;
                }

                if (item.Prev != "null")
                {
                    item.Node.Prev = nodes[Convert.ToInt32(item.Prev)].Node;
                }

                if (item.Rand != "null")
                {
                    item.Node.Rand = nodes[Convert.ToInt32(item.Rand)].Node;
                }
            }

            list.Head = nodes.First().Node;
            list.Tail = nodes.Last().Node;
            list.Count = nodes.Count;
        }


        private static NodeFromJson ReadNodeFromStream(FileStream s)
        {
            ListNode node = new ListNode();
            Dictionary<string, string> fields = new Dictionary<string, string>();
            StringBuilder sb = new StringBuilder();
            string[] field = new string[2];

            int currentByte = s.ReadByte();
            while (currentByte != '}')
            {
                if (currentByte == -1)
                {
                    throw new EndOfStreamException("Unexpected end of stream");
                }

                if (currentByte != ',')
                {
                    sb.Append((char)currentByte);
                }
                else
                {
                    field = sb.ToString().Split('=', 2);
                    fields.Add(field[0], field[1]);
                    sb.Clear();
                }

                currentByte = s.ReadByte();
            }

            field = sb.ToString().Split('=', 2);
            fields.Add(field[0], field[1]);

            return new NodeFromJson(node, fields["Data"], fields["Next"], fields["Prev"], fields["Rand"]);
        }

        private static void CheckFirstByte(FileStream s)
        {
            int firstByte = s.ReadByte();
            if (firstByte == -1 || firstByte != '[')
            {
                throw new ArgumentException("Incorrect FileStream", nameof(s));
            }
        }
    }
}
