using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeweyDecimalGame
{
    public class DeweyDecimalData
    {
        //lists for each node to be used by the tree data class
        private List<string> firstNodes = new List<string>();
        private List<string> secondNodes = new List<string>();
        private List<string> thirdNodes = new List<string>();

        public List<string> getFirstNodeList() { return firstNodes; }
        public List<string> getSecondNodeList() { return secondNodes; }
        public List<string> getThirdNodeList() { return thirdNodes; }

        public void setFirstNodeList()
        {

            //stream reader to read from the text file
            using (StreamReader reader = new StreamReader(Directory.GetCurrentDirectory() + @"\dewey.txt"))
            {

                //counter variables
                string line;
                int lineCount = 0;

                //while loop to iterate through the list
                while ((line = reader.ReadLine()) != null)
                {

                    //logic to get each top level heading to store as first node in the tree data class
                    if ((lineCount % 13) == 0)
                    {

                        firstNodes.Add(line);

                    }

                    lineCount++;

                }

                reader.Close();

            }

        }

        public void setSecondNodeList()
        {

            //stream reader to read from the text file
            using (StreamReader reader = new StreamReader(Directory.GetCurrentDirectory() + @"\dewey.txt"))
            {

                //counter variables
                string line;
                int lineCount = 0;

                //while loop to iterate through the list
                while ((line = reader.ReadLine()) != null)
                {

                    //switch case to get all second level headings for each top level heading, top level headings are static and not changing
                    switch (line)
                    {
                        case "000 General Knowledge":
                            var firstCat = File.ReadLines(Directory.GetCurrentDirectory() + @"\dewey.txt").ElementAt(lineCount + 1);
                            var secondCat = File.ReadLines(Directory.GetCurrentDirectory() + @"\dewey.txt").ElementAt(lineCount + 7);
                            secondNodes.Add(firstCat);
                            secondNodes.Add(secondCat);
                            break;
                        case "100 Philosophy and Psychology":
                            var thirdCat = File.ReadLines(Directory.GetCurrentDirectory() + @"\dewey.txt").ElementAt(lineCount + 1);
                            var fourthCat = File.ReadLines(Directory.GetCurrentDirectory() + @"\dewey.txt").ElementAt(lineCount + 7);
                            secondNodes.Add(thirdCat);
                            secondNodes.Add(fourthCat);
                            break;
                        case "200 Religion":
                            var fifthCat = File.ReadLines(Directory.GetCurrentDirectory() + @"\dewey.txt").ElementAt(lineCount + 1);
                            var sixthCat = File.ReadLines(Directory.GetCurrentDirectory() + @"\dewey.txt").ElementAt(lineCount + 7);
                            secondNodes.Add(fifthCat);
                            secondNodes.Add(sixthCat);
                            break;
                        case "300 Social Sciences":
                            var seventhCat = File.ReadLines(Directory.GetCurrentDirectory() + @"\dewey.txt").ElementAt(lineCount + 1);
                            var eigthCat = File.ReadLines(Directory.GetCurrentDirectory() + @"\dewey.txt").ElementAt(lineCount + 7);
                            secondNodes.Add(seventhCat);
                            secondNodes.Add(eigthCat);
                            break;
                        case "400 Languages":
                            var ninethCat = File.ReadLines(Directory.GetCurrentDirectory() + @"\dewey.txt").ElementAt(lineCount + 1);
                            var tenthCat = File.ReadLines(Directory.GetCurrentDirectory() + @"\dewey.txt").ElementAt(lineCount + 7);
                            secondNodes.Add(ninethCat);
                            secondNodes.Add(tenthCat);
                            break;
                        case "500 Science":
                            var eleventhCat = File.ReadLines(Directory.GetCurrentDirectory() + @"\dewey.txt").ElementAt(lineCount + 1);
                            var twelfthCat = File.ReadLines(Directory.GetCurrentDirectory() + @"\dewey.txt").ElementAt(lineCount + 7);
                            secondNodes.Add(eleventhCat);
                            secondNodes.Add(twelfthCat);
                            break;
                        case "600 Technology":
                            var thirteenthCat = File.ReadLines(Directory.GetCurrentDirectory() + @"\dewey.txt").ElementAt(lineCount + 1);
                            var fourteenthCat = File.ReadLines(Directory.GetCurrentDirectory() + @"\dewey.txt").ElementAt(lineCount + 7);
                            secondNodes.Add(thirteenthCat);
                            secondNodes.Add(fourteenthCat);
                            break;
                        case "700 Arts and Recreation":
                            var fifteenthCat = File.ReadLines(Directory.GetCurrentDirectory() + @"\dewey.txt").ElementAt(lineCount + 1);
                            var sixteenthCat = File.ReadLines(Directory.GetCurrentDirectory() + @"\dewey.txt").ElementAt(lineCount + 7);
                            secondNodes.Add(fifteenthCat);
                            secondNodes.Add(sixteenthCat);
                            break;
                        case "800 Literature":
                            var seventeenthCat = File.ReadLines(Directory.GetCurrentDirectory() + @"\dewey.txt").ElementAt(lineCount + 1);
                            var eighteenthCat = File.ReadLines(Directory.GetCurrentDirectory() + @"\dewey.txt").ElementAt(lineCount + 7);
                            secondNodes.Add(seventeenthCat);
                            secondNodes.Add(eighteenthCat);
                            break;
                        case "900 History and Geography":
                            var nineteenthCat = File.ReadLines(Directory.GetCurrentDirectory() + @"\dewey.txt").ElementAt(lineCount + 1);
                            var twentythCat = File.ReadLines(Directory.GetCurrentDirectory() + @"\dewey.txt").ElementAt(lineCount + 7);
                            secondNodes.Add(nineteenthCat);
                            secondNodes.Add(twentythCat);
                            break;
                        default:
                            break;
                    }

                    lineCount++;

                }

                reader.Close();

            }

        }

        public void setThirdNodeList()
        {

            //stream reader to read from the text file
            using (StreamReader reader = new StreamReader(Directory.GetCurrentDirectory() + @"\dewey.txt"))
            {

                //counter variables
                string line;
                int lineCount = 0;

                //loop to iterate through text file
                while ((line = reader.ReadLine()) != null)
                {

                    for (int i = 0; i < secondNodes.Count; i++)
                    {

                        //gets the first 5 dewey decimal codes from the text file for the first third level heading
                        if (line.Equals(secondNodes[i]) && i % 2 == 0)
                        {
                            var firstFive = File.ReadLines(Directory.GetCurrentDirectory() + @"\dewey.txt").Skip(lineCount + 1).Take(5);

                            //adds all third level dewey decimal codes to list
                            foreach (var firstDeweySelection in firstFive)
                            {

                                thirdNodes.Add(firstDeweySelection);

                            }

                        }

                        //gets the first 5 dewey decimal codes from the text file for the second third level heading
                        else if (line.Equals(secondNodes[i]) && i % 2 != 0)
                        {
                            var secondFive = File.ReadLines(Directory.GetCurrentDirectory() + @"\dewey.txt").Skip(lineCount + 1).Take(5);

                            //adds all third level dewey decimal codes to list
                            foreach (var secondDeweySelection in secondFive)
                            {

                                thirdNodes.Add(secondDeweySelection);

                            }

                        }


                    }

                    lineCount++;

                }

                reader.Close();

            }

        }

    }
}
