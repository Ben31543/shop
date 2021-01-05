namespace Shop.Models
{
    public class ProductCriteriaModel
	{
		public string SearchString { get; set; }
		public int? CategoryId { get; set; }
        public int? MinValue { get; set; }
    	public int? MaxValue { get; set; }
	}
}
