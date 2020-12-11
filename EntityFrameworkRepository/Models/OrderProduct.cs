using EntityFrameworkRepository.Interfaces;

#nullable disable

namespace EntityFrameworkRepository.Models
{
    public partial class OrderProduct: BaseEntity
    {
        #region Properties
        public long OrderId { get; set; }
        public long ProductId { get; set; }
        public int Amount { get; set; }

        public virtual Order Order { get; set; }
        public virtual Product Product { get; set; }
        #endregion
    }
}
