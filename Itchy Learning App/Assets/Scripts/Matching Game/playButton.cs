using UnityEngine;
using System.Collections;

public class playButton : MonoBehaviour
{
    [SerializeField]
    private GameObject targetObject;
    [SerializeField]
    private string targetMessage;
	public Color highlightColor;
    [SerializeField] private letterSettings checkEmpty; // create an instance of letterSettings so we can retrieve the list of id's to be added

    void Awake()
    {
        checkEmpty = checkEmpty.GetComponent<letterSettings>();
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
        print(checkEmpty.getIsEmpty());

        if (checkEmpty.getIsEmpty() == false)
        {
            Application.LoadLevel("Game");
            transform.localScale = new Vector3(1.1f, 1.1f, 1.1f);
        }
        else
            print("pick some letters before starting");
       
        
    }
    public void OnMouseUp()
    {
        transform.localScale = Vector3.one;
        if (targetObject != null)
        {
            targetObject.SendMessage(targetMessage);
        }
    }
}


