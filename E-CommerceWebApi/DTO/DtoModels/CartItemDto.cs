using System.ComponentModel.DataAnnotations.Schema;

namespace E_CommerceWebApi.DTO.DtoModels
{
    public class CartItemDto
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
    }
}
