using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadSceneName : MonoBehaviour
{
    public void ScenaMainMenu()
    {
        LoaderScene.Instance.LoadScene(ConstantGame.MAINMENU);
    }

    public void ScenaWorld()
    {
        LoaderScene.Instance.LoadScene(ConstantGame.WORLD);
    }
    
    public void ScenaMapDecoration()
    {
        LoaderScene.Instance.LoadScene(ConstantGame.MAPDECORATION);
    }

    public void ScenaCredit()
    {
        LoaderScene.Instance.LoadScene(ConstantGame.CREDITSCENE);
    }

    public void ScenaOptions()
    {
        LoaderScene.Instance.LoadScene(ConstantGame.OPTIONSMENU);
    }
}
