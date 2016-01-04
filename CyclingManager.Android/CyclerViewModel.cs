using System;

namespace CyclingManager.Android
{
	public class CyclerViewModel : Java.Lang.Object
	{
		public int Id { get; set;}
		public string Name { get; set;}

		public override string ToString ()
		{
			return string.Format ("[CyclerViewModel: Id={0}, Name={1}]", Id, Name);
		}
	}
}

