using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ScrapManagement.Application.Constants;
using ScrapManagement.Application.Features.ScrapTypes.Queries.GetAllCached;
using ScrapManagement.Application.Features.Scraps.Commands.Create;
using ScrapManagement.Application.Features.Scraps.Commands.Delete;
using ScrapManagement.Application.Features.Scraps.Commands.Update;
using ScrapManagement.Application.Features.Scraps.Queries.GetAllCached;
using ScrapManagement.Application.Features.Scraps.Queries.GetById;
using ScrapManagement.Web.Abstractions;
using ScrapManagement.Web.Areas.Apps.Models;
using ScrapManagement.Web.Extensions;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ScrapManagement.Domain.Entities;

namespace ScrapManagement.Web.Areas.Apps.Controllers
{
    [Area("Apps")]
    public class ScrapController : BaseController<ScrapController>
    {
        public IActionResult Index()
        {
            var model = new ScrapViewModel();
            return View(model);
        }

        public async Task<IActionResult> LoadAll()
        {
            var response = await _mediator.Send(new GetAllScrapsCachedQuery());
            if (response.Succeeded)
            {
                var viewModel = _mapper.Map<List<ScrapViewModel>>(response.Data);
                return PartialView("_ViewAll", viewModel);
            }
            return null;
        }

        [Authorize(Policy = Permissions.Users.View)]
        public async Task<JsonResult> OnGetCreateOrEdit(int id = 0)
        {
            var scrapTypesResponse = await _mediator.Send(new GetAllScrapTypesCachedQuery());

            if (id == 0)
            {
                var scrapViewModel = new ScrapViewModel();
                if (scrapTypesResponse.Succeeded)
                {
                    var scrapTypeViewModel = _mapper.Map<List<ScrapTypeViewModel>>(scrapTypesResponse.Data);
                    scrapViewModel.ScrapTypes = new SelectList(scrapTypeViewModel, nameof(ScrapTypeViewModel.Id), nameof(ScrapTypeViewModel.Name), null, null);
                }
                return new JsonResult(new { isValid = true, html = await _viewRenderer.RenderViewToStringAsync("_CreateOrEdit", scrapViewModel) });
            }
            else
            {
                var response = await _mediator.Send(new GetScrapByIdQuery() { Id = id });
                if (response.Succeeded)
                {
                    var scrapViewModel = _mapper.Map<ScrapViewModel>(response.Data);
                    if (scrapTypesResponse.Succeeded)
                    {
                        var scrapTypeViewModel = _mapper.Map<List<ScrapTypeViewModel>>(scrapTypesResponse.Data);
                        scrapViewModel.ScrapTypes = new SelectList(scrapTypeViewModel, nameof(ScrapTypeViewModel.Id), nameof(ScrapTypeViewModel.Name), null, null);
                    }
                    return new JsonResult(new { isValid = true, html = await _viewRenderer.RenderViewToStringAsync("_CreateOrEdit", scrapViewModel) });
                }
                return null;
            }
        }

        [HttpPost]
        public async Task<JsonResult> OnPostCreateOrEdit(int id, ScrapViewModel scrap)
        {
            if (ModelState.IsValid)
            {
                if (id == 0)
                {
                    var createScrapCommand = _mapper.Map<CreateScrapCommand>(scrap);
                    var result = await _mediator.Send(createScrapCommand);
                    if (result.Succeeded)
                    {
                        id = result.Data;
                        _notify.Success($"{result.Data} kimlikli hurda oluşturuldu.");
                    }
                    else _notify.Error(result.Message);
                }
                else
                {
                    var updateScrapCommand = _mapper.Map<UpdateScrapCommand>(scrap);
                    var result = await _mediator.Send(updateScrapCommand);
                    if (result.Succeeded) _notify.Information($"{result.Data} kimlikli hurda güncellendi.");
                }
                if (Request.Form.Files.Count > 0)
                {
                    IFormFile file = Request.Form.Files.FirstOrDefault();
                    var image = file.OptimizeImageSize(700, 700);
                    await _mediator.Send(new UpdateScrapImageCommand() { Id = id, Image = image });
                }
                var response = await _mediator.Send(new GetAllScrapsCachedQuery());
                if (response.Succeeded)
                {
                    var viewModel = _mapper.Map<List<ScrapViewModel>>(response.Data);
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
                var html = await _viewRenderer.RenderViewToStringAsync("_CreateOrEdit", scrap);
                return new JsonResult(new { isValid = false, html = html });
            }
        }

        [HttpPost]
        public async Task<JsonResult> OnPostDelete(int id)
        {
            var deleteCommand = await _mediator.Send(new DeleteScrapCommand { Id = id });
            if (deleteCommand.Succeeded)
            {
                _notify.Information($"{id} kimlikli hurda silindi.");
                var response = await _mediator.Send(new GetAllScrapsCachedQuery());
                if (response.Succeeded)
                {
                    var viewModel = _mapper.Map<List<ScrapViewModel>>(response.Data);
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