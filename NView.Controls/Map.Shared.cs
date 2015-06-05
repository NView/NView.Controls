using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
using System.Linq;

namespace NView.Controls
{
	public struct MapCoordinate
	{
		public double Latitude;
		public double Longitude;

		public MapCoordinate ()
		{
			Latitude = 0.0;
			Longitude = 0.0;
		}

		public MapCoordinate (double latitude, double longitude)
		{
			Latitude = latitude;
			Longitude = longitude;
		}

		public override string ToString ()
		{
			return string.Format ("{{lat: {0}, lng: {1}}}", Latitude, Longitude);
		}
	}

	public class MapTappedEventArgs : EventArgs
	{
		public MapCoordinate Coordinate;
	}
}

