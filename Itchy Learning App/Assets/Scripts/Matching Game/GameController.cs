using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/***********************************************************************/
// NOTES TO SELF: things that need fixing:
// 1) center the bottom cards if row != 6 cards
/*********************************************************************/

public class GameController : MonoBehaviour {
    [SerializeField] private cardBack originalCard; // reference for the card in the scene
    [SerializeField] private cardBack originalCard2;


    [SerializeField] private Sprite[] letters; // array for the sprites assets to be added
    [SerializeField] private Sprite[] pictureCues;
    [SerializeField] private AudioClip[] soundClips;
    [SerializeField] private AudioClip[] congratulations;
    [SerializeField] private AudioClip openingMessage;
    private List<Sprite> userChoicePictures = new List<Sprite>(); // this will hold the id of the chosen letters to be shown in the game
    private List<Sprite> userChoiceLetters = new List <Sprite>();
    private List<AudioClip> userChoiceSounds = new List<AudioClip>();
	private List<cardBack> chosenPics = new List<cardBack> ();
	private List<cardBack> chosenLets = new List<cardBack>();
    [SerializeField] private letterSettings userSettings; // create an instance of letterSettings so we can retrieve the list of id's to be added
	private selectAll pickAll = new selectAll (); // initialize an instance for the selectAll class
    // Values used for positioning
    public int gridRows = 3; 
    public int gridCols = 4;
    public float offsetX = 1.5f;
    public float offsetY = 4f;

    private string letterType = "letter"; // use to assign a card type
    private string pictureType = "picture"; // use to assign a card type

    private cardBack firstReveal; // store the first card clicked
    private cardBack secondReveal; // store the second card clicked
    static bool allow = true; // boolean to store whether or not a user is allowed to pick the card they have chosen (ex, not allowed to pick a letter card first)

    private int score = 0; // using to debug for now, maybe later use to check if score == userChoices.Length then they have won the game.. show animation

    AudioSource playAudio;

	
    // Use this for initialization
    void Start () {
		//print ("Test -1 setting: " + PlayerPrefs.GetInt ("letter20"));
        //print("list size: " + userSettings.getListSize());
        getUserChoices();
        userSettings = GetComponent<letterSettings>();
        createLetters();
        createPictures();
        playAudio = GetComponent<AudioSource>();
        playAudio.PlayOneShot(openingMessage); // "have fun finding the letters + pic cues that match"
		ShufflePictures(chosenPics, chosenLets);
    }



    // This method will extract the ids of each letter from the PlayerPrefs, and store into a list to be used for displaying their choices
    public void getUserChoices()
    {	if (!pickAll.getBoolSelectAll()) {
			for (int i = 0; i <= 25; i++) {
				int letterId = PlayerPrefs.GetInt ("letter" + i);
				//print ("Curr Letters" + letterId.ToString ());
				if (letterId != -1) {
					// Store these choices into lists so we can position them on screen
					userChoiceSounds.Add (soundClips [letterId]);
					userChoiceLetters.Add (letters [letterId]);
					userChoicePictures.Add (pictureCues [letterId]);
                       
				}
			}
		}
		// pick all of the letters if the user has decided to use these
		else if(pickAll.getBoolSelectAll()){
			for (int i = 0; i <= 25; i++){
				userChoiceSounds.Add (soundClips [i]);
				userChoiceLetters.Add (letters [i]);
				userChoicePictures.Add (pictureCues [i]);
			}
		}
        
    }


    // Method to create the letter cards and position them on screen.
    // This method will instantiate cardBack object, and from that create clones to avoid hardcoding each card in the letter array.
    // This method will also assign an id based on the card index assuming that the array will (hopefully) maintain the order given in the inspector.
    // This might have to be changed later depending on how we choose to shuffle the cards. 
    public void createLetters()
    {
        //Vector3 startPos = originalCard2.transform.position; // position of the first card, all cards will be offset from here
      
        for (int i = 0; i < userChoiceLetters.Count; i++)
        {
            cardBack letterCard; // hold either the original card, or the copies created in the inspector

            if (i == 0)
            {
                letterCard = originalCard2;
            }
            else
            {
                letterCard = Instantiate(originalCard2) as cardBack;
            }


           letterCard.setCard(userChoiceLetters[i], i, letterType, userChoiceSounds[i]);
		   chosenLets.Add(letterCard); // add all of the letter cards to a list to maintain the id.  Shuffle these. 

           //float posX = startPos.x + ((i) % gridCols) * offsetX;
           //float posY = startPos.y + (int)Mathf.Floor((float) (i)/ gridCols) * -offsetY;
           //letterCard.transform.position = new Vector3(posX, posY, startPos.z); // create a new position based on this offset for the newly instatiated card
       

            //checking ids
            //print("letter " + i + " id = " + letterCard.getId());
        }

		//ShuffleNumbers(chosenLets);



    }

    // Method to create the picture cards and position them on screen.
    // This method will instantiate cardBack object, and from that create clones to avoid hardcoding each card in the picture array.
    // This method will also give an id, which will match the id of the letter. 
    // This might have to be changed later depending on how we choose to shuffle the cards. 
    public void createPictures()
    {
        //Vector3 startPos = originalCard.transform.position; // position of the first card, all cards will be offset from here

        for (int i = 0; i < userChoicePictures.Count; i++)
        {
            cardBack pictureCard; // hold either the original card, or the copies created in the inspector

            if (i == 0)
            {
                pictureCard = originalCard;
            }
            else
            {
                pictureCard = Instantiate(originalCard) as cardBack;
            }


            pictureCard.setCard(userChoicePictures[i], i, pictureType, userChoiceSounds[i]);
			chosenPics.Add (pictureCard); // add all of the picture cards to an array to maintain ids.  Shuffle these. 

            //float posX = startPos.x + ((i) % gridCols) * offsetX;
            //float posY = startPos.y + (int)Mathf.Floor((float)(i) / gridCols) * -offsetY;
            //pictureCard.transform.position = new Vector3(posX, posY, startPos.z); // create a new position based on this offset for the newly instatiated card
            

            //checking ids
            //print("picture " + i + " id = " + pictureCard.getId());
        }

		//ShuffleNumbers(chosenPics);


    }



    // Method to get first, use this to check what "type" the card is (picture or letter)
    public cardBack getFirst()
    {
        return firstReveal;
    }

    //Getter method to check if a second has been chosen or not (equals null if one has not been selected)
    public cardBack getSecond()
    {
        return secondReveal;
    }

    // Method to determine whether or not a user still has one more card to pick
    public bool canChoose()
    {
        if (secondReveal == null)
        {
            return true;
        }
        else
            return false;
    }

    public void cardChosen(cardBack card)
    {


        if (firstReveal == null) {
			print (PlayerPrefs.GetInt ("letter").ToString ());
			firstReveal = card; // set the first card as the first chosen by user
			Debug.Log ("Id =" + firstReveal.getType ());
			playAudio.PlayOneShot (firstReveal.getClip ()); // play sound once user has decided to choose a card
		} else if (firstReveal.getType () == pictureType) {
			playAudio.Stop (); // stop the audio if a second card is being chosen right away
			secondReveal = card; // if a first card has already been chose, set the secondReveal as the chosen card
			StartCoroutine (checkIDs ()); // begin coroutine of comparing id's between the card
		} else {
			print ("srry you must chose a pic first");
		}
    }

    // This needs work
    private IEnumerator checkCardType()
    {
       
        yield return new WaitForSeconds(.5f);
        firstReveal.Unreveal();
               
        firstReveal = null;
        secondReveal = null;
        allow = true;    
    }

    private IEnumerator checkIDs()
    {
        if (firstReveal.getId() == secondReveal.getId())
        {
            int playThisOne = Random.Range(0, congratulations.Length); // randomize the sounds played each time
            playAudio.PlayOneShot(congratulations[playThisOne]);
            score++;
            Debug.Log("Score: " + score);
            if (score == userChoicePictures.Count)
            {
                Debug.Log("u win"); //
            }
        }
        else
        {
            yield return new WaitForSeconds(.5f);
            firstReveal.Unreveal();
            secondReveal.Unreveal();
        }
        firstReveal = null;
        secondReveal = null;
    }


    // This method will be used to shuffle the picture cards and place them appropriately into the scene. 
    private void ShufflePictures(List<cardBack> inputPictures, List<cardBack> inputLetters)
    {
		Vector3 startPosPictures = originalCard.transform.position; // position of the first card, all cards will be offset from here
		Vector3 startPosLetters = originalCard2.transform.position;
        int n = inputPictures.Count;
        while (n >= 1)
        {
            n--;
			// create random values to shuffle the letters by
            int k = Random.Range(0, n + 1);
			int l = Random.Range (0, n + 1);
            cardBack temp = inputLetters[k];
			cardBack temp2 = inputPictures[l];

            inputLetters[k] = inputLetters[n];
            inputLetters[n] = temp;
			inputPictures[l] = inputPictures[n];
			inputPictures[n] = temp2;
		}

		for (int i = 0; i < inputPictures.Count; i ++) {
			float posXX = startPosPictures.x + ((i) % gridCols) * offsetX;
			float posYY = startPosPictures.y + (int)Mathf.Floor((float)(i) / gridCols) * -offsetY;
			// handle the sizing of the letters to fit on screen if there is more than 18
			if (inputPictures.Count >= 18) {
				gridCols = 6;
				inputPictures[i].transform.localScale = new Vector3(0.6f, 0.6f, 0.6f);
				posXX = startPosPictures.x + ((i) % gridCols) * offsetX/(1.2f);
				posYY = startPosPictures.y + (int)Mathf.Floor((float)(i) / gridCols) * -offsetY/(1.2f);
			}
            if (inputPictures.Count % 5 != 0)
            {
                // work on this, change position of the last few cards if %5 != 0
            }

			inputPictures[i].transform.position = new Vector3(posXX, posYY, startPosPictures.z); // create a new position based on this offset for the newly instatiated card

		}



		for(int j = 0; j < inputLetters.Count; j++){
			float posX = startPosLetters.x + (j % gridCols) * offsetX;
			float posY = startPosLetters.y + (int)Mathf.Floor((float)(j) / gridCols) * -offsetY;
			// Handle the sizing of the letters to fit on screen if there is more than 18
			if (inputLetters.Count >= 18){
				gridCols = 6;
				inputLetters[j].transform.localScale = new Vector3(0.6f, 0.6f, 0.6f);
				posX = startPosLetters.x + (j % gridCols) * offsetX/(1.2f);
				posY = startPosLetters.y + (int)Mathf.Floor((float)(j)/gridCols)* -offsetY/(1.2f);
			}

			inputLetters[j].transform.position = new Vector3(posX, posY, startPosLetters.z);
			
		}

    }


    // getter method to return the value of whether or not a user is allowed to pick the chosen card. This will be passed through to the "on mouse down" method in cardBack.cs
    public bool isAllowed()
    {
        return allow;
    }

	void onDestroy(){
		PlayerPrefs.Save ();
	}
    
	
}
