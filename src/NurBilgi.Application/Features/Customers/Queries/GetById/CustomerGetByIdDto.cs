namespace NurBilgi.Application.Features.Customers.Queries.GetById
{
    public sealed record CustomerGetByIdDto
    {
        public long Id { get; set; }
        public string UserName { get; set; } = string.Empty;
        public string FullName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;

        public static CustomerGetByIdDto Create(long id, string userName, string fullName, string email)
        {
            return new CustomerGetByIdDto
            {
                Id = id,
                UserName = userName,
                FullName = fullName,
                Email = email
            };
        }

        public CustomerGetByIdDto(long id, string userName, string fullName, string email)
        {
            Id = id;
            UserName = userName;
            FullName = fullName;
            Email = email;
        }

        public CustomerGetByIdDto() { }
    }
}