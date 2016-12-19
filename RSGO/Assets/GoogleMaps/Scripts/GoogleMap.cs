using UnityEngine;
using System.Collections;

public class GoogleMap : MonoBehaviour
{
	public enum MapType
	{
		RoadMap,
		Satellite,
		Terrain,
		Hybrid
	}
	public bool loadOnStart = true;
	public bool autoLocateCenter = true;
	public GoogleMapLocation centerLocation;
	public int zoom = 13;
	public MapType mapType;
	public int size = 512;
	public bool doubleResolution = false;
	public GoogleMapMarker[] markers;
	public GoogleMapPath[] paths;
    public string DebugLog;
    public float count = 0;
	void Start() {
		if(loadOnStart) Refresh();	
	}
	
	public void Refresh() {
		if(autoLocateCenter && (markers.Length == 0 && paths.Length == 0)) {
			Debug.LogError("Auto Center will only work if paths or markers are used.");	
		}
		StartCoroutine(_Refresh());
	}

    IEnumerator _Refresh()
    {
        var url = "http://maps.googleapis.com/maps/api/staticmap";
        var qs = "key=AIzaSyDYAO3AFpRCBjqQvA2Fo94qXkytGF6UH78";
        if (!autoLocateCenter)
        {
            if (centerLocation.address != "")
                qs += "&center=" + WWW.UnEscapeURL(centerLocation.address);
            else
            {
                qs += "&center=" + WWW.UnEscapeURL(string.Format("{0},{1}", centerLocation.latitude, centerLocation.longitude));
            }

            qs += "&zoom=" + zoom.ToString();
        }
        qs += "&size=" + WWW.UnEscapeURL(string.Format("{0}x{0}", size));
        qs += "&scale=" + (doubleResolution ? "2" : "1");
        qs += "&maptype=" + mapType.ToString().ToLower();
        var usingSensor = false;
#if UNITY_IPHONE
		usingSensor = Input.location.isEnabledByUser && Input.location.status == LocationServiceStatus.Running;
#endif
        qs += "&sensor=" + (usingSensor ? "true" : "false");

        foreach (var i in markers)
        {
            qs += "&markers=" + string.Format("size:{0}|color:{1}|label:{2}", i.size.ToString().ToLower(), i.color, i.label);
            foreach (var loc in i.locations)
            {
                if (loc.address != "")
                    qs += "|" + WWW.UnEscapeURL(loc.address);
                else
                    qs += "|" + WWW.UnEscapeURL(string.Format("{0},{1}", loc.latitude, loc.longitude));
            }
        }

        foreach (var i in paths)
        {
            qs += "&path=" + string.Format("weight:{0}|color:{1}", i.weight, i.color);
            if (i.fill) qs += "|fillcolor:" + i.fillColor;
            foreach (var loc in i.locations)
            {
                if (loc.address != "")
                    qs += "|" + WWW.UnEscapeURL(loc.address);
                else
                    qs += "|" + WWW.UnEscapeURL(string.Format("{0},{1}", loc.latitude, loc.longitude));
            }
        }
        
        qs += "&style=element:geometry%7Ccolor:0xebe3cd&style=element:labels%7Cvisibility:off&style=element:labels.text.fill%7Ccolor:0x523735&style=element:labels.text.stroke%7Ccolor:0xf5f1e6&style=feature:administrative%7Celement:geometry.stroke%7Ccolor:0xc9b2a6&style=feature:administrative.land_parcel%7Cvisibility:off&style=feature:administrative.land_parcel%7Celement:geometry.stroke%7Ccolor:0xdcd2be&style=feature:administrative.land_parcel%7Celement:labels.text.fill%7Ccolor:0xae9e90&style=feature:administrative.neighborhood%7Cvisibility:off&style=feature:landscape.natural%7Celement:geometry%7Ccolor:0xdfd2ae&style=feature:poi%7Celement:geometry%7Ccolor:0xdfd2ae&style=feature:poi%7Celement:labels.text.fill%7Ccolor:0x93817c&style=feature:poi.park%7Celement:geometry.fill%7Ccolor:0xa5b076&style=feature:poi.park%7Celement:labels.text.fill%7Ccolor:0x447530&style=feature:road%7Celement:geometry%7Ccolor:0xf5f1e6&style=feature:road.arterial%7Celement:geometry%7Ccolor:0xfdfcf8&style=feature:road.arterial%7Celement:labels%7Cvisibility:off&style=feature:road.highway%7Celement:geometry%7Ccolor:0xf8c967&style=feature:road.highway%7Celement:geometry.stroke%7Ccolor:0xe9bc62&style=feature:road.highway%7Celement:labels%7Cvisibility:off&style=feature:road.highway.controlled_access%7Celement:geometry%7Ccolor:0xe98d58&style=feature:road.highway.controlled_access%7Celement:geometry.stroke%7Ccolor:0xdb8555&style=feature:road.local%7Cvisibility:off&style=feature:road.local%7Celement:labels.text.fill%7Ccolor:0x806b63&style=feature:transit.line%7Celement:geometry%7Ccolor:0xdfd2ae&style=feature:transit.line%7Celement:labels.text.fill%7Ccolor:0x8f7d77&style=feature:transit.line%7Celement:labels.text.stroke%7Ccolor:0xebe3cd&style=feature:transit.station%7Celement:geometry%7Ccolor:0xdfd2ae&style=feature:water%7Celement:geometry.fill%7Ccolor:0xb9d3c2&style=feature:water%7Celement:labels.text.fill%7Ccolor:0x92998d";
        var req = new WWW(url + "?" + qs);
        yield return req;
        GetComponent<Renderer>().material.mainTexture = req.texture;
        count++;
        DebugLog = "Map downloaded for " + count;
    }

    public void UpdateMap(float lat, float lon)
    {
        centerLocation.latitude = lat;
        centerLocation.longitude = lon;
        StartCoroutine(_Refresh());
    }
	
}

public enum GoogleMapColor
{
	black,
	brown,
	green,
	purple,
	yellow,
	blue,
	gray,
	orange,
	red,
	white
}

[System.Serializable]
public class GoogleMapLocation
{
	public string address;
	public float latitude;
	public float longitude;
}

[System.Serializable]
public class GoogleMapMarker
{
	public enum GoogleMapMarkerSize
	{
		Tiny,
		Small,
		Mid
	}
	public GoogleMapMarkerSize size;
	public GoogleMapColor color;
	public string label;
	public GoogleMapLocation[] locations;
	
}

[System.Serializable]
public class GoogleMapPath
{
	public int weight = 5;
	public GoogleMapColor color;
	public bool fill = false;
	public GoogleMapColor fillColor;
	public GoogleMapLocation[] locations;	
}