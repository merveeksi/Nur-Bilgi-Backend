namespace NurBilgi.Application.Features.Customers.Queries.GetAll
{
    public sealed record CustomerGetAllDto
    {
        public long Id { get; set; }
        public string UserName { get; set; } = string.Empty;
        public string FullName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;

        public CustomerGetAllDto(long id, string userName, string fullName, string email)
        {
            Id = id;
            UserName = userName;
            FullName = fullName;
            Email = email;
        }

        public CustomerGetAllDto() { }

        public static CustomerGetAllDto Create(long id, string userName, string fullName, string email)
            => new CustomerGetAllDto(id, userName, fullName, email);
    }
}