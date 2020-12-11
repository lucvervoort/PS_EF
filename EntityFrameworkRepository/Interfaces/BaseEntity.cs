namespace EntityFrameworkRepository.Interfaces
{
    public abstract class BaseEntity
    {
        #region Properties
        public long Id { get; set; } // bigint on database level
        #endregion
    }
}
