using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class Loader
{   
    public enum Scene{

        Level_0,
        Level_1,
        Level_2,
        Level_3,
        Level_4,
        Level_5,
        Level_6,
        Level_7,
        Level_8
    }
    public static void Load(Scene scene){
        SceneManager.LoadScene(scene.ToString());
    }
}
