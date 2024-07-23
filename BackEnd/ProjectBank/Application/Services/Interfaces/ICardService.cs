using Microsoft.AspNetCore.Mvc;
using ProjectBank.Entities;
using ProjectBank.Models;

namespace ProjectBank.Application.Services.Interfaces
{
    public interface ICardService
    {
        Task<ActionResult<List<CardRequestModel>>> Get(string? search, string? sortItem, string? sortOrder);
        Task<Card> Post(CardRequestModel card);
        Task<Card> Update(Guid id, CardRequestModel requestModel);
        Task<Card> Delete(Guid id);
    }
}
