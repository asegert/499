using UnityEngine;
using System.Collections;

public class cardBack : MonoBehaviour {

    /* SerializeField forces Unity to display a drag and drop section in the inspector when the variable is set to private (in case you are like "wtf is tht 4??!") */

    [SerializeField] private GameObject card; // variable that will appear in the inspector so we can drag the card_back into
    [SerializeField] private GameController controller; 
    private int id; // id to match cards to one another
    private string type; // type refers to either a "picture" or "letter" type
    private AudioClip clip;
	private bool chooseSecond = false; // bool to determine whether or not a user can then select their next card if the first has been of the "picture" type
    

	
    // Getter method to return the id of a card
    public int getId()
    {
        return this.id;
    }

    // Setter method for the id of a card
    public void setId(int input)
    {
        this.id = input;
    }

    //Getter method for the type of card
    public string getType()
    {
        return this.type;
    }
    
    //Setter method for the type of card
    public void setType(string input)
    {
        this.type = input;
    }

    //Getter method for the corresponding audio clip
    public AudioClip getClip()
    {
        return this.clip;
    }

    //Setter method to provide an audio clip
    public void setClip(AudioClip input)
    {
        this.clip = input;
    }

    //Method that other scripts can use to pass new sprites into this object
    public void setCard(Sprite image, int inputId, string t, AudioClip c)
    {
        clip = c;
        id = inputId;
        type = t;
        GetComponent<SpriteRenderer>().sprite = image;
       
    }

	// This method will also be used to handle whether or not a user is able to choose a card based on the "type" of the previous card chosen.
	// If it is a picture card, they are able to choose their second card, which would include a letter.
	// Otherwise, they will not be able to pick.
    public void OnMouseDown()
    {
		if (controller.getFirst () == null && this.getType ().Equals ("picture")) {
			chooseSecond = true;
			card.SetActive (false);
			controller.cardChosen (this);
		}
			else if (this.getType ().Equals ("letter") && controller.getFirst ()!= null){
			card.SetActive (false);
			controller.cardChosen (this);
				
		}
       
    }

    //Method to hide the card again
    public void Unreveal()
    {
        card.SetActive(true);
    }
	



}
