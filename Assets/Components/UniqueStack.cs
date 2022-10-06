using System.Collections;
using System.Collections.Generic;

/*
    Add node to unique stack:
    typeStack.AddNode(value);

    Remove node from unique stack
    typeStack.removeNode(value);

    Return value from top of the stack:
    typeStack.returnFirstNodeValue();
*/


public class UniqueStack<T>
{
    public Node<T> firstNode;

    public void AddNode(T value)
    {
        RemoveNode(value);
        if (firstNode == null)
        {
            firstNode = new Node<T>();
        }
        else
        {
            Node<T> newNode = new Node<T>();
            firstNode.prevNode = newNode;
            newNode.nextNode = firstNode;
            firstNode = newNode;
        }
        firstNode.value = value;
    }

    public void RemoveNode(T value)
    {

        Node<T> currentNode = firstNode;
        while (currentNode != null)
        {
            if (EqualityComparer<T>.Default.Equals(currentNode.value, value))
            {
                if (currentNode.prevNode != null)
                    currentNode.prevNode.nextNode = currentNode.nextNode == null ? null : currentNode.nextNode;
                else
                    firstNode = currentNode.nextNode;

                if (currentNode.nextNode != null)
                    currentNode.nextNode.prevNode = currentNode.prevNode == null ? null : currentNode.prevNode;
                currentNode = null;
            }
            if (currentNode != null)
                currentNode = currentNode.nextNode;
        }
    }

    public T returnFirstNodeValue()
    {
        if (firstNode != null)
            return firstNode.value;
        else
            return default(T);
    }
}

public class Node<T>
{
    public Node<T> nextNode;
    public Node<T> prevNode;
    public T value;
}