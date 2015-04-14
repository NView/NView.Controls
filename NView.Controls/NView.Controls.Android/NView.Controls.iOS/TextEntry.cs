using System;

namespace NView.Controls
{
	/// <summary>
	/// Cross platform Text Entry for NView.
	/// </summary>
	public class TextEntry : IView
	{
		private UIKit.UITextField textField;
		/// <summary>
		/// Gets or sets the text.
		/// </summary>
		/// <value>The text.</value>
		public string Text 
		{
			get { return textField.Text; }
			set { textField.Text = value; }
		}

		/// <summary>
		/// Gets or sets the placeholder text to display.
		/// </summary>
		/// <value>The placeholder text.</value>
		public string Placeholder
		{
			get { return textField.Placeholder; }
			set { textField.Placeholder = value; }
		}

		/// <summary>
		/// Gets or sets a value indicating whether this <see cref="NView.Controls.TextEntry"/> is enabled.
		/// </summary>
		/// <value><c>true</c> if enabled; otherwise, <c>false</c>.</value>
		public bool Enabled 
		{
			get { return textField.Enabled; }
			set { textField.Enabled = value; }
		}

		/// <summary>
		/// Event that occurs when text has changed.
		/// </summary>
		public event EventHandler TextChanged;

		#region IView implementation

		/// <summary>
		/// Binds the IView to a native view.
		/// </summary>
		/// <returns>A disposable view</returns>
		/// <param name="nativeView">Native view to bind with.</param>
		public IDisposable BindToNative (object nativeView)
		{
			textField = ViewHelpers.GetView<UIKit.UITextField> (nativeView);
			textField.ValueChanged += TextField_ValueChanged;

			return new DisposeAction (() => {
				textField.ValueChanged -= TextField_ValueChanged;
				textField = null;
			});
		}

		void TextField_ValueChanged (object sender, EventArgs e)
		{
			if (TextChanged == null)
				return;

			TextChanged (this, new EventArgs ());
		}



		/// <summary>
		/// Gets the type of the preferred native control.
		/// </summary>
		/// <value>The type of the preferred native.</value>
		public Type PreferredNativeType {
			get {
				return typeof(UIKit.UITextField);
			}
		}

		#endregion

	}
}

