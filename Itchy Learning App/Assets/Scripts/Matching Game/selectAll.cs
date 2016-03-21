using UnityEngine;
using System.Collections;

public class selectAll : MonoBehaviour {
	public Color highlightColor = Color.white;
	[SerializeField] private letterSettings settings;
	static bool selectAllLetters = true;


	void Start()
	{
		settings = GetComponent<letterSettings> ();
	}
	
	public void OnMouseEnter()
	{
		SpriteRenderer sprite = GetComponent<SpriteRenderer>();
		if (sprite != null)
		{
			sprite.color = highlightColor;
		}
	}
	public void OnMouseExit()
	{
		SpriteRenderer sprite = GetComponent<SpriteRenderer>();
		if (sprite != null)
		{
			sprite.color = Color.white;
		}
	}
	
	//This button will clear all of the current user choices for the letters
	public void OnMouseDown()
	{
		selectAllLetters = true;
		transform.localScale = new Vector3(0.30f, 0.30f, 0.30f);
		
	}
	
	public void OnMouseUp()
	{
		transform.localScale = new Vector3 (0.4f, 0.4f, 0.4f);

	}

	//Getter method to return whether or not the user has clicked the select all button, use this to pass into the main game controller
	public bool getBoolSelectAll(){
		return selectAllLetters;
	}

	public void setBoolSelectAll(bool input){
		selectAllLetters = input;
	}
}
