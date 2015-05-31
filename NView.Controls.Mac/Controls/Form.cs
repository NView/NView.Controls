using System;
using System.Linq;

using Foundation;
using AppKit;
using CoreGraphics;

namespace NView.Controls
{
	/// <summary>
	/// Cross platform Form View for NView. Based on MonoTouch.Dialog.
	/// </summary>
	[Preserve]
	public class Form : IView
	{
		TableViewController tcontroller;
		RootElement root = new RootElement ();

		public RootElement Root {
			get {
				return root;
			}
			set {
				root = value ?? new RootElement ();
				WithTV (tv => {
					var fvs = tv.Source as FormSource;
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

		void WithTV (Action<NSTableView> action)
		{
			if (tcontroller != null) {
				var tv = tcontroller.TableView;
				if (tv != null)
					action (tv);
			}
		}

		void RemoveDelegates (NSTableView tv)
		{
			tv.Source = null;
		}

		void SetDelegates (NSTableView tv)
		{
			tv.Source = new FormSource { Root = root, Controller = tcontroller };
		}

		class FormCell : NSTableCellView
		{
			public IView BoundValueView = null;
			public FormCell (string reuseId)
			{
				Identifier = reuseId;
				var t = new NSTextField (new CGRect (16, 6, 140, 20)) {
					Editable = false,
					BackgroundColor = NSColor.Clear,
					Bordered = false,
					Font = NSFont.SystemFontOfSize (NSFont.SystemFontSize),
				};
				this.AddSubview (t);
				TextField = t;
			}
		}

		class FormSource : NSTableViewSource
		{
			public RootElement Root;
			public NSViewController Controller;

			public override nint GetRowCount (NSTableView tableView)
			{
				return Root.Sum (x => x.Count);
			}

			public override NSView GetViewForItem (NSTableView tableView, NSTableColumn tableColumn, nint row)
			{
				var c = tableView.MakeView ("R", tableView) as FormCell;
				if (c == null) {
					c = new FormCell ("R");
				}
				var sec = 0;
				var r = (int)row;
				while (sec < Root.Count && r >= Root [sec].Count) {
					r -= Root [sec].Count;
					sec++;
				}
				c.TextField.StringValue = Root [sec] [r].Text;
				return c;
			}
		}

		#region IView implementation

		/// <inheritdoc/>
		public void BindToNative (object nativeView, BindOptions options = BindOptions.None)
		{
			UnbindFromNative ();
			tcontroller = nativeView as TableViewController;
			if (tcontroller == null)
				throw new Exception ("Cannot bind Form to " + nativeView);
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
				return typeof(NSViewController);
			}
		}

		/// <inheritdoc/>
		public object CreateNative (object context = null)
		{
			return new TableViewController ();
		}

		class TableViewController : NSViewController
		{
			public readonly NSTableView TableView;
			public TableViewController ()
			{
				var rect = new CGRect (0, 0, 320, 480);
				TableView = new NSTableView (rect) {
					RowHeight = 32,
					HeaderView = null,
					//AllowsColumnResizing = false,
					AllowsColumnReordering = false,
					AllowsColumnSelection = false,
					AllowsEmptySelection = true,
					BackgroundColor = NSColor.FromWhite ((nfloat)(247.0/255.0), 1),
				};
				TableView.AddColumn (new NSTableColumn ("col0") {
					Width = rect.Width/3,
				});
				var scroll = new NSScrollView (rect);
				scroll.DocumentView = TableView;
				scroll.HasVerticalScroller = true;
				View = scroll;
			}
		}

		#endregion
	}
}

