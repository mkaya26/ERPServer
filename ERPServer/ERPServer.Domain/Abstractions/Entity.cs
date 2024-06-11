namespace ERPServer.Domain.Abstractions
{
    public abstract class Entity
    {
        public Guid Id { get; set; }
        protected Entity()
        {
            Id = Guid.NewGuid();
        }
        public string CreateBy { get; set; } = string.Empty;
        public DateTime CreateDate { get; set; }
        public string UpdateBy { get; set; } = string.Empty;
        public DateTime UpdateDate { get; set; }
    }
}
