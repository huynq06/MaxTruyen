using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaxStory.Utilities.Dtos
{
	public abstract class PagedResultBase
	{
		public int CurrentPage { get; set; }

		//public int PageCount
		//{
		//	get
		//	{
		//		var pageCount = (double)RowCount / PageSize;
		//		return (int)Math.Ceiling(pageCount);
		//	}
		//	set { PageCount = value; }
		//}
		public int PageCount { get; set; }
		public int PageSize { get; set; }

		public int RowCount { get; set; }

		public int FirstRowOnPage
		{
			get
			{
				return (CurrentPage - 1) * PageSize + 1;
			}
		}
		public int LastRowOnPage
		{
			get
			{
				return Math.Min(CurrentPage * PageSize, RowCount);
			}
		}
	}
}
