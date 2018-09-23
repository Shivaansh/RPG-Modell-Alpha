using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LevelManager : MonoBehaviour
{

    public void LoadLevel(string name)
    {//public because it needs to be called outside the class as well
        Debug.Log("Level Load requested for: " + name);
        //HitCount.brickCount=0;
        Application.LoadLevel(name);

    }
    public void QuitRequest()
    {
        Debug.Log("I want to quit!");
        Application.Quit();
    }

    public void LoadNextLevel()
    {
        //HitCount.brickCount=0;
        Application.LoadLevel(Application.loadedLevel + 1);

    }

    public void BrickDestroyed()
    {
        //if(HitCount.brickCount<=0){
        LoadNextLevel();

    }
}

