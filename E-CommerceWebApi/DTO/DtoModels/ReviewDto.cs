namespace E_CommerceWebApi.DTO.DtoModels
{
    public class ReviewDto
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public string ReviewName { get; set; }
        public string Content { get; set; }
    }
}
