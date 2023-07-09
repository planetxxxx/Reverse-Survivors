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

	private bool isDragging = false;
    private Vector3 offset;
	private float doubleClickTimeThreshold = 0.3f;
    private bool isClickInProgress = false;
    private float lastClickTime = 0f;
	private bool isTriggered = false;
	bool doubleClicked;



	

	private void Start()
	{
		gm = FindObjectOfType<GameManager>();
		anim = GetComponent<Animator>();
		camAnim = Camera.main.GetComponent<Animator>();
		//InvokeRepeating("destroyCardAfterTime",5f,5f);
	}

    private void Update()
    {

        if (isDragging)
        {
            // Update the object's position based on the mouse position
            Vector3 newPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition) + offset;
            transform.position = new Vector3(newPosition.x, newPosition.y, transform.position.z);
		}
		if (Input.GetMouseButtonDown(0))
        {
            if (!isClickInProgress)
            {
                // Start the click sequence
                isClickInProgress = true;
                lastClickTime = Time.time;
				doubleClicked = false;
            }
            else
            {
                // Double-click detected
                float timeSinceLastClick = Time.time - lastClickTime;
                if (timeSinceLastClick <= doubleClickTimeThreshold)
                {
					doubleClicked = true;
                }

                isClickInProgress = false;
            }
		}
        
    }

	void MoveToDiscardPile()
	{
		Instantiate(effect, transform.position, Quaternion.identity);
		gm.discardPile.Add(this);
		gameObject.SetActive(false);
	}

	private void OnMouseDown()
    {
        offset = transform.position - Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

    private void OnMouseDrag()
    {
        isDragging = true;
    }

    private void OnMouseUp()
    {
        if (isDragging)
        {
			Invoke("destroyCard", 0.5f);
			Debug.Log(isDragging);
        }

        isDragging = false;
    }

	private void OnTriggerExit2D(Collider2D collision)
	{
		if(collision.gameObject.tag == "card ui")
		{
			isTriggered = false;
		}
		else if (collision.gameObject.tag == "playground")
		{
			isTriggered = true;
		}
	}

	public void destroyCard()
	{
		if(!isTriggered && !doubleClicked)
		{
			Instantiate(hollowCircle, transform.position, Quaternion.identity);
		
			camAnim.SetTrigger("shake");
			anim.SetTrigger("move");
			
			gm.availableCardSlots[handIndex] = true;
			Invoke("MoveToDiscardPile", 0.5f);
			
			gm.cardInHand--;
		}
	}

	public void destroyCardAfterTime()
	{
		//Instantiate(hollowCircle, transform.position, Quaternion.identity);
		
		//camAnim.SetTrigger("shake");
		//anim.SetTrigger("move");
			
		gm.availableCardSlots[handIndex] = true;
		Invoke("MoveToDiscardPile", 0.5f);
			
		gm.cardInHand--;
	}
}
