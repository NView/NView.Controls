using System;
using System.Collections.Generic;
using System.Linq;
using UIKit;

namespace NView.Controls
{
	public class Page : IView
	{
		readonly List<IView> tools = new List<IView> ();

		public string Title { get; set; }
		public IView View { get; set; }
		public IList<IView> Tools { get { return tools; } }
		public Button AddButton { get; set; }

		public Page ()
		{
			Title = "";
		}

		public Page (string title, IView view)
		{
			Title = title ?? "";
			View = view;
		}

		#region IView implementation

		UIBarButtonItem CreateToolItem (IView tool)
		{
			var b = tool as Button;
			if (b != null) {
				return new UIBarButtonItem (b.Text, UIBarButtonItemStyle.Plain, (s, e) => b.Click ());
			}
			var v = tool.CreateBoundNativeView ();
			return new UIBarButtonItem (v);
		}

		UIBarButtonItem CreateAddItem (IView tool)
		{
			return new UIBarButtonItem (UIBarButtonSystemItem.Add, (s, e) => {
				var b = tool as Button;
				if (b != null) {
					b.Click ();
				}
			});
		}

		public object CreateNative (object context = null)
		{
			if (View == null) {
				return new UIViewController ();
			}
			var n = View.CreateNative ();
			var vc = n as UIViewController;
			if (vc == null) {
				var v = n as UIView;
				if (v == null)
					throw new Exception ("Cannot create native for " + View);
				vc = new UIViewController ();
				vc.View = v;
			}
			return vc;
		}

		public void BindToNative (object native, BindOptions options = BindOptions.None)
		{
			if (View != null) {
				View.BindToNative (native, options);

				var vc = native as UIViewController;
				if (vc != null) {
					var toolItems = tools.Select (x => CreateToolItem (x)).ToArray ();
					vc.NavigationItem.RightBarButtonItems = toolItems;

					if (AddButton != null) {
						vc.NavigationItem.LeftBarButtonItem = CreateAddItem (AddButton);
					} else {
						vc.NavigationItem.LeftBarButtonItem = null;
					}
				}
			}
		}

		public void UnbindFromNative ()
		{
			if (View != null) {
				View.UnbindFromNative ();
			}
		}

		#endregion
	}
}

