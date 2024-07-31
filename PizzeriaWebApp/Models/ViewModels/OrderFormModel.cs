namespace PizzeriaWebApp.Models.ViewModels
{
    public class OrderFormModel
    {
        public string Address { get; set; }
        public string Notes { get; set; }
        public List<ProductOrderModel> Products { get; set; }
    }
}
