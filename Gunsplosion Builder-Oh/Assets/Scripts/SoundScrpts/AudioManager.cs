using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public GameObject Sound_Prefab;
    public List<string> Sound_Names = new List<string>();										// A list to hold the audioclip names
    public List<AudioClip> Sound_Clips = new List<AudioClip>();									// A list to hold the audioclips themselves
    public Dictionary<string, AudioClip> Sound_Lib = new Dictionary<string, AudioClip>();       // Dictionary that holds the audio names and clips

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < Sound_Names.Count; i++)         // For loop that populates the dictionary with all the sound assets in the lists
        {
            Sound_Lib.Add(Sound_Names[i], Sound_Clips[i]);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void PlaySound(string request)                   // Fuction to select and play a sound asset from the start and destroy the prefab once it has played
    {
        if (Sound_Lib.ContainsKey(request))									// If the sound is in the library
        {
            GameObject clip = Instantiate(Sound_Prefab);						// Instantiate a Sound prefab
            clip.GetComponent<AudioSource>().clip = Sound_Lib[request];			// Get the prefab and add the requested clip to it
            clip.GetComponent<AudioSource>().Play();                            // play the audio from the prefab
            Destroy(clip, clip.GetComponent<AudioSource>().clip.length);        // Destroy the prefab once the clip has finished playing
            ChangeVolume(1);                                                    // reset the volume if it was altered
        }
    }



    public void PlaySound(string request, float loudness)                   // Fuction to select and play a sound asset from the start and destroy the prefab once it has played
    {
        if (Sound_Lib.ContainsKey(request))                                 // If the sound is in the library
        {
            GameObject clip = Instantiate(Sound_Prefab);                        // Instantiate a Sound prefab
            clip.GetComponent<AudioSource>().clip = Sound_Lib[request];         // Get the prefab and add the requested clip to it
            clip.GetComponent<AudioSource>().volume = loudness;
            clip.GetComponent<AudioSource>().Play();                            // play the audio from the prefab
            Destroy(clip, clip.GetComponent<AudioSource>().clip.length);        // Destroy the prefab once the clip has finished playing
            ChangeVolume(1);                                                    // reset the volume if it was altered
        }
    }

    public void PlayRandomSoundFromRange(int LowerNumber, int HigherNumber)
    {
        int RandomSound;

        RandomSound = Random.Range(LowerNumber, HigherNumber);

        GameObject clip = Instantiate(Sound_Prefab);                        // Instantiate a Sound prefab
        clip.GetComponent<AudioSource>().clip = Sound_Clips[RandomSound];         // Get the prefab and add the requested clip to it
        clip.GetComponent<AudioSource>().Play();                            // play the audio from the prefab
        Destroy(clip, clip.GetComponent<AudioSource>().clip.length);        // Destroy the prefab once the clip has finished playing
        ChangeVolume(1);
    }

    public void ChangeVolume(float input)       // Function the change the clips volume
    {
        GameObject clip = Sound_Prefab;                         // gets the sound prefab	
        clip.GetComponent<AudioSource>().volume = input;        // adjusts the volume to the input
    }

    public void PlayRandomWoof()
    {
        PlayRandomSoundFromRange(3, 6);
    }
}
