using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.IO;
using System;

public class dialogueSystem : MonoBehaviour
{
    [SerializeField]
    private Text characterName, dialogueText;

    [SerializeField]
    private Canvas dialogueBox;

    private List<Dialogue> dialogueLines = new List<Dialogue>();
    private int index;

    [SerializeField]
    private Button continueButton;

    [SerializeField]
    private Image background, charSprite, charSprite2;

    [SerializeField]
    private Sprite[] maluSprites, VincenzoSprites, backgroundLevel1;
    //public Image charSprite;
    //public Image charSprite2;
    //public Sprite[] VincenzoSprites;
    //public Sprite[] backgroundLevel1;
    //public Text dialogueText;
    [SerializeField]
    private Sprite transparency;
    

    public struct Dialogue
    {
        public string name;
        public string content;
        public Sprite image;
        public Sprite image2;
        public Sprite background;
    }

    void Start()
    {
        continueButton.gameObject.SetActive(false);
        LoadDialogue("Chapter1", maluSprites, VincenzoSprites, backgroundLevel1);
        //say("Josh", "What the hell are you doing here man?");
        //say("Paul", "I'm not doing anything. What are you doing here?");
        //say("Josh", "Don't try that. I'm on my way home.");

        StartCoroutine(Saying());
        continueButton.onClick.AddListener(nextSentence);
    }

    void Update()
    {
        if (dialogueText.text == dialogueLines[index].content)
        {
            continueButton.gameObject.SetActive(true);
        }
    }

    //public void say(string charName, string charSay)
    //{
    //Dialogue s;
    //s.name = charName;
    //s.content = charSay;

    //dialogueLines.Add(s);
    //}

    IEnumerator Saying()
    {
        foreach (char name in dialogueLines[index].name)
        {
            characterName.text += name;
        }

        background.sprite = dialogueLines[index].background;
        charSprite.sprite = dialogueLines[index].image;
        charSprite2.sprite = dialogueLines[index].image2;

        foreach (char content in dialogueLines[index].content)
        {
            dialogueText.text += content;
            yield return new WaitForSeconds(0.01f);
        }
    }

    void LoadDialogue(string fileName, Sprite [] sprites, Sprite [] sprites2, Sprite[] background)
    {
        dialogueLines.Clear();
        index = 0;
        TextAsset texttoread = Resources.Load(fileName) as TextAsset;
        string[] linesFromfile = texttoread.text.Split("\n"[0]);
        
        foreach (string lines in linesFromfile)
        {
            string[] line_values = lines.Split('|');
            Dialogue s;
            s.name = line_values[0];
            s.content = line_values[1];
            //s.image = daveSprites[line_values[2].ToString().Length];
            int x = 0;
            Int32.TryParse(line_values[2], out x); //converts string to int
            s.image = sprites[x];
            int z = 0;
            Int32.TryParse(line_values[3], out z); //converts string to int
            s.image2 = sprites2[z];
            int y = 0;
            Int32.TryParse(line_values[4], out y); //converts string to int
            s.background = background[y];
            dialogueLines.Add(s);
        }    
    }

    public void nextSentence()
    {
        continueButton.gameObject.SetActive(false);
        if (index < dialogueLines.Count - 1)
        {
            index++;
            dialogueText.text = "";
            characterName.text = "";
            StartCoroutine(Saying());
        }
        else
        {
            dialogueText.text = "";
            characterName.text = "";
            charSprite.sprite = transparency;
            charSprite2.sprite = transparency;
            background.sprite = transparency;
            continueButton.gameObject.SetActive(false);
            SceneManager.LoadScene("menuScene");
        }
    }
}
