using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FancyTextWriter : MonoBehaviour
{

    public Text UIText;
    [Multiline]
    public string desiredString;
    [Multiline]
    string currentString;
    public float timeBetweenCharacters;
    float timer;
    int index = 0;

    // Use this for initialization
    void Start()
    {
        currentString = desiredString.Substring(0, index);
        timer = 0;
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            index++;
            if (index <= desiredString.Length)
            {
                currentString = desiredString.Substring(0, index);
                UIText.text = currentString;
                timer = timeBetweenCharacters;

            }
        }
    }
}
