using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeweyDecimalGame
{
    public class CallNumbers
    {

        private Random random = new Random();

        private List<double> numberList = new List<double>();
        private List<string> authorNameList = new List<string>();
        private char[] vowels = { 'A', 'E', 'I', 'O', 'U' };
        private List<char> alphabetList = new List<char>();

        //populates the alphabet array
        private void populateAlphabet()
        {

            for (char alpha = 'A'; alpha <= 'Z'; alpha++)
            {

                alphabetList.Add(alpha);

            }

        }

        //generates a random decimal within range
        private double RandomNumberBetween(double minValue, double maxValue)
        {
            var next = random.NextDouble();

            return minValue + (next * (maxValue - minValue));
        }

        public List<string> generateNumbers(int min, int max)
        {

            numberList.Clear();
            authorNameList.Clear();

            List<string> callNumberList = new List<string>();

            populateAlphabet();

            for (int i = 0; i < 10; i++)
            {

                //adds random decimals to the number list
                numberList.Add(RandomNumberBetween(min, max));

                //generates a random author name
                string name = "";

                int randomIndexAlpha = random.Next(alphabetList.Count);

                int randomIndexVowel = random.Next(vowels.Length);

                name += alphabetList[randomIndexAlpha];

                name += vowels[randomIndexVowel];

                randomIndexAlpha = random.Next(alphabetList.Count);

                name += alphabetList[randomIndexAlpha];

                authorNameList.Add(name);

            }

            //loop to generate the call numbers
            for (int i = 0; i < numberList.Count; i++)
            {

                string number = String.Format("{0:0.00}", numberList[i]).Replace(',', '.');

                if (numberList[i] > 99)
                {
                    callNumberList.Add(number + " " + authorNameList[i]);
                }

                else if (numberList[i] < 10)
                {
                    callNumberList.Add("00" + number + " " + authorNameList[i]);
                }

                else if (numberList[i] >= 10 && numberList[i] < 100)
                {
                    callNumberList.Add("0" + number + " " + authorNameList[i]);
                }

            }

            return callNumberList;

        }

    }
}
