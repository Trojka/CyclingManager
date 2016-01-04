using System;
using Android.Views.Animations;
using Android.Views;

namespace CyclingManager.Android
{
	public class CyclerCellAnimation : Animation
	{
		View m_view;
		int m_startWidth;
		float m_startX;

		public CyclerCellAnimation(View view) {
			this.m_view = view;
			m_startWidth = this.m_view.Width;
			m_startX = this.m_view.GetX ();
		}

		protected override void ApplyTransformation (float interpolatedTime, Transformation t)
		{
//			int newWidth;
//			newWidth = m_startWidth - (int) (m_startWidth * 0.1 * interpolatedTime);
//			m_view.LayoutParameters.Width = newWidth;
//			m_view.RequestLayout();

//			float newX;
//			newX = m_startX * (1 - interpolatedTime);
//			m_view.SetX (newX);

			int newWidth;
			newWidth = m_startWidth - (int) (m_startWidth * 0.1 * interpolatedTime);
			m_view.LayoutParameters.Width = newWidth;
			m_view.RequestLayout();

			float newX;
			newX = m_startX + ((m_startWidth - newWidth)/2) * interpolatedTime;
			m_view.SetX (newX);
		}

		public override bool WillChangeBounds ()
		{
			return true;
		}
	}
}

