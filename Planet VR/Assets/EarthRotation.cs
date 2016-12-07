using UnityEngine;
using System;

public class EarthRotation : MonoBehaviour {

	// Update is called once per frame
	void Update () {

        var fractionOfDay = (DateTime.UtcNow - DateTime.Today).TotalDays;
        transform.rotation = Quaternion.AngleAxis(Mathf.LerpAngle(-180f, 180f, (float)(fractionOfDay / 1)), Vector3.up);
	}
}
