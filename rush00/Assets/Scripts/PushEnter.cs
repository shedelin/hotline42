using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PushEnter : MonoBehaviour
{
	public Texture2D	cursorTexture;
	private int			i = 0;
	private bool		active = true;

	void Start()
	{
		Cursor.SetCursor(cursorTexture, Vector2.zero, CursorMode.Auto);
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (Input.GetKeyDown (KeyCode.Return))
			Application.LoadLevel(1);
		if (Input.GetKeyDown (KeyCode.Escape))
			Application.Quit();
		if (i++ > 10)
		{
			i = 0;
			transform.GetChild(0).GetComponent<Text>().enabled = (active = !active);
			active = !active;
			transform.GetChild(1).GetComponent<Text>().enabled = !(active = !active);
		}

	}
}
