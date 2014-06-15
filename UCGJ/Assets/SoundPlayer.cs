using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(AudioSource))]
public class SoundPlayer : SingletonMonoBehaviour<SoundPlayer>{
	public AudioClip bgmClip;
	Dictionary<string, AudioClipInfo> audioClips = new Dictionary<string, AudioClipInfo>();
		
	// AudioClip information
	public class AudioClipInfo {
		public string resourceName;
		public string name;
		public AudioClip clip;			
	
		public AudioClipInfo( string resourceName, string name ) {
			this.resourceName = resourceName;
			this.name = name;
		}
	}

	void Start(){
		audio.clip = bgmClip;
		audio.Play();
	}

	public void addSe(AudioClipInfo info){
		if(!audioClips.ContainsKey(info.name)){
			audioClips.Add(info.name, info);
		}
	}
		
	public bool playSE( string seName ) {
		if (!audioClips.ContainsKey( seName )){
			Debug.Log ("Se not Regist:" + seName);
			return false; // not register
		}
		AudioClipInfo info = audioClips[ seName ];

		// Load
		if ( info.clip == null ){
			Debug.Log ("Se not Found:" + info.resourceName);
			info.clip = (AudioClip)Resources.Load( info.resourceName );
		}
			
			// Play SE
		audio.PlayOneShot( info.clip );
					return true;
		}
}
