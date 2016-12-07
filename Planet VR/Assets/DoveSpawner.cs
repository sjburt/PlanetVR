using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.IO;

public class DoveSpawner : MonoBehaviour {

    string line;
    public GameObject SatellitePrefab;
    public Transform Earth;

    // Use this for initialization
    void Start()
    {
        StartCoroutine("DL");
    }
    IEnumerator DL() {
        WWW www = new WWW("http://ephemerides.planet-labs.com/planet_mc.tle");
        yield return www;

        StreamReader TLE = new StreamReader(GenerateStreamFromString(www.text));
        {
            while ((line = TLE.ReadLine()) != null)
            {
                GameObject satPrefabInstance = GameObject.Instantiate(SatellitePrefab, Vector3.zero, Quaternion.identity) as GameObject;
                satPrefabInstance.GetComponent<BasicOrbit>().Init(line, TLE.ReadLine(), TLE.ReadLine());
                satPrefabInstance.name = line;
                satPrefabInstance.transform.GetChild(0).GetComponent<UnityStandardAssets.Cameras.AbstractTargetFollower>().m_Target = Earth;
                satPrefabInstance.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<Text>().text = line.Split(' ')[3];
            }

            TLE.Close();
        }
    }

    public static Stream GenerateStreamFromString(string s)
    {
        MemoryStream stream = new MemoryStream();
        StreamWriter writer = new StreamWriter(stream);
        writer.Write(s);
        writer.Flush();
        stream.Position = 0;
        return stream;
    }
}
