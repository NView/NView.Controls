using System;

namespace NView.Controls
{
	/// <summary>
	/// Cross platform Text Entry for NView.
	/// </summary>
	public class TextEntry : IView
	{
		/// <summary>
		/// Gets or sets the text.
		/// </summary>
		/// <value>The text.</value>
		public string Text { get; set; }

		/// <summary>
		/// Gets or sets the placeholder text to display.
		/// </summary>
		/// <value>The placeholder text.</value>
		public string Placeholder { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether this <see cref="NView.Controls.TextEntry"/> is enabled.
		/// </summary>
		/// <value><c>true</c> if enabled; otherwise, <c>false</c>.</value>
		public bool Enabled { get; set; }

		//ignore warning as it is used in base implementation
		#pragma warning disable 67 
		/// <summary>
		/// Event that occurs when text has changed.
		/// </summary>
		public event EventHandler TextChanged;
		#pragma warning restore

		#region IView implementation

		/// <inheritdoc/>
		public IDisposable BindToNative (object nativeView, BindOptions options = BindOptions.None)
		{
			throw Helpers.ThrowNotImplementedException ();
		}

		/// <inheritdoc/>
		public Type PreferredNativeType {
			get {
				throw Helpers.ThrowNotImplementedException ();
			}
		}

		#endregion
	}
}

