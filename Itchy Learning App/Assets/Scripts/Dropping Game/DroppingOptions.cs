using UnityEngine;
using System.Collections;

public class DroppingOptions : MonoBehaviour {

    // Use this for initialization
    public void useSpelling()
    {

        StartCoroutine("Wait");
        PlayerPrefs.SetInt("droppingOptions", 1); // set the player prefs to 1 (they have chosen to use the spelling option)
        Debug.Log("Im clicked");

    }

    public void useRandom()
    {
        StartCoroutine("Wait");
        PlayerPrefs.SetInt("droppingOptions", 0); // set the player prefs to 0 (they have chosen not to use the spelling option)
        Debug.Log("Im clicked");
    }



    IEnumerator Wait()
    {
        yield return new WaitForSeconds(2.0f);
        //audioVolume -= 0.1 * Time.deltaTime; // update this to have the sound fade out when exiting the scene
        Application.LoadLevel(3);

    }
}
