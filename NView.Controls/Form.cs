using System;

namespace NView.Controls
{
	/// <summary>
	/// Cross platform Form View for NView. Based on MonoTouch.Dialog.
	/// </summary>
	public class Form : IView
	{
		public RootElement Root { get; set; }

		public Form ()
		{
			
		}
		public Form (RootElement root)
		{

		}
		#region IView implementation

		/// <inheritdoc/>
		public void BindToNative (object nativeView, BindOptions options = BindOptions.None)
		{
			throw Helpers.ThrowNotImplementedException ();
		}

		/// <inheritdoc/>
		public void UnbindFromNative ()
		{
			throw Helpers.ThrowNotImplementedException ();
		}

		/// <inheritdoc/>
		public Type NativeType {
			get {
				throw Helpers.ThrowNotImplementedException ();
			}
		}

		/// <inheritdoc/>
		public object CreateNative (object context = null)
		{
			throw Helpers.ThrowNotImplementedException ();
		}

		#endregion
	}
}

