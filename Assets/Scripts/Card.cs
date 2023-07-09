using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Card : MonoBehaviour
{
	public bool hasBeenPlayed;
	public int handIndex;

	GameManager gm;

	private Animator anim;
	private Animator camAnim;

	public GameObject effect;
	public GameObject hollowCircle;

	GameObject childObject;
	bool activChild, isfixed = false;


	private void Start()
	{
		gm = FindObjectOfType<GameManager>();
		anim = GetComponent<Animator>();
		camAnim = Camera.main.GetComponent<Animator>();
	}

	void MoveToDiscardPile()
	{
		Instantiate(effect, transform.position, Quaternion.identity);
		gm.discardPile.Add(this);
		gameObject.SetActive(false);
	}

  

    private void OnMouseOver()
    {
		if(Input.GetMouseButtonDown(0) && !isfixed)
		{
			Invoke("destroyCard",0.5f);
		}
		/*if(Input.GetMouseButtonDown(1))
		{
			childObject.SetActive(!activChild);
			activChild = !activChild;
			isfixed = !isfixed;
		}*/
		childObject = transform.GetChild(0).gameObject;
    }


	public void destroyCard()
	{
		Instantiate(hollowCircle, transform.position, Quaternion.identity);
		
		camAnim.SetTrigger("shake");
		anim.SetTrigger("move");
			
		gm.availableCardSlots[handIndex] = true;
		Invoke("MoveToDiscardPile", 0.5f);
			
		gm.cardInHand--;
	}
}
