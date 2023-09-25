using MaxStory.Data.Entities;
using MaxStory.Utilities.Dtos;
using System.Collections.ObjectModel;

namespace MaxStory.Models
{
    public class CategoryViewModel
    {
		public ObservableCollection<Story>? Stories { get; set; }
		public PagedResult<Story>? Data { get; set; }
		public string MaxPage { get; set; }
	}
}
