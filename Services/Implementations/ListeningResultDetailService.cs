using DotnetAPIProject.Data;
using DotnetAPIProject.Models.DTOs;
using DotnetAPIProject.Models.Entities;
using DotnetAPIProject.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DotnetAPIProject.Services.Implementations;

public class ListeningResultDetailService : IListeningResultDetailService
{
    private readonly ApplicationDbContext _context;

    public ListeningResultDetailService(ApplicationDbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public async Task<ListeningResultDetailsDto> CreateListeningResultDetailAsync(Guid listeningResultId, ListeningResultDetailCreateDto createContent)
    {
        if (createContent == null)
            throw new ArgumentNullException(nameof(createContent));

        var newDetail = new ListeningResultDetails
        {
            ListeningResultId = listeningResultId,
            ListeningQuestionId = createContent.ListeningQuestionId,
            isMarked = createContent.IsMarked, // Default to false if not provided
            AnswerId = string.IsNullOrWhiteSpace(createContent.AnswerId) ? null : Guid.Parse(createContent.AnswerId)
        };

        _context.ListeningResultDetails.Add(newDetail);
        await _context.SaveChangesAsync();

        return new ListeningResultDetailsDto
        {
            Id = newDetail.Id,
            ListeningResultId = newDetail.ListeningResultId,
            ListeningQuestionId = newDetail.ListeningQuestionId,
            IsMarked = newDetail.isMarked,
            Status = newDetail.Status ?? string.Empty,
            AnswerId = newDetail.AnswerId
        };
    }

    public async Task<List<ListeningResultDetailsDto>> GetListeningResultDetailsByResultIdAsync(Guid resultId)
    {

        var details = await _context.ListeningResultDetails
            .AsNoTracking()
            .Where(x => x.ListeningResultId == resultId)
            .Select(x => new ListeningResultDetailsDto
            {
                Id = x.Id,
                ListeningResultId = x.ListeningResultId,
                ListeningQuestionId = x.ListeningQuestionId,
                IsMarked = x.isMarked,
                Status = x.Status ?? string.Empty,
                AnswerId = x.AnswerId
            })
            .ToListAsync();

        return details;
    }

    public async Task<ListeningResultDetailsDto> UpdateListeningResultDetailAsync(Guid id, ListeningResultDetailUpdateDto updateContent)
    {
        if (updateContent == null)
            throw new ArgumentNullException(nameof(updateContent));

        var detail = await _context.ListeningResultDetails
            .FirstOrDefaultAsync(x => x.Id == id);

        if (detail == null)
            return null;

        detail.Status = updateContent.Status ?? detail.Status;

        _context.ListeningResultDetails.Update(detail);
        await _context.SaveChangesAsync();

        return new ListeningResultDetailsDto
        {
            Id = detail.Id,
            ListeningResultId = detail.ListeningResultId,
            ListeningQuestionId = detail.ListeningQuestionId,
            IsMarked = detail.isMarked,
            Status = detail.Status ?? string.Empty,
            AnswerId = detail.AnswerId
        };
    }
    
    public async Task<List<ListeningResultDetailsDto>> GetCorrectResultDetailByResultIdAsync(Guid resultId)
    {
        var correctDetails = await _context.ListeningResultDetails
            .AsNoTracking()
            .Where(x => x.ListeningResultId == resultId && x.Status == "correct")
            .Select(x => new ListeningResultDetailsDto
            {
                Id = x.Id,
                ListeningResultId = x.ListeningResultId,
                ListeningQuestionId = x.ListeningQuestionId,
                IsMarked = x.isMarked,
                Status = x.Status ?? string.Empty,
                AnswerId = x.AnswerId
            })
            .ToListAsync();

        return correctDetails;
    }
}