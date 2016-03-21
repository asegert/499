using UnityEngine;
using System.Collections;

public class helpButton : MonoBehaviour {

    public Color highlightColor = Color.cyan;
    private bool showHelp;

    // Use this for initialization
    void Awake()
    {
        
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

    public void OnMouseDown()
    {
        showHelp = true;
        print(showHelp);

    }
    public void OnMouseUp()
    {
        /*transform.localScale = Vector3.one;
        if (targetObject != null)
        {
            targetObject.SendMessage(targetMessage);
        }*/
    }

    void onGUI()
    {
        if (showHelp == true)
        {
            // popup window
        }
    }
}

