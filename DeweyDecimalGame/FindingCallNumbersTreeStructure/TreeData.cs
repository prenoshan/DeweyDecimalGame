using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeweyDecimalGame
{
    public class TreeData
    {
        
        //instance of dewey decimal data class which reads data from text file
        private DeweyDecimalData deweyDecimalData = new DeweyDecimalData();

        //arrays to store each node row of the tree
        private TreeNode<string>[] firstTreeNodeRow = new TreeNode<string>[10];

        private TreeNode<string>[] secondTreeNodeRow = new TreeNode<string>[20];

        private TreeNode<string>[] thirdTreeNodeRow = new TreeNode<string>[100];

        //method to use the data from the text file and store it in a tree structure
        public TreeNode<string> setData()
        {

            //root node of the tree
            TreeNode<string> root = new TreeNode<string>("root");

            //first node row of the tree
            deweyDecimalData.setFirstNodeList();

            //stores first row (top level call numbers) of the tree as child nodes of the parent root node (root)
            for (int i = 0; i < deweyDecimalData.getFirstNodeList().Count; i++)
            {

                firstTreeNodeRow[i] = root.AddChild(deweyDecimalData.getFirstNodeList()[i]);

            }

            //second node of the tree
            deweyDecimalData.setSecondNodeList();

            int secondNodeCount = 0;

            //stores second row (second level call numbers) of the tree as child nodes of the parent first row (top level call numbers) as shown previously
            for (int i = 0; i < deweyDecimalData.getSecondNodeList().Count; i++)
            {

                if(i % 2 == 0)
                {

                    secondTreeNodeRow[i] = firstTreeNodeRow[secondNodeCount].AddChild(deweyDecimalData.getSecondNodeList()[i]);

                    secondTreeNodeRow[i + 1] = firstTreeNodeRow[secondNodeCount].AddChild(deweyDecimalData.getSecondNodeList()[i + 1]);

                    secondNodeCount++;

                }

            }

            //third node of the tree
            deweyDecimalData.setThirdNodeList();

            int thirdNodeCount = 0;

            //stores third row (third level call numbers) of the tree as child nodes of the parent second row (second level call numbers) as shown previously
            for (int i = 0; i < deweyDecimalData.getThirdNodeList().Count; i++)
            {

                if (i % 5 == 0)
                {

                    thirdTreeNodeRow[i] = secondTreeNodeRow[thirdNodeCount].AddChild(deweyDecimalData.getThirdNodeList()[i]);

                    thirdTreeNodeRow[i + 1] = secondTreeNodeRow[thirdNodeCount].AddChild(deweyDecimalData.getThirdNodeList()[i + 1]);

                    thirdTreeNodeRow[i + 2] = secondTreeNodeRow[thirdNodeCount].AddChild(deweyDecimalData.getThirdNodeList()[i + 2]);

                    thirdTreeNodeRow[i + 3] = secondTreeNodeRow[thirdNodeCount].AddChild(deweyDecimalData.getThirdNodeList()[i + 3]);

                    thirdTreeNodeRow[i + 4] = secondTreeNodeRow[thirdNodeCount].AddChild(deweyDecimalData.getThirdNodeList()[i + 4]);

                    thirdNodeCount++;

                }

            }

            deweyDecimalData.getFirstNodeList().Clear();
            deweyDecimalData.getSecondNodeList().Clear();
            deweyDecimalData.getThirdNodeList().Clear();

            return root;

        }

    }
}
