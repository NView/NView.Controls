using System;

using Foundation;
using UIKit;

namespace NView.Controls
{
	/// <summary>
	/// Cross platform Form View for NView. Based on MonoTouch.Dialog.
	/// </summary>
	[Preserve]
	public class FormView : IView
	{
		UITableViewController tcontroller;
		RootElement root = new RootElement ();

		public RootElement Root {
			get {
				return root;
			}
			set {
				root = value ?? new RootElement ();
			}
		}

		public FormView ()
		{			
		}

		public FormView (RootElement root)
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
			tv.Source = new FormViewSource { Form = this };
		}

		class FormViewSource : UITableViewSource
		{
			public FormView Form;
			public override nint NumberOfSections (UITableView tableView)
			{
				return Form.root.Count;
			}
			public override nint RowsInSection (UITableView tableview, nint section)
			{
				return Form.root [(int)section].Count;
			}
			public override UITableViewCell GetCell (UITableView tableView, NSIndexPath indexPath)
			{
				var reuseId = new NSString ("R");
				var cell = tableView.DequeueReusableCell (reuseId);
				if (cell == null) {
					cell = new UITableViewCell (UITableViewCellStyle.Default, reuseId);
				}
				cell.TextLabel.Text = Form.root [indexPath.Section] [indexPath.Row].Text;
				return cell;
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

