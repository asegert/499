using UnityEngine;
using System.Collections;

public class SpawnCoconuts : MonoBehaviour {

	private int id; // give each letter an ID so we can refer to it
	private AudioClip clip;
    private string let;




	// Call our spawn method on start
	void Start () {
	}

    public AudioClip getSound()
    {
        return this.clip;
    }

    public string getLetter()
    {
        return let;
    }

	// Set a coconut to the input array of sprite images (coconuts with different letters on each)
	// Will have to change to add more parameters later (ex: sound of letters, etc)
	public SpawnCoconuts setCoconut(Sprite image, AudioClip c, int inputId, string letter){
        let = letter;
		id = inputId;
        clip = c;
		GetComponent<SpriteRenderer>().sprite = image;
		//Debug.Log (this.ToString());
		return this;
	}

}
