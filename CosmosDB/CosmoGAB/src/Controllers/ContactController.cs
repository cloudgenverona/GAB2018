namespace CosmoGab.Controllers
{
    using System;
    using System.Linq.Expressions;
    using System.Net;
    using System.Threading.Tasks;
    using System.Web.Mvc;

    using CosmoGab.Models;

    using FizzWare.NBuilder;
    using FizzWare.NBuilder.Generators;

    public class ContactController : Controller
    {
        private readonly IDocumentDBRepository<Contact> documentDbRepository;

        public ContactController(IDocumentDBRepository<Contact> documentDbRepository)
        {
            this.documentDbRepository = documentDbRepository;
        }

        public async Task<ActionResult> Index()
        {
            // Expression<Func<Contact, bool>> expression = x => x.EntityType == "Contact";
            var items = await this.documentDbRepository.GetItemsAsync();
            return this.View(items);
        }

        public async Task<ActionResult> AddRandom()
        {
            var items = Builder<Contact>.CreateListOfSize(5)
                .All().With(x => x.Id = Guid.NewGuid().ToString())
                .With(x => x.Firstname = GetRandom.FirstName())
                .With(x => x.Lastname = GetRandom.LastName())
                .With(x => x.Description = GetRandom.Phrase(10))
                .With(x => x.BirthDate = GetRandom.DateTime())
                .With(x => x.Address = Builder<Address>.CreateNew()
                               .With(y => y.Country = GetRandom.Usa.State())
                               .With(y => y.AddressLine = GetRandom.Phrase(20))
                               .With(y => y.ZipCode = GetRandom.Int(0, 99999).ToString())
                               .Build())
                .Build();

            foreach (var item in items)
            {
                await this.documentDbRepository.CreateItemAsync(item);
            }

            return this.RedirectToAction("Index");
        }

        public async Task<ActionResult> Create()
        {
            return this.View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create( Contact item)
        {
            if (this.ModelState.IsValid)
            {
                await this.documentDbRepository.CreateItemAsync(item);
                return this.RedirectToAction("Index");
            }

            return this.View(item);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(Contact item)
        {
            if (this.ModelState.IsValid)
            {
                await this.documentDbRepository.UpdateItemAsync(item.Id, item);
                return this.RedirectToAction("Index");
            }

            return this.View(item);
        }

        public async Task<ActionResult> Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Contact item = await this.documentDbRepository.GetItemAsync(id);
            if (item == null)
            {
                return this.HttpNotFound();
            }

            return this.View(item);
        }

        public async Task<ActionResult> Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            await this.documentDbRepository.DeleteItemAsync(id);
            return this.RedirectToAction("Index");
        }
    }
}