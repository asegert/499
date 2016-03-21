/* This script will be used to take the user */
/*input from the cards that they want to have within the game, and then pass*/
/*them into the main game scene*/



using UnityEngine;
using System.Collections;

public class settingsMouse : MonoBehaviour {
	SpriteRenderer sprite;
    [SerializeField] private letterSettings settings;
    [SerializeField] private GameObject letter;
	private selectAll allSelected = new selectAll(); // initialize an instance of the selectAll class
    private int id;
	private int avail;
    private int[] userChoices; // an array to store the letters which a user would like to use in the game
                               // Use this for initialization
    

    public int getId()
    {
        return this.id;
    }

    public void setId(int input)
    {
        this.id = input;
    }

    public void OnMouseDown()
    {
			allSelected.setBoolSelectAll (false); // this will now trigger the selectAll boolean to be false, therefore instead using players preferences
			sprite = GetComponent<SpriteRenderer>();
        // call the lettersChosen method from the letterSettings screen, the input will refer to the current button being clicked.
        	settings.lettersChosen(this);
			this.transform.localScale = new Vector3(1.1f, 1.1f, 1.1f);
			sprite.color = Color.grey;


		if (settings.hasAlready () == true) {
			settings.setHasAlready ();
			this.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
			sprite.color = Color.white;
			settings.lettersChosen (this);
		}
        
       
    }

    public void setLetter(Sprite image, int letterId)
    {
        id = letterId;
        GetComponent<SpriteRenderer>().sprite = image;

    }
}
