using System;

namespace NurBilgi.Domain.Common.Entities;

public interface ICreatedByEntity
{
    string CreatedByUserId { get; set; }
    DateTimeOffset CreatedOn { get; set; }
}
