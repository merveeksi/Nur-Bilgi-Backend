namespace NurBilgi.Application.Features.Notes.Queries.GetById
{
    public sealed record NoteGetByIdDto
    {
        public long Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        public long CustomerId { get; set; }

        public static NoteGetByIdDto Create(long id, string title, string content, long customerId)
        {
            return new NoteGetByIdDto
            {
                Id = id,
                Title = title,
                Content = content,
                CustomerId = customerId
            };
        }

        public NoteGetByIdDto(long id, string title, string content, long customerId)
        {
            Id = id;
            Title = title;
            Content = content;
            CustomerId = customerId;
        }

        public NoteGetByIdDto() { }
    }
}