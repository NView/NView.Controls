using System;

namespace NView.Controls
{
	/// <summary>
	/// Cross platform Text View for NView.
	/// </summary>
	public class Label : IView
	{
		private Android.Widget.TextView textView;
		/// <summary>
		/// Gets or sets the text.
		/// </summary>
		/// <value>The text.</value>
		public string Text 
		{
			get { return textView.Text; }
			set { textView.Text = value; }
		}

		#region IView implementation

		/// <summary>
		/// Binds the IView to a native view.
		/// </summary>
		/// <returns>A disposable view</returns>
		/// <param name="nativeView">Native view to bind with.</param>
		public IDisposable BindToNative (object nativeView)
		{
			textView = ViewHelpers.GetView<Android.Widget.TextView> (nativeView);
		

			return new DisposeAction (() => {
				textView = null;
			});
		}

		/// <summary>
		/// Gets the type of the preferred native control.
		/// </summary>
		/// <value>The type of the preferred native.</value>
		public Type PreferredNativeType {
			get {
				return typeof(Android.Widget.TextView);
			}
		}

		#endregion

	}
}

