
using System;
using UIKit;
using Foundation;

namespace NView.Controls
{		
	/// <summary>
	/// Cross platform Stack layout for NView. Stacks can be horizontal or vertical.
	/// </summary>
	[Preserve]
	public class Toggle : IView
	{
		UISwitch switchControl;


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

				switchControl.On = isChecked; 
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

			switchControl = ViewHelpers.GetView<UISwitch> (nativeView);

			if (switchControl == null)
				throw new InvalidOperationException ("Cannot convert " + nativeView + " to UISwitch");

			if (options.HasFlag (BindOptions.PreserveNativeProperties)) {

				isChecked = switchControl.On;
				enabled = switchControl.Enabled;

			} else {

				switchControl.On = isChecked;
				switchControl.Enabled = enabled;

			}

			switchControl.ValueChanged += SwitchControl_ValueChanged;
		}

		/// <inheritdoc/>
		public void UnbindFromNative ()
		{
			if (switchControl == null)
				return;
			switchControl.ValueChanged -= SwitchControl_ValueChanged;
			switchControl = null;
		}

		void SwitchControl_ValueChanged (object sender, EventArgs e)
		{
			if (switchControl != null)
				isChecked = switchControl.On;

			if (CheckedChanged == null)
				return;

			CheckedChanged (this, new EventArgs ());
		}

		/// <inheritdoc/>
		public object CreateNative (object context = null)
		{
			return new UISwitch ();
		}

		#endregion
	}
}

