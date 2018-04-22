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

    public class ItemController : Controller
    {
        private readonly IDocumentDBRepository<Item> documentDbRepository;

        public ItemController(IDocumentDBRepository<Item> documentDbRepository)
        {
            this.documentDbRepository = documentDbRepository;
        }

        public async Task<ActionResult> Index()
        {
            // Expression<Func<Item, bool>> expression = x => x.EntityType == "Item";
            var items = await this.documentDbRepository.GetItemsAsync();
            return this.View(items);
        }

        public async Task<ActionResult> AddRandom()
        {
            var items = Builder<Item>.CreateListOfSize(5)
                .All()
                .With(x => x.Id = Guid.NewGuid().ToString())
                .With(x => x.Description = GetRandom.Phrase(10))
                .With(x => x.Name = GetRandom.String(10))
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
        public async Task<ActionResult> Create(Item item)
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
        public async Task<ActionResult> Edit(Item item)
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

            Item item = await this.documentDbRepository.GetItemAsync(id);
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