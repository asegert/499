using UnityEngine;
using System.Collections;

public class removeButton : MonoBehaviour {

    [SerializeField]
    private GameObject targetObject;
    [SerializeField]
    private string targetMessage;
    public Color highlightColor = Color.cyan;
    public bool hasRemoved = false;
    [SerializeField] private letterSettings settings;


    void Start()
    {
        settings = settings.GetComponent<letterSettings>();
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
        settings.setIsEmpty(true); // set is empty to true to correspond with an empty list of user choices
        //PlayerPrefs.DeleteAll();
        hasRemoved = true;
        transform.localScale = new Vector3(0.30f, 0.30f, 0.30f);
		settings.onAllRemove ();
    }

    public void OnMouseUp()
    {
        transform.localScale = new Vector3(0.35f, 0.35f, 0.35f);
        if (targetObject != null)
        {
            targetObject.SendMessage(targetMessage);
        }
    }

	// return the value of this bool to check if we need to transform the letters back to their original shape
	public bool getHasRemoved(){
		return hasRemoved;
	}



}
