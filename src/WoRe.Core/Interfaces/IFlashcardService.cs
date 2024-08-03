using Arch.EntityFrameworkCore.UnitOfWork.Collections;
using ErrorOr;
using WoRe.Core.Kernel.Models;

namespace WoRe.Core.Interfaces;

public interface IFlashcardService
{
    Task<ErrorOr<PagedList<FlashcardSetDetails>>> GetPagedSetsAsync(int page = 0, int pageSize = 5);
    Task<ErrorOr<IReadOnlyCollection<FlashcardSetDetails>>> GetAllSetsAsync();
    Task<ErrorOr<string>> CreateSetAsync(FlashcardSetCreate flashcardSet);
    Task<ErrorOr<FlashcardSetCreate>> GetSetForEditByIdAsync(Guid id);
    Task<ErrorOr<string>> UpdateSetAsync(FlashcardSetUpdate flashcardSet);
    Task<ErrorOr<FlashcardSetView>> GetSetById(Guid id);
}