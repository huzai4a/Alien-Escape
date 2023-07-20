using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSwitch : MonoBehaviour
{

    public GameObject Player;
    public GameObject Leaderboard;
    public GameObject TitleScreen;
    public GameObject HowToPlay;
    public GameObject PlayButton;
    public GameObject LeaderButton;
    public GameObject HowToPlayButton;
    public GameObject BackButtonL;
    public GameObject BackButtonH;

    //for when the game is being played
    public GameObject playerScreenUI;
    // Start is called before the first frame update
    void Start()
    {
        TitleCam();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void PlayerCam()
    {
        Player.SetActive(true);
        Leaderboard.SetActive(false);
        TitleScreen.SetActive(false);
        HowToPlay.SetActive(false);

        PlayButton.SetActive(false);
        LeaderButton.SetActive(false);
        HowToPlayButton.SetActive(false);
        BackButtonL.SetActive(false);
        BackButtonH.SetActive(false);

        playerScreenUI.SetActive(true);
        Time.timeScale = 1;
    }

    public void LeaderCam()
    {
        Player.SetActive(false);
        Leaderboard.SetActive(true);
        TitleScreen.SetActive(false);
        HowToPlay.SetActive(false);

        PlayButton.SetActive(false);
        LeaderButton.SetActive(false);
        HowToPlayButton.SetActive(false);
        BackButtonL.SetActive(true);
        BackButtonH.SetActive(false);

        playerScreenUI.SetActive(false);
    }

    public void TitleCam()
    {
        Player.SetActive(false);
        Leaderboard.SetActive(false);
        TitleScreen.SetActive(true);
        HowToPlay.SetActive(false);

        PlayButton.SetActive(true);
        LeaderButton.SetActive(true);
        HowToPlayButton.SetActive(true);
        BackButtonL.SetActive(false);
        BackButtonH.SetActive(false);

        playerScreenUI.SetActive(false);
    }

    public void HowToPlayCam()
    {
        Player.SetActive(false);
        Leaderboard.SetActive(false);
        TitleScreen.SetActive(false);
        HowToPlay.SetActive(true);

        PlayButton.SetActive(false);
        LeaderButton.SetActive(false);
        HowToPlayButton.SetActive(false);
        BackButtonL.SetActive(false);
        BackButtonH.SetActive(true);

        playerScreenUI.SetActive(false);
    }
}
