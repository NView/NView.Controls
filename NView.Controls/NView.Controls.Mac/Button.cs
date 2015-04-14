using System;
using AppKit;

namespace NView.Controls
{
	/// <summary>
	/// Button implementation for Android.
	/// </summary>
	public class Button : IView
	{
		/// <summary>
		/// Gets or sets the title.
		/// </summary>
		/// <value>The title.</value>
		public string Text {
			get { return button.Title; }
			set { button.Title = value; }
		}

		/// <summary>
		/// Gets or sets a value indicating whether this <see cref="NView.Controls.Button"/> is enabled.
		/// </summary>
		/// <value><c>true</c> if enabled; otherwise, <c>false</c>.</value>
		public bool Enabled 
		{
			get { return button.Enabled; }
			set { button.Enabled = value; }
		}

		/// <summary>
		/// Event that occurs when button is clicked.
		/// </summary>
		public event EventHandler Clicked;

		private NSButton button;

		#region IView implementation

		/// <inheritdoc/>
		public IDisposable BindToNative (object nativeView)
		{
			button = ViewHelpers.GetView<NSButton> (nativeView);
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

