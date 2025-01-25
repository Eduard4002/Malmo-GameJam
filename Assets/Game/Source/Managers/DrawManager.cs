using FMODUnity;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using UnityEngine;

public class DrawManager : Singleton<DrawManager>
{
	[SerializeField]
	private float drawTime = 5;
	private float timer = 0;

	private bool isDrawing = false;
	private Vector2 lastPos;
	private Vector2 startPos;
	
	private Material brushMat;
	private LineRenderer brush;
	private PolygonCollider2D poly;

	protected override void Awake()
	{
		base.Awake();
		brush = GetComponentInChildren<LineRenderer>();
		poly = GetComponentInChildren<PolygonCollider2D>();
		brushMat = brush.material;
	}

	private void Update()
	{
		if (Input.GetMouseButtonDown(0))
			StartDraw();

		if (!isDrawing)
			return;

		timer -= Time.deltaTime;
		UpdateVisual();

		if (Input.GetMouseButton(0) && timer > 0)
		{
			Draw();
		}
		else if (Input.GetMouseButtonUp(0) || timer <= 0)
		{
			EndDraw();
		}
	}

	void AddPoint(Vector2 pointPos)
	{
		brush.positionCount++;
		int positionIndex = brush.positionCount - 1;
		brush.SetPosition(positionIndex, pointPos);

		UpdatePolygon();
	}

	void Draw()
	{
		var mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		var dist = Vector2.Distance(lastPos, mousePos);

		if (dist > .1f)
		{
			AddPoint(mousePos);
			lastPos = mousePos;
		}

		if (HasEncirled())
		{
			Encircle();
		}
	}

	void StartDraw()
	{
		var mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		startPos = mousePos;

		brush.positionCount = 2;
		brush.SetPosition(0, mousePos);
		brush.SetPosition(1, mousePos);

		isDrawing = true;
		timer = drawTime;
		poly.points = new Vector2[5];
	}

	void EndDraw()
	{
		brush.positionCount = 0;
		isDrawing = false;
	}

	void Encircle()
	{
		UpdatePolygon();
		print("CIRCLED AROUND SOME SHIT!");
		EndDraw();
		
		var overlapped = GetOverlappedObjects();
		ObjectSelection.Instance.CheckSelection(overlapped);
	}

	private bool HasEncirled()
	{
		if (brush.positionCount < 2)
			return false;

		var endA = brush.GetPosition(brush.positionCount - 2);
		var endB = brush.GetPosition(brush.positionCount - 1);
		var intersection = Vector2.zero;

		for (int i = 0; i < brush.positionCount - 3; i++)
		{
			var a = brush.GetPosition(i + 0);
			var b = brush.GetPosition(i + 1);

			if (Helpers.LineIntersection(endA, endB, a, b, ref intersection))
				return true;
		}
		return false;
	}

	private void UpdatePolygon()
	{
		var points = new Vector3[brush.positionCount];
		brush.GetPositions(points);
		poly.points = Helpers.ConvertToVector2Array(points);
	}

	private void UpdateVisual()
	{
		// go towards red until last 3rd of time
		float t = (5f-timer) / (drawTime-drawTime/3); // sorry i got tired
		brushMat.color = Color.Lerp(Color.green, Color.red, t);

		// blink on last 3rd of time
		if (t > 1f) 
		{
			// blink
			brushMat.color = Color.Lerp(Color.red, Color.white, t*t*t*3 % 1f); //it works, so shut up
		}
	}

	private List<Object> GetOverlappedObjects()
	{
		var overlapped = new List<Object>();
		foreach (var obj in ObjectSpawner.instance.objectsSpawned)
		{
			if (poly.OverlapPoint(obj.transform.position))
			{
				overlapped.Add(obj);
			}
		}
		return overlapped;
	}

	private void OnDrawGizmos()
	{
		if (!Application.isPlaying || brush.positionCount < 2)
			return;

		Gizmos.DrawCube(startPos, Vector2.one * .5f);
		
		//endline
		var endA = brush.GetPosition(brush.positionCount - 2);
		var endB = brush.GetPosition(brush.positionCount - 1);
		var intersection = Vector2.zero;

		for (int i = 0; i < brush.positionCount-3; i++)
		{
			var a = brush.GetPosition(i+0);
			var b = brush.GetPosition(i+1);

			if (Helpers.LineIntersection(endA, endB, a, b, ref intersection))
				print("CROSSED");

			Gizmos.DrawLine(a, b);
		}
	}
}
