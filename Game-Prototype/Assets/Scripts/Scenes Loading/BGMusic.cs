using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMusic : MonoBehaviour
{
    GameObject[] musicObject;
    // Use this for initialization
     void Start () {
         musicObject = GameObject.FindGameObjectsWithTag ("Background Music");
         if (musicObject.Length == 1 ) {
                         GetComponent<AudioSource>().Play ();
         } else {
             for(int i = 1; i < musicObject.Length; i++){
                 Destroy(musicObject[i]);
             }
 
         }
             
 
     }
     
     // Update is called once per frame
     void Awake(){
         DontDestroyOnLoad (this.gameObject);
     }
    // public bool played = false;
    // [SerializeField] public AudioSource bgmusic;

    // void Awake()
    // {
    //     if(!played){
    //         GetComponent<AudioSource>().clip = bgmusic;
    //         GetComponent<AudioSource>().Play();
    //         played = true;
    //     }
    //     DontDestroyOnLoad(transform.gameObject);
    // }
}
