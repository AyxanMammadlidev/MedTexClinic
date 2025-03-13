using FinalProject.Application.DTOs.Comment;
using FluentValidation;

namespace FinalProject.Application.Validators.CommentValidators
{
    public class UpdateCommentDtoValidator : AbstractValidator<UpdateCommentDto>
    {
        public UpdateCommentDtoValidator()
        {
            RuleFor(x => x.Content)
              .NotEmpty().WithMessage("Comment content is required")
              .MaximumLength(800)
              .WithMessage("Comment must not exceed 800 characters");



        }
    }

}
