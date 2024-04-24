using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class FireCooldown : MonoBehaviour 
{
 public Image FireImage;
 public float cooldown= 1;
 bool isCooldown =false;
 public KeyCode Shoot;
	
	void Start () 
	{
		FireImage.fillAmount=0;
	}

	
	
	void Update () 
	{
		Shoot1();
	}
	void Shoot1()
	{
		if(Input.GetKey(Shoot)&& isCooldown == false)
        {
			isCooldown =true;
			FireImage.fillAmount=1;
		}
		if (isCooldown)
		{
          FireImage.fillAmount -=1 / cooldown * Time.deltaTime;
		  if(FireImage.fillAmount <= 0)
		  {
			  FireImage.fillAmount = 0;
			  isCooldown =false;
		  }
		}

	}
}
