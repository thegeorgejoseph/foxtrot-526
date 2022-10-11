using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class InputNameScript : MonoBehaviour
{
    public TMP_InputField Inputname;
    public TMP_Text Error_Msg;
    public static string username;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void onClickOKBtn()
    {
        
        Debug.Log(Inputname.text);
        if(Inputname.text == "")
        {
            Error_Msg.gameObject.SetActive(true);
        }
        else
        {
            username = Inputname.text;
            SceneManager.LoadScene("Level_1");
        }
    }
}
