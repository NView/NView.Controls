
using System;

using Android.Widget;
using Android.Runtime;

namespace NView.Controls
{		
	/// <summary>
	/// Cross platform Switch for NView
	/// </summary>
	[Preserve]
	public class Switch : IView
	{
		Android.Widget.Switch switchControl;


		bool enabled = true;

		/// <summary>
		/// Gets or sets a value indicating whether this <see cref="NView.Controls.Switch"/> is enabled.
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
		/// Gets or sets a value indicating whether this <see cref="NView.Controls.Switch"/> is checked.
		/// </summary>
		/// <value><c>true</c> if enabled; otherwise, <c>false</c>.</value>
		public bool Checked {
			get { return isChecked; }
			set { 
				isChecked = value;
				if (switchControl == null)
					return;

				switchControl.Checked = isChecked; 
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

			switchControl = ViewHelpers.GetView<Android.Widget.Switch> (nativeView);

			if (options.HasFlag (BindOptions.PreserveNativeProperties)) {

				isChecked = switchControl.Checked;
				enabled = switchControl.Enabled;

			} else {

				switchControl.Checked = isChecked;
				switchControl.Enabled = enabled;

			}

			switchControl.CheckedChange += SwitchControl_CheckedChange;
		}



		/// <inheritdoc/>
		public void UnbindFromNative ()
		{
			if (switchControl == null)
				return;
			switchControl.CheckedChange -= SwitchControl_CheckedChange;
			switchControl = null;
		}

		void SwitchControl_CheckedChange (object sender, CompoundButton.CheckedChangeEventArgs e)
		{
			if (switchControl != null)
				isChecked = switchControl.Checked;

			if (CheckedChanged == null)
				return;

			CheckedChanged (this, new EventArgs ());
		}

		/// <inheritdoc/>
		public object CreateNative (object context = null)
		{
			return new Android.Widget.Switch ((Android.Content.Context)context);
		}

		#endregion
	}
}

