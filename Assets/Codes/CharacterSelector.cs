﻿using UnityEngine;
using System;
using System.Collections;

using System.Collections.Generic;


[Serializable]
public struct CharacterInfo
{
	public Sprite CharacterSprite;
	public int CharacterId;
	public string CharacterName;
	public string CharacterDescription;
	public string CharacterProductId;
	public int Cost;
	public float Cost_Dollar;
};

public class CharacterSelector : MonoBehaviour
{


	public List<CharacterInfo> CharacterList = new List<CharacterInfo> ();
	public GameObject CharacterTemplate;
	public int maxDisplayNum = 5;

	private List<GameObject> CharObjectList = new List<GameObject>();
	private int currentSelectedIndex = -1;
	private int lastSelectedIndex = -1;


	void Start () {
		CreateCharacters ();
		if (CharObjectList.Count > 0)
			currentSelectedIndex = 0;
	}

	void CreateCharacters ()
	{
		foreach (CharacterInfo c in CharacterList)
		{
			CharObjectList.Add(CreateObject2D(c));

		}
	}

	GameObject CreateObject2D(CharacterInfo CharInfo )
	{
		GameObject obj = Instantiate (CharacterTemplate);
		obj.GetComponent<SpriteRenderer> ().sprite = CharInfo.CharacterSprite;
		obj.GetComponent<Character> ().CharacterInfo = CharInfo;

		return obj;

	}

	// Update is called once per frame
	void Update () {

		#if UNITY_STANDALONE || UNITY_WEBPLAYER

		if(Input.GetKeyUp ("a"))
			currentSelectedIndex--;
		else if(Input.GetKeyUp ("d"))
			currentSelectedIndex++;
		
		currentSelectedIndex = Math.Min(Math.Max(0,currentSelectedIndex),CharObjectList.Count-1);

		if(currentSelectedIndex != lastSelectedIndex)
			ReArrange(currentSelectedIndex);
		
		
		#elif UNITY_IOS || UNITY_ANDROID || UNITY_WP8 || UNITY_IPHONE
		 
		if (Input.touchCount > 0) 
		{
			Touch touch = Input.GetTouch(0);

			if (touch.phase == TouchPhase.Moved)
			{
				TouchMovementX += touch.deltaPosition.x;
				if(Mathf.Abs(TouchMovementX) > 100)
				{
					if(TouchMovementX > 0)
						currentSelectedIndex--;
					else
						currentSelectedIndex++;

					currentSelectedIndex = Math.Min(Math.Max(0,currentSelectedIndex),CharObjectList.Count-1);

					TouchMovementX = 0;
				}

			}
 
		}
		if(currentSelectedIndex != lastSelectedIndex)
			ReArrange(currentSelectedIndex);
		#endif

	}

	void ReArrange(int idx)
	{
		foreach (GameObject gobject in CharObjectList) {
			gobject.GetComponent<SpriteRenderer> ().enabled = false;
		}

		CharObjectList [idx].GetComponent<Character> ().SetPosition (0, 0, 10, 10);

		for(int i = 1; i <= maxDisplayNum/2; i++)
		{
			if(idx - i  >= 0)
			{
				CharObjectList[idx - i].GetComponent<Character>().SetPosition(-i * 4, 0, 5, 5);
			}

			if(idx + i < CharObjectList.Count )
			{
				CharObjectList[idx + i].GetComponent<Character>().SetPosition(i * 4, 0, 5, 5);
			}
		}

		lastSelectedIndex = idx;
	}
}
