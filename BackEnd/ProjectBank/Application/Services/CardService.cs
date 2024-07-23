using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjectBank.Application.Services.FunctionalityService;
using ProjectBank.Application.Services.Interfaces;
using ProjectBank.Application.Services.Mappers;
using ProjectBank.Data;
using ProjectBank.Entities;
using ProjectBank.Models;
using System.Linq.Expressions;

namespace ProjectBank.Controller.Services
{
    public class CardService : ICardService
    {
        private readonly DataContext _context;
        private readonly CardMapper _cardMapper;
        private readonly IValidator<Card> _validator;

        public CardService(DataContext context, CardMapper cardMapper, IValidator<Card> validator)
        {
            _context = context;
            _cardMapper = cardMapper;
            _validator = validator;
        }
        public async Task<ActionResult<List<CardRequestModel>>> Get(string? search, string? sortItem, string? sortOrder)
        {
            IQueryable<Card> cards = _context.Card;

            if (!string.IsNullOrEmpty(search))
            {
                cards = cards.Where(c => c.CardName.ToLower().Contains(search.ToLower()));
            }

            Expression<Func<Card, object>> selectorKey = sortItem?.ToLower() switch
            {
                "name" => card => card.CardName,
                _ => card => card.NumberCard,
            };

            cards = sortOrder?.ToLower() == "desc"
                ? cards.OrderByDescending(selectorKey)
                : cards.OrderBy(selectorKey);

            List<Card> accountList = await cards.ToListAsync();

            List<CardRequestModel> response = _cardMapper.GetRequestModels(accountList);

            return response;
        }

        public async Task<Card> Post(CardRequestModel card)
        {
            var res = _cardMapper.GetCard(card);

            var validationResult = await _validator.ValidateAsync(res);
            if (!validationResult.IsValid)
            {
                var errorMessages = string.Join("; ", validationResult.Errors.Select(e => e.ErrorMessage));
                throw new ValidationException(errorMessages);
            }

            await _context.Card.AddAsync(res);
            await _context.SaveChangesAsync();

            return res;
        }

        public async Task<Card>Update(Guid id, CardRequestModel requestModel)
        {
            var card = await _context.Card.FindAsync(id);
            if (card == null)
            {
                throw new KeyNotFoundException($"Account with ID {id} not found.");
            }

            card = _cardMapper.PutRequestModelInCard(card, requestModel);
            var validationResult = await _validator.ValidateAsync(card);
            if (!validationResult.IsValid)
            {
                var errorMessages = string.Join("; ", validationResult.Errors.Select(e => e.ErrorMessage));
                throw new ValidationException(errorMessages);
            }
            _context.Card.Update(card);
            await _context.SaveChangesAsync();
            return card;
        }

        public async Task<Card> Delete(Guid id)
        {
            var card = await _context.Card.FindAsync(id);
            if (card == null)
            {
                throw new KeyNotFoundException($"Account with ID {id} not found.");
            }

            card.AccountID = Guid.Empty;

            _context.Card.Remove(card);
            await _context.SaveChangesAsync();

            return card;
        }
    }
}
