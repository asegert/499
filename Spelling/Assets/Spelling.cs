using UnityEngine;
using System.Collections;

public class Spelling : MonoBehaviour {

	//Arrays to hold Picture and Letter Cues
	public Texture[]pictureLetters;
	public Texture[]fontLetters;
	//Arrays to hold Words
	public Texture[]Word;
	//Arrays to hold Spellings
	public int[]Spell1;
	public int[]Spell2;
	public int[]Spell3;
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
	//Indicates if answer was correct
	public bool respons = false;
	//True if pictures False if letters
	public bool picKeyBoard;
	//Boolean values for the skip and replay buttons
	public bool b1;
	public bool b2;
	//Checks if congrat audio should be played 0=no, 1=yay sound, 2=nay sound
	public int congrat;
	//Holds Skip Icon Texture
	public Texture skip;
	//Holds replay Icon Texture
	public Texture replay;
	//Keeps track of remaining rounds
	public int rounds;
	//Keeps track of level
	public int level;
	//keeps track of negative score points
	public int score=0;
	//Keeps track of letter position of word (1-3)
	public int letPos=0;
	//Stores letters to be displayed to make word
	public Texture let1;
	public Texture let2;
	public Texture let3;
	//Indicates first tier answered correctly
	public int one;
	//Indicates second tier answered correctly
	public int two;
	//Indicates third tier answered correctly
	public int three;

	// Use this for initialization
	void Start () {
		//Set level
		level = 1;
		//Checks if a new Word is necessary, if so play the corresponding sound
		if (newWord == 0) {
			//playSound(wordSounds [ranDisplay],0.8f);
			newWord = 1;
		}
		//Initializes AudioSource
		playAud=gameObject.AddComponent<AudioSource> ();
		//Indicates if a picture or font keyboard is to be used
		picKeyBoard = false;
		//Initialize congrat
		congrat = 0;
		//Initialize Word
		SetRandom();
	}


	void OnGUI() {
		b1 = GUI.Button (new Rect (1200, 575, 150, 100), skip);

		if (level == 1) {
			//Creates Word Image
			b2 = GUI.Button(new Rect(450, 50, 400, 380), replay);
			GUI.DrawTexture(new Rect(450, 50, 400, 250), Word[ranDisplay]);

			GUI.DrawTexture(new Rect(450, 300, 133, 133), let1);
			GUI.DrawTexture(new Rect(575, 300, 133, 133), let2);
			GUI.DrawTexture(new Rect(700, 300, 133, 133), let3);
		}
		else if (level == 2) {
			if(three==1)
			{
				//Creates Word Image
				b2 = GUI.Button(new Rect(450, 50, 400, 380), replay);
				GUI.DrawTexture(new Rect(450, 50, 400, 250), Word[ranDisplay]);
				
				GUI.DrawTexture(new Rect(450, 300, 133, 133), let1);
				GUI.DrawTexture(new Rect(575, 300, 133, 133), let2);
				GUI.DrawTexture(new Rect(700, 300, 133, 133), let3);
			}
			else
			{
				//Creates Word Image
				b2 = GUI.Button(new Rect(450, 170, 400, 133), replay);

				GUI.DrawTexture(new Rect(450, 170, 133, 133), let1);
				GUI.DrawTexture(new Rect(575, 170, 133, 133), let2);
				GUI.DrawTexture(new Rect(700, 170, 133, 133), let3);
			}
		}
		else if (level == 3) {
			//Creates Word Image
			b2 = GUI.Button(new Rect(450, 50, 400, 380), replay);
			GUI.DrawTexture(new Rect(450, 50, 400, 250), Word[ranDisplay]);

			if(one==1)
			{
				GUI.DrawTexture(new Rect(450, 300, 133, 133), let1);
			}
			if(two==1)
			{
				GUI.DrawTexture(new Rect(575, 300, 133, 133), let2);
			}
			if(three==1)
			{
				GUI.DrawTexture(new Rect(700, 300, 133, 133), let3);
			}
		}

		if (newWord == 0) {
			playSound (wordSounds [ranDisplay], 0.8f);
			newWord=-1;
		}

		int buttonSizeWidth = 55;
		int buttonSizeHeight = 70;
		int buttonSpacing = 3;
		int xOffset = 300;
		int yOffset = 450;
		int numCols = 12;
		int InDeX = 0;
		int numButtons = 26;
		float nextUse = 0f;
		float delay = 5f;
		//*****************************************************************************************
		Rect r = new Rect (0, 0, buttonSizeWidth, buttonSizeHeight); // rect for picture cues
		Rect r2 = new Rect (0, 0, buttonSizeWidth, buttonSizeHeight);
		
		//Loop through the amount of buttons user will have selected and assign a new Button object per texture.
		for (int i = 0; i < numButtons; i++) {
			if (i < 12) {
				r.x = xOffset + (i % numCols) * (buttonSizeWidth + buttonSpacing);
				r.y = yOffset + (int)Mathf.Floor ((float)i / numCols) * (buttonSizeHeight + buttonSpacing);
			}
			if (i < 22 && i > 11) {
				numCols = 10;
				r.x = xOffset + (i % numCols) * (buttonSizeWidth + buttonSpacing);
				r.y = yOffset + (int)Mathf.Floor ((float)i / numCols) * (buttonSizeHeight + buttonSpacing);
			}
			if (i > 21) {
				numCols = 8;
				r.x = xOffset + (i % numCols) * (buttonSizeWidth + buttonSpacing);
				r.y = yOffset + (int)Mathf.Floor ((float)i / numCols) * (buttonSizeHeight + buttonSpacing);
			}
			
		}
		for (int i = 0; i < numButtons; i++) {
			if (i < 12) {
				r2.x = xOffset + (i % 12) * (buttonSizeWidth + buttonSpacing);
				r2.y = yOffset + (int)Mathf.Floor ((float)i / 12) * (buttonSizeHeight + buttonSpacing);
			}
			if (i < 20 && i > 11) {
				r2.x = xOffset + (i % 10) * (buttonSizeWidth + buttonSpacing);
				r2.y = yOffset + (int)Mathf.Floor ((float)i / 10) * (buttonSizeHeight + buttonSpacing);
			}
			if (i > 19 && i < 26) {
				r2.x = (xOffset + 58) + (i % 9) * (buttonSizeWidth + buttonSpacing);
				r2.y = yOffset + (int)Mathf.Floor ((float)i / 9) * (buttonSizeHeight + buttonSpacing);
			}
			if (picKeyBoard == true && InDeX < 26) {
				if (GUI.Button (new Rect (r2), pictureLetters [InDeX]) == true) {
					int[]spellet=Spell1;
					if(letPos==2)
					{
						spellet=Spell2;
					}
					else if(letPos==3)
					{
						spellet=Spell3;
					}
					
					if (InDeX == spellet[ranDisplay]) {
						playSound(letterSounds [InDeX],0.8f);
						respons=true;
						if(letPos==1)
						{
							let1=pictureLetters[Spell1[ranDisplay]];
							one=1;
						}
						else if(letPos==2)
						{
							let2=pictureLetters[Spell2[ranDisplay]];
							two=1;
						}
						else if(letPos==3)
						{
							let3=pictureLetters[Spell3[ranDisplay]];
							three=1;
						}
						congrat=1;
					} else
					{
						playSound(letterSounds [InDeX],0.8f);
						congrat=2;
						score++;
					}
				}
				InDeX++;
			} else if (picKeyBoard == false && InDeX < 26) {
				if (GUI.Button (new Rect (r2), fontLetters [InDeX]) == true) { //object type = bool (determine whether user has clicked (true) or not (false)
					int[]spellet=Spell1;
					if(letPos==2)
					{
						spellet=Spell2;
					}
					else if(letPos==3)
					{
						spellet=Spell3;
					}

					if (InDeX == spellet[ranDisplay]) {
						playSound(letterSounds [InDeX],0.8f);
						respons=true;
						if(letPos==1)
						{
							let1=fontLetters[Spell1[ranDisplay]];
							one=1;
						}
						else if(letPos==2)
						{
							let2=fontLetters[Spell2[ranDisplay]];
							two=1;
						}
						else if(letPos==3)
						{
							let3=fontLetters[Spell3[ranDisplay]];
							three=1;
						}
						congrat=1;
					} else
					{
						playSound(letterSounds [InDeX],0.8f);
						congrat=2;
						score++;
					}
				}
				InDeX++;
			}
		}
	}
	//Sets Random Location in Word Array for the selected word
	void SetRandom()
	{
		//Sets first tier to false
		one = 0;
		//Sets second tier to false
		two = 0;
		//Sets third tier to false
		three = 0;
		//Reset Letter Position
		letPos = 1;
		//Stores index of previous word
		int prev = ranDisplay;
		//Sets new index(word)
		ranDisplay = Random.Range (0, 33);
		//Checks that new word is not the same as the previous word
		if (prev == ranDisplay)
			SetRandom ();
		//Indicates a new Word is ready to be displayed
		newWord = 0;
		//Increases rounds
		rounds++;
		//Sets letters for the word
		Texture[]letDisplay=fontLetters;
		if(picKeyBoard==false)
		{
			letDisplay=pictureLetters;
		}
		
		let1 = letDisplay [Spell1 [ranDisplay]];
		let2 = letDisplay [Spell2 [ranDisplay]];
		let3 = letDisplay [Spell3 [ranDisplay]];
	}
	
	//Plays Sounds
	void playSound(AudioClip sound, float vol)
	{
		if (playAud.isPlaying) {
			playSound(sound, vol);
		}
		//Assigns an audio source	
		playAud = gameObject.AddComponent<AudioSource> ();
		
		//Assigns Clip and Volume then plays sound
		playAud.clip = sound;
		playAud.volume = vol;
		playAud.Play();
	}
	
	// Update is called once per frame
	void Update () {
		//Skip function
		if (b1 == true) {
			b1 = false;
			SetRandom ();
		}
		//Replay function
		if (b2 == true) {
			b2 = false;
			playSound(wordSounds[ranDisplay], 0.8f);
		}
		/*If the audio has stopped playing and letter is true
		(a letter sound was played and the one chosen was correct) reset letter to false 
		and set a new random word using SetRandom()*/
		if(!playAud.isPlaying){
			if(congrat==1){
				congrat=Random.Range(0, 11);
				playSound(Yay[congrat], 0.8f);
				congrat=0;
			}
			else if(congrat==2){
				congrat=Random.Range(0, 6);
				playSound(Nay[congrat], 0.8f);
				congrat=0;
			}
			else if (congrat==0 && respons == true) {
				respons=false;
				if(letPos==3)
				{
					SetRandom ();
				}
				else{
					letPos++;
				}
			}
		}
	}
}
