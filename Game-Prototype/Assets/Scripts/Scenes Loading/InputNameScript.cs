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
    public static bool gameStart = false;
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
            Error_Msg.text = "Please enter name";
            Error_Msg.gameObject.SetActive(true);
        }
        else
        {
            username = Inputname.text;
            if(username.Length > 8)
            {
                Error_Msg.text = "Please enter name upto 8 characters";
                Error_Msg.gameObject.SetActive(true);
            }
            else
            {
                gameStart = true;
                SceneManager.LoadScene("Level Selector");
            }
            
        }
    }
}
