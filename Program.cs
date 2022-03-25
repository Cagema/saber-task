using saber_task;

SerializeSample();
DeserializeSample();



void SerializeSample()
{
    ListRand list = new ListRand();
    ListNode head = new ListNode { Data = "headData" };
    ListNode secondNode = new ListNode { Data = "secondNode data", Prev = head };
    ListNode ThirdNode = new ListNode { Data = "thirdNode data", Prev = secondNode };
    ListNode FourthNode = new ListNode { Data = "fourthNode data", Prev = ThirdNode };
    ListNode tail = new ListNode { Data = "tailData", Prev = FourthNode };
    list.Head = head;
    list.Tail = tail;
    head.Next = secondNode;
    secondNode.Next = ThirdNode;
    ThirdNode.Next = FourthNode;
    FourthNode.Next = tail;
    secondNode.Rand = secondNode;
    FourthNode.Rand = head;
    ThirdNode.Rand = tail;
    using (FileStream fs = new FileStream("test.json", FileMode.Create))
    {
        list.Serialize(fs);
    }
}

void DeserializeSample()
{
    ListRand list = new ListRand();
    using (FileStream fs = new FileStream("test.json", FileMode.Open))
    {
        list.Deserialize(fs);
    }

    Console.WriteLine("Head data: " + list.Head.Data);
    Console.WriteLine("SecondNode data: " + list.Head.Next.Data);
    Console.WriteLine("ThirdNode rand: " + list.Head.Next.Next.Rand.Data);
}
