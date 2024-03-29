﻿using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

[RequireComponent(typeof(AudioSource))]
public class MainMenu : MonoBehaviour {

    [SerializeField]
    private GameObject creditsGO;
    [SerializeField]
    private string startText = "Starten";
    [SerializeField]
    private string creditsText = "Credits";
    [SerializeField]
    private string endText = "Beenden";
    [SerializeField]
    private float threshhold = 0.4f;
    [SerializeField]
    private AudioClip selectedClip;
    [SerializeField]
    private float[] pitches;

    private new AudioSource audio;
    private GameObject title;
    private Text[] texts;
    private string[] strings;
    private int selector = 0;
    private bool hasSelected = false;
    private bool credits = false;
    private int easterEggCounter = 0;

    void Awake()
    {
        texts = new Text[3];
        texts[0] = transform.GetChild(0).GetComponent<Text>();
        texts[1] = transform.GetChild(1).GetComponent<Text>();
        texts[2] = transform.GetChild(2).GetComponent<Text>();
        strings = new string[]{startText, creditsText, endText};
        title = transform.parent.FindChild("Title").gameObject;
        Assert.IsNotNull<GameObject>(title);
        Assert.IsNotNull<GameObject>(creditsGO);
        audio = GetComponent<AudioSource>();
        Assert.IsNotNull<AudioSource>(audio);
    }

    void Start()
    {
        texts[0].text = ">> " + startText + " <<";
        creditsGO.SetActive(false);
    }

    void Update()
    {
        if (Input.GetButtonDown("FireSword") || Input.GetKeyDown(KeyCode.Return)) {
            if (!credits) {
                switch (selector) {
                    case 0: SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1); break;
                    case 1:
                        title.SetActive(false);
                        credits = true;
                        creditsGO.SetActive(true);
                        for (int i = 0; i < 3; ++i) {
                            texts[i].enabled = false;
                        }
                            break;
                    case 2: Application.Quit(); break;
                }
            }
            else {
                title.SetActive(true);
                credits = false;
                creditsGO.SetActive(false);
                for (int i = 0; i < 3; ++i) {
                    texts[i].enabled = true;
                }
            }

            // Play Sound
            PlaySound();
        }

        if (credits == true)
            return;


        float v = Input.GetAxis("Vertical");
        Debug.Log("Vertical: " + v);

        if (Mathf.Abs(v) < threshhold)
            hasSelected = false;

        if (hasSelected)
            return;

        if (v > threshhold){
            texts[selector].text = strings[selector];
            if (selector == 0)
                selector = 2;
            else
                selector--;
        }
        else if (v < -threshhold){
            texts[selector].text = strings[selector];
            selector++;
        }
        else
            return;

        selector %= 3;
        hasSelected = true;
        texts[selector].text = ">> " + strings[selector] + " <<";
        // Play Sound
        PlaySound();
        
    }

    void PlaySound()
    {
        //float pitch = Random.RandomRange(randomPitch.x, randomPitch.y);
        audio.pitch = pitches[easterEggCounter];
        easterEggCounter++;
        easterEggCounter %= pitches.Length;
        audio.PlayOneShot(selectedClip);
    }
}
