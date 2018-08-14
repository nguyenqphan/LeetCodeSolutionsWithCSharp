﻿using System;
using System.Collections.Generic;

namespace LeetCode
{
    class Program
    {
        static void Main(string[] args)
        {

            string[] strs = new string[]{"flower", "flow", "flight"};
            string[] strs2 = new string[] { "Sun", "Mon", "Tue", "Wed", "Thu", "Fri", "Sat"};

            Console.WriteLine(LongestCommonPrefix(strs));
            Console.WriteLine(LongestCommonPrefix(strs2));
         
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
        public static int RomanToInt(string s) {
        
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
        for(int i = 0; i < roman.Length; i++)
        {
            num += dic[roman[i]] ;
            
            if(temp < dic[roman[i]])
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
    }


}
