using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ImageController : MonoBehaviour
{

    public Sprite[] gameImages; // instantiate an array of images as the current game picture
    public AudioClip[] wordSounds; // instantiate an array of sounds corresponding to their images
    private int rand;
    AudioSource playAudio;
    private CatchController cc = new CatchController(); // use this to access whether or not the game is currently in progress

    // Use this for initialization
    void Start()
    {
    }

    void Awake()
    {
        playAudio = GetComponent<AudioSource>();
        randomizeImage(); // call this on awake of the game to randomize the image being displayed

    }

    // Update is called once per frame
    void Update()
    {

    }

    public int getIndex()
    {
        return rand;
    }

    // Method that will grab an image from gameImages and display it for the user to match the letters to
    void randomizeImage()
    {
        rand = Random.Range(0, gameImages.Length);
        this.GetComponent<SpriteRenderer>().sprite = gameImages[rand];

    }

    public void OnMouseDown()
    {
        cc.setIsPlaying(false);
        playAudio.PlayOneShot(wordSounds[rand]); // replay the sound for the user if they touch the image
        StartCoroutine(returnToGame());

    }


    IEnumerator returnToGame()
    {
        yield return new WaitForSeconds(5.0f);
        cc.setIsPlaying(true); // return to playing the game
    }
}
