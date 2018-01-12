using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class RetryNextlvl2 : MonoBehaviour
{
	public int				enemy;
	public GameObject		player;
	public GameObject 		Stairs;

	void Start()
	{
		transform.GetComponent<CanvasGroup>().alpha = 0;
		transform.GetComponent<CanvasGroup>().interactable = false;
		transform.GetComponent<CanvasGroup>().blocksRaycasts = false;
	}

	void Update()
	{
		if (player.transform.GetComponent<Player>().dead || enemy == player.GetComponent<Player>().frags || Stairs.GetComponent<Exit>().end)
		{
			transform.GetComponent<CanvasGroup>().alpha = 1;
			transform.GetComponent<CanvasGroup>().interactable = true;
			transform.GetComponent<CanvasGroup>().blocksRaycasts = true;
			if (player.GetComponent<Player>().frags >= enemy || Stairs.GetComponent<Exit>().end)
			{
				transform.GetChild(0).GetComponent<Text>().text = "You Win !!";
				transform.GetChild(1).GetChild(0).GetComponent<Text>().text = "Next lvl";
			}
			else
			{
				transform.GetChild(0).GetComponent<Text>().text = "You Lose !!";
				transform.GetChild(1).GetChild(0).GetComponent<Text>().text = "Retry";
			}
		}
	}

	public void RetryNextButton()
	{
		if (player.transform.GetComponent<Player>().dead)
			Application.LoadLevel("lvl2");
		else
			Application.LoadLevel("TittleScreen");
	}
}
