using System;
using System.Collections.Generic;

class Node
{
    public int Value;
    public Node Left;
    public Node Right;

    public Node(int value)
    {
        Value = value;
        Left = null;
        Right = null;
    }
}

class BinarySearchTree
{
    public Node Root;

    public void Insert(int value)
    {
        Root = InsertRec(Root, value);
    }

    private Node InsertRec(Node root, int value)
    {
        if (root == null) return new Node(value);

        if (value < root.Value)
            root.Left = InsertRec(root.Left, value);
        else if (value > root.Value)
            root.Right = InsertRec(root.Right, value);

        return root;
    }

    public void InOrder(Node node)
    {
        if (node != null)
        {
            InOrder(node.Left);
            Console.Write(node.Value + " ");
            InOrder(node.Right);
        }
    }

    public Node Find(int value)
    {
        Node current = Root;
        while (current != null)
        {
            if (current.Value == value) return current;
            current = value < current.Value ? current.Left : current.Right;
        }
        return null;
    }

    public void LevelOrder()
    {
        if (Root == null) return;

        Queue<Node> queue = new Queue<Node>();
        queue.Enqueue(Root);

        while (queue.Count > 0)
        {
            Node current = queue.Dequeue();
            Console.Write(current.Value + " ");

            if (current.Left != null) queue.Enqueue(current.Left);
            if (current.Right != null) queue.Enqueue(current.Right);
        }
    }

    public void Delete(int value)
    {
        Root = DeleteRec(Root, value);
    }

    private Node DeleteRec(Node root, int value)
    {
        if (root == null) return null;

        if (value < root.Value)
            root.Left = DeleteRec(root.Left, value);
        else if (value > root.Value)
            root.Right = DeleteRec(root.Right, value);
        else
        {
            if (root.Left == null) return root.Right;
            if (root.Right == null) return root.Left;

            Node temp = FindMin(root.Right);
            root.Value = temp.Value;
            root.Right = DeleteRec(root.Right, temp.Value);
        }
        return root;
    }

    private Node FindMin(Node node)
    {
        while (node.Left != null) node = node.Left;
        return node;
    }
}

class Program
{
    static void Main()
    {
        BinarySearchTree bst = new BinarySearchTree();
        bool continueMenu = true;

        while (continueMenu)
        {
            Console.WriteLine("\nMenu:");
            Console.WriteLine("1. Insert");
            Console.WriteLine("2. Display In-Order");
            Console.WriteLine("3. Search");
            Console.WriteLine("4. Display Level Order");
            Console.WriteLine("5. Delete");
            Console.WriteLine("6. Exit");
            Console.Write("Select an option: ");

            string option = Console.ReadLine();

            switch (option)
            {
                case "1":
                    Console.Write("Enter value to insert: ");
                    if (int.TryParse(Console.ReadLine(), out int insertVal))
                        bst.Insert(insertVal);
                    else
                        Console.WriteLine("Invalid input. Please enter a number.");
                    break;

                case "2":
                    Console.WriteLine("In-Order: ");
                    bst.InOrder(bst.Root);
                    Console.WriteLine();
                    break;

                case "3":
                    Console.Write("Enter value to search: ");
                    if (int.TryParse(Console.ReadLine(), out int searchVal))
                    {
                        var foundNode = bst.Find(searchVal);
                        Console.WriteLine(foundNode != null ? $"Value found: {foundNode.Value}" : "Value not found.");
                    }
                    else
                    {
                        Console.WriteLine("Invalid input. Please enter a number.");
                    }
                    break;

                case "4":
                    Console.WriteLine("Level Order:");
                    bst.LevelOrder();
                    Console.WriteLine();
                    break;

                case "5":
                    Console.Write("Enter value to delete: ");
                    if (int.TryParse(Console.ReadLine(), out int deleteVal))
                        bst.Delete(deleteVal);
                    else
                        Console.WriteLine("Invalid input. Please enter a number.");
                    break;

                case "6":
                    continueMenu = false;
                    break;

                default:
                    Console.WriteLine("Invalid option, please try again.");
                    break;
            }
        }
    }
}


