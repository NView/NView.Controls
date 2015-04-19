using System;

namespace NView.Controls
{
	/// <summary>
	/// Cross platform Text View for NView.
	/// </summary>
	public class Label : IView
	{
		/// <summary>
		/// Gets or sets the text.
		/// </summary>
		/// <value>The text.</value>
		public string Text { get; set; }

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

