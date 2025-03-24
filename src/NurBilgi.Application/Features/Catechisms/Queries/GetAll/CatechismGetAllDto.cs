namespace NurBilgi.Application.Features.Catechisms.Queries.GetAll
{
    public sealed record CatechismGetAllDto
    {
        public long Id { get; set; }
        public string BookName { get; set; } = string.Empty;
        public string AuthorName { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Tags { get; set; } = string.Empty;
        public DateTimeOffset? CreatedAt { get; set; }
        public DateTimeOffset? UpdatedAt { get; set; }

        public CatechismGetAllDto(long id, string bookName, string authorName, string title, string description, string tags, DateTimeOffset createdAt, DateTimeOffset updatedAt)
        {
            Id = id;
            BookName = bookName;
            AuthorName = authorName;
            Title = title;
            Description = description;
            Tags = tags;
            CreatedAt = createdAt;
            UpdatedAt = updatedAt;
        }

        public CatechismGetAllDto() { }

        public static CatechismGetAllDto Create(long id, string bookName, string authorName, string title, string description, string tags, DateTimeOffset createdAt, DateTimeOffset updatedAt)
            => new CatechismGetAllDto(id, bookName, authorName, title, description, tags, createdAt, updatedAt);
    }
} 