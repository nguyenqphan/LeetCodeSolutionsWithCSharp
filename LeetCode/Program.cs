using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace LeetCode
{
    class Program
    {
        static void Main(string[] args)
        {

       
        }

        //323. Number of connected components in a grapth - medium
        public static int CountComponents(int n, int[][] edges)
        {
            List<List<int>> adjacencies = new List<List<int>>();

            for (int i = 0; i < n; i++)
                adjacencies.Add(new List<int>());

            foreach (var e in edges)
            {
                adjacencies[e[0]].Add(e[1]);
                adjacencies[e[1]].Add(e[0]);
            }

            Stack<int> st = new Stack<int>();
            HashSet<int> seen = new HashSet<int>();

            int count = 0;
            for (int i = 0; i < n; i++)
            {
                if (!seen.Contains(i))
                {
                    st.Push(i);
                    count++;
                    seen.Add(i);
                    while (st.Count > 0)
                    {
                        int curr = st.Pop();
                        for (int j = 0; j < adjacencies[curr].Count; j++)
                        {
                            int connected = adjacencies[curr][j];
                            if (!seen.Contains(connected))
                            {
                                st.Push(connected);
                                seen.Add(connected);
                            }
                        }
                    }
                }
            }

            return count;
        }

        //261. GRAPH VALID TREE - MEDIUM
        public static bool ValidTree(int n, int[][] edges)
        {
            //A valid tree if number of edges == n - 1 and all nodes are visited after BDS or DFS. 
            if (edges.Length != n - 1)
                return false;

            List<List<int>> adjacencies = new List<List<int>>();

            for (int i = 0; i < n; i++)
                adjacencies.Add(new List<int>());

            foreach (var edge in edges)
            {
                adjacencies[edge[0]].Add(edge[1]);
                adjacencies[edge[1]].Add(edge[0]);
            }

            Stack<int> st = new Stack<int>();
            HashSet<int> seen = new HashSet<int>();
            st.Push(0);
            seen.Add(0);

            while (st.Count > 0)
            {
                int curr = st.Pop();
                foreach (var item in adjacencies[curr])
                {
                    if (!seen.Contains(item))
                    {
                        st.Push(item);
                        seen.Add(item);
                    }
                }
            }

            return seen.Count == n;
        }

        //HAMMING DISTANCE
        //Given a hamming distance between two integer numbers is the number of positions at which corresponding bits are different.
        //Given two integer x and y, calculate the hamming distance
        public static int HammingDistance(int x, int y)
        {

            int count = 0;
            while (x != 0 || y != 0)
            {
                int temp = x ^ y;
                count += temp % 2 != 0 ? 1 : 0;
                x = x >> 1;
                y = y >> 1;
            }

            return count;
        }

        //REPEATEDSUBSTRINGPATTERN
        //Given a non-empty string s, check if it can be constructed by taking a substring of it and append multiple copies of
        //the substring together. You may assume the string contain the lowercase English only and its length will not exceed 1000.
        public static bool RepeatedSubstringPattern(string s)
        {

            for (int l = 1; l <= s.Length / 2; l++)
            {
                if (RepeatedSubStringPatternHelper(s, l))
                {
                    return true;
                }
            }

            return false;
        }

        public static bool RepeatedSubStringPatternHelper(string s, int length)
        {
            if ((s.Length) % length != 0)
            {
                return false;
            }

            for (int i = 0; i < length; i++)
            {
                int j = i + length;
                while (j < s.Length)
                {
                    if (s[i] != s[j])
                    {
                        return false;
                    }

                    j += length;
                }

            }

            return true;
        }

        //LONGEST PALINDROMIC SUBSTRING - MEDIUM
        //Given a string s, find the longest palindromic substring in s.You may assume that the maximum length of s is 1000.
        public static string LongestPalindrome(string s)
        {
            if (s == null || s.Length < 1)
                return "";

            int start = 0;
            int max = 0;

            for (int i = 0; i < s.Length; i++)
            {
                int len1 = PalindromeLength(s, i, i);
                int len2 = PalindromeLength(s, i, i + 1);
                int temp = Math.Max(len1, len2);

                if (temp > max)
                {
                    max = temp;
                    start = i - (max - 1) / 2;
                }

            }

            return s.Substring(start, max);
        }

        public static int PalindromeLength(string s, int left, int right)
        {
            while (left >= 0 && right < s.Length && s[left] == s[right])
            {
                left--;
                right++;
            }

            return right - left - 1;
        }

        //CONSTRUCT FROM BINARY TREE FROM INORDER AND POSTORDER TRAVERSAL
        public static TreeNode BuildTreeInorderPostorder(int[] inorder, int[] postorder)
        {
            return BuildTreeHelperInorderPostOrder(0, inorder.Length - 1, postorder.Length - 1, inorder, postorder);
        }

        public static TreeNode BuildTreeHelperInorderPostOrder(int inEnd, int inStart, int postStart, int[] inorder, int[] postorder)
        {
            if (postStart < 0 || inStart < inEnd)
                return null;

            TreeNode node = new TreeNode(postorder[postStart]);

            int rIndex = 0;

            for (int i = inStart; i >= inEnd; i--)
            {
                if (postorder[postStart] == inorder[i])
                {
                    rIndex = i;
                    break;
                }
            }

            node.right = BuildTreeHelperInorderPostOrder(rIndex + 1, inStart, postStart - 1, inorder, postorder);
            node.left = BuildTreeHelperInorderPostOrder(inEnd, rIndex - 1, postStart - (inStart - rIndex) - 1, inorder, postorder);

            return node;
        }

        //CONSTRUCT A TREE FROM PREORDER AND INORDER TRAVERSAL
        public static TreeNode BuildTree(int[] preorder, int[] inorder)
        {
            return BuildTreeHelper(0, preorder.Length - 1, 0, preorder, inorder);
        }

        public static TreeNode  BuildTreeHelper(int left, int right, int root, int[] preorder, int[] inorder)
        {
            if (left > right || root >= inorder.Length)
            {
                return null;
            }

            TreeNode node = new TreeNode(preorder[root]);

            int target = 0;
            for (int i = left; i <= right; i++)
            {
                if (inorder[i] == preorder[root])
                {
                    target = i;
                    break;
                }
            }

            node.left = BuildTreeHelper(left, target - 1, root + 1, preorder, inorder);
            node.right = BuildTreeHelper(target + 1, right, root + 1 + target - left, preorder, inorder);

            return node;
        }
        //BINARY TREE ZIGZAG LEVEL ORDER TRAVERSAL
        //Given a binary tree, return the zigzag level order traversal of its nodes values
        //(from left to right, then right to left for the next level and alternate between them)
        public static IList<IList<int>> ZigzagLevelOrder(TreeNode root)
        {
            IList<IList<int>> res = new List<IList<int>>();
            Queue<TreeNode> nodes = new Queue<TreeNode>();

            nodes.Enqueue(root);
            bool leftToRight = false;
            while (nodes.Count > 0)
            {
                IList<int> level = new List<int>();
                int quantity = nodes.Count;

                for (int i = 0; i < quantity; i++)
                {
                    TreeNode temp = nodes.Dequeue();
                    if (temp != null)
                    {
                        if (leftToRight)
                            level.Insert(0, temp.val);
                        else
                            level.Insert(level.Count, temp.val);
                        if (temp.left != null)
                            nodes.Enqueue(temp.left);
                        if (temp.right != null)
                            nodes.Enqueue(temp.right);
                    }
                }
                if (level.Count > 0)
                    res.Add(level);
                leftToRight = !leftToRight;
            }

            return res;
        }

        //BINARY TREE LEVEL ORDER TRAVERSAL
        //Given a binary tree, return the level order traversal of its node's value(from left to right, level by level)
        public static IList<IList<int>> LevelOrder(TreeNode root)
        {

            IList<IList<int>> res = new List<IList<int>>();

            Queue<TreeNode> queue = new Queue<TreeNode>();
            queue.Enqueue(root);

            while (queue.Count > 0)
            {
                IList<int> level = new List<int>();
                int quantity = queue.Count;
                for (int i = 0; i < quantity; i++)
                {
                    TreeNode temp = queue.Dequeue();
                    if (temp != null)
                        level.Add(temp.val);

                    if (temp != null && temp.left != null)
                        queue.Enqueue(temp.left);

                    if (temp != null && temp.right != null)
                        queue.Enqueue(temp.right);
                }

                if (level.Count > 0)
                    res.Add(level);
            }

            return res;
        }

        //BASE 7
        public static string ConvertToBase7(int num)
        {
            StringBuilder sb = new StringBuilder();
            bool neg = false;
            if (num < 0)
            {
                neg = true;
                num *= -1;
            }
            else if (num == 0)
                sb.Append("0");


            while (num != 0)
            {
                int remain = num % 7;

                sb.Insert(0, remain);
                num /= 7;
            }

            return neg ? sb.Insert(0, "-").ToString() : sb.ToString();
        }

        //AVERAGE LEVELS IN BINARY TREE
        //Given a non-empty binary tree, returnt he average value of the node on each
        //level in the form of an array
        public static IList<double> AverageOfLevels(TreeNode root)
        {
            IList<double> res = new List<double>();
            Queue<TreeNode> queue = new Queue<TreeNode>();
            queue.Enqueue(root);

            while (queue.Count > 0)
            {
                int length = queue.Count;
                double sum = 0;
                for (int i = 0; i < length; i++)
                {
                    TreeNode node = queue.Dequeue();
                    if (node.left != null)
                        queue.Enqueue(node.left);

                    if (node.right != null)
                        queue.Enqueue(node.right);

                    sum += node.val;
                }

                res.Add(sum / length);
            }

            return res;
        }
        //ADD TO ARRAY-FORM OF INTEGER
        //For a non-negative integer x, the array-form of x is an array of its digits in the
        //left to right order. For example, if x = 1231, then the array form is [1,2,3,1]
        //Given the array form A of a non-negative integer x, return the array-form of the integer x + k
        public static IList<int> AddToArrayForm(int[] A, int K)
        {
            IList<int> temp = new List<int>();
            IList<int> res = new List<int>();

            int carry = 0;
            int remainder = 0;
            int index = A.Length - 1;
            int digit = 0;

            while (index >= 0 || K > 0)
            {
                remainder = K % 10;
                K = K / 10;
                digit = index >= 0 ? A[index] : 0;
                int currValue = digit + remainder + carry;
                carry = currValue / 10;

                temp.Add(currValue % 10);
                index--;
            }

            if (carry == 1)
                temp.Add(carry);

            for (int i = temp.Count - 1; i >= 0; i--)
            {
                res.Add(temp[i]);
            }

            return res;
        }

        //FIND N UNIQUE INTEGERS SUM UP TO ZERO
        //Given an integer n, return an array containing n unique integers such
        //that they add up to 0
        public static int[] SumZero(int n)
        {
            int[] res = new int[n];
            int j = 0;
            if (n % 2 != 0)
            {
                res[0] = 0;

                for (int i = 1; i <= n / 2; i++)
                {
                    res[i] = i;
                    res[n - i] = -i;
                }
            }
            else
            {
                for (int i = 0; i < n / 2; i++)
                {
                    res[i] = i + 1;
                    res[n - (i + 1)] = -(i + 1);
                }
            }

            return res;
        }

        //FIND LUCKY NUMBER IN AN ARRAY
        //Given an array of integers arr, a lucky number is an integer which has
        //a frequency in the array equal to its value.
        //Return a lucky integer in the array, if there are multiple lucky numbers
        //return the largest of them. If there is no lucky number return -1

        public static int FindLucky(int[] arr)
        {
            int[] frequency = new int[500];

            for (int i = 0; i < arr.Length; i++)
            {
                frequency[arr[i]]++;
            }

            int max = -1;
            for (int i = arr.Length - 1; i > 0; i--)
            {
                if (arr[i] == frequency[arr[i]])
                    max = Math.Max(max, arr[i]);
            }

            return max;
        }

        //FIND COMMON CHARACTERS
        //Given an array A of strings made only from lowercase letters,
        //return a list of all characters that show up in all strings within the list
        //(including duplicates). For Example, if a character occurs three times in all strings
        //but not 4 times, you need to include that character 3 times in the final answer.
        public static IList<string> CommonChars(string[] A)
        {
            if (A.Length == 0)
                return null;
            int num = 97;
            Console.WriteLine((char)num);

            IList<string> res = new List<string>();

            int[] letters = new int[26];

            for (int i = 0; i < A[0].Length; i++)
            {
                letters[A[0][i] - 'a']++;
            }

            for (int i = 1; i < A.Length; i++)
            {
                int[] temp = new int[26];

                for (int j = 0; j < A[0].Length; j++)
                {
                    temp[A[i][j] - 'a']++;
                }

                for (int k = 0; k < 26; k++)
                {
                    if (letters[k] > temp[k])
                        letters[k] = temp[k];
                }
            }

            for (int i = 0; i < 26; i++)
            {
                while (letters[i] > 0)
                {
                    res.Add(((char)(i + 97)).ToString());
                    letters[i]--;
                }
            }

            return res;
        }

        //BINARY TREE TILT
        //Given a binary tree, return the tilt of the whole tree.
        //the tilt of the tree node is defined as the difference between of
        //all the left subtree node values and the sum of all right subtree node
        //values. Null node has tilt 0.
        //The tilt of the whole tree is defined as the sum of all nodes' tilt
        public static int FindTilt(TreeNode root)
        {
            int tilt = 0;
            FindTiltHelper(root, ref tilt);
            return tilt;
        }

        public static int FindTiltHelper(TreeNode root, ref int tilt)
        {
            if (root == null)
                return 0;

            int leftSum = FindTiltHelper(root.left, ref tilt);
            int rightSum = FindTiltHelper(root.right, ref tilt);

            tilt += Math.Abs(leftSum - rightSum);
            return leftSum + rightSum + root.val;
        }

        //MAXIMUM DISTANCE TO THE CLOSEST PERSON
        //In a row of seats, 1 represents a person sitting in that seat,
        //and 0 represents that the seat is empty.
        //There is at least one empty seat, and at least one person sitting.
        //Alex wants to sit in the seat such that the distance between him
        //and the closest person to him is maximized
        //Return that maximum distance to closest person.

        public static int MaxDistToClosest(int[] seats)
        {
            int i = 0;
            int j = i;
            int temp = 0;
            int count = 0;

            while (j < seats.Length)
            {
                if (seats[j] == 0)
                {
                    temp++;
                    if (j == seats.Length - 1)
                    {
                        count = Math.Max(temp, count);
                        break;
                    }
                    j++;
                }
                else
                {
                    if (i == 0)
                        count = Math.Max(temp, count);
                    else
                        count = Math.Max((temp + 1) / 2, count);

                    temp = 0;
                    j++;
                    i = j;
                }
            }

            return count;
        }

        //K-DIFF PAIRS IN AN ARRAY
        //Given an array of integers and an integer k, you need to find the
        //number of unique k-diff pairs in the array.Here a k-diff pair
        //is defined as an integer pair (i, j), where i and j are both
        //numbers in the array and their absolute difference is k.
        public static int FindPairs(int[] nums, int k)
        {
            if (k < 0)
                return 0;

            Dictionary<int, int> dict = new Dictionary<int, int>();
            int count = 0;

            for (int i = 0; i < nums.Length; i++)
            {
                if (!dict.ContainsKey(nums[i]))
                    dict.Add(nums[i], 1);
                else
                    dict[nums[i]]++;
            }

            foreach (var n in dict)
            {
                if (k == 0)
                {
                    if (dict[n.Key] >= 2)
                        count++;
                }
                else
                {
                    if (dict.ContainsKey(n.Key + k))
                        count++;
                }
            }

            return count;

        }

        //ISLAND PERIMETER
        //You are given a map in form of a two-dimensional integer grid where 1 represents
        //land and 0 represents water.
        //Grid cells are connected horizontally/vertically (not diagonally).
        //The grid is completely surrounded by water, and there is exactly
        //one island (i.e., one or more connected land cells).
        //The island doesn't have "lakes" (water inside that isn't connected to
        //the water around the island). One cell is a square with side length 1.
        //The grid is rectangular, width and height don't exceed 100.
        //Determine the perimeter of the island.
        public static int IslandPerimeter(int[][] grid)
        {
            int count = 0; //count the number of edges that are surrounded by water

            for (int row = 0; row < grid.Length; row++)
            {
                for (int col = 0; col < grid[row].Length; col++)
                {
                    if (grid[row][col] == 1)
                    {
                        if (col == 0 || grid[row][col - 1] == 0) //left
                            count++;

                        if (col == grid[row].Length - 1 || grid[row][col + 1] == 0) //right
                            count++;

                        if (row == 0 || grid[row - 1][col] == 0) //up
                            count++;

                        if (row == grid.Length - 1 || grid[row + 1][col] == 0)//bottom
                            count++;

                    }
                }
            }

            return count;
        }
        //LONGEST WORDS IN A DICTIONARY
        //Given a list of strings words representing an English Dictionary,
        //find the longest word in words that can be built one character at a
        //time by other words in words.If there is more than one possible answer,
        //return the longest word with the smallest lexicographical order.
        //If there is no answer, return the empty string.
        public static string LongestWord(string[] words)
        {
            Array.Sort(words);
            string res = "";

            HashSet<string> hashSet = new HashSet<string>();
            foreach (string e in words)
            {
                if (hashSet.Contains(e.Substring(0, e.Length - 1)) || e.Length == 1)
                {
                    res = res.Length >= e.Length ? res : e;
                    hashSet.Add(e);
                }
            }

            return res;
        }

        //X IS KIND IN A DECK OF CARDS
        //In a deck of cards, each card has an integer written on it.
        //Return true if and only if you can choose X >= 2 such that it
        //is possible to split the entire deck into 1 or more groups of cards, where:
        //Each group has exactly X cards.
        //All the cards in each group have the same integer.
        public static bool HasGroupsSizeX(int[] deck)
        {
            Dictionary<int, int> dict = new Dictionary<int, int>();

            for (int i = 0; i < deck.Length; i++)
            {
                if (!dict.ContainsKey(deck[i]))
                    dict.Add(deck[i], 1);
                else
                    dict[deck[i]]++;
            }

            int gcd = 0;
            foreach (var d in dict)
            {
                gcd = GCD(gcd, d.Value);
                if (gcd < 2)
                    return false;
            }

            return true;
        }
        //GREATEST COMMON DIVISOR
        public static int GCD(int a, int b)
        {
            if (a == 0)
                return b;
            if (b == 0)
                return a;

            if (a < b)
                return GCD(a, b % a);
            else
                return GCD(a % b, b);        
        }

        //REVERSE ONLY LETTERS
        public static string ReverseOnlyLetters(string S)
        {
            int i = 0;
            int j = S.Length - 1;

            char[] s = S.ToCharArray();

            while (i < j)
            {
                if (Char.IsLetter(s[i]))
                {
                    if (Char.IsLetter(s[j]))
                    {
                        char temp = s[i];
                        s[i] = s[j];
                        s[j] = temp;
                        i++;
                    }

                    j--;
                }
                else
                {
                    i++;
                }
            }

            return new string(s);
        }
        //Perfect Square 
        public static bool IsPerfectSquare(int num)
        {
            if (num == 1)
                return true;

            long left = 1;
            long right = num / 2;

            while (left <= right)
            {
                long mid = left + (right - left) / 2; //overflow if int is used here

                if (mid * mid == num)
                    return true;
                else if (mid * mid < num)
                    left = mid + 1;
                else
                    right = mid - 1;
            }

            return false;
        }

        //COUNT AND SAY (Refer to LeetCode for the problem discription)
        public static string CountAndSay(int n)
        {
            string ans = "1";

            for (int i = 2; i <= n; i++)
            {
                ans = CountAndSayHelper(ans);
            }

            return ans;
        }

        public static string CountAndSayHelper(string num)
        {
            StringBuilder res = new StringBuilder();

            int count = 0;
            char val = num[0];

            for (int i = 0; i < num.Length; i++)
            {
                if (num[i] == val)
                {
                    count++;
                }
                else
                {
                    res.Append(count);
                    res.Append(val);

                    count = 1;
                    val = num[i];
                }
            }

            res.Append(count);
            res.Append(val);    //the last group of a string

            return res.ToString();
        }

        //INTERSECTION OF TWO ARRAY OF NUMBERS
        public static int[] Intersection(int[] nums1, int[] nums2)
        {

            HashSet<int> numbers = new HashSet<int>();
            HashSet<int> ans = new HashSet<int>();

            for (int i = 0; i < nums1.Length; i++)
            {
                if (!numbers.Contains(nums1[i]))
                {
                    numbers.Add(nums1[i]);
                }
            }

            for (int i = 0; i < nums2.Length; i++)
            {
                if (numbers.Contains(nums2[i]) && !ans.Contains(nums2[i]))
                {
                    ans.Add(nums2[i]);
                }
            }

            int[] res = new int[ans.Count];
            ans.CopyTo(res);
            return res;
        }
            //PATH SUM THREE
            //You are given a binary tree in which each node contains an integer value.
            //Find the number of paths that sum to a given value.
            //The path does not need to start or end at the root or a leaf,
            //but it must go downwards(traveling only from parent nodes to child nodes).
            public static int PathSumThree(TreeNode root, int sum)
        {
            if (root == null)
                return 0;

            return PathSumHelperThree(root, sum, 0, new List<int>());
        }

        public static int PathSumHelperThree(TreeNode root, int sum, int count, List<int> path)
        {
            path.Add(root.val);

            int newSum = 0;

            //compute all the sums. Iterate from the newly added node to prevent duplidate results
            for (int j = path.Count - 1; j >= 0; j--)
            {
                newSum += path[j];

                if (newSum == sum)
                {
                    count++;
                }
            }

            int length = path.Count; //length before adding left or right

            if (root.left != null)
            {
                count = PathSumHelperThree(root.left, sum, count, path);
            }

            if (root.right != null)
            {
                int l = path.Count; //length after adding left node
                for (int i = l - 1; i >= length; i--)
                {
                    path.RemoveAt(i);   //remove the left tree
                }

                count = PathSumHelperThree(root.right, sum, count, path);
            }

            return count;
        }

        //PATH SUM II
        //Given a binary tree and a sum, find all root-to-leaf paths
        //where each path's sum equals the given sum.
        public static IList<IList<int>> PathSum(TreeNode root, int sum)
        {

            IList<IList<int>> ans = new List<IList<int>>();
            IList<int> path = new List<int>();

            if (root == null)
                return ans;

            return PathSumHelperTwo(root, sum, path, ans);
        }

        public static IList<IList<int>> PathSumHelperTwo(TreeNode root, int sum, IList<int> path, IList<IList<int>> ans)
        {
            path.Add(root.val);
            int newSum = sum - root.val;

            if (root.left == null && root.right == null)
            {
                if (newSum == 0)
                {
                    // IList<int> p = new List<int>();
                    // for(int i = 0; i < path.Count; i++)
                    // {
                    //     p.Add(path[i]);
                    // }

                    ans.Add(path.ToList());
                }
                return ans;
            }

            int length = path.Count;

            if (root.left != null)
            {
                PathSumHelperTwo(root.left, newSum, path, ans);
            }

            if (root.right != null)
            {
                int l = path.Count;
                for (int i = l - 1; i >= length; i--)
                    path.RemoveAt(i);
                PathSumHelperTwo(root.right, newSum, path, ans);
            }

            return ans;
        }
        //REVESRSE VOWELS OF A STRING
        public static string ReverseVowels(string s)
        {
            int first = 0;              //first pointer
            int last = s.Length - 1;    //last pointer

            char[] c = s.ToCharArray();

            while (first < last)
            {
                if (isVowel(c[first]))
                {
                    while (last > first)
                    {
                        if (isVowel(c[last]))
                        {
                            char temp = c[first];
                            c[first] = c[last];
                            c[last] = temp;
                            last--;
                            break;
                        }
                        last--;
                    }
                }
                first++;
            }

            return new string(c);
        }

        public static bool isVowel(char c)
        {
            return c == 'a' || c == 'e' || c == 'i' || c == 'o' || c == 'u'
                || c == 'A' || c == 'E' || c == 'I' || c == 'O' || c == 'U';
        }

        //LOWEST COMMON ANCESTOR BINARY SEARCH TREE
        public static TreeNode LowestCommonAncestor(TreeNode root, TreeNode p, TreeNode q)
        {
            if (root.val < p.val && root.val < q.val)
                root = LowestCommonAncestor(root.right, p, q);

            if (root.val > p.val && root.val > q.val)
                root = LowestCommonAncestor(root.left, p, q);

            return root;
        }

        //ISOMORPHIC STRING
        //Given two strings s and t, determine if they are isomorphic.
        //Two strings are isomorphic if the characters in s can be replaced to get t.
        //All occurrences of a character must be replaced with another character
        //while preserving the order of characters.No two characters may map to
        //the same character but a character may map to itself.

        public static bool IsIsomorphic(string s, string t)
        {
            Dictionary<char, char> chars = new Dictionary<char, char>();
            if (s == null && t == null)
                return true;

            char[] first = s.ToCharArray();
            char[] second = t.ToCharArray();
            for (int i = 0; i < first.Length; i++)
            {
                if (!chars.ContainsKey(first[i]))
                {
                    if (chars.ContainsValue(second[i]))
                        return false;    //each key has a unique value
                    chars.Add(first[i], second[i]);
                }
                else
                {
                    if (chars[first[i]] != second[i])
                        return false;
                }
            }

            return true;
        }

        //HAPPY NUMBER
        //Write an algorithm to determine if a number is "happy".
        //A happy number is a number defined by the following process:
        //Starting with any positive integer, replace the number by the sum of
        //the squares of its digits, and repeat the process until the number equals 1
        //(where it will stay), or it loops endlessly in a cycle which does not include 1.
        //Those numbers for which this process ends in 1 are happy numbers.
        public static bool IsHappy(int n)
        {
            Dictionary<int, int> sums = new Dictionary<int, int>();

            while (true)
            {
                n = SumOfNumberDigits(n);
                if (n == 1)
                    return true;

                if (!sums.ContainsValue(n))
                    sums.Add(n, n);
                else
                    break;
            }

            return false;
        }

        public static int SumOfNumberDigits(int n)
        {
            int sum = 0;
            while (n > 0)
            {
                int remain = n % 10;
                n = n / 10;
                sum += remain * remain;
            }

            return sum;
        }

        //TWO SUM II - INPUT ARRAY IS SORTED
        //Given an array of integers that is already sorted in ascending order,
        //find two numbers such that they add up to a specific target number.
        //The function twoSum should return indices of the two numbers such that
        //they add up to the target, where index1 must be less than index2.
        //Note:
        //Your returned answers (both index1 and index2) are not zero-based.
        //You may assume that each input would have exactly one solution and
        //you may not use the same element twice.
        public static int[] TwoSum(int[] numbers, int target)
        {
            int first = 0;
            int last = numbers.Length - 1;

            while (first < last)
            {
                if (numbers[first] + numbers[last] > target)
                    last--;
                else if (numbers[first] + numbers[last] < target)
                    first++;
                else
                    break;      //muss be equal to the target because one solution is guaranteed
            }

            return new int[] { first + 1, last + 1 };
        }

        //PATH SUM
        //Given a binary tree and a sum, determine if the tree has
        //a root-to-leaf path such that adding up all the values along
        //the path equals the given sum.
        //Note: A leaf is a node with no children.
        public static bool HasPathSum(TreeNode root, int sum)
        {
            if (root == null)
                return false;

            sum -= root.val;

            if (root.left == null && root.right == null && sum == 0)
                return true;

            return HasPathSum(root.left, sum) || HasPathSum(root.right, sum);
        }

        //PASCAL TRIANGLE II
        //Given a non-negative index k where k ≤ 33,
        //return the kth index row of the Pascal's triangle.
        //Note that the row index starts from 0.

        public static IList<int> GetRow(int rowIndex)
        {
            IList<List<int>> pascalTri = new List<List<int>>();

            List<int> newRow = new List<int>();
            newRow.Add(1);      //special case for top row
            pascalTri.Add(newRow);

            for (int r = 1; r <= rowIndex; r++)
            {
                newRow = new List<int>();
                newRow.Add(1);  //first element of each row
                for (int c = 1; c < r; c++)
                {
                    //elements in between the first and last of each row
                    newRow.Add(pascalTri[r - 1][c - 1] + pascalTri[r - 1][c]);
                }

                newRow.Add(1);  //last element of each row
                pascalTri.Add(newRow);
            }

            return pascalTri[rowIndex];
        }

        //SAME TREE
        //Given two binary trees, write a function to check if they are the same or not.
        //Two binary trees are considered the same if they are structurally identical
        //and the nodes have the same value.
        public static bool IsSameTree(TreeNode p, TreeNode q)
        {
            if (p == null && q != null)
                return false;

            if (p != null && q == null)
                return false;

            if (q == null && p == null)
                return true;

            if (q.val != p.val)
                return false;

            bool sameRight = IsSameTree(p.right, q.right);
            bool sameLeft = IsSameTree(p.left, q.left);

            return sameRight && sameLeft;
        }

        public static string AddBinary(string a, string b)
        {
            char[] first = a.ToCharArray();
            char[] second = b.ToCharArray();
            int carry = 0;

            StringBuilder result = new StringBuilder();

            int aLength = first.Length - 1;
            int bLength = second.Length - 1;

            while (aLength >= 0 || bLength >= 0 || carry > 0)
            {
                int currentA = (aLength >= 0) ? first[aLength] - '0' : 0;
                int currentB = (bLength >= 0) ? second[bLength] - '0' : 0;

                int sum = (currentA + currentB + carry) % 2;
                carry = (currentA + currentB + carry) > 1 ? 1 : 0;

                result.Append(sum);
                aLength--;
                bLength--;
            }

            return new string(result.ToString().Reverse().ToArray());

        }

        //SEARCH IN A BINARY SEARCH TREE
        //Given the root node of a binary search tree(BST) and a value.
        //You need to find the node in the BST that the node's value equals the given value.
        //Return the subtree rooted with that node. If such node doesn't exist,
        //you should return NULL.
        public TreeNode SearchBST(TreeNode root, int val)
        {
            if (root == null)
                return null;

            if (root.val == val)
                return root;

            if (root.val > val)
                return SearchBST(root.left, val);
            else
                return SearchBST(root.right, val);
        }

        //MAXIMUM SUBARRAY
        //Given an integer array nums, find the contiguous subarray(containing at least one number)
        //which has the largest sum and return its sum.
        public static int MaxSubArray(params int[] nums)
        {
            int max = nums[0];
            int largestSum = nums[0];
            int sum = 0;

            for (int i = 0; i < nums.Length; i++)
            {
                if (nums[i] > max)
                {
                    max = nums[i];
                }

                sum += nums[i];

                if (sum > largestSum)
                {
                    largestSum = sum;
                }

                if (sum < nums[i])
                {
                    sum = nums[i];
                }
            }

            return Math.Max(max, largestSum);

        }

        //CLIMBING STAIRS
        //You are climbing a stair case. It takes n steps to reach to the top.
        //Each time you can either climb 1 or 2 steps.In how many distinct ways can you climb to the top?
        public int ClimbStairs(int n)
        {
            int[] steps = new int[n + 1];

            if (n < 3)
            {
                return steps[n] = n;
            }

            steps[0] = 0;
            steps[1] = 1;
            steps[2] = 2;

            for (int i = 3; i < n + 1; i++)
            {
                steps[i] = steps[i - 1] + steps[i - 2];
            }

            return steps[n];
        }

        //REMOVE OUTERMOST PARENTHESIS
        public string RemoveOuterParentheses(string S)
        {
            StringBuilder str = new StringBuilder();

            Stack<char> stack = new Stack<char>();
            Queue<char> queue = new Queue<char>();

            char[] parens = S.ToCharArray();

            for (int i = 0; i < parens.Length; i++)
            {
                if (parens[i] == '(')
                {
                    stack.Push(parens[i]);
                    queue.Enqueue(parens[i]);
                }
                else
                {
                    stack.Pop();
                    if (stack.Count == 0)
                    {
                        queue.Dequeue();            //remove the most outer '('
                        while (queue.Count > 0)     //dequeue everthing from the queue
                            str.Append(queue.Dequeue());
                    }
                    else
                        queue.Enqueue(parens[i]);
                }

            }

            return str.ToString();
        }

        // CELLS WITH ODD VALUES IN A MATRIX
        //Given n and m which are the dimensions of a matrix initialized by zeros and given
        //an array indices where indices[i] = [ri, ci]. For each pair of[ri, ci] you
        //have to increment all cells in row ri and column ci by 1.
        //Return the number of cells with odd values in the matrix after applying the increment to all indices.

        public int OddCells(int n, int m, int[][] indices)
        {
            int[,] cells = new int[n, m];

            for (int k = 0; k < indices.Length; k++)
            {
                for (int i = 0; i < m; i++)
                {
                    cells[indices[k][0], i]++;
                }

                for (int j = 0; j < n; j++)
                {
                    cells[j, indices[k][1]]++;
                }
            }

            int count = 0;

            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < m; j++)
                {
                    if (cells[i, j] % 2 != 0)
                        count++;
                }
            }

            return count;
        }

        //RANGE SUM OF BST
        //Given the root node of a binary search tree, return the sum of
        //values of all nodes with value between L and R(inclusive).
        //The binary search tree is guaranteed to have unique values.

        public int RangeSumBST(TreeNode root, int L, int R)
        {
            if (root == null)
                return 0;

            if (root.val < L)
                return RangeSumBST(root.right, L, R);

            if (root.val > R)
                return RangeSumBST(root.left, L, R);

            return RangeSumBST(root.left, L, R) + RangeSumBST(root.right, L, R) + root.val;
        }

        //FIND ALL NUMBER THAT DISSAPEAR IN AN ARRAY
        //Given an array of integers where 1 ≤ a[i] ≤ n (n = size of array),
        //some elements appear twice and others appear once.
        //Find all the elements of[1, n] inclusive that do not appear in this array.
        //Could you do it without extra space and in O(n) runtime? You may assume the returned list does not count as extra space.
        public static IList<int> FindDisappearedNumbers(int[] nums)
        {
            IList<int> result = new List<int>();

            for (int i = 0; i < nums.Length; i++)
            {
                int temp = Math.Abs(nums[i]);   //num[i] may have been negated

                if (nums[temp - 1] >= 0)
                    nums[temp - 1] *= -1;
            }

            for (int j = 0; j < nums.Length; j++)
            {
                if (nums[j] > 0)
                    result.Add(j + 1);
            }

            return result;
        }

        //Invert Binary Tree
        public static TreeNode InvertTree(TreeNode root)
        {
            if (root == null)
                return root;

            TreeNode temp = root.left;
            root.left = root.right;
            root.right = temp;

            InvertTree(root.left);
            InvertTree(root.right);

            return root;
        }
        //MERGE TWO TREES
        //Given two binary trees and imagine that when you put one of them to cover the other, 
        //some nodes of the two trees are overlapped while the others are not.
        //You need to merge them into a new binary tree. The merge rule is that if two nodes overlap, 
        //then sum node values up as the new value of the merged node. Otherwise,
        //the NOT null node will be used as the node of new tree.
        public static TreeNode MergeTrees(TreeNode t1, TreeNode t2)
        {
            if (t1 == null)
                return t2;

            if (t2 == null)
                return t1;

            t1.val += t2.val;

            t1.left = MergeTrees(t1.left, t2.left);
            t1.right = MergeTrees(t1.right, t2.right);

            return t1;

        }

        //SYMMETRIC TREE (iterative solution)
        //Given a binary tree, check whether it is a mirror of itself (ie, symmetric around its center).
        public static bool IsSymmetric2(TreeNode root)
        {
            if (root == null)
                return true;

            Queue<TreeNode> left = new Queue<TreeNode>();
            Queue<TreeNode> right = new Queue<TreeNode>();

            left.Enqueue(root.left);
            right.Enqueue(root.right);

            while (left.Count > 0 && right.Count > 0)
            {
                TreeNode leftN = left.Dequeue();
                TreeNode rightN = right.Dequeue();

                if (leftN != null && rightN != null && leftN.val == rightN.val)
                {
                    left.Enqueue(leftN.left);
                    left.Enqueue(leftN.right);
                    right.Enqueue(rightN.right);
                    right.Enqueue(rightN.left);
                }
                else
                {
                    if (leftN == null && rightN == null)
                    {
                        //do nothing; 
                        //or can check to see left and right are not empty, then dequeue
                    }
                    else
                    {
                        return false;
                    }

                }
            }

            return true;

        }

        //SYMMETRIC TREE (recursive solution)
        //Given a binary tree, check whether it is a mirror of itself (ie, symmetric around its center).
        public static bool IsSymmetric(TreeNode root)
        {
            if (root == null)
                return true;

            return IsSymmetricHelper(root.left, root.right);


        }

        public static bool IsSymmetricHelper(TreeNode left, TreeNode right)
        {
            if (left != null && right != null && left.val == right.val)
            {
                return IsSymmetricHelper(left.left, right.right) && IsSymmetricHelper(left.right, right.left);

            }
            else
            {
                if (left == null && right == null)
                    return true;

                return false;
            }
        }

        //MAXIUM DEPTH OF BINARY TREE
        public static int MaxDepth(TreeNode root)
        {
            if (root == null)
                return 0;
            int l = MaxDepth(root.left) + 1;
            int r = MaxDepth(root.right) + 1;

            return Math.Max(l, r);
        }

        public static TreeNode PostOrderTraversal(TreeNode root)
        {
            if (root == null)
                return root;

            PostOrderTraversal(root.left);
            PostOrderTraversal(root.right);
            ProcessNode(root);

            return root;
        }

        public static TreeNode InOrderTraversal(TreeNode root)
        {
            if (root == null)
                return root;

            PreOrderTraversal(root.left);
            ProcessNode(root);
            PreOrderTraversal(root.right);

            return root;
        }

        public static TreeNode PreOrderTraversal(TreeNode root)
        {
            if (root == null)
                return root;

            ProcessNode(root);
            PreOrderTraversal(root.left);
            PreOrderTraversal(root.right);

            return root;
        }

        public static void ProcessNode(TreeNode node)
        {
            //Do whatever we want to do with the node
            Console.WriteLine(node.val);
        }

        //Convert Sorted Array to Binary Search Tree
        //Given an array where elements are sorted in ascending order,
        //convert it to a height balanced BST.
        public static TreeNode SortedArrayToBST(int[] nums)
        {
            TreeNode root = null;
            root = BSTHelper(nums, root, 0, nums.Length - 1);

            return root;
        }

        public static TreeNode BSTHelper(int[] nums, TreeNode newRoot, int start, int end)
        {
            if (start > end)
                return null;

            int mid = (start + end) / 2;

            newRoot = new TreeNode(nums[mid]);
            newRoot.left = BSTHelper(nums, newRoot.left, start, mid - 1);
            newRoot.right = BSTHelper(nums, newRoot.right, mid + 1, end);

            return newRoot;

        }

        //PASCAL'S TRIANGLE
        //Given a non-negative integer numRows, generate the first numRows of Pascal's triangle.
        //In Pascal's triangle, each number is the sum of the two numbers directly above it.
        public IList<IList<int>> Generate(int numRows)
        {
            IList<IList<int>> nums = new List<IList<int>>();

            for (int i = 1; i <= numRows; i++)
            {
                IList<int> newRow = new List<int>();
                for (int j = 1; j <= i; j++)
                {
                    if (j == 1 || j == i)
                    {
                        newRow.Add(1);
                    }
                    else
                    {
                        newRow.Add(nums[i - 2][j - 2] + nums[i - 2][j - 1]);
                    }
                }

                nums.Add(newRow);

            }

            return nums;


        }

        //BEST TIME TO BUY AND SELL STOCK
        /* Say you have an array for which the ith element is the price of a given stock on day i.
         * If you were only permitted to complete at most one transaction
         * (i.e., buy one and sell one share of the stock), design an algorithm to find the maximum profit.
         * Note that you cannot sell a stock before you buy one.       
        */
        public static int MaxProfit2(int[] prices)
        {
            if (prices.Length < 2)
                return 0;

            int smallest = prices[0];
            int largest = prices[0];
            int max = 0;

            for (int i = 1; i < prices.Length; i++)
            {
                if (smallest > prices[i])
                {

                    smallest = prices[i];
                    largest = prices[i];
                }

                if (largest < prices[i])
                {
                    largest = prices[i];
                }

                max = Math.Max(max, largest - smallest);
            }


            return max;
        }

        //BEST TIME TO SELL AND BUY STOCK !!
        /*Say you have an array for which the ith element is the price of a given stock on day i.
         * Design an algorithm to find the maximum profit. 
         * You may complete as many transactions as you like        
        */
        public static int MaxProfit(int[] prices)
        {
            int profit = 0;
            if (prices.Length < 1)
                return profit;

            for (int i = 0; i < prices.Length - 1; i++)
            {
                if (prices[i] < prices[i + 1])
                {
                    profit += prices[i + 1] - prices[i];
                }
            }

            return profit;
        }

        //SINGLE NUMBER  
        //Given a non-empty array of integers,
        //every element appears twice except for one. Find that single one.
        public int SingleNumber(int[] nums)
        {

            int result = nums[0];

            for (int i = 1; i < nums.Length; i++)
            {
                result ^= nums[i];
            }

            return result;
        }

        //INTERSECTION OF TWO LINKED LISTS
        //Write a program to find the node at which the intersection of two singly linked lists begins.
        public static ListNode GetIntersectionNode(ListNode headA, ListNode headB)
        {

            ListNode a = headA;
            ListNode b = headB;

            while (a != b)
            {
                if (a == null)
                    a = headB;
                else
                    a = a.next;

                if (b == null)
                    b = headA;
                else
                    b = b.next;
            }

            return b;
        }

        //MAJORITY ELEMENT (reference solution)
        public int MajorityElement1(int[] nums)
        {

            Array.Sort(nums);
            /*
            Based on sorted array, nums[nums.length/2] must be the answer
            Otherwise, the length requirement (>n/2) can't be satisfied
            */
            return nums[nums.Length / 2];
        }

        //MAJORITY ELEMENT (My solution)
        public static int MajorityElement(int[] nums)
        {

            Dictionary<int, int> dict = new Dictionary<int, int>();

            for (int i = 0; i < nums.Length; i++)
            {
                if (!dict.ContainsKey(nums[i]))
                {
                    dict.Add(nums[i], 1);
                }
                else
                {
                    dict[nums[i]]++;
                }
            }

            foreach (KeyValuePair<int, int> d in dict)
            {
                if (d.Value >= nums.Length / 2 + 1)
                    return d.Key;
            }

            return -1;
        }
        //EXCEL SHEET COLUMN NUMBER\
        /*
        Given a column title as appear in an Excel sheet, return its corresponding column number.
        */
        public int TitleToNumber(string s)
        {
            int result = 0;

            foreach (char c in s)
            {
                result = result * 26 + c - 'A' + 1;
            }

            return result;
        }
        //FACTORIAL TRAILING ZEROES
        //Given an integer n, return the number of trailing zeroes in n!.
        public int TrailingZeroes(int n)
        {
            int result = 0;

            while (n > 0)
            {
                n /= 5;
                result += n;
            }

            return result;
        }

        //NUMBER OF 1 BITS
        /*
         * Write a function that takes an unsigned integer and return the number
         * of '1' bits it has (also known as the Hamming weight).      
         */
        public static int HammingWeight(uint n)
        {
            int count = 0;

            while (n > 0)
            {
                if (n % 2 != 0)
                {
                    count++;
                }
                n = n >> 1;
            }

            return count;
        }

        /* TWO SUM
         * Given an array of integers, return indices of the two numbers such that they add up to a specific target.

        You may assume that each input would have exactly one solution, and you may not use the same element twice.*/

        static int[] TwoSum2(int[] nums, int target)
        {

            int[] result = new int[2];
            Dictionary<int, int> dic = new Dictionary<int, int>();

            for (int i = 0; i < nums.Length; i++)
            {
                int complement = target - nums[i];
                if (dic.ContainsKey(complement))
                {
                    result[0] = dic[complement];
                    result[1] = i;
                    return result;
                }
                if (!dic.ContainsKey(nums[i]))
                {
                    dic.Add(nums[i], i);
                }
            }

            return result;
        }

        //REVERSE AN 32 BIT INTEGER
        public static int Reverse(int x)
        {
            int rev = 0;
            int remaind;
            while (x != 0)
            {

                remaind = x % 10;
                x /= 10;

                if (rev > Int32.MaxValue / 10 || (rev == Int32.MaxValue / 10 && remaind > 7)) return 0;
                if (rev < Int32.MinValue / 10 || rev == Int32.MinValue / 10 && remaind < -8) return 0;
                rev = rev * 10 + remaind;

            }

            return rev;
        }


        //IS A NUMBER A PALLINDROME
        public static bool IsPalindrome(int x)
        {
            if (x < 0)
                return false;
            int reverse = 0;
            int remaind = 0;
            int temp = x;

            while (temp != 0)
            {
                remaind = temp % 10;
                temp = temp / 10;

                reverse = reverse * 10 + remaind;
            }

            if (reverse == x)
                return true;
            else
                return false;
        }

        //CONVERT A ROMAN TO INTEGER
        public static int RomanToInt(string s)
        {

            Dictionary<char, int> dic = new Dictionary<char, int>();
            dic.Add('M', 1000);
            dic.Add('D', 500);
            dic.Add('C', 100);
            dic.Add('L', 50);
            dic.Add('X', 10);
            dic.Add('V', 5);
            dic.Add('I', 1);

            char[] roman = s.ToCharArray();
            int num = 0;
            int temp = dic['M'];
            for (int i = 0; i < roman.Length; i++)
            {
                num += dic[roman[i]];

                if (temp < dic[roman[i]])
                {
                    num -= temp * 2;
                }

                temp = dic[roman[i]];
            }

            return num;
        }

        //LONGEST COMMON PREFIX STRING AMONGST AN ARRAY OF STRINGS
        public static string LongestCommonPrefix(string[] strs)
        {

            if (strs == null || strs.Length == 0)
                return "";


            for (int i = 0; i < strs[0].Length; i++)
            {
                string temp = strs[0];
                char c = temp[i];
                for (int j = 1; j < strs.Length; j++)
                {
                    string current = strs[j];
                    if (i == current.Length || c != current[i])
                    {
                        return temp.Substring(0, i);
                    }
                }
            }

            return strs[0];
        }


        //VALID PARENTHESES
        public static bool IsValid(string s)
        {

            if (s == null) return true;

            Stack<char> chars = new Stack<char>();

            for (int i = 0; i < s.Length; i++)
            {
                if (s[i] == '(' || s[i] == '{' || s[i] == '[')
                {
                    chars.Push(s[i]);
                }
                else
                {
                    if (chars.Count == 0)
                        return false;

                    char temp = chars.Pop();
                    if (s[i] == ')' && temp != '(')
                    {
                        return false;
                    }
                    else
                    {
                        if (s[i] == '}' && temp != '{')
                        {
                            return false;
                        }
                        else
                        {
                            if (s[i] == ']' && temp != '[')
                            {
                                return false;
                            }
                        }
                    }
                }
            }

            if (chars.Count != 0)
            {
                return false;
            }

            return true;
        }

        //MERGE TWO SORTED LINKED-LIST
        public static ListNode MergeTwoLists(ListNode l1, ListNode l2)
        {

            if (l1 == null)
                return l2;
            if (l2 == null)
                return l1;

            ListNode head;
            ListNode current;

            if (l1.val <= l2.val)
            {
                head = l1;
                l1 = head.next;
                current = head;
            }
            else
            {
                head = l2;
                l2 = head.next;
                current = head;
            }

            while (l1 != null && l2 != null)
            {
                if (l1.val <= l2.val)
                {
                    current.next = l1;
                    current = l1;
                    l1 = l1.next;
                }
                else
                {
                    current.next = l2;
                    current = l2;
                    l2 = l2.next;
                }
            }

            if (l2 == null)
            {
                current.next = l1;
            }

            if (l1 == null)
            {
                current.next = l2;
            }

            return head;

        }

        //NON-DECREASING ARRAY
        /*Given an array with n integers, your task is to check if it could become non-decreasing by modifying at most 1 element.
        We define an array is non-decreasing if array[i] <= array[i + 1] holds for every i(1 <= i<n).*/
        public static bool CheckPossibility(int[] nums)
        {
            if (nums.Length <= 2)
            {
                return true;
            }
            int count = 1;


            for (int i = 0; i < nums.Length - 1; i++)
            {
                if (nums[i] > nums[i + 1])
                {
                    if (i == 0)
                    {
                        count--;
                    }
                    else
                    {
                        if (nums[i - 1] <= nums[i + 1])
                        {
                            count--;
                        }
                        else
                        {
                            count--;
                            nums[i + 1] = nums[i];
                        }
                    }

                    if (count == -1)
                    {
                        return false;
                    }
                }

            }
            return true;
        }

        //ROTATE ARRAY
        //Given an array, rotate the array to the right by k steps, where k is non-negative.
        public static void Rotate(int[] nums, int k)
        {
            if (k == 0 || nums.Length == 1)
            {
                return;
            }

            k = k % nums.Length;
            int numOfSwap = nums.Length;


            int nextIndex = 0;
            int nextValue;

            for (int i = 0; i < nums.Length; i++)
            {
                int currentValue = nums[i];
                nextIndex = i;
                while (numOfSwap > 0)
                {

                    if (k + nextIndex >= nums.Length)
                    {
                        nextIndex = k + nextIndex - nums.Length;
                    }
                    else
                    {
                        nextIndex = k + nextIndex;
                    }

                    nextValue = nums[nextIndex];
                    nums[nextIndex] = currentValue;
                    currentValue = nextValue;

                    numOfSwap--;

                    if (nextIndex == i)
                        break;
                }

                if (numOfSwap == 0)
                {
                    break;
                }
            }

        }

        public static bool IsPalindrome(string s)
        {
            int length = s.Length - 1;

            for (int i = 0; i < length; i++)
            {
                if (!IsValidCharacter(s[i]))
                    continue;

                while (length != i)
                {
                    if (IsValidCharacter(s[length]))
                        break;
                    length--;
                }

                if (Char.ToLower(s[i]) != Char.ToLower(s[length]))
                {
                    return false;
                }
                else
                {
                    length--;
                }

            }


            return true;

        }

        public static bool IsValidCharacter(char c)
        {

            return (c >= 'a' && c <= 'z') || (c >= 'A' && c <= 'Z') || (c >= '0' && c <= '9');
        }

        //Reverse bits of a given 32 bits unsigned integer.
        public static uint Reverse32Bits(uint num)
        {
            uint result = 0;

            for (int i = 0; i < 32; i++)
            {

                result = result << 1;
                if (num % 2 == 0)
                {
                    result = result | 0;
                }
                else
                {
                    result = result | 1;
                }

                num /= 2;
            }

            return result;
        }

        /*
         * Implement int sqrt(int x).
          Compute and return the square root of x,
          where x is guaranteed to be a non-negative integer.
          Since the return type is an integer, the decimal digits are 
          truncated and only the integer part of the result is returned.  
         */
        //O(n) runtime solution
        public static int MySqrt(int x)
        {
            if (x < 1)
                return 0;

            if (x < 4)
                return 1;
            int i;
            for (i = 2; i <= x / 2; i += 2)
            {
                if (i * i > x || i * i < 0)
                    break;

                //Console.WriteLine(i*i);
            }

            if ((i - 1) * (i - 1) > x || (i - 1) * (i - 1) < 0)
            {
                return i - 2;
            }
            else
            {
                return i - 1;
            }
        }

        public static int MySqrt2(int x)
        {
            if (x < 1)
                return 0;

            if (x < 4)
                return 1;

            int left = 2;
            int right = x / 2;
            int mid = (left + right) / 2;

            while (true)
            {
                mid = (right + left) / 2;

                //not mid * mid > x to prevent overflow
                if (mid > x / mid)
                {
                    right = mid - 1;

                }
                else
                {
                    if (mid + 1 > x / (mid + 1))
                        return mid;
                    left = mid + 1;
                }

            }

        }

        /*FIZZ BUZZ
         * Write a program that outputs the string representation of numbers from 1 to n.
           But for multiples of three it should output “Fizz” instead of the number and for
           the multiples of five output “Buzz”. For numbers which are multiples of both three 
           and five output “FizzBuzz”.      
         */
        public IList<string> FizzBuzz(int n)
        {
            IList<string> result = new List<string>();

            for (int i = 1; i <= n; i++)
            {
                if (i % 3 == 0 && i % 5 == 0)
                    result.Add("FizzBuzz");
                else
                    if (i % 3 == 0)
                    result.Add("Fizz");
                else
                        if (i % 5 == 0)
                    result.Add("Buzz");
                else
                    result.Add(i.ToString());
            }

            return result;
        }

        /*FIRST UNIQUE CHARACTER IN A STRING
        Given a string, find the first non-repeating character in it and return it's index.
        If it doesn't exist, return -1.
        */
        public static int FirstUniqChar(string s)
        {
            Dictionary<char, int> dict = new Dictionary<char, int>();
            int indexToreturn = -1;
            for (int i = 0; i < s.Length; i++)
            {
                if (dict.ContainsKey(s[i]))
                {
                    dict[s[i]] += 1;
                }
                else
                {
                    dict.Add(s[i], 1);
                }
            }

            for (int i = 0; i < s.Length; i++)
            {
                if (dict[s[i]] == 1)
                {
                    indexToreturn = i;
                    break;
                }
            }
            return indexToreturn;
        }
        /* SUM OF TWO INTEGER
         * Calculate the sum of two integers a and b, 
         * but you are not allowed to use the operator + and -.
         */
        public static int GetSum(int a, int b)
        {
            if (a == 0)
                return b;
            if (b == 0)
                return a;

            int carry;

            while (b != 0)
            {
                carry = a & b;
                a = a ^ b;

                b = carry << 1;
            }

            return a;
        }
        /* INTERSECTION OF TWO ARRAY II
        Given two arrays, write a function to compute their intersection.
        */

        public static int[] Intersect(int[] nums1, int[] nums2)
        {
            Array.Sort(nums1);
            Array.Sort(nums2);

            List<int> results = new List<int>();

            int i = 0;
            int j = 0;

            while (i < nums1.Length && j < nums2.Length)
            {
                if (nums1[i] == nums2[j])
                {
                    results.Add(nums1[i]);
                    i++;
                    j++;
                }
                else
                {
                    if (nums1[i] < nums2[j])
                        i++;
                    else
                        j++;
                }
            }

            return results.ToArray();
        }
        /* REVERSE STRING
         * Write a function that reverses a string.
         * The input string is given as an array of characters char[].      
         * */
        public static void ReverseString(char[] s)
        {
            int last = s.Length - 1;
            int i = 0;

            char temp;
            while (i < s.Length / 2)
            {
                temp = s[i];
                s[i] = s[last];
                s[last] = temp;
                last--;
                i++;
            }
        }

        //POWER OF THREE
        public static bool IsPowerOfThree(int n)
        {
            if (n == 0)
                return false;
            if (n == 1)
                return true;

            while (n > 3)
            {
                n /= 3;
            }

            if (n == 3)
                return true;

            return false;
        }


        /*MOVE ZEROES
        Given an array nums, write a function to move all 0's to the end of 
        it while maintaining the relative order of the non-zero elements.
        */
        public void MoveZeroes(int[] nums)
        {
            for (int i = 0; i < nums.Length - 1; i++)
            {
                if (nums[i] != 0)
                    continue;

                for (int j = i + 1; j < nums.Length; j++)
                {
                    if (nums[j] == 0)
                    {
                        continue;
                    }
                    else
                    {
                        int temp = nums[i];
                        nums[i] = nums[j];
                        nums[j] = temp;
                        break;
                    }
                }
            }
        }

        //MISSING NUMBER (not optimal)
        /*
        Given an array containing n distinct numbers taken from 0, 1, 2, ..., n,
        find the one that is missing from the array.
        */
        public int MissingNumber(int[] nums)
        {
            Array.Sort(nums);
            if (nums[0] != 0)
                return 0;
            for (int i = 0; i < nums.Length - 1; i++)
            {
                if (nums[i] + 1 != nums[i + 1])
                {
                    return nums[i + 1] - 1;
                }
            }

            return nums[nums.Length - 1] + 1;
        }

        //MISSING NUMBER (optimal)
        /*
        Given an array containing n distinct numbers taken from 0, 1, 2, ..., n,
        find the one that is missing from the array.
        */
        public static int MissingNumber2(int[] nums)
        {
            int sum = nums.Length * (nums.Length + 1) / 2; //1 + 2 + 3 +.. + n = n(n+1)/2

            for (int i = 0; i < nums.Length; i++)
            {
                sum -= nums[i];
            }

            return sum;
        }


        //ANAGRAM
        public static bool IsAnagram(string s, string t)
        {
            if (s.Length != t.Length)
                return false;

            Dictionary<char, int> dict = new Dictionary<char, int>();

            for (int i = 0; i < s.Length; i++)
            {
                if (!dict.ContainsKey(s[i]))
                {
                    dict.Add(s[i], 1);
                }
                else
                {
                    dict[s[i]]++;
                }
            }

            for (int j = 0; j < t.Length; j++)
            {
                if (!dict.ContainsKey(t[j]))
                    return false;
                else
                {
                    if (dict[t[j]] > 1)
                        dict[t[j]]--;
                    else
                    {
                        dict.Remove(t[j]);

                    }
                }
            }

            return true;
        }


        //PALINDROME LINKED LIST
        public bool IsPalindrome(ListNode head)
        {
            if (head == null)
                return true;
            ListNode f = head, s = head, prev = null;
            while (f != null && f.next != null)
            {
                prev = s;
                s = s.next;
                f = f.next.next;
            }

            //cut the list in half
            if (prev != null)
                prev.next = null;

            f = head;

            //reverse the second of of the list
            ListNode newHead = reverse(s);

            while (newHead != null && f != null)
            {
                if (newHead.val != f.val)
                    return false;
                newHead = newHead.next;
                f = f.next;
            }
            return true;

        }

        private ListNode reverse(ListNode root)
        {
            ListNode prev = null, next = null, temp = root;
            while (temp != null)
            {
                next = temp.next;
                temp.next = prev;
                prev = temp;
                temp = next;
            }
            return prev;
        }

        //MIDDLE OF THE LINED LIST
        //Given a non-empty, singly linked list with head node head, 
        //return a middle node of linked list.
        //If there are two middle nodes, return the second middle node.
        public static ListNode MiddleNode(ListNode head)
        {

            ListNode fast = head;

            while (fast != null && fast.next != null)
            {
                head = head.next;
                fast = fast.next.next;
            }

            return head;
        }

        public static ListNode ReverseList(ListNode head)
        {
            if (head == null)
                return head;

            ListNode temp = head;
            ListNode pre = null;
            ListNode curr = head;

            while (curr != null)
            {
                temp = curr.next;
                curr.next = pre;
                pre = curr;
                curr = temp;
            }

            return pre;
        }

        //REMOVE DUPLICATES FROM SORTED LIST
        public static ListNode DeleteDuplicates(ListNode head)
        {
            ListNode nex = null;
            ListNode curr = head;

            if (head == null)
                return head;

            while (curr != null && curr.next != null)
            {
                if (curr.val == curr.next.val)
                {
                    nex = curr.next.next;
                    curr.next = nex;
                }
                else
                {
                    curr = curr.next;
                }
            }

            return head;
        }

        //LINKED LIST CYCLE
        public static bool HasCycle(ListNode head)
        {
            if (head == null)
                return false;

            ListNode slow = head;
            ListNode fast = head;

            while (fast != null && fast.next != null)
            {
                slow = slow.next;
                fast = fast.next.next;

                if (slow == fast)
                    return true;

            }

            return false;
        }


        //REMOVE LINKED LIST ELEMENTS
        public static ListNode RemoveElements(ListNode head, int val)
        {
            if (head == null)
            {
                return head;
            }

            ListNode curr = head;
            ListNode next = head;
            ListNode pre = head;

            while (curr != null)
            {
                //move the head forward
                if (head.val == val)
                    head = head.next;

                next = curr.next;

                if (curr.val == val)
                {
                    curr.next = null;

                    if (next != null)
                        pre.next = next;
                    else
                        pre.next = null;

                }
                else
                {
                    pre = curr;
                }

                curr = next;
            }

            return head;
        }

        //CONTAINS DUPPLICATE
        /*
        Given an array of integers, find if the array contains any duplicates.
        Your function should return true if any value appears at least twice in the array,
        and it should return false if every element is distinct.
        */
        public bool ContainsDuplicate(int[] nums)
        {
            Dictionary<int, int> dict = new Dictionary<int, int>();

            for (int i = 0; i < nums.Length; i++)
            {
                if (!dict.ContainsKey(nums[i]))
                    dict.Add(nums[i], i);
                else
                    return true;
            }

            return false;
        }

        //HOUSE ROBBER
        /*
        You are a professional robber planning to rob houses along a street.
        Each house has a certain amount of money stashed, the only constraint 
        stopping you from robbing each of them is that adjacent houses have
        security system connected and it will automatically contact the police 
        if two adjacent houses were broken into on the same night.

        Given a list of non-negative integers representing the amount of money 
        of each house, determine the maximum amount of money you can rob tonight 
        without alerting the police.
        */
        public int Rob(int[] nums)
        {
            int odd = 0;
            int even = 0;
            for (int i = 0; i < nums.Length; i++)
            {
                if (i % 2 == 0)
                {
                    even = Math.Max(even + nums[i], odd);
                }
                else
                {
                    odd = Math.Max(odd + nums[i], even);
                }
            }
            return Math.Max(even, odd);
        }



    }//close program class
}//close name space
