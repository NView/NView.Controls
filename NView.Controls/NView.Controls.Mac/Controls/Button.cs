using System;
using AppKit;

namespace NView.Controls
{
	/// <summary>
	/// Button implementation for Android.
	/// </summary>
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
		public IDisposable BindToNative (object nativeView)
		{
			if (nativeView == null)
				throw new ArgumentNullException ("nativeView");
			
			button = ViewHelpers.GetView<NSButton> (nativeView);

			//Toggle enabled if needed
			if (button.Enabled != Enabled) {
				Enabled = button.Enabled;
			}

			//If the user didn't set text, set local version, 
			//else we want to take in the button text to sync
			if (string.IsNullOrEmpty (button.StringValue)) {
				button.StringValue = text;
			} else {
				text = button.StringValue;
			}


			button.Activated += Button_Activated;

			return new DisposeAction (() => {
				button.Activated -= Button_Activated;
				button = null;
			});
		}

		void Button_Activated (object sender, EventArgs e)
		{
			if (Clicked != null)
				Clicked (this, new EventArgs ());
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

