using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms;

public class ScoreBoardManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        AuthenticateUser();
    }
    
    public bool loginSuccessful;

    private string _leaderboardID = "com.unboxingsolutions.RelaxedLegsHighScore";

    private void AuthenticateUser()
    {
        Social.localUser.Authenticate((bool success) => {
            if(success)
            {
                loginSuccessful = true;

                Debug.Log("success");

            } else
            {

                Debug.Log("unsuccessful");

            }
            // handle success or failure
        });
    }
    
    
    public void PostScoreOnLeaderBoard(int myScore)
    {
        if(loginSuccessful)
        {
            Social.ReportScore(myScore, _leaderboardID, (bool success) => {
                if (success)
                {
                    Debug.Log("Successfully uploaded");            
                }
                // handle success or failure
            });
        } else
        {
            Social.localUser.Authenticate((bool success) => {

                if(success)
                {
                    loginSuccessful = true;

                    Social.ReportScore(myScore ,_leaderboardID, (bool successful) => {
                        // handle success or failure
                    });
                } else
                {
                    Debug.Log("unsuccessful");

                }
                // handle success or failure
            });
        }
    }

    public void ShowLeaderBord()
    {
        Social.ShowLeaderboardUI();
    }
}
