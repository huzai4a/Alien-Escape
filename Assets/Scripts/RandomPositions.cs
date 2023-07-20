using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices.ComTypes;
using UnityEngine;
using System.Linq;

public class RandomPositions : MonoBehaviour
{
    // The children of this gameobject will be positioned in Start method
    [SerializeField] public GameObject parent;

    // Start is called before the first frame update
    void Start()
    {
        //this array holds the randomly generated coordinates
        String [] positionsArray = new String[5]; 
        //this array holds the random numbers to ensure no repeats
        int[] ranNums = new int[5];
        //this integer holds the random number each loop through
        int tempRandom;
        //loop as long as the position array hasn't been filled
        for (int i = 0; i < positionsArray.Length; i++)
        {
            //gets a random number
            tempRandom = getRanNum(0, 15);
            //if the random number has already been chosen redo this loop runthrough
            if (ranNums.Contains(tempRandom))
            {
                i--;
            } else
            {
                //add this random number to the chosen lines
                ranNums[i] = tempRandom;
                // fill the current coordinate element i from line tempRandom of coordinates file
                positionsArray[i] = File.ReadLines(Application.dataPath + "/Scripts/Coordinates.txt").Skip(tempRandom).Take(1).First();
            }
        }

        setPositions(positionsArray);
    }

    public void setPositions (String [] posArray)
    {
        //Initializing variables for each of x, y, and z coordinates that will come from the file
        //splitArray will hold the strings for x and ybefore they're converted to floats
        //z can be set to 0 since its 2d and we won't need a different z ever
        float x;
        float y;
        float z = 0;
        String[] splitArray = new String[2];

        // set the positions of the children game objects
        for (var i = 0; i < parent.transform.childCount; i++)
        {
            //split the coordinate in element i of posArray into 3 strings
            splitArray = posArray[i].Split(',');
            //parse each string to make them the floats for x, y and z
            x = float.Parse(splitArray[0]);
            y = float.Parse(splitArray[1]);
            var child = parent.transform.GetChild(i);
            child.position = new Vector3(x, y, z);
        }
    }

    //This routine gets a random number between the range of
    //lowVal and highVal (both sent in), returning this value as an integer.
    private int getRanNum(int lowVal, int highVal)
    {
        System.Random randGen = new System.Random();

        //To generate a random integer value use this:
        int tempRandom = randGen.Next(lowVal, highVal);

        //returns the random integer
        return tempRandom;
    }
}
