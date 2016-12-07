using UnityEngine;
using System;
using Zeptomoby.OrbitTools;

public class BasicOrbit : MonoBehaviour {
    
    Tle TLE;
    Satellite Sat;

    static float OrbitScale = 6300f;

    public string str1;
    public string str2;
    public string str3;

	// Update is called once per frame
	void Update () {

        if (str1.Length == 0) return;

        Eci e = Sat.PositionEci(Time.time / 60.0f);
     //   Debug.Log("Position: "+ e.Position.X+" "+ e.Position.Y+" "+ e.Position.Z);

        transform.position = new Vector3((float)e.Position.X, (float)e.Position.Y, (float)e.Position.Z) / OrbitScale;

    }

    public void Init (string name, string line1, string line2)
    {
        str1 = name;
        str2 = line1;
        str3 = line2;

        TLE = new Tle(str1, str2, str3);
        Sat = new Satellite(TLE);

        int year = (int)Tle.Field.EpochYear;
        double day = (double)Tle.Field.EpochDay;
    }

}
