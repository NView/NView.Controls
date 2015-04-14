using System;

namespace NView.Controls
{
	/// <summary>
	/// Cross platform Text Entry for NView.
	/// </summary>
	public class TextEntry : IView
	{
		private Android.Widget.EditText editText;
		/// <summary>
		/// Gets or sets the text.
		/// </summary>
		/// <value>The text.</value>
		public string Text 
		{
			get { return editText.Text; }
			set { editText.Text = value; }
		}

		/// <summary>
		/// Gets or sets the placeholder text to display.
		/// </summary>
		/// <value>The placeholder text.</value>
		public string Placeholder
		{
			get { return editText.Hint; }
			set { editText.Hint = value; }
		}

		/// <summary>
		/// Gets or sets a value indicating whether this <see cref="NView.Controls.TextEntry"/> is enabled.
		/// </summary>
		/// <value><c>true</c> if enabled; otherwise, <c>false</c>.</value>
		public bool Enabled 
		{
			get { return editText.Enabled; }
			set { editText.Enabled = value; }
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
			editText = ViewHelpers.GetView<Android.Widget.EditText> (nativeView);
			editText.TextChanged += EditText_TextChanged;

			return new DisposeAction (() => {
				editText.TextChanged -= EditText_TextChanged;
				editText = null;
			});
		}

		void EditText_TextChanged (object sender, Android.Text.TextChangedEventArgs e)
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
				return typeof(Android.Widget.EditText);
			}
		}

		#endregion

	}
}

