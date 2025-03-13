namespace FinalProject.Application.DTOs.Comment
{
    public record CommentItemDto(int Id,
       string UserId,
       string Content,
       int DoctorId);

}

