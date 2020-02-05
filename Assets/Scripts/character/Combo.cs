//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;


//public class Combo : MonoBehaviour
//{
//    public int front;
//    public int rear;
//    private Queue<Timer> queue = new Queue<Timer>(4);
//    void Update()
//    {
//        if (Input.GetKeyDown("h"))
//        {
//            if (queue.Count == 4)
//            {
//                queue.Dequeue();
//                queue.Enqueue(new Timer(3, "H"));
//            }
//            else
//            {
//                queue.Enqueue(new Timer(3, "H"));
//            }

//            ComboChecker();
//        }

//        if (Input.GetKeyDown("l"))
//        {
//            if (queue.Count == 4)
//            {
//                queue.Dequeue();
//                queue.Enqueue(new Timer(3, "L"));
//            }
//            else
//            {
//                queue.Enqueue(new Timer(3, "L"));
//            }

//            ComboChecker();
//        }

//        if (Input.GetKeyDown("p"))
//        {
//            Debug.Log("QUEUE COUNT");
//            Debug.Log(queue.Count);
//            foreach (Timer timer in queue)
//            {
//                Debug.Log(timer.name);
//            }
//        }

//        if (queue.Count > 0)
//        {
//            if (queue.Peek().timeUp)
//            {
//                queue.Dequeue();
//            }
//        }

//    }

//    private void ComboChecker()
//    {
//        string contents = null;
//        foreach (Timer timer in queue)
//        {
//            contents += timer.name;
//        }

//        if (contents == "HHHH")
//        {
//            Debug.Log("ALL H COMBO");
//            queue.Clear();
//        }
//        else if (contents == "LLLL")
//        {
//            Debug.Log("ALL L COMBO");
//            queue.Clear();
//        }
//    }
//}
