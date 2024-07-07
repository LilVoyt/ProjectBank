using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjectBank.Data;
using ProjectBank.Entities;
using ProjectBank.Models;

namespace ProjectBank.Controller.Services
{
    public interface ICardService
    {
        Task<ActionResult<List<Card>>> GetAllCard();
        Task<CardRequestModel> GetCard(Guid id);
        Task<Card> AddCard(CardRequestModel card);
        Task<Guid> UpdateCard(Guid id, CardRequestModel requestModel);
        Task<Guid> DeleteCard(Guid id);
    }
    public class CardServise : ICardService
    {
        private readonly DataContext _context;

        public CardServise(DataContext context)
        {
            _context = context;
        }
        async Task<Card> ICardService.AddCard(CardRequestModel card)
        {
            if (card == null)
            {
                throw new ArgumentNullException(nameof(card));
            }
            var res = MapRequestToCard(card);

            _context.Card.AddAsync(res);
            await _context.SaveChangesAsync();

            return res;
        }

        async Task<Guid> ICardService.DeleteCard(Guid id)
        {
            var card = await _context.Card.FindAsync(id);
            if (card == null)
            {
                return Guid.Empty;
            }

            card.AccountID = Guid.Empty;

            _context.Card.Remove(card);
            await _context.SaveChangesAsync();

            return id;
        }

        async Task<ActionResult<List<Card>>> ICardService.GetAllCard()
        {
            var card = await _context.Card.ToListAsync();

            return card;
        }

        async Task<CardRequestModel> ICardService.GetCard(Guid id)
        {
            var card = await _context.Card.FindAsync(id);

            if (card == null)
            {
                return null;
            }
            var res = MapRequestToDB(card);

            return res;
        }

        async Task<Guid> ICardService.UpdateCard(Guid id, CardRequestModel requestModel)
        {
            var card = await _context.Card.FindAsync(id);
            if (card == null)
            {
                return Guid.Empty;
            }
            card = MapRequestToSet(card, requestModel);
            _context.Card.Update(card);
            await _context.SaveChangesAsync();

            return id;
        }

        private Card MapRequestToCard(CardRequestModel requestModel)
        {
            var card = new Card();
            card.Id = Guid.NewGuid();
            card.NumberCard = requestModel.NumberCard;
            card.CardName = requestModel.CardName;
            card.Pincode = requestModel.Pincode;
            card.Data = requestModel.Data;
            card.CVV = requestModel.CVV;
            card.Balance = requestModel.Balance;
            card.AccountID = requestModel.AccountID;

            return card;
        }

        private CardRequestModel MapRequestToDB(Card card)
        {
            var requestModel = new CardRequestModel();
            requestModel.NumberCard = card.NumberCard;
            requestModel.CardName = card.CardName;
            requestModel.Pincode = card.Pincode;
            requestModel.Data = card.Data;
            requestModel.CVV = card.CVV;
            requestModel.Balance = card.Balance;

            return requestModel;
        }

        private Card MapRequestToSet(Card res, CardRequestModel card)
        {
            res.NumberCard = card.NumberCard;
            res.CardName = card.CardName;
            res.Pincode = card.Pincode;
            res.Data = card.Data;
            res.CVV = card.CVV;
            res.Balance = card.Balance;

            return res;
        }
    }
}
