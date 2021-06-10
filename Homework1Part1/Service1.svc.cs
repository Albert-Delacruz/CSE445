using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace Homework1Part1
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
    public class Service1 : IService1
    {
        //Determine the number of Vowels in a string and returns the int count
        public int vowelCount(string vowel)
        {
            int total = 0;
            for (int i = 0; i < vowel.Length; i++)
            {
                if (vowel[i] == 'a')//check if index is a vowel, then increment
                {
                    total++;
                }
                else if (vowel[i] == 'e')
                {
                    total++;
                }
                else if (vowel[i] == 'i')
                {
                    total++;
                }
                else if (vowel[i] == 'o')
                {
                    total++;
                }
                else if (vowel[i] == 'u')
                {
                    total++;
                }
            }
            return total;
        }

        //Finds the number of Capitals in a string and returns the int count
        public int capitalCount(string capital)
        {
            int total = 0;
            for(int i = 0; i < capital.Length; i++)
            {
                if(Char.IsUpper(capital[i]))
                {
                    total++;//check if its a capital and increment
                }
            }
            return total;
        }
        //return the reverse the order of the input string
        public string reverseString(string input)
        {
            string reverse = "";
            for(int i = input.Length-1; i >=0; i--)
            {
                reverse += input[i];
            }
            return reverse;
        }
    }
}
