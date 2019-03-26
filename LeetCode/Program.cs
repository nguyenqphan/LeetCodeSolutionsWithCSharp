using System;
using System.Collections.Generic;
using System.Text;


namespace LeetCode
{
    class Program
    {
        static void Main(string[] args)
        {
    
            string myString = "nguyenphan";
            int result = FirstUniqChar(myString);
            Console.WriteLine(result);


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

            return (c >= 'a' && c <= 'z') || (c >= 'A' && c <= 'Z') || (c <= 0 && c >= 9);
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
    }//close program class
}//close name space
