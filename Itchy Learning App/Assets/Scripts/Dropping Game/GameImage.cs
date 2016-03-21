using UnityEngine;
using System.Collections;

public class GameImage {

    private bool check;
    private string letter;
    private int key;
    private bool win = false;

    // Begin GameImage constructor 
    public GameImage(int k, string l, bool c)
    {
        key = k; // assign each letter a key
        letter = l;
        check = c; // bool to check if the letter has been collected
    }

    /************************************************************************/
    /************************ GETTERS / SETTERS *****************************/
    /************************************************************************/

    /**Get Methods**/

    public int getKey()
    {
        return key;
    }

    public string getLetter()
    {
        return letter;
    }

    public bool getCheck()
    {
        return check; 
    }

   
    public bool checkIfWon()
    {
        return win;
    }

    /** Set Methods **/
    
    public void setKey(int k)
    {
        key = k;
    }

    public void setLetter(string l)
    {
        letter = l;
    }

    public void setCheck(bool b)
    {
        check = b;
    }

    /**************************************************************************/
    /************************ END GETTER/SETTERS *****************************/
    /************************************************************************/



    // Method that will allow you to search for a GameImage object using the letter
    public GameImage findWithLetter(string s, GameImage [] g)
    {
        for(int i = 0; i < g.Length; i++)
        {
            if (g[i].getLetter() == s && g[i].getCheck() == false) // check to see if the letter is within the provided array, and that it hasn't already been set to true (caught)
            {
                return g[i];
            }
        }
        return null;

    }

    
    //Contain method to check whether or not the current letter being caught exists as a letter in the current image
    public bool Contains(GameImage [] g, string l)
    {
        bool b = false;
        for(int i = 0; i < g.Length; i++)
        {
            if (g[i].getLetter().Equals(l) && g[i].getCheck() == false) // check if the letter is equal, and that the bool is set to false (it hasn't yet been printed)
                b = true;            
         
        }
        return b;
    }


    //Method to check if the spelling of the word is in the correct order
    public bool correctOrder(GameImage[] g, string l)
    {
        bool b = false;
        for (int i = 0; i < g.Length; i++) {
            if (i != 0) // if we are not searching the first index, i-1 will still exist
            {
                if (g[i].getLetter().Equals(l) && g[i].getCheck() == false && g[i - 1].getCheck() == true) // check if the previous letter has already been caught, if so return true
                {
                    b = true;
                }
            }
            else // otherwise, we are catching the first index
                if (g[i].getLetter().Equals(l) && g[i].getCheck() == false)
            {
                b = true;
            }
        }


        return b;
    }

    // Call this method in Update in the CatchController class to determine whether or not the user has collected all of the letters required to make the word
    // If returned true, randomize a new image to be displayed
    public bool checkWinning(GameImage [] g)
    {
        int count = 0; // keep a counter to increment the amount of "caught" letters, and check if it is equal to the array length
        for(int i = 0; i < g.Length; i++)
        {
            if(g[i].getCheck() == true) // check each bool in the GameImage objects to see if it has been caught
            {
                count++;
                if (count == g.Length)
                {
                    win = true; // if the count is equal to the amount of letters in the word, the user has won 
                    Debug.Log("You Win");
                }
                else
                    win = false;
            }
        }

        return win; 
    }
}
