using UnityEngine;
using System.Collections;

public class PauseButton : MonoBehaviour {

    public Color highlight;
    private CatchController cc = new CatchController();
    private bool touched = false; // hold whether or not the button has already been touched
  

    public void OnMouseDown()
    {
        SpriteRenderer sprite = GetComponent<SpriteRenderer>();
        if (touched == false)
        {
            sprite.color = highlight;
            cc.pauseGame();
            touched = true;
            Debug.Log("You are paused");
        }
        else if(touched == true)
        {
            cc.gamePlaying();
            touched = false;
            Debug.Log("You are no longer paused");
        }
    }
}
