namespace NurBilgi.Application.Features.Notes.Queries.GetAll
{
    public sealed record NoteGetAllDto
    {
        public long Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        public long CustomerId { get; set; }

        public NoteGetAllDto(long id, string title, string content, long customerId)
        {
            Id = id;
            Title = title;
            Content = content;
            CustomerId = customerId;
        }

        public NoteGetAllDto() { }

        public static NoteGetAllDto Create(long id, string title, string content, long customerId)
            => new NoteGetAllDto(id, title, content, customerId);
    }
}