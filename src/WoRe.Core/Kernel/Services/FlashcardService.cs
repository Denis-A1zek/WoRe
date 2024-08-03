using Arch.EntityFrameworkCore.UnitOfWork;
using Arch.EntityFrameworkCore.UnitOfWork.Collections;
using ErrorOr;
using Microsoft.EntityFrameworkCore;
using WoRe.Core.Domain.Entities;
using WoRe.Core.Interfaces;
using WoRe.Core.Kernel.Models;

namespace WoRe.Core.Kernel.Services;

public class FlashcardService : IFlashcardService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IRepository<FlashcardSet> _flashcardsRepository;

    public FlashcardService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
        _flashcardsRepository = unitOfWork.GetRepository<FlashcardSet>();
    }

    public Task<ErrorOr<PagedList<FlashcardSetDetails>>> GetPagedSetsAsync(
        int page = 0, int pageSize = 5)
    {
        throw new NotImplementedException();
    }

    public async Task<ErrorOr<IReadOnlyCollection<FlashcardSetDetails>>> GetAllSetsAsync()
    {
        return await _flashcardsRepository
            .GetAll()
            .Include(i => i.Cards)
            .Select(flashcardSet =>
                new FlashcardSetDetails()
                {
                    Id = flashcardSet.Id,
                    Title = flashcardSet.Title,
                    Description = flashcardSet.Description,
                    MasteredCount = flashcardSet.Cards.Where(c => c.Status.Value == Domain.ValueObjects.Status.Mastered.Value).Count(),
                    NotStudiedCount = flashcardSet.Cards.Where(c => c.Status.Value == Domain.ValueObjects.Status.NotStudied.Value).Count(),
                    StillLearningCount = flashcardSet.Cards.Where(c => c.Status.Value == Domain.ValueObjects.Status.StillLearning.Value).Count(),
                    TermsCount = flashcardSet.Cards.Count(),
                })
            .ToListAsync();
    }

    public async Task<ErrorOr<string>> CreateSetAsync(FlashcardSetCreate flashcardSet)
    {
        FlashcardSet newFlashcardSet = new()
        { Title = flashcardSet.Title, Description = flashcardSet.Description };

        List<CommonCard> cards = new();
        flashcardSet.Flashcards.ForEach(flashcard =>
        {
            if (string.IsNullOrEmpty(flashcard.Extra))
                cards.Add(new CommonCard(flashcard.Term, flashcard.Definition, flashcard.ImageUrl));
            else
                cards.Add(new AdditionalCard(flashcard.Term, flashcard.Extra, flashcard.Definition, flashcard.ImageUrl));
        });

        newFlashcardSet.UpdateCards(cards);
        _flashcardsRepository.Insert(newFlashcardSet);

        var result = await _unitOfWork.SaveChangesAsync();
        return newFlashcardSet.Title;
    }

    public async Task<ErrorOr<FlashcardSetCreate>> GetSetForEditByIdAsync(Guid id)
    {
        var flashcard = await _flashcardsRepository
            .GetFirstOrDefaultAsync(
                predicate: f => f.Id == id,
                include: i => i.Include(fs => fs.Cards));

        if (flashcard is null)
            return Error.Failure("Flashcard set not found");

        return new FlashcardSetCreate(
            flashcard.Title,
            flashcard.Description,
            flashcard.Cards.Select(fc =>
            {
                var extra = string.Empty;
                if (fc is AdditionalCard)
                {
                    extra = (fc as AdditionalCard)?.Extra;
                }
                return new FlashcardCreate()
                {
                    Definition = fc.Definition,
                    Extra = (fc as AdditionalCard)?.Extra,
                    ImageUrl = fc.ImageUrl,
                    Term = fc.Term,
                };
            }).ToList());
    }

    public async Task<ErrorOr<string>> UpdateSetAsync(FlashcardSetUpdate flashcardSet)
    {
        var flashcard = await _flashcardsRepository
            .GetFirstOrDefaultAsync(
                predicate: f => f.Id == flashcardSet.Id,
                include: i => i.Include(fs => fs.Cards),
                disableTracking: false);

        if (flashcard is null)
            return Error.Failure("Flashcard set not found");

        flashcard.Title = flashcardSet.Title;
        flashcard.Description = flashcardSet.Description;

        List<CommonCard> cards = new();
        flashcardSet.Flashcards.ForEach(flashcard =>
        {
            if (string.IsNullOrEmpty(flashcard.Extra))
                cards.Add(new CommonCard(flashcard.Term, flashcard.Definition, flashcard.ImageUrl));
            else
                cards.Add(new AdditionalCard(flashcard.Term, flashcard.Extra, flashcard.Definition, flashcard.ImageUrl));
        });

        flashcard.UpdateCards(cards);

        _flashcardsRepository.Update(flashcard);

        var result = await _unitOfWork.SaveChangesAsync();
        return flashcard.Title;
    }

    public async Task<ErrorOr<FlashcardSetView>> GetSetById(Guid id)
    {
        var set = await _flashcardsRepository.GetFirstOrDefaultAsync(
            predicate: f => f.Id == id,
            include: i => i.Include(f => f.Cards),
            selector: fs => new FlashcardSetView()
            {
                Id = fs.Id,
                Title = fs.Title,
                Flashcards = fs.Cards.Select(
                    c => new FlashcardView()
                    {
                        Id = c.Id,
                        Term = c.Term,
                        Extra = (c as AdditionalCard).Extra ?? string.Empty,
                        Definition = c.Definition,
                        Status = c.Status,
                        ImageUrl = c.ImageUrl
                    })
                    .ToList()
            });
        return set;
    }
}