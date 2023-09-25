using MaxStory.Data.Entities;
using MaxStory.Utilities.Dtos;
using System.Collections.ObjectModel;

namespace MaxStory.Models
{
    public class DetailViewModel
    {
        public Story? StoryDetail { get; set; }
		public ObservableCollection<Story>? listStoriesSameAuthor { get; set; }
		public ObservableCollection<Story>? listStoriesSimilar { get; set; }
		public PagedResult<Chapter>? Data { get; set; }
		public string? StoryAlias { get; set; }
	}
}
