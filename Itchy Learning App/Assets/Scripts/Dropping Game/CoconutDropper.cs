using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CoconutDropper : MonoBehaviour {
	[SerializeField] private SpawnCoconuts coconut; // hold the coconut prefab into this variable in the inspector
	private SpawnCoconuts thisCoconut;
	public Sprite [] coconutImages; // store all of the sprites to be instantiated into this array
	public AudioClip[] letterSounds; // store each letter sound
    [SerializeField]public string[] letters;
	private List<SpawnCoconuts> allCoconuts = new List<SpawnCoconuts>();
    AudioSource playAudio;
    private int rand;
    private bool dropping = true;
	int spawnMin = 2; // min time of dropper
	int spawnMax = 5; // max time of dropper
    private string imageName;
    public ImageController currImage; // instantiate this in inspector
    private int[] index;


    // Use this for initialization
    // Instantiate all of the prefabs off screen, and then change the position to begin height of dropper
    void Awake(){
		Vector3 position = this.transform.position;
		position.y = 6;
		this.transform.position = position;
	}

	void Start () {
        playAudio = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame, change the position of the dropper to varying x-values
	void Update () {
	}

	// this method will be used to create the variety of letters that will be dropping, storing them into an array.
	public void createCoconuts(){
		for (int i = 0; i < coconutImages.Length; i++) {
			SpawnCoconuts currCoconut;
			if (i == 0)
			{
				currCoconut = coconut;
			}
			else
			{
				currCoconut = Instantiate(coconut) as SpawnCoconuts;
			}

			thisCoconut = currCoconut.setCoconut (coconutImages [i], letterSounds[i], i, letters[i]);
			//currCoconut.GetComponent<Rigidbody2D> ().gravityScale = 0;
			allCoconuts.Add (thisCoconut); // store all of the coconuts that are created into an array so we can use this for dropping
		}

	}

  

    // find the random index being used
    public int getRand()
    {
        return rand;
    }

    //hacky terrible way of getting grabbing indexes of the letters in the image name to show more frequency
    //work more on this later
    public void getIndex(char[] array)
    {
        index = new int[array.Length];
        for(int i = 0; i < allCoconuts.Count; i++)
        {
            for (int j = 0; j < array.Length; j++)
            {
                if (allCoconuts[i].getLetter().Equals(array[j]))
                {
                    int k = 0;
                    index[k] = i;
                    k++;
                }
            }
        }
    }


	//Spawn method used to drop the coconuts randomly depending on letters in the current word that need to be spelt
	public void Spawn(){


        imageName = currImage.GetComponent<SpriteRenderer>().sprite.name.ToString(); // get the current name of the image being displayed
        getIndex(imageName.ToCharArray()); // call the getIndex() method to get the index values of the coconut array
        if (dropping == true)
        {
            rand = Random.Range(0, allCoconuts.Count);

            int percent = Random.Range(0, 100); // generate a percentage to calculate frequency of dropped letters
            Debug.Log("Current percent" + percent);
            if (percent < 40) // 40% chance of dropping a letter other than the ones in the current image name
            {
                //playAudio.PlayOneShot(letterSounds[rand]); // play the corresponding letter sound on drop
                Instantiate(allCoconuts[rand], transform.position, Quaternion.identity);
            }
            else // 70% chance of dropping a letter in the current image being shown
            {
                int rand2 = Random.Range(0, 2);
                Instantiate(allCoconuts[index[rand2]], transform.position, Quaternion.identity);
            }

            Invoke("Spawn", Random.Range(spawnMin, spawnMax)); // after random time between min and max, respawn
        }
	}

    public void positionDropper()
    {
        Vector3 dropperPosition = this.transform.position;
        dropperPosition.x = Random.Range(-5, 10);
        this.transform.position = dropperPosition;
    }

    public void setDropper(bool b)
    {
        dropping = b;
    }

    public void pauseDropper()
    {
        dropping = false;
    }
}
