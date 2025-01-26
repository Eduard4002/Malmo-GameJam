using FMOD.Studio;
using FMODUnity;
using UnityEngine;

public class Music : Singleton<Music>
{
	[SerializeField]
	private EventReference music;
	private EventInstance musicInstance;

	[SerializeField]
	private float target = 0;
	private float cur = 0;

	private void Start()
	{
		musicInstance = AudioManager.Instance.CreateInstance(music);
		musicInstance.start();
		target = 0;
	}

	private void Update()
	{
		cur = Mathf.Lerp(cur, target, Time.deltaTime);
		musicInstance.setParameterByName("switch", cur);
	}

	public void SetTarget(float newTarget)
	{
		target = newTarget;
	}
}
