using FMOD.Studio;
using FMODUnity;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using UnityEngine;

public class DrawManager : Singleton<DrawManager>
{
	[SerializeField]
	private EventReference drawSFX;
	private EventInstance drawSFXInstance;

	[SerializeField] private Color startColor;
	[SerializeField] private Color endColor;
	[SerializeField] private Color flashColor;

	private float drawTime = 10;
	private float timer = 0;

	private bool isDrawing = false;
	private int firstPointIndex = 0;

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

	private void Start()
	{
		drawSFXInstance = AudioManager.Instance.CreateInstance(drawSFX);
	}

	private void Update()
	{
		if(!GameManager.instance.gameStarted) return;
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

			var mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			if (Vector2.Distance(startPos, mousePos) < .5f)
				Encircle();
		}
	}

	public void SetDrawTime(float t)
	{
		drawTime = t;
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
		drawSFXInstance.start();

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
		drawSFXInstance.stop(STOP_MODE.IMMEDIATE);
		brush.positionCount = 0;
		isDrawing = false;
	}

	void Encircle()
	{
		UpdatePolygon();
		EndDraw();
		
		var overlapped = GetOverlappedObjects();
		ObjectSelection.Instance.CheckSelection(overlapped);

		var names = overlapped.Select(o => o.name).ToArray();
		print("Encircled: " + string.Join(", ", names));
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
			{
				firstPointIndex = i;
				return true;
			}
		}
		return false;
	}

	private void UpdatePolygon()
	{
		var points = new Vector3[brush.positionCount];
		brush.GetPositions(points);
		poly.points = Helpers.ConvertToVector2Array(points, firstPointIndex);
	}

	private void UpdateVisual()
	{
		// audio
		float p = timer / drawTime;
		drawSFXInstance.setParameterByName("pitch", p);

		// go towards red until last 3rd of time
		float t = (drawTime-timer) / (drawTime-drawTime/3); // sorry i got tired
		brushMat.color = Color.Lerp(startColor, endColor, t);

		// blink on last 3rd of time
		if (t > 1f) 
		{
			// blink
			brushMat.color = Color.Lerp(endColor, flashColor, t*t*t*3 % 1f); //it works, so shut up
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
}
