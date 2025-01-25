using UnityEngine;

public class DrawManager : Singleton<DrawManager>
{
	public Camera cam;
	public GameObject brush;

	private LineRenderer curLineRenderer;
	private Vector2 lastPos;

	private void Update()
	{
		Drawing();
	}

	void Drawing()
	{
		if (Input.GetKeyDown(KeyCode.Mouse0))
		{
			CreateBrush();
		}
		else if (Input.GetKey(KeyCode.Mouse0))
		{
			PointToMousePos();
		}
		else
		{
			curLineRenderer = null;
		}
	}

	void CreateBrush()
	{
		GameObject brushInstance = Instantiate(brush);
		curLineRenderer = brushInstance.GetComponent<LineRenderer>();

		Vector2 mousePos = cam.ScreenToWorldPoint(Input.mousePosition);

		curLineRenderer.SetPosition(0, mousePos);
		curLineRenderer.SetPosition(1, mousePos);
	}

	void AddPoint(Vector2 pointPos)
	{
		curLineRenderer.positionCount++;
		int positionIndex = curLineRenderer.positionCount - 1;
		curLineRenderer.SetPosition(positionIndex, pointPos);
	}

	void PointToMousePos()
	{
		Vector2 mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
		if (lastPos != mousePos)
		{
			AddPoint(mousePos);
			lastPos = mousePos;
		}
	}
}
