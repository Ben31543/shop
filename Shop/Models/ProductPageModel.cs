﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop.Models
{
	public class ProductPageModel
	{
		public List<ProductModel> Products { get; set; }

		public ProductCriteria SearchCriteria { get; set; }
	}
}
