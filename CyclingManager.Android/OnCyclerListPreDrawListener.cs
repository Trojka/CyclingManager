using System;
using Android.Views;

namespace MegaVelo.Android
{
	public class OnCyclerListPreDrawListener : ViewTreeObserver.IOnPreDrawListener
	{
		#region IOnPreDrawListener implementation

		public bool OnPreDraw ()
		{
			throw new NotImplementedException ();
		}

		#endregion

		#region IDisposable implementation

		public void Dispose ()
		{
			throw new NotImplementedException ();
		}

		#endregion

		#region IJavaObject implementation

		public IntPtr Handle {
			get {
				throw new NotImplementedException ();
			}
		}

		#endregion


	}
}

