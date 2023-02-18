using System.IO;
using System.Numerics;
using System.Runtime.Intrinsics;
using static System.Net.Mime.MediaTypeNames;
using System.Text;

namespace Lab_10_3sem_SHARP
{
    class program
    {
        public class Node 
        {
            public int data;
            public Node? left_branch;
            public Node? right_branch;
        };

        static void BuildTree(ref Node? root, in List<int> vec, in int input)
        {
            if (root == null && input - 1 < vec.Count)
            {
                root = new Node();
                root.data = vec[input - 1];
                root.left_branch = null;
                root.right_branch = null;

                BuildTree(ref root.left_branch, vec, input * 2);
                BuildTree(ref root.right_branch, vec, input * 2 + 1);
            }
        }

        static void InvertTree(ref Node? root)
        {
            if (root == null) return;
            else
            {
                Node? temp = root.left_branch;
                root.left_branch = root.right_branch;
                root.right_branch = temp;

                InvertTree(ref root.left_branch);
                InvertTree(ref root.right_branch);
            }
        }

        static void PrintTree(ref Node? root, in int level, ref string tree)
        {
            if (root == null) return;
            else
            {
                PrintTree(ref root.left_branch, level + 1, ref tree);
                if (root.left_branch != null)
                {
                    for (int i = 0; i < level; i++) tree += "   ";
                    tree += "  /\n";
                }
                for (int i = 0; i < level; i++) tree += "   ";
                tree += Convert.ToString(root.data) + "\n";
                if (root.right_branch != null)
                {
                    for (int i = 0; i < level; i++) tree += "   ";
                    tree += "  ";
                    tree += Convert.ToChar(92);
                    tree += "\n";
                }
                PrintTree(ref root.right_branch, level + 1, ref tree);
            }
        }

        static void Main()
        {
            FileStream outf = new FileStream(@"C:\Users\nikit\Desktop\BinaryTree.txt", FileMode.OpenOrCreate);

            Node? root = null;
            List<int> vec = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12 };

            BuildTree(ref root, vec, 1);

            string tree = "";
            PrintTree(ref root, 0, ref tree);
            Console.WriteLine(tree);
            byte[] buffer = Encoding.Default.GetBytes(tree);
            outf.Write(buffer, 0, buffer.Length);

            InvertTree(ref root);
            tree = "";
            PrintTree(ref root, 0, ref tree);
            Console.WriteLine(tree);
            buffer = Encoding.Default.GetBytes(tree);
            outf.Write(buffer, 0, buffer.Length);

            outf.Close();
        }
    }
}