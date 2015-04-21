
using System;
using AppKit;
using Foundation;

namespace NView.Controls
{		
	/// <summary>
	/// Cross platform Stack layout for NView. Stacks can be horizontal or vertical.
	/// </summary>
	[Preserve]
	public class Toggle : IView
	{
		NSButton switchControl;


		bool enabled = true;

		/// <summary>
		/// Gets or sets a value indicating whether this <see cref="NView.Controls.Toggle"/> is enabled.
		/// </summary>
		/// <value><c>true</c> if enabled; otherwise, <c>false</c>.</value>
		public bool Enabled {
			get { return enabled; }
			set { 
				enabled = value;
				if (switchControl == null)
					return;

				switchControl.Enabled = enabled; 
			}
		}

		bool isChecked = true;

		/// <summary>
		/// Gets or sets a value indicating whether this <see cref="NView.Controls.Toggle"/> is checked.
		/// </summary>
		/// <value><c>true</c> if enabled; otherwise, <c>false</c>.</value>
		public bool Checked {
			get { return isChecked; }
			set { 
				isChecked = value;
				if (switchControl == null)
					return;

				switchControl.State = isChecked ? NSCellStateValue.On : NSCellStateValue.Off;
			}
		}

		/// <summary>
		/// Event that occurs when checked state has changed.
		/// </summary>
		public event EventHandler CheckedChanged;

		#region IView implementation

		/// <inheritdoc/>
		public void BindToNative (object nativeView, BindOptions options = BindOptions.None)
		{
			if (nativeView == null)
				throw new ArgumentNullException ("nativeView");

			UnbindFromNative ();

			switchControl = ViewHelpers.GetView<NSButton> (nativeView);
			switchControl.SetButtonType (NSButtonType.Switch);
			switchControl.AllowsMixedState = false;

			if (options.HasFlag (BindOptions.PreserveNativeProperties)) {

				isChecked = switchControl.State == NSCellStateValue.On;
				enabled = switchControl.Enabled;

			} else {

				switchControl.State = isChecked ? NSCellStateValue.On : NSCellStateValue.Off;
				switchControl.Enabled = enabled;

			}

			switchControl.Activated += SwitchControl_Activated;
		}



		/// <inheritdoc/>
		public void UnbindFromNative ()
		{
			if (switchControl == null)
				return;
			switchControl.Activated -= SwitchControl_Activated;
			switchControl = null;
		}

		void SwitchControl_Activated (object sender, EventArgs e)
		{

			if (switchControl != null)
				isChecked = switchControl.State == NSCellStateValue.On;

			if (CheckedChanged == null)
				return;

			CheckedChanged (this, new EventArgs ());
		}

		/// <inheritdoc/>
		public Type PreferredNativeType {
			get {
				return typeof(NSButton);
			}
		}

		#endregion
	}
}

