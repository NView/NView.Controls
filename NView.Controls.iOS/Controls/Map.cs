using System;

using UIKit;
using Foundation;
using MapKit;
using CoreLocation;

namespace NView.Controls
{
	public class Map : IView
	{
		MKMapView map;
		UIGestureRecognizer tap;

		MapCoordinate setRegionCoord;
		double setRegionDistance = 100000;

		public event EventHandler<MapTappedEventArgs> Tapped = delegate {};

		public Map ()
		{
		}

		CLLocationCoordinate2D GetCoord (MapCoordinate c)
		{
			return new CLLocationCoordinate2D (c.Latitude, c.Longitude);
		}

		public void SetCenterCoordinate (MapCoordinate centerCoord, bool animated = false)
		{
			setRegionCoord = centerCoord;
			if (map == null)
				return;
			map.SetCenterCoordinate (GetCoord (centerCoord), animated);
		}

		public void SetRegion (MapCoordinate centerCoord, double visibleMeters, bool animated = false)
		{
			setRegionCoord = centerCoord;
			setRegionDistance = visibleMeters;
			if (map == null)
				return;
			map.SetRegion (MKCoordinateRegion.FromDistance (GetCoord (centerCoord), visibleMeters, visibleMeters), animated);
		}

		#region IView implementation

		void HandleTap (UITapGestureRecognizer g)
		{
			if (map == null)
				return;
			var c = map.ConvertPoint (g.LocationInView (map), map);
			Tapped (this, new MapTappedEventArgs { Coordinate = new MapCoordinate (c.Latitude, c.Longitude) });
		}

		public object CreateNative (object context = null)
		{
			return new MKMapView {
				ZoomEnabled = true,
				PitchEnabled = true,
				RotateEnabled = true,
			};
		}

		public void BindToNative (object native, BindOptions options = BindOptions.None)
		{
			UnbindFromNative ();
			map = ViewHelpers.GetView<MKMapView> (native);

			tap = new UITapGestureRecognizer (HandleTap);
			map.AddGestureRecognizer (tap);

			map.SetRegion (MKCoordinateRegion.FromDistance (
				GetCoord (setRegionCoord), setRegionDistance, setRegionDistance),
				false);
		}

		public void UnbindFromNative ()
		{
			if (map != null && tap != null) {
				map.RemoveGestureRecognizer (tap);
			}
			tap = null;
			map = null;
		}

		#endregion
	}
}

