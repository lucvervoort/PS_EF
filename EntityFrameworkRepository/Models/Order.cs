using EntityFrameworkRepository.Interfaces;
using System;
using System.Collections.Generic;

#nullable disable

namespace EntityFrameworkRepository.Models
{
    public partial class Order: BaseEntity
    {
        #region Properties
        public int Paid { get; set; }
        // LVET: NULL values in database!!
        public decimal? Price { get; set; } // = null;
        public long CustomerId { get; set; }
        public DateTime? Time { get; set; }
        public int AutoValid { get; set; }
        public DateTime AutoTimeCreation { get; set; }
        public long AutoUpdateCount { get; set; }
        public DateTime AutoTimeUpdate { get; set; }
        public string AutoUpdatedBy { get; set; }
        public virtual Customer Customer { get; set; }
        public virtual ICollection<OrderProduct> OrderProducts { get; set; }
        #endregion

        #region Ctor
        public Order()
        {
            OrderProducts = new HashSet<OrderProduct>();
        }
        #endregion
    }
}
