using UnityEngine;

public class DrawManager : Singleton<DrawManager>
{
	private LineRenderer brush;
	private Vector2 lastPos;

	private void Update()
	{
		if (Input.GetMouseButtonDown(0))
		{
			StartDraw();
		}
		else if (Input.GetMouseButtonDown(0))
		{
			Draw();
		}
		else // finish
		{
			EndDraw();
		}
	}

	void AddPoint(Vector2 pointPos)
	{
		curLineRenderer.positionCount++;
		int positionIndex = curLineRenderer.positionCount - 1;
		curLineRenderer.SetPosition(positionIndex, pointPos);
	}

	void Draw()
	{
		Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		if (lastPos != mousePos)
		{
			AddPoint(mousePos);
			lastPos = mousePos;
		}
	}

	void StartDraw()
	{
		Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

		brush.SetPosition(0, mousePos);
		brush.SetPosition(1, mousePos);
	}

	void EndDraw()
	{
		brush.positionCount = 0;
	}
}
