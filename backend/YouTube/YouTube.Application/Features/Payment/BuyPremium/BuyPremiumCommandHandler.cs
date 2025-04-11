// using Grpc.Core;
// using MediatR;
// using Microsoft.EntityFrameworkCore;
// using YouTube.Application.Common.Messages.Error;
// using YouTube.Application.Common.Responses;
// using YouTube.Application.Interfaces;
// using YouTube.Domain.Common;
// using YouTube.Domain.Entities;
// using YouTube.Payment.Protos;
//
// namespace YouTube.Application.Features.Payment.BuyPremium;
//
// public class BuyPremiumCommandHandler : IRequestHandler<BuyPremiumCommand, BaseResponse>
// {
//     private readonly IDbContext _context;
//
//     public BuyPremiumCommandHandler(IDbContext context)
//     {
//         _context = context;
//     }
//
//     public async Task<BaseResponse> Handle(BuyPremiumCommand request, CancellationToken cancellationToken)
//     {
//         var user = await _context.Users
//             .Include(u => u.Subscriptions)
//             .FirstOrDefaultAsync(u => u.Id == request.Id, cancellationToken);
//
//         if (user == null)
//             return new BaseResponse { Message = UserErrorMessage.UserNotFound };
//
//         bool paymentWithdrawn = false;
//
//         var transaction = new Transaction
//         {
//             Id = Guid.NewGuid(),
//             Date = DateTime.UtcNow,
//             Price = request.Price,
//             Description = "Premium subscription payment",
//             Operation = $"-{request.Price}",
//             Status = PaymentStatus.Pending,
//             UserId = user.Id
//         };
//         
//         var originalSubscription = user.Subscriptions != null
//             ? new Premium
//             {
//                 Id = user.Subscriptions.Id,
//                 StartDate = user.Subscriptions.StartDate,
//                 EndDate = user.Subscriptions.EndDate,
//                 Price = user.Subscriptions.Price,
//                 IsActive = user.Subscriptions.IsActive
//             }
//             : null;
//         
//         await _context.Transactions.AddAsync(transaction, cancellationToken);
//         await _context.SaveChangesAsync(cancellationToken);
//         
//         try
//         {
//             var paymentResponse = await _paymentClient.WithdrawAsync(
//                 new WithdrawRequest
//                 {
//                     UserId = user.Id.ToString(),
//                     Amount = request.Price,
//                     TransactionId = transaction.Id.ToString()
//                 },
//                 cancellationToken: cancellationToken);
//
//             if (!paymentResponse.Success)
//                 return new BaseResponse { Message = paymentResponse.Error };
//
//             paymentWithdrawn = true;
//
//             BuyOrExtendPremium(user, request.Price);
//             await _context.SaveChangesAsync(cancellationToken);
//
//             transaction.Status = PaymentStatus.Completed;
//             await _context.SaveChangesAsync(cancellationToken);
//
//             return new BaseResponse
//             {
//                 IsSuccessfully = true, Message = user.Subscriptions!.IsActive
//                     ? $"Подписка продлена до {user.Subscriptions.EndDate:dd.MM.yyyy}"
//                     : "Премиум подписка успешно активирована"
//             };
//         }
//         catch (RpcException ex) when (ex.StatusCode == StatusCode.DeadlineExceeded)
//         {
//             await TryRollbackAsync(user, originalSubscription, transaction, paymentWithdrawn);
//             return new BaseResponse { Message = "Сервис оплаты временно недоступен" };
//         }
//         catch (Exception ex)
//         {
//             await TryRollbackAsync(user, originalSubscription, transaction, paymentWithdrawn);
//             return new BaseResponse { Message = $"Ошибка: {ex.Message}" };
//         }
//     }
//
//     private async Task TryRollbackAsync(
//         User user,
//         Premium? originalSubscription,
//         Transaction? transaction,
//         bool paymentWithdrawn)
//     {
//         try
//         {
//             if (originalSubscription != null)
//             {
//                 user.Subscriptions = originalSubscription;
//             }
//             else
//             {
//                 user.Subscriptions = null;
//             }
//
//             if (paymentWithdrawn && transaction != null)
//             {
//                 await _paymentClient.RefundAsync(new RefundRequest
//                 {
//                     UserId = user.Id.ToString(),
//                     Amount = (double)transaction.Price,
//                     TransactionId = transaction.Id.ToString(),
//                 });
//             }
//
//             if (transaction != null)
//             {
//                 _context.Transactions.Remove(transaction);
//             }
//
//             await _context.SaveChangesAsync();
//         }
//         catch (Exception)
//         {
//             Console.WriteLine("Пизда вообще");
//         }
//     }
//
//     private void BuyOrExtendPremium(User user, int price)
//     {
//         if (user.Subscriptions == null)
//         {
//             user.Subscriptions = new Premium
//             {
//                 Id = Guid.NewGuid(),
//                 StartDate = DateTime.UtcNow,
//                 EndDate = DateTime.UtcNow.AddMonths(1),
//                 Price = price,
//                 IsActive = true,
//                 UserId = user.Id
//             };
//         }
//         else
//         {
//             var currentEndDate = user.Subscriptions.EndDate > DateTime.UtcNow
//                 ? user.Subscriptions.EndDate.Value
//                 : DateTime.UtcNow;
//
//             user.Subscriptions.EndDate = currentEndDate.AddMonths(1);
//             user.Subscriptions.IsActive = true;
//             user.Subscriptions.Price = price;
//         }
//     }
// }