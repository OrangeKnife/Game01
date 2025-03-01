﻿using UnityEngine;
using System.Collections;

public class AbilityBase : MonoBehaviour {

	public bool bActiveAbility;
	protected float CDTIMER = 10.0f;
	protected float ACTIVETIMER = 4.0f;
	protected float active_remain;
	protected float timer;
	protected float startAbilityTime;
	protected float startCoolDownTime;

	protected bool isActiveEnable;
	protected Sprite IconSprite;

	protected SpriteRenderer tmpFXRenderer;
	protected Sprite vfxAbilityReady;
	protected Sprite vfxAbilityActive;

	GameObject UIIconObjectMask = null;

	protected float cdMultiplier = 1;

	protected void Start()
	{
		print ("start abi_passive");
		active_remain = ACTIVETIMER;
		timer = CDTIMER;

		tmpFXRenderer = transform.FindChild("AbilityFX").GetComponent<SpriteRenderer>();
		vfxAbilityReady = Resources.Load<Sprite>("Ability/Ability_Ready");
		vfxAbilityActive = Resources.Load<Sprite>("Ability/Ability_Active");
		EnableAbilityPassive();
	}
	protected void Update()
	{
		//print("duration : "+GetActiveRemainingDuration());
		//print("cooldown : "+GetRemainingCooldown());

		float cooldown = GetRemainingCooldown();
		if (cooldown >= 0 && UIIconObjectMask != null) {
			float percentage = Mathf.Max(cooldown / GetTotalCooldown(),0f);
			UIIconObjectMask.GetComponent<UnityEngine.UI.Image>().fillAmount = percentage;
			//print (percentage);
			UIIconObjectMask.GetComponent<UnityEngine.UI.Image>().enabled = percentage > 0f;
		}

		if (cooldown <= 0 && !tmpFXRenderer.enabled)
		{
			tmpFXRenderer.enabled = true;
			tmpFXRenderer.sprite = vfxAbilityReady;
		}

		if (isActiveEnable && GetActiveRemainingDuration() < 0)
		{
			StopActiveEffect();
		}
	}

	public void bindUIIconObject(GameObject inUIIcon, Sprite inSprite)
	{
		foreach(Transform tf in inUIIcon.GetComponentsInChildren<Transform>())
		{
			if(tf.name == "AbilityMaskImg")
			{
				UIIconObjectMask = tf.gameObject;
				break;
			}

			if(tf.name == "AbilityImg")
			{
				tf.GetComponent<UnityEngine.UI.Image>().sprite = inSprite;
			}
		}
	}

	public virtual bool IsActiveAbility()
	{
		return bActiveAbility;
		//return timer > 0 ? true : false;
	}
	public virtual void EnableAbilityActive()
	{
		if (GetRemainingCooldown () <= 0) 
		{
			print("Enable Active Base");
			startAbilityTime = Time.time;
			startCoolDownTime = Time.time;
			isActiveEnable = true;

			// play vfx here
			tmpFXRenderer.enabled = true;
			tmpFXRenderer.sprite = vfxAbilityActive;

			StartActiveEffect();
		}
	}
	public virtual void StartActiveEffect()
	{

	}
	public virtual void DisableAbilityActive()
	{

	}
	public virtual void setCDMultipler(float multi)
	{
		cdMultiplier = multi;
	}
	public virtual float getCDMultipler()
	{
		return cdMultiplier;
	}
	public virtual void StopActiveEffect()
	{
		print("Disable Active Base");
		isActiveEnable = false;
		tmpFXRenderer.enabled = false;
	}
	public virtual void EnableAbilityPassive() {

	}
	public virtual void DisableAbilityPassive() {
	}
	public virtual float GetTotalCooldown() {
		return CDTIMER;
	}
	public virtual float GetRemainingCooldown() {
		float _currentCDTime = Time.time - startCoolDownTime;
		//print ("test tim  e" +startCoolDownTime);
		return Mathf.Max(CDTIMER - _currentCDTime,0f) * cdMultiplier ;
		//return System.Convert.ToSingle(_currentCDTime.TotalSeconds);
	}
	public virtual float GetActiveTotalDuration() {
		return ACTIVETIMER;
	}
	public virtual float GetActiveRemainingDuration() {
		float _currentActiveTime = Time.time - startAbilityTime;
		return ACTIVETIMER - _currentActiveTime;
		//return System.Convert.ToSingle(_currentActiveTime.TotalSeconds);
	}

	public virtual Sprite GetIcon()
	{
		if (IconSprite == null)
			IconSprite = Resources.Load<Sprite>("Ability/Icon/Combat_64");

		return IconSprite;
	}
}
