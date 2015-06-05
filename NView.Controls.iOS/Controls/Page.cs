using System;
using System.Collections.Generic;
using System.Linq;
using UIKit;

namespace NView.Controls
{
	public class Page : IView
	{
		static readonly bool isPhone = UIDevice.CurrentDevice.UserInterfaceIdiom == UIUserInterfaceIdiom.Phone;
		UIViewController controller;
		readonly List<IView> tools = new List<IView> ();

		public string Title { get; set; }
		public IView View { get; set; }
		public IList<IView> Tools { get { return tools; } }
		public Button AddButton { get; set; }

		public Page ()
		{
			Title = "";
		}

		public Page (string title)
		{
			Title = title ?? "";
		}

		public Page (string title, IView view)
		{
			Title = title ?? "";
			View = view;
		}

		public void PopoverPage (Page page, IView presenter)
		{
			if (controller == null)
				return;
			
			var vc = page.CreateBoundNativeViewController ();

			if (isPhone && !(vc is UINavigationController)) {

				var nav = new UINavigationController (vc);

				vc.NavigationItem.RightBarButtonItem = new UIBarButtonItem (UIBarButtonSystemItem.Done, (s, e) => {
					nav.DismissViewController (true, null);
				});

				vc = nav;
			}

			vc.ModalPresentationStyle = UIModalPresentationStyle.Popover;

			var pc = vc.PopoverPresentationController;

			var toolPresenter = Tools.IndexOf (presenter);
			if (toolPresenter >= 0) {
				pc.BarButtonItem = controller.NavigationItem.RightBarButtonItems [toolPresenter];
			} else {
				var addPresenter = presenter == AddButton;
				if (addPresenter) {
					pc.BarButtonItem = controller.NavigationItem.LeftBarButtonItem;
				} else {
					pc.SourceView = controller.View;
				}
			}

			controller.PresentViewController (vc, true, null);
		}

		public void PushPage (Page page)
		{
			if (controller == null)
				return;

			var n = controller.NavigationController;
			if (n == null)
				return;

			n.PushViewController (page.CreateBoundNativeViewController (), true);
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
			return View.CreateNativeViewController ();
		}

		public void BindToNative (object native, BindOptions options = BindOptions.None)
		{
			UnbindFromNative ();

			if (View == null)
				return;
			
			View.BindToNative (native, options);

			controller = native as UIViewController;
			if (controller != null) {
				var toolItems = tools.Select (x => CreateToolItem (x)).ToArray ();
				controller.NavigationItem.RightBarButtonItems = toolItems;

				if (AddButton != null) {
					controller.NavigationItem.LeftBarButtonItem = CreateAddItem (AddButton);
				} else {
					controller.NavigationItem.LeftBarButtonItem = null;
				}

				controller.Title = Title ?? "";
			}
		}

		public void UnbindFromNative ()
		{
			if (View != null) {
				View.UnbindFromNative ();
			}

			controller = null;
		}

		#endregion
	}
}

