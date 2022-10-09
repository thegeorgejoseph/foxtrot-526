using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class Loader
{   
    public enum Scene{

        Level_0,
        Level_1,
        Level_2
    }
    public static void Load(Scene scene){
        SceneManager.LoadScene(scene.ToString());
    }
}
