using FMODUnity;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using UnityEngine;

public class DrawManager : Singleton<DrawManager>
{
	public EventReference sfx;

	private float drawnDist = 0;
	private bool isDrawing = false;
	private Vector2 lastPos;
	private Vector2 startPos;
	
	private LineRenderer brush;
	private PolygonCollider2D poly;

	protected override void Awake()
	{
		base.Awake();
		brush = GetComponentInChildren<LineRenderer>();
		poly = GetComponentInChildren<PolygonCollider2D>();
	}

	private void Update()
	{
		if (Input.GetMouseButtonDown(0))
			StartDraw();

		if (!isDrawing)
			return;

		if (Input.GetMouseButton(0))
		{
			Draw();
		}
		else if (Input.GetMouseButtonUp(0))
		{
			print("finish");
			EndDraw();
		}
	}

	void AddPoint(Vector2 pointPos)
	{
		brush.positionCount++;
		int positionIndex = brush.positionCount - 1;
		brush.SetPosition(positionIndex, pointPos);

		// copying points feat awful conversion bullshit
		var points = new Vector3[brush.positionCount];
		brush.GetPositions(points);
		poly.points = Helpers.ConvertToVector2Array(points);
	}

	void Draw()
	{
		var mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		var dist = Vector2.Distance(lastPos, mousePos);

		if (dist > .1f)
		{
			drawnDist += dist;
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

		drawnDist = 0;
		isDrawing = true;
	}

	void EndDraw()
	{
		brush.positionCount = 0;
		isDrawing = false;
	}

	void Encircle()
	{
		print("CIRCLED AROUND SOME SHIT!");
		EndDraw();
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
