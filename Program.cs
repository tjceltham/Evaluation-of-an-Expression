using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.Collections;
namespace ConsoleApp2
{
    class Program
    {
        static void Main(string[] args)
        {
            //Evaluate("  1 + 2 ");
           // Console.WriteLine(calculate("3 * (4 + (2 / 3) * 6 - 5)"));
           Console.WriteLine(calculate(" 123 - (4 ^ (3 - 1) * 8 - 8 / (1 + 1) * (3 - 1))"));
           
            //Console.WriteLine(calculate("3 * (4 +       (2 / 3) * 6 - 5)"));
            //Console.WriteLine(calculate("3 + 5 * 2"));


            //Console.WriteLine(calculate("5 - 3 * 8 / 8"));
            Console.ReadLine();
        }

        public static double calculate(string expression)
        {
            List<string> postfix = ToPostFix(expression);
            Stack s = new Stack();
            double a;
            double b;
        
            foreach (var ch in postfix)
            {               
                if (isNumeric(ch))
                {
                    s.Push(ch);
                }

                if (ch == "+" || ch == "-" || ch == "/" || ch == "*" || ch =="^")
                {
                    a = Convert.ToDouble(s.Pop());
                    b = Convert.ToDouble(s.Pop());
                    if (ch == "-")
                    {
                        s.Push(b - a);
                    }
                    if (ch == "+")
                    {
                        s.Push(b + a);
                    }
                    if (ch == "/")
                    {
                        s.Push(b / a);
                    }
                    if (ch == "*")
                    {
                        s.Push(b * a);
                    }
                    if (ch=="^")
                    {
                        s.Push(Math.Pow(b,a));
                    }
                }


            }
  
            return Convert.ToDouble(s.Pop());
        }

        public static Boolean isNumeric(string num)
        {
           Boolean canConvert=false;
           decimal numC;
           canConvert = decimal.TryParse(num, out numC);
           return canConvert;
        }
            
        public static List<string> SplitMaths(string expression)
        {
            List<string> express = new List<string>();
            int lastIndex=0;

            for(int x = 0; x< expression.Length; x++)
            {
                if((expression[x] == '-' && x!=0 && isNumeric(""+expression[x-1])) ||
                   expression[x] == '+' || 
                   expression[x] == '/' ||
                   expression[x] == '*' ||
                   expression[x]== '^' || 
                   expression[x] == ')'
                   )
                {
                    if(isNumeric(expression.Substring(lastIndex, x - lastIndex))) express.Add(expression.Substring(lastIndex, x-lastIndex));
                    express.Add(expression.Substring(x,1));
                    lastIndex = x + 1;
                }
                if( expression[x] == '(')
                {
                    express.Add(expression.Substring(x, 1));
                    lastIndex = x + 1;
                }

            }
            if(expression[expression.Length-1] !=')') express.Add(expression.Substring(lastIndex, expression.Length - lastIndex));
            return express;
        }
        static int precedence(char c)
        {

            switch (c)
            {

                case '+':

                case '-':

                    return 1;

                case '*':

                case '/':

                    return 2;

                case '^':

                    return 3;

            }

            return -1;

        }
        public static List<string> ToPostFix(string expression)
        {

          
           
            List<string> postFixExpression = new List<string>();          
            Stack s = new Stack();
            expression = Regex.Replace(expression, @"\s+", "");       
            List<string> exp = SplitMaths(expression);

            foreach (String ch in exp)
            {

                if(isNumeric(ch))
                {
                    postFixExpression.Add(ch);
                }

                if (ch == "(")
                {
                    s.Push(ch);
                }
                if (ch == ")")
                {
                    while (s.Count!=0 && s.Peek().ToString() != "(")
                    {
                        postFixExpression.Add(s.Pop().ToString());

                    }
                    s.Pop();
                }
                if (ch == "+" || ch == "-" || ch == "/" || ch == "*" || ch=="^")
                {
                    if (s.Count == 0 || s.Peek().ToString()=="(")
                    {
                        s.Push(ch);
                    }
                    else
                    {
                        while(s.Count!=0 
                              && s.Peek().ToString()!="("
           
                              && precedence(Convert.ToChar(ch)) <= precedence(Convert.ToChar(s.Peek())))
                        {
                          //  s.Pop();
                            postFixExpression.Add(s.Pop().ToString());
                        }
                        s.Push(ch);
                    }
                }                             
            }
            while (s.Count != 0)
            {
                postFixExpression.Add(s.Pop().ToString());
            }
            
            return postFixExpression;
        }
    }
}
