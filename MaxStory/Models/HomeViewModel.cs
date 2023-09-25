using MaxStory.Data.Entities;
using System.Collections.ObjectModel;

namespace MaxStory.Models
{
	public class HomeViewModel
	{
		public ObservableCollection<Story>? HotStories { get; set; }
		public ObservableCollection<Story>? NewStoriesUpdate { get; set; }
		public ObservableCollection<Story>? FinishedStories { get; set; }
	}
}
