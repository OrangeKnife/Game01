﻿using UnityEngine;
using System.Collections;

public class MaterialController : MonoBehaviour {

	public float ScaleReductionX = 1.0f;
	public float ScaleReductionY = 1.0f;
	private SpriteRenderer tmpSpriteRenderer;

	// Use this for initialization
	void Start () {
		tmpSpriteRenderer = GetComponent<SpriteRenderer>();

		tmpSpriteRenderer.material.mainTextureScale = new Vector2( transform.localScale.x / ScaleReductionX ,transform.localScale.y / ScaleReductionY);
		tmpSpriteRenderer.material.SetFloat("RepeatX", transform.localScale.x / ScaleReductionX);
		tmpSpriteRenderer.material.SetFloat("RepeatY", transform.localScale.y / ScaleReductionY);


//		if (transform.localScale.x > transform.localScale.y)
//			tmpSpriteRenderer.material.mainTextureScale = new Vector2( transform.localScale.x / transform.localScale.y ,mainRow);
//		else
//			tmpSpriteRenderer.material.mainTextureScale = new Vector2(mainRow, transform.localScale.y / transform.localScale.x );
	}
	
	// Update is called once per frame
	void Update () {

	}
	
}
