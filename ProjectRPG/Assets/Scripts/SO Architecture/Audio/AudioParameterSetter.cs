﻿using UnityEngine;
using UnityEngine.Audio;
using SOArchitecture.Variable;
using SOArchitecture.Reference;

namespace SOArchitecture.Audio{
	/// <summary>
	/// Takes a FloatVariable and sends a curve filtered version of its value 
	/// to an exposed audio mixer parameter every frame on Update.
	/// </summary>
	public class AudioParameterSetter : ISetter
	{
		[Tooltip("Variable to send to the mixer parameter.")]
		public FloatVariable variable;

		[Tooltip("Minimum value of the Variable that is mapped to the curve.")]
		public FloatReference min;

		[Tooltip("Maximum value of the Variable that is mapped to the curve.")]
		public FloatReference max;

		[Tooltip("Mixer to set the parameter in.")]
		public AudioMixer mixer;

		[Tooltip("Name of the parameter to set in the mixer.")]
		public string parameterName = "";

		[Tooltip("Curve to evaluate in order to look up a final value to send as the parameter.\n" +
				 "T=0 is when Variable == Min\n" +
				 "T=1 is when Variable == Max")]
		public AnimationCurve curve;

		public override void OnUpdate(){
			float t = Mathf.InverseLerp(min, max, variable);
			float value = curve.Evaluate(Mathf.Clamp01(t));
			mixer.SetFloat(parameterName, value);
		}
	}
}