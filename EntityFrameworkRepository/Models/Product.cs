using EntityFrameworkRepository.Interfaces;
using System;
using System.Collections.Generic;

#nullable disable

namespace EntityFrameworkRepository.Models
{
    public partial class Product: BaseEntity
    {
        #region Properties
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int Valid { get; set; }
        public DateTime AutoTimeCreation { get; set; }
        public long AutoUpdateCount { get; set; }
        public DateTime AutoTimeUpdate { get; set; }
        public string AutoUpdatedBy { get; set; }

        public virtual ICollection<OrderProduct> OrderProducts { get; set; }
        #endregion

        #region Ctor
        public Product()
        {
            OrderProducts = new HashSet<OrderProduct>();
        }
        #endregion
    }
}
