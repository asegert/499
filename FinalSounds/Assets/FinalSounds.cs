﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

//[RequireComponent(typeof(AudioSource))]
public class FinalSounds: MonoBehaviour {
	
	//Arrays to hold Picture and Letter Cues
	public Texture[]pictureLetters;
	public Texture[]fontLetters;
	
	//For Word Storage
	public Texture[]Words1;
	public int prev;
	
	//Border Image
	public Texture border;
	//Arrays to hold Sounds for Words and Individual Letters
	public AudioClip[]wordSounds;
	public AudioClip[]letterSounds;
	//Yay sounds
	public AudioClip[]Yay;
	//Nay sounds
	public AudioClip[]Nay;
	//Audio Source
	public AudioSource playAud;
	//Random Number to Choose Picture Cue
	public int ranDisplay;
	//Check value to check if a new Word needs to be generated
	public int newWord = -1;
	//Holds the four Options to match
	public int[] letterOptions = new int[4];
	//Boolean to catch and hold until second cue has played
	public bool letter;
	//Boolean to store correctness
	public bool respons;
	//Level indicator
	public int level;
	//Boolean values for the four buttons to indicate which has been selected
	public bool b1;
	public bool b2;
	public bool b3;
	public bool b4;
	//Button to skip question
	public bool b5;
	//Button to replay sound
	public bool b6;
	//Boolean to check itchy mode
	public bool itchyMode;
	//Array to store whether or not certain letters are being used
	public bool[]usable;
	//Holds alphabet value (0-25)
	public int indeX;
	//Checks if congrat audio should be played 0=no, 1=yay sound, 2=nay sound
	public int congrat;
	//Keeps track of remaining rounds
	public int rounds;
	//keeps track of negative score points
	public int score=0;
	//Holds Skip Icon Texture
	public Texture skip;
	//Holds replay Icon Texture
	public Texture replay;
	
	//Runs at Startup
	void Start()
	{
		//Initializes usable letters
		
		//Initializes level
		level = 1;
		//Initializes Itchy Mode
		itchyMode = true;
		//Initalize congrat
		congrat = 0;
		
		//Initializes AudioSource
		playAud=gameObject.AddComponent<AudioSource> ();
		//Initializes ranDisplay in order to initialize prev in SetRandom();
		ranDisplay = -1;
		//Sets ranDisplay
		SetRandom ();
	}
	
	//Creates Buttons
	void OnGUI()
	{
		//Holds new Word Texture
		Texture temp = Words1 [ranDisplay];
		//Checks if a new Word is necessary, if so play the corresponding sound
		if (newWord == 0) {
			playSound(wordSounds [ranDisplay],0.8f, 1);
			newWord = 1;
		}
		//Replays sound if image is pressed
		if (b6 == true) {
			playSound(wordSounds [ranDisplay],0.8f, 1);
			b6=false;
		}
		//Checks if user wants to skip
		if (b5 == true) {
			score++;
			rounds++;
			b5=false;
			SetRandom();
		}
		//Option Buttons b1-b4
		if (itchyMode == true) {
			b1 = GUI.Button (new Rect (340, 25, 125, 125), pictureLetters[letterOptions[0]]);
			b2 = GUI.Button (new Rect (855, 25, 125, 125), pictureLetters[letterOptions[1]]);
			b3 = GUI.Button (new Rect (340, 535, 125, 125), pictureLetters[letterOptions[2]]);
			b4 = GUI.Button (new Rect (855, 540, 125, 125), pictureLetters[letterOptions[3]]);
		} else {
			b1 = GUI.Button (new Rect (340, 25, 125, 125), fontLetters[letterOptions[0]]);
			b2 = GUI.Button (new Rect (855, 25, 125, 125), fontLetters[letterOptions[1]]);
			b3 = GUI.Button (new Rect (340, 535, 125, 125), fontLetters[letterOptions[2]]);
			b4 = GUI.Button (new Rect (855, 540, 125, 125), fontLetters[letterOptions[3]]);
		}
		
		b5 = GUI.Button (new Rect (1200, 575, 150, 100), skip);
		//Creates Word Image
		b6 = GUI.Button(new Rect(460, 150, 400, 400), replay);
		GUI.DrawTexture(new Rect(460, 150, 400, 400), temp, ScaleMode.ScaleToFit, true, 1.0F);
		//Creates surrounding border
		GUI.DrawTexture(new Rect(460, 150, 400, 400), border, ScaleMode.ScaleToFit, true, 1.0F);
	}
	
	//Sets Random Location in Word List for the selected word
	void SetRandom()
	{
		prev = ranDisplay;
		ranDisplay = Random.Range (0, 72);
		//index (0-25) of alphabet
		indeX = ranDisplay % 6;
		//Checks if valid
		if (usable [indeX] == false) {
			SetRandom ();
		}
		//Indicates a new Word is ready to be displayed
		newWord = 0;
		//Creates new Letter options for users to choose
		SetLetOptions ();
	}
	
	//Creates new Letter options for users to choose
	void SetLetOptions()
	{
		//Randomly generates options
		int i = 0;
		int j;
		//Values for already selected values
		int x=-1;
		int y=-1;
		int z=-1;
		
		while (i<3) {
			j = Random.Range (0, 12);
			//Checks that value is not the same as correct answer
			if(j!=indeX)
			{
				if(i==0)
				{
					x=j;
					i++;
				}
				else if(i==1&&j!=x)
				{
					y=j;
					i++;
				}
				else if(i==2)
					if(j!=x&&j!=y)
				{
					z=j;
					i++;
				}
			}
		}
		//Sets a Random Place in the Array to move the correct answer to a random location
		j = Random.Range (0, 4);
		//Switches the value at the random location and the correct answer at location 0
		if (j == 1) {
			letterOptions [0] = x;
			letterOptions [1] = indeX;
			letterOptions [2] = y;
			letterOptions [3] = z;
		} else if (j == 2) {
			letterOptions [0] = y;
			letterOptions [1] = x;
			letterOptions [2] = indeX;
			letterOptions [3] = z;
		} else if (j == 3) {
			letterOptions [0] = z;
			letterOptions [1] = x;
			letterOptions [2] = y;
			letterOptions [3] = indeX;
		} else {
			letterOptions [0] = indeX;
			letterOptions [1] = x;
			letterOptions [2] = y;
			letterOptions [3] = z;
		}
		
	}
	
	//Checks that the correct answer was selected
	void checkCorrectness()
	{
		//Placeholder value
		int buttonClicked = 27;
		
		//Checks boolean values to determine which button was clicked
		if (b1 == true) {
			buttonClicked = 1;
		}
		if (b2 == true) {
			buttonClicked = 2;
		}
		if (b3 == true) {
			buttonClicked = 3;
		}
		if (b4 == true) {
			buttonClicked = 4;
		}
		//Resets Boolean Values
		b1=false;
		b2=false;
		b3=false;
		b4=false;
		
		//Calls response to indicate if the button chosen results in true or false answers
		response (buttonClicked);
		//Plays the sound of the chosen letter
		playSound(letterSounds [letterOptions [buttonClicked - 1]],0.8f, 2);
	}
	
	/*Checks if audio is playing and stops it, then indicates with the respons value if 
	 answer is true or false*/
	void response(int buttonClicked)
	{
		if (playAud.isPlaying)
			playAud.Stop ();
		
		if (letterOptions [buttonClicked - 1] == indeX) {
			congrat=1;
			respons = true;
			rounds=rounds-1;
			//if rounds=0 escape
		} else {
			congrat=2;
			respons = false;
			score=1;
		}
	}
	
	//Plays Sounds
	void playSound(AudioClip sound, float vol, int version)
	{
		//Assigns an audio source	
		playAud = gameObject.AddComponent<AudioSource> ();
		
		//Assigns Clip and Volume then plays sound
		playAud.clip = sound;
		playAud.volume = vol;
		playAud.Play();
		
		/*If this is a letter sound (version: 2) and the letter chosen is correct 
		(respons: true) the letter value is true*/
		if (version == 2&&respons==true)
			letter = true;
	}
	
	// Update is called once per frame
	void Update () {
		
		//If any of the letter buttons have been clicked (true) check the buttons correctness
		if (b1 == true || b2 == true || b3 == true || b4 == true) {
			checkCorrectness();
		}
		/*If the audio has stopped playing and letter is true
		(a letter sound was played and the one chosen was correct) reset letter to false 
		and set a new random word using SetRandom()*/
		if(!playAud.isPlaying){
			if(congrat==1){
				congrat=Random.Range(0, 11);
				playSound(Yay[congrat], 0.8f, 1);
				congrat=0;
			}
			else if(congrat==2){
				congrat=Random.Range(0, 6);
				playSound(Nay[congrat], 0.8f, 1);
				congrat=0;
			}
			else if (congrat==0 && respons == true) {
				SetRandom ();
				respons=false;
			}
		}
	}
}