using Kaching.MailViewModels;
using Kaching.Repositories;
using Kaching.Services;
using Microsoft.AspNetCore.Mvc;

namespace Kaching.Controllers
{
    public class PaymentsController : Controller
    {

        private readonly IEmailService _emailService;
        private readonly IExpenseRepository _expenseRepository;
        private readonly IPersonRepository _personRepository;

        public PaymentsController(IEmailService emailService,
            IExpenseRepository expenseRepository,
            IPersonRepository personRepository)
        {
            _emailService = emailService;
            _expenseRepository = expenseRepository;
            _personRepository = personRepository;
        }

        // POST: Payment/PaymentReminder/
        [HttpPost("Payment/PaymentReminder/"), ActionName("PaymentReminder")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> PaymentReminder(int personId)
        {


            var person = _personRepository.GetPersonByPersonId(personId);

            var paymentVM = new PaymentReminderViewModel
            {
                Username = person.ConnectedUserName,
                OwedAmount = 10,
                MonthName = "July",
                PersonToPay = "nottest",
                Deadline = DateTime.Now
            };

            _emailService.SendPaymentReminder(paymentVM);


            return RedirectToAction();
        }
    }
}
