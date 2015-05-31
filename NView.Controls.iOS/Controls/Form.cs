using System;

using Foundation;
using UIKit;

namespace NView.Controls
{
	/// <summary>
	/// Cross platform Form View for NView. Based on MonoTouch.Dialog.
	/// </summary>
	[Preserve]
	public class Form : IView
	{
		UITableViewController tcontroller;
		RootElement root = new RootElement ();

		public RootElement Root {
			get {
				return root;
			}
			set {
				root = value ?? new RootElement ();
				WithTV (tv => {
					var fvs = tv.Source as FormViewSource;
					if (fvs != null) {
						fvs.Root = root;
						tv.ReloadData ();
					}
				});
			}
		}

		public Form ()
		{			
		}

		public Form (RootElement root)
		{
			this.root = root;
		}

		void WithTV (Action<UITableView> action)
		{
			if (tcontroller != null) {
				var tv = tcontroller.TableView;
				if (tv != null)
					action (tv);
			}
		}

		void RemoveDelegates (UITableView tv)
		{
			tv.Source = null;
		}

		void SetDelegates (UITableView tv)
		{
			tv.Source = new FormViewSource { Root = root, Controller = tcontroller };
		}

		class FormViewSource : UITableViewSource
		{
			public RootElement Root;
			public UIViewController Controller;
			public override nint NumberOfSections (UITableView tableView)
			{
				return Root.Count;
			}
			public override nint RowsInSection (UITableView tableview, nint section)
			{
				return Root [(int)section].Count;
			}
			readonly NSString defaultReuseId = new NSString ("_T");
			readonly NSString valueTextReuseId = new NSString ("_V");
			public override UITableViewCell GetCell (UITableView tableView, NSIndexPath indexPath)
			{
				var elm = Root [indexPath.Section] [indexPath.Row];

				var reuseId = defaultReuseId;
				var style = UITableViewCellStyle.Default;
				if (!string.IsNullOrEmpty (elm.ValueText)) {
					reuseId = valueTextReuseId;
					style = UITableViewCellStyle.Value1;
				} else if (elm.ValueView != null) {
					var r = elm.ValueView.GetType ().Name;
					style = UITableViewCellStyle.Subtitle;
					if (string.IsNullOrEmpty (elm.Text)) {
						r = "_N" + r;	
					}
					reuseId = new NSString (r);
				}

				var cell = tableView.DequeueReusableCell (reuseId);
				if (cell == null) {
					cell = new UITableViewCell (style, reuseId);
					cell.SelectionStyle = UITableViewCellSelectionStyle.None;
				}

				var acc = UITableViewCellAccessory.None;
				var sel = UITableViewCellSelectionStyle.None;
				if (elm is RootElement) {
					acc = UITableViewCellAccessory.DisclosureIndicator;
					sel = UITableViewCellSelectionStyle.Default;
				}
				cell.Accessory = acc;
				cell.SelectionStyle = sel;

				cell.TextLabel.Text = elm.Text;

				if (style == UITableViewCellStyle.Value1) {
					cell.DetailTextLabel.Text = elm.ValueText;
				} else if (style == UITableViewCellStyle.Subtitle) {
					cell.DetailTextLabel.Text = elm.DetailText;
				}

				return cell;
			}
			public override string TitleForFooter (UITableView tableView, nint section)
			{
				return Root [(int)section].FooterText;
			}
			public override void RowSelected (UITableView tableView, NSIndexPath indexPath)
			{
				var elm = Root [indexPath.Section] [indexPath.Row];

				elm.Select ();

				var root = elm as RootElement;
				if (root != null) {
					var nav = Controller.NavigationController;
					if (nav != null) {
						var nextForm = new Form (root);
						nav.PushViewController (ViewHelpers.CreateBoundNativeViewController (nextForm), true);
					}
				}
			}
		}

		#region IView implementation

		/// <inheritdoc/>
		public void BindToNative (object nativeView, BindOptions options = BindOptions.None)
		{
			UnbindFromNative ();
			tcontroller = nativeView as UITableViewController;
			if (tcontroller == null)
				throw new Exception ("Cannot bind FormView to " + nativeView);
			tcontroller.Title = root.Text;
			WithTV (SetDelegates);
		}

		/// <inheritdoc/>
		public void UnbindFromNative ()
		{
			WithTV (RemoveDelegates);
			tcontroller = null;
		}

		/// <inheritdoc/>
		public Type NativeType {
			get {
				return typeof(UITableViewController);
			}
		}

		/// <inheritdoc/>
		public object CreateNative (object context = null)
		{
			return new UITableViewController (UITableViewStyle.Grouped);
		}

		#endregion
	}
}

