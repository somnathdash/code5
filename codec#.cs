using System;
using System.Linq;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Text;
using System.Numerics;


public class Test
{
    public static int GetInt() => int.Parse(Console.ReadLine());
    public static long GetLong() => long.Parse(Console.ReadLine());
    
    public static int[] GetIntArray() =>  Console.ReadLine().Trim().Split(' ').Select(int.Parse).ToArray();
    public static long[] GetLongArray() =>  Console.ReadLine().Trim().Split(' ').Select(long.Parse).ToArray();
    
    public static string[] GetLines(int n)
    {
        var ans = new string[n];
        for(int i=0; i<n; i++)
        {
            ans[i] = Console.ReadLine();
        }
        return ans;
    }
	
	public static int Gcd(int a, int b) => b == 0 ? a : Gcd(b, a%b);
	public static int Gcd(int[] n) => n.Aggregate((a,b) => Gcd(a,b));

	public static void Main()
	{
    var t=GetInt();
    for(int i = 0; i<t; i++)
    {
        Solve();
    }
	}
	
	public static void Solve()
	{
	    string s = Console.ReadLine();
	    string ans = GetMex(s);
      Console.WriteLine(ans);
	}
	
	
	public static  Tail DetermineTail(string v)
        {
            if (v.All(c => c == '0'))
                return Tail.JustZeros;
            else if (v.All(c => c == '1'))
                return Tail.JustOnes;
            else if (v == "01")
                return Tail.ZeroOne;
            else if (v == "10")
                return Tail.OneZero;
            else if (v.Last() == '0')
                return Tail.OnesAndAZero;
            else
                return Tail.ZerosAndAOne;
        }
	
public enum Tail
        {
			JustOnes,
			JustZeros,
			OneZero,
			ZeroOne,
			OnesAndAZero,
			ZerosAndAOne
        }

				public static string GetMex(string s)
		{
			if (s.All(c => c == '1'))
				return "0";
				if (s.All(c => c == '0'))
				return "1";
			//split
			var ans = new List<string>();
      string cur = "";
      int firstOne = s.IndexOf("1");
      if(firstOne == s.Length-1)
      {
        return "10";
      }
			
				ans.Add("1");
				s = s.Substring(firstOne+1);
			foreach (char c in s)
			{
				cur += c;
				if (c != cur[0])
				{
					ans.Add(cur);
					cur = "";
				}
			}
			if (cur != "")
				ans.Add(cur);
            
            Tail tail = DetermineTail(ans.Last());

//            Console.WriteLine($@"
//Input string was:    1{s}
//    Parts:           {string.Join(" - ", ans)}
//Registered type is : {tail}.");

            //prepocces? yes !
            for (int i = ans.Count - 2; i > 0; i--)
            {
                if (ans[i] == "01" && ((ans[i + 1] == "0" && i < ans.Count-2)|| ans[i + 1].All(c => c == '1') || DetermineTail(ans[i + 1]) == Tail.OneZero || DetermineTail(ans[i + 1]) == Tail.OnesAndAZero || (i == ans.Count-2 && ans[i + 1] == "01")))
                {
                    //swap the 1 over
                    ans[i] = ans[i].Substring(0, ans[i].Length - 1);
                    ans[i+1] = "1" + ans[i+1];
                }
            }

//            Console.WriteLine($@"
//after preprocessing  :     {string.Join(" - ", ans)}");

            for (int i=1; i<ans.Count-1; i++)
            {
                var t = DetermineTail(ans[i]);
                if ( t == Tail.JustOnes && ans[i].Length > 1)
                {
                    ans[i] = "0";
                    //01 kann auch zu null werden, aber wohl nicht immer
                }
                else if (t == Tail.JustZeros && ans[i].Length > 1)
                    ans[i] = "1";
                else
                    ans[i] = ans[i].Last().ToString();
            }
//            Console.WriteLine($@"
//after decision :          {string.Join(" - ", ans)}");
            switch (tail)
            {
                case Tail.JustOnes:
                    ans[ans.Count - 1] = "0";
                    break;
                case Tail.JustZeros:
                    ans[ans.Count - 1] = "1";
                    break;
                case Tail.OneZero:
                case Tail.ZeroOne:
                case Tail.OnesAndAZero:
                    ans[ans.Count - 1] = "00";
                    ans[ans.Count - 1] = "00";
                    break;
                case Tail.ZerosAndAOne:
                    ans[ans.Count - 1] = "10";
                    break;
            }
//            Console.WriteLine($@"
//after setting tail :     {string.Join(" - ", ans)}");
            return string.Concat(ans);
        }
	
}
