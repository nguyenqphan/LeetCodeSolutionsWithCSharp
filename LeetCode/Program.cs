using System;
using System.Collections.Generic;
using System.Text;


namespace LeetCode
{
    class Program
    {
        static void Main(string[] args)
        {
           
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
        public static bool  IsSymmetric(TreeNode root)
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
 
            return Math.Max(l,r);
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

        static int[] TwoSum(int[] nums, int target)
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
          
            for(int i = 0; i < length; i++ )
            {
                if (!IsValidCharacter(s[i]))
                    continue;

                while(length != i)
                {
                    if (IsValidCharacter(s[length]))
                        break;
                    length--;
                }

                if(Char.ToLower(s[i]) != Char.ToLower(s[length]))
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

            for(int i = 0; i < 32; i++)
            {

                result = result << 1;
                if(num % 2 == 0)
                {
                    result = result | 0;
                }
                else
                {
                    result = result | 1;
                }

                num /=  2;
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
            int right = x/2;
            int mid = (left + right) / 2;

            while(true)
            {
                mid =  (right + left) / 2;

                //not mid * mid > x to prevent overflow
                if (mid > x/mid)
                {
                    right = mid -1 ;
                   
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

            while(b != 0)
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

            while(i < nums1.Length && j < nums2.Length)
            { 
                if(nums1[i] == nums2[j])
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

            for(int i = 0; i < nums.Length; i++)
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
