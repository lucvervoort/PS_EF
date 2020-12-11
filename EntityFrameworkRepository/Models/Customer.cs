using EntityFrameworkRepository.Interfaces;
using System;
using System.Collections.Generic;

#nullable disable

namespace EntityFrameworkRepository.Models
{
    public partial class Customer: BaseEntity
    {
        #region Properties
        public string Name { get; set; }
        public string Address { get; set; }
        public int AutoValid { get; set; }
        public DateTime AutoTimeCreation { get; set; }
        public long AutoUpdateCount { get; set; }
        public DateTime AutoTimeUpdate { get; set; }
        public string AutoUpdatedBy { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
        #endregion

        #region Ctor
        public Customer()
        {
            Orders = new HashSet<Order>();
        }
        #endregion

    }
}
