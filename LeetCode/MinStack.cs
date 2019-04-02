using System;
namespace LeetCode
{

    /**
    * Design a stack that supports push, pop, top, and retrieving the minimum element
    * in constant time.
        push(x) -- Push element x onto stack.
        pop() -- Removes the element on top of the stack.
        top() -- Get the top element.
        getMin() -- Retrieve the minimum element in the stack.   
     */
    public class MinStack
    {

        Node node;
        Node top;

        /** initialize your data structure here. */
        public MinStack()
        {
            node = null;
        }

        public void Push(int x)
        {
            Node newNode = new Node(x);
            if (node == null)
                newNode.min = x;
            else
            {
                if (newNode.min > node.min)
                {
                    newNode.min = node.min;
                }
                else
                    newNode.min = x;
            }

            newNode.next = node;
            node = newNode;
        }

        public void Pop()
        {

            top = node.next;
            node.next = null;
            node = top;
        }

        public int Top()
        {
            if (node != null)
            {
                return node.val;
            }

            return -1;
        }

        public int GetMin()
        {
            return node.min;
        }
    }

    public class Node
    {
        public int min;
        public int val;
        public Node next;
        public Node(int x)
        {
            val = x;
            min = x;
            next = null;

        }
    }

}
