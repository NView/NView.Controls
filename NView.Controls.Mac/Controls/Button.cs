using System;
using AppKit;
using Foundation;

namespace NView.Controls
{
	/// <summary>
	/// Button implementation for Android.
	/// </summary>
	[Preserve]
	public class Button : IView
	{
		NSButton button;

		string text = string.Empty;

		/// <summary>
		/// Gets or sets the title.
		/// </summary>
		/// <value>The title.</value>
		public string Text {
			get { return text; } 
			set { 

				text = value;

				if (button == null)
					return;

				button.StringValue = text ?? string.Empty; 
			} 
		}

		bool enabled = true;

		/// <summary>
		/// Gets or sets a value indicating whether this <see cref="NView.Controls.Button"/> is enabled.
		/// </summary>
		/// <value><c>true</c> if enabled; otherwise, <c>false</c>.</value>
		public bool Enabled {
			get { return enabled; }
			set { 

				enabled = value;
				if (button == null)
					return;

				button.Enabled = enabled; 
			}
		}

		/// <summary>
		/// Event that occurs when button is clicked.
		/// </summary>
		public event EventHandler Clicked;

		#region IView implementation

		/// <inheritdoc/>
		public void BindToNative (object nativeView, BindOptions options = BindOptions.None)
		{
			if (nativeView == null)
				throw new ArgumentNullException ("nativeView");

			UnbindFromNative ();
			
			button = ViewHelpers.GetView<NSButton> (nativeView);

			if (options.HasFlag (BindOptions.PreserveNativeProperties)) {

				enabled = button.Enabled;
				text = button.Title;

			} else {

				button.Enabled = enabled;
				button.Title = text;

			}

			button.Activated += Button_Activated;
		}

		/// <inheritdoc/>
		public void UnbindFromNative ()
		{
			if (button == null)
				return;
			button.Activated -= Button_Activated;
			button = null;
		}

		void Button_Activated (object sender, EventArgs e)
		{
			if (Clicked != null)
				Clicked (this, new EventArgs ());
		}

		/// <inheritdoc/>
		public object CreateNative (object context = null)
		{
			return new NSButton ();
		}

		#endregion
	}
}

