using System;

using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using System.Collections.Generic;
using CyclingManager.Shared.DomainModel;
using System.Linq;
using Android.Views.Animations;
using CyclingManager.Shared;

namespace CyclingManager.Android
{
	[Activity (Label = "CyclingManager.Android", MainLauncher = true, Icon = "@drawable/icon")]
	public class MainActivity : Activity
	{
		ListView m_listView;

		JavaList<CyclerViewModel>  m_team;
		AnimatableCellAdapter m_adapter;

		private int id = 0;

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			// Set our view from the "main" layout resource
			SetContentView (Resource.Layout.main);

            var countries = DataSource.Countries();

			var team = DataSource.MyTeam().Select (c => new CyclerViewModel (){ Id = id++, Name = c.Name }).ToList ();
			m_team = new JavaList<CyclerViewModel> (team);
			m_adapter = new AnimatableCellAdapter (this, Resource.Layout.cycler_layout, m_team);

			m_listView = FindViewById<ListView> (Resource.Id.myListView);
			m_listView.Adapter = m_adapter;

			// Get our button from the layout resource,
			// and attach an event to it
			FindViewById<Button> (Resource.Id.removeButton).Click += delegate {
				//RemoveRow();
				AnimateCellShrinkingCommon();
			};
			FindViewById<Button> (Resource.Id.addButton).Click += delegate {
				AddRow();
			};

		}

		void AnimateCellShrinkingCommon() {
			var child = m_listView.GetChildAt (0);
			var cyclerHolder = child.FindViewById (Resource.Id.cyclerName);
			var animation = new CyclerCellAnimation (cyclerHolder);
			animation.Duration = 2000;

			cyclerHolder.StartAnimation (animation);
		}

		ViewTreeObserver m_observer;
		Dictionary<long, int> m_cellTopMap = new Dictionary<long, int>();
		static int AnimationDuration = 500;

		void RemoveRow() {

			m_cellTopMap.Clear ();
			int firstVisiblePosition = m_listView.FirstVisiblePosition;
			for (int i = 0; i < m_listView.ChildCount; i++) {
				var child = m_listView.GetChildAt (i);
				int position = firstVisiblePosition + i;
				long itemId = m_adapter.GetItemId (position);
				m_cellTopMap.Add (itemId, child.Top);
			}
				
			m_team.Remove (0);
			m_team.Remove (3);
			m_adapter.NotifyDataSetChanged ();

			m_observer = m_listView.ViewTreeObserver;
			m_observer.PreDraw += this.onPreDrawRemoveHandler;
		}

		void onPreDrawRemoveHandler(object sender, ViewTreeObserver.PreDrawEventArgs e){
			m_observer.PreDraw -= this.onPreDrawRemoveHandler;
			m_observer = null;

			//bool firstAnimation = true;
			int firstVisiblePosition = m_listView.FirstVisiblePosition;
			for (int i = 0; i < m_listView.ChildCount; i++) {
				View child = m_listView.GetChildAt (i);
				int position = firstVisiblePosition + i;
				long itemId = m_adapter.GetItemId (position);

				int newTop = child.Top;
				if (m_cellTopMap.ContainsKey (itemId)) {
					int oldTop = m_cellTopMap [itemId];
					int delta = oldTop - newTop;
					child.TranslationY = delta;
					child.Animate ().SetDuration (AnimationDuration).TranslationY(0);
				} else {
					int oldTop = newTop + child.Height;
					int delta = oldTop - newTop;
					child.TranslationY = delta;
					child.Animate ().SetDuration (AnimationDuration).TranslationY(0);
				}
			}

			e.Handled = true;
		}

		void AddRow() {

			m_cellTopMap.Clear ();
			int firstVisiblePosition = m_listView.FirstVisiblePosition;
			for (int i = 0; i < m_listView.ChildCount; i++) {
				var child = m_listView.GetChildAt (i);
				int position = firstVisiblePosition + i;
				long itemId = m_adapter.GetItemId (position);
				m_cellTopMap.Add (itemId, child.Top);
			}

			m_team.Insert(2, new CyclerViewModel (){ Id = id++, Name = "Nieuwe renner:" + id }) ;
			m_team.Insert(5, new CyclerViewModel (){ Id = id++, Name = "Nieuwe renner:" + id }) ;
			m_adapter.NotifyDataSetChanged ();

			m_observer = m_listView.ViewTreeObserver;
			m_observer.PreDraw += this.onPreDrawAddHandler;
		}

		public List<View> newViewList = new List<View>();
		void onPreDrawAddHandler(object sender, ViewTreeObserver.PreDrawEventArgs e){
			m_observer.PreDraw -= this.onPreDrawAddHandler;
			m_observer = null;

			//bool firstAnimation = true;
			int firstVisiblePosition = m_listView.FirstVisiblePosition;
			for (int i = 0; i < m_listView.ChildCount; i++) {
				View child = m_listView.GetChildAt (i);
				int position = firstVisiblePosition + i;
				long itemId = m_adapter.GetItemId (position);

				int newTop = child.Top;
				if (m_cellTopMap.ContainsKey (itemId)) {
					int oldTop = m_cellTopMap [itemId];
					int delta = oldTop - newTop;
					child.TranslationY = delta;
					child.Animate ().SetDuration (AnimationDuration).TranslationY (0).WithEndAction(new RunnableAnonymousInnerClassHelper(this));
				} else {
//					int oldTop = newTop + child.Height;
//					int delta = oldTop - newTop;
//					child.TranslationY = delta;
//					child.Animate ().SetDuration (AnimationDuration).TranslationY(0);
					child.Visibility = ViewStates.Invisible;
					newViewList.Add (child);
				}
			}

			e.Handled = true;
		}	

		private class RunnableAnonymousInnerClassHelper : Java.Lang.Object, Java.Lang.IRunnable
		{
			private readonly MainActivity outerInstance;
			//private View child;

			public RunnableAnonymousInnerClassHelper(MainActivity outerInstance)
			{
				this.outerInstance = outerInstance;
			}

			public void Run()
			{
				foreach(var newView in outerInstance.newViewList)
					newView.Visibility = ViewStates.Visible;
			}
		}
	}
}


