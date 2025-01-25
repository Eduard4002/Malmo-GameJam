using UnityEngine;
using FMODUnity;
using FMOD.Studio;
using System.Threading.Tasks;
using System.Collections.Generic;

public class AudioManager : Singleton<AudioManager>
{
	[field: SerializeField, Range(0f, 1f)] public float MasterVolume	{ get; set; } = 1f;
	[field: SerializeField, Range(0f, 1f)] public float MusicVolume		{ get; set; } = 1f;
	[field: SerializeField, Range(0f, 1f)] public float SFXVolume		{ get; set; } = 1f;
	[field: SerializeField, Range(0f, 1f)] public float AmbienceVolume	{ get; set; } = 1f;
	private Bus masterBus, sfxBus, ambienceBus, musicBus;

	private List<EventInstance> instances = new List<EventInstance>();

	protected override void Awake()
	{
		base.Awake();
		masterBus	= RuntimeManager.GetBus("bus:/");
		sfxBus		= RuntimeManager.GetBus("bus:/SFX");
		ambienceBus = RuntimeManager.GetBus("bus:/Ambience");
		musicBus	= RuntimeManager.GetBus("bus:/Music");
	}

	private void Update()
	{
		sfxBus.setVolume(SFXVolume);
		musicBus.setVolume(MusicVolume);
		masterBus.setVolume(MasterVolume);
		ambienceBus.setVolume(AmbienceVolume);
	}

	public void PlayOneShot(EventReference sfx) => RuntimeManager.PlayOneShot(sfx);
	public void PlayOneShot(EventReference sfx, Vector3 pos) => RuntimeManager.PlayOneShot(sfx, pos);

	public async void PlayOneShotDelayed(EventReference sfx, float delay = 0f)
	{
		await Task.Delay((int)(delay * 1000));
		RuntimeManager.PlayOneShot(sfx);
	}

	public EventInstance CreateInstance(EventReference sfx)
	{
		if (sfx.IsNull)
		{
			Debug.LogWarning($"SFX is null: {sfx}");
			return new EventInstance();
		}

		var instance = RuntimeManager.CreateInstance(sfx);
		instances.Add(instance);
		return instance;
	}

	private void CleanUp()
	{
		foreach (var instance in instances)
		{
			instance.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
			instance.release();
		}
	}

	private void OnDestroy()
	{
		CleanUp();
	}
}
