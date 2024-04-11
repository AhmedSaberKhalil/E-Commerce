namespace E_CommerceWebApi.DTO.DtoModels
{
    public class ProductNameWithAllReviews
    {
        public int Id { get; set; }
        public string ProductName { get; set; }
        public List<string> ReviewsList { get; set; } = new List<string>();
    }
}
