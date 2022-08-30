using AspNetCoreHero.Results;
using Microsoft.AspNetCore.Mvc;
using ScrapManagement.Application.Features.ScrapTypes.Commands.Create;
using ScrapManagement.Application.Features.ScrapTypes.Commands.Delete;
using ScrapManagement.Application.Features.ScrapTypes.Commands.Update;
using ScrapManagement.Application.Features.ScrapTypes.Queries.GetAllCached;
using ScrapManagement.Application.Features.ScrapTypes.Queries.GetById;
using ScrapManagement.Domain.Entities;
using ScrapManagement.Web.Abstractions;
using ScrapManagement.Web.Areas.Apps.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ScrapManagement.Web.Areas.Apps.Controllers
{
    [Area("Apps")]
    public class ScrapTypeController : BaseController<ScrapTypeController>
    {
        public IActionResult Index()
        {
            var model = new ScrapTypeViewModel();
            return View(model);
        }

        public async Task<IActionResult> LoadAll()
        {
            var response = await _mediator.Send(new GetAllScrapTypesCachedQuery());
            if (response.Succeeded)
            {
                var viewModel = _mapper.Map<List<ScrapTypeViewModel>>(response.Data);
                return PartialView("_ViewAll", viewModel);
            }
            return null;
        }

        public async Task<JsonResult> OnGetCreateOrEdit(int id = 0)
        {
            var scrapTypesResponse = await _mediator.Send(new GetAllScrapTypesCachedQuery());

            if (id == 0)
            {
                var scrapTypeViewModel = new ScrapTypeViewModel();
                return new JsonResult(new { isValid = true, html = await _viewRenderer.RenderViewToStringAsync("_CreateOrEdit", scrapTypeViewModel) });
            }
            else
            {
                var response = await _mediator.Send(new GetScrapTypeByIdQuery() { Id = id });
                if (response.Succeeded)
                {
                    var scrapTypeViewModel = _mapper.Map<ScrapTypeViewModel>(response.Data);
                    return new JsonResult(new { isValid = true, html = await _viewRenderer.RenderViewToStringAsync("_CreateOrEdit", scrapTypeViewModel) });
                }
                return null;
            }
        }

        [HttpPost]
        public async Task<JsonResult> OnPostCreateOrEdit(int id, ScrapTypeViewModel scrapType)
        {
            if (ModelState.IsValid)
            {
                if (id == 0)
                {
                    var createScrapTypeCommand = _mapper.Map<CreateScrapTypeCommand>(scrapType);
                    var result = await _mediator.Send(createScrapTypeCommand);
                    if (result.Succeeded)
                    {
                        id = result.Data;
                        _notify.Success($"{result.Data} kimlikli hurda tipi oluşturuldu.");
                    }
                    else _notify.Error(result.Message);
                }
                else
                {
                    var updateScrapTypeCommand = _mapper.Map<UpdateScrapTypeCommand>(scrapType);
                    var result = await _mediator.Send(updateScrapTypeCommand);
                    if (result.Succeeded) _notify.Information($"{result.Data} kimlikli hurda tipi güncellendi.");
                }
                var response = await _mediator.Send(new GetAllScrapTypesCachedQuery());
                if (response.Succeeded)
                {
                    var viewModel = _mapper.Map<List<ScrapTypeViewModel>>(response.Data);
                    var html = await _viewRenderer.RenderViewToStringAsync("_ViewAll", viewModel);
                    return new JsonResult(new { isValid = true, html = html });
                }
                else
                {
                    _notify.Error(response.Message);
                    return null;
                }
            }
            else
            {
                var html = await _viewRenderer.RenderViewToStringAsync("_CreateOrEdit", scrapType);
                return new JsonResult(new { isValid = false, html = html });
            }
        }

        [HttpPost]
        public async Task<JsonResult> OnPostDelete(int id)
        {
            var deleteCommand = await _mediator.Send(new DeleteScrapTypeCommand { Id = id });
            if (deleteCommand.Succeeded)
            {
                _notify.Information($"{id} kimlikli hurda tipi silindi.");
                var response = await _mediator.Send(new GetAllScrapTypesCachedQuery());
                if (response.Succeeded)
                {
                    var viewModel = _mapper.Map<List<ScrapTypeViewModel>>(response.Data);
                    var html = await _viewRenderer.RenderViewToStringAsync("_ViewAll", viewModel);
                    return new JsonResult(new { isValid = true, html = html });
                }
                else
                {
                    _notify.Error(response.Message);
                    return null;
                }
            }
            else
            {
                _notify.Error(deleteCommand.Message);
                return null;
            }
        }
    }
}