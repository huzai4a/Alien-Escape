using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AdjustTimes : MonoBehaviour
{
    public Text names;
    public Text times;
    string[] Names = System.IO.File.ReadAllLines("Assets\\Scripts\\LeaderboardNames.txt");
    string[] Times = System.IO.File.ReadAllLines("Assets\\Scripts\\LeaderboardTimes.txt");
    string[,] Leaderboard = new string[System.IO.File.ReadAllLines("Assets\\Scripts\\LeaderboardNames.txt").Length, 2];
    public CameraSwitch CameraSwitch;
    public GameObject NameInput;
    public GameObject backBut;

    //chooses whether we won or lost
    public GameManagerScript gameManager;

    //this boolean is used when waiting for the enter press when a user gets a high score
    private bool winOrLoseUpdate;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < Names.Length; i++)
        {
            Leaderboard[i, 0] = Names[i];
            Leaderboard[i, 1] = Times[i];
            names.text += Leaderboard[i, 0] + "\n";
            times.text += Leaderboard[i, 1] + "\n";
        }
    }

    public string[,] InsertionSort(string[,] inputArray)
    {
        for (int i = 0; i < inputArray.Length/2 - 1; i++)
        {
            for (int j = i + 1; j > 0; j--)
            {
                if (int.Parse(inputArray[j - 1, 1]) < int.Parse(inputArray[j, 1]))
                {
                    string temp = inputArray[j - 1, 1];
                    inputArray[j - 1, 1] = inputArray[j, 1];
                    inputArray[j, 1] = temp;

                    string temp2 = inputArray[j - 1, 0];
                    inputArray[j - 1, 0] = inputArray[j, 0];
                    inputArray[j, 0] = temp2;
                }
            }
        }
        return inputArray;
    }

    // Update is called once per frame
    void Update()
    {
        /*if (Input.GetKeyDown(KeyCode.Z))
        {
            CameraSwitch.PlayerCam();
        }
        if (Input.GetKeyDown(KeyCode.C))
        {
            CameraSwitch.LeaderCam();
        }
        if (Input.GetKeyDown(KeyCode.X))
        {
            CameraSwitch.TitleCam();
        }
        if (Input.GetKeyDown(KeyCode.Enter))
        {
            LeaderBOnGameEnd(50);
        }*/

        if (Input.GetKeyDown(KeyCode.Return))
        {
            if (winOrLoseUpdate)
            {
                gameManager.win();
            }
            else
            {
                //indication of game ending
                gameManager.gameOver();
            }
        }
    }

    public void LeaderBOnGameEnd(int score, bool winOrLose)
    {
        //if on leaderboard
        if (score > int.Parse(Leaderboard[9, 1]))
        {
            //allows the update function to know the correct redirection when user 
            //finishes inputting their name
            winOrLoseUpdate = winOrLose;

            //Switches the camera to the leaderboard
            CameraSwitch.LeaderCam();
            //activates the name input box, hides the leaderboard back button
            NameInput.SetActive(true);
            backBut.SetActive(false);
            Leaderboard[9, 1] = "" + score;
            //otherwise the score wasn't high enough to make the leaderboard
        }
        else
        {
            //boolean sent in determines whether they won or lost
            if (winOrLose)
            {
                gameManager.win();
            }
            else
            {
                //indication of game ending
                gameManager.gameOver();
            }
        }
    }

    public void ProcessScore(string name)
    {
        Leaderboard[9, 0] = name;

        InsertionSort(Leaderboard);

        names.text = "";
        times.text = "";

        for (int i = 0; i < Names.Length; i++)
        {
            Names[i] = Leaderboard[i, 0];
            Times[i] = Leaderboard[i, 1];
            names.text += Leaderboard[i, 0] + "\n";
            times.text += Leaderboard[i, 1] + "\n";
        }

        System.IO.File.WriteAllLines("Assets\\Scripts\\LeaderboardNames.txt", Names);
        System.IO.File.WriteAllLines("Assets\\Scripts\\LeaderboardTimes.txt", Times);

        NameInput.SetActive(false);
        backBut.SetActive(true);
    }
}
