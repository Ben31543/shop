using System;
using System.Collections.Generic;
using System.Text;

namespace Shop.Models
{
	public class ProductCriteria
	{
		public string SearchString { get; set; }
		
		public int? CategoryId { get; set; }
        public int? MinValue { get; set; }
		public int? MaxValue { get; set; }

	}
}
